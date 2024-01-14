using CipherNew.Cyphers;
using CipherNew.DataLayer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics.Metrics;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Azure;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Npgsql;
using System.Net.Sockets;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using CypherNew.Cyphers;
using CipherNew.DTO;
using CipherNew.Hashes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Microsoft.WindowsAPICodePack.Dialogs;


namespace CipherNew.Forms
{

    public partial class Chat : Form
    {
        private readonly string _username;
        private readonly int _ID;
        private bool _encrypted = false;
        private List<DTO.MessageWithSender> _messages = new List<DTO.MessageWithSender>();
        private List<DTO.CachedFile> _receivedFiles = new List<DTO.CachedFile>();
        private Dictionary<int, string> _usernamesForIdsByUsers = new Dictionary<int, string>();
        private FileDTO? _fileBuffer;

        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageManager _messageManager;
        private readonly IUserManager _userManager;
        private ICypher _cypher;

        //_____
        private bool _closingWindow = false;

        public Chat(IServiceProvider provider, string username)
        {

            _serviceProvider = provider;
            _messageManager = _serviceProvider.GetRequiredService<IMessageManager>();
            _userManager = _serviceProvider.GetRequiredService<IUserManager>();
            //_cypher = _serviceProvider.GetRequiredService<ICypher>();
            _cypher = new RailFenceCypher(true);
            _username = username;
            _ID = _userManager.GetIdByUsername(_username);
            _fileBuffer = new FileDTO();


            InitializeComponent();

        }



        private void btnSend_Click(object sender, EventArgs e)
        {
            uint key = UInt32.Parse(ConfigurationManager.AppSettings["key"]);
            string encryptedMessage = _cypher.Encrypt(txtInputMessage.Text);
            bool messageSent = _messageManager.InsertMessage(encryptedMessage, _username);
            if (!messageSent)
            {
                MessageBox.Show("ERROR, MESSAGE NOT SENT!");
            }

            txtInputMessage.Text = String.Empty;
            txtInputMessage.Focus();
        }

        private void Chat_Load(object sender, EventArgs e)
        {
            int numOfUsers;
            btnSend.Enabled = false;
            int counter = 0;
            int deadline = Int32.Parse(ConfigurationManager.AppSettings["deadline_time"]);
            do
            {
                numOfUsers = _userManager.GetTotalAmountOfUsers();
                Thread.Sleep(1000);
                counter++;
            } while (numOfUsers < 2 && counter < deadline);

            if (counter >= deadline)
            {
                MessageBox.Show("DEADLINE TIME FOR OTHER USER HAS EXPIRED, WINDOW WILL SHUT DOWN...");
                this.Close();
                return;
            }
            btnSend.Enabled = true;

            var users = _userManager.GetUsers();

            foreach (var user in users)
            {
                _usernamesForIdsByUsers.Add(user.ID, user.Name);
            }


            Task.Run(() => PostgreConn());

        }

        public async Task PostgreConn()
        {
            string connString = ConfigurationManager.ConnectionStrings["database_connection"].ConnectionString;

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            //e.Payload is string representation of JSON we constructed in NotifyOnDataChange() function
            conn.Notification += (o, e) =>
            {
                ReceiveMessage(o, e);
            };

            await using (var cmd = new NpgsqlCommand(ReturnSQLCommandForGettingMessages(), conn))
                cmd.ExecuteNonQuery();

            while (true)
                conn.Wait(); // wait for events
        }


        public void ReceiveMessage(object sender, EventArgs e)
        {
            var obj = new
            {
                table = "",
                action = "",
                data = new
                {
                    id = 0,
                    isRead = false,
                    isFile = false,
                    Filename = "",
                    Text = "",
                    Hash = "",
                    SenderId = 0,
                    RecieverId = 0
                }
            };
            var message = (NpgsqlNotificationEventArgs)e;
            var x = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(message.Payload, obj);


            string decrypted = _cypher.Decrypt(x.data.Text);
            string decryptedFilename = "";
            if (x.data.isFile)
            {
                decryptedFilename = _cypher.Decrypt(x.data.Filename);
            }

            int senderId = x.data.SenderId;

            string usernameOfSender = _usernamesForIdsByUsers[senderId];
            DTO.MessageWithSender mess = new DTO.MessageWithSender();
            mess.Message = decrypted;
            mess.Sender = usernameOfSender;
            mess.isFile = x.data.isFile;
            mess.Filename = decryptedFilename;
            mess.Hash = x.data.Hash;

            int idOfSender = x.data.SenderId;

            if (!_closingWindow)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    //IF RECEIVED DATA IS FILE
                    if (x.data.isFile)
                    {
                        //add to list of received files
                        string onlyFileName = Path.GetFileName(mess.Filename);

                        List<string> allNamesOfReceivedFiles = _receivedFiles.Select(file => file.Name).ToList();
                        if (idOfSender != _ID && !allNamesOfReceivedFiles.Contains(onlyFileName))  //IF SENDER_ID IS NOT EQUAL MY ID, OR AM I A RECEIVER
                        {
                            _receivedFiles.Add(new CachedFile(onlyFileName, mess.Message));
                        }


                        //adding a message to list of messages
                        //IF HASHES MATCH, THEN ADD A MESSAGE, IF NOT, ADD IT AGAIN, BUT INFORM THAT THERE IS AN ERROR
                        mess.Message = (mess.Hash == Hashes.MD5.Hash(mess.Message)) ?
                                        $"SENT A FILE {onlyFileName}" : $"***SENT A FILE {onlyFileName}, FILE RECEIVED WITH ERRORS (Hash code doesn't match)***";
                        _messages.Add(mess);

                        RefreshMessagesBox();
                    }
                    else
                    {
                        //IF RECEIVED DATA IS MESSAGE

                        _messages.Add(mess);
                        RefreshMessagesBox();

                    }

                    if (_userManager.GetTotalAmountOfUsers() < 2)
                    {
                        MessageBox.Show("Other user has left the chat... Window is closing");
                        this.Close();
                    }
                }));
            }

        }

        private string ReturnSQLCommandForGettingMessages()
        {
            //return $"SELECT * FROM \"Messages\" WHERE \"Messages\".\"ReceiverId\"={_ID}";
            if (_ID % 2 == 0)
            {
                return $"LISTEN datachange1";
            }
            else
            {
                return $"LISTEN datachange2";
            }
        }

        private bool CanRequestNotifications()
        {
            SqlClientPermission permission =
                new SqlClientPermission(System.Security.Permissions.PermissionState.Unrestricted);

            try
            {
                permission.Demand();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        private void Chat_FormClosing(object sender, FormClosingEventArgs e)
        {
            _closingWindow = true;
            bool messagesRemoved = false;
            while (!messagesRemoved)
            {
                messagesRemoved = _messageManager.RemoveMessages();
            }

            bool userRemoved = false;

            int counter = 0;
            while (!userRemoved && counter < 10)
            {
                userRemoved = _userManager.RemoveUser(_username);
                counter++;
            }

        }

        public void RefreshMessagesBox()
        {
            listBoxMessages.Items.Clear();
            foreach (var message in _messages)
            {

                listBoxMessages.Items.Add((_encrypted) ? $"{message.Sender} :  {_cypher.Encrypt(message.Message)}"
                    : $"{message.Sender} :  {message.Message}");
            }
        }

        private void chkEncrypted_CheckedChanged(object sender, EventArgs e)
        {
           /* if (chkEncrypted.Checked)
            {
                chkRailFenceCaseSensitive.Enabled = true;
                chkXXTeaWithCBC.Enabled = true;
                chkRailFence.Enabled = true;
                chkRailFenceCaseSensitive.Checked = true;
            }
            else
            {
                chkRailFenceCaseSensitive.Enabled = false;
                chkXXTeaWithCBC.Enabled = false;
                chkRailFence.Enabled = false;
            }*/

            _encrypted = !_encrypted;
            RefreshMessagesBox();
        }

        private void chkRailFence_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkRailFenceCaseSensitive.Checked)
            {
                bool isCaseSensitive = true;
                _cypher = new RailFenceCypher(isCaseSensitive);
                RefreshMessagesBox();
            }

        }

        private void chkXXTeaWithCBC_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkXXTeaWithCBC.Checked)
            {
                _cypher = new XXTeaWithCBCCypher();
                RefreshMessagesBox();
            }

        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            if (openFileDialogSend.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string fileName = openFileDialogSend.FileName;
            if (fileName == null || fileName == "openFileDialogSend")
            {
                return;
            }
            string text = File.ReadAllText(fileName);

            _fileBuffer.isSet = true;
            _fileBuffer.Name = _cypher.Encrypt(fileName);
            _fileBuffer.Text = _cypher.Encrypt(text);
            _fileBuffer.Hash = (chkRailFence.Checked)? Hashes.MD5.Hash(text.ToLower()) : Hashes.MD5.Hash(text);

            uint key = UInt32.Parse(ConfigurationManager.AppSettings["key"]);
            string encryptedMessage = _cypher.Encrypt(_fileBuffer.Text);
            bool messageSent = _messageManager.InsertFile(_fileBuffer.Name, _fileBuffer.Text, _username, _fileBuffer.Hash);
            if (!messageSent)
            {
                MessageBox.Show("ERROR, FILE NOT SENT!\nTry sending lower sized files");
            }
        }

        private void btnReceivedFiles_Click(object sender, EventArgs e)
        {
            var window = new Forms.ReceivedFiles(_receivedFiles);
            window.ShowDialog();
        }

        private void chkRailFence_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRailFence.Checked)
            {
                bool isCaseSensitive = false;
                _cypher = new RailFenceCypher(isCaseSensitive);
                RefreshMessagesBox();
            }
        }
    }
}

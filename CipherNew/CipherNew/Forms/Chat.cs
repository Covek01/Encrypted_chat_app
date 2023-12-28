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

namespace CipherNew.Forms
{

    public partial class Chat : Form
    {
        private readonly string _username;
        private readonly int _ID;
        private bool _encrypted = false;
        private List<DTO.MessageWithSender> _messages = new List<DTO.MessageWithSender>();
        private Dictionary<int, string> _usernamesForIdsByUsers = new Dictionary<int, string>();


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
            _cypher = _serviceProvider.GetRequiredService<ICypher>();
            _username = username;
            _ID = _userManager.GetIdByUsername(_username);



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
                    Text = "",
                    SenderId = 0,
                    RecieverId = 0
                }
            };
            var message = (NpgsqlNotificationEventArgs)e;
            var x = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(message.Payload, obj);


            string decrypted = _cypher.Decrypt(x.data.Text);
            int senderId = x.data.SenderId;

            string usernameOfSender = _usernamesForIdsByUsers[senderId];
            DTO.MessageWithSender mess = new DTO.MessageWithSender();
            mess.Message = decrypted;
            mess.Sender = usernameOfSender;

            if (!_closingWindow)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {

                    _messages.Add(mess);
                    listBoxMessages.Items.Clear();
                    foreach (var message in _messages)
                    {

                        listBoxMessages.Items.Add((_encrypted) ? $"{message.Sender} :  {_cypher.Encrypt(message.Message)}"
                            : $"{message.Sender} :  {message.Message}");

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

            while (!userRemoved)
            {
                userRemoved = _userManager.RemoveUser(_username);
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
            if (chkEncrypted.Checked)
            {
                chkRailFence.Enabled = true;
                chkXXTeaWithCBC.Enabled = true;
                chkRailFence.Checked = true;
            }
            else
            {
                chkRailFence.Enabled = false;
                chkXXTeaWithCBC.Enabled = false;
            }

            _encrypted = !_encrypted;
            RefreshMessagesBox();
        }

        private void chkRailFence_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkRailFence.Checked)
            {
                _cypher = new RailFenceCypher();
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
    }
}

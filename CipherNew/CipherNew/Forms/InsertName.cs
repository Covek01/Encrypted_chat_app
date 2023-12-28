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

namespace CipherNew.Forms
{
    public partial class InsertName : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageManager _messageManager;
        private readonly IUserManager _userManager;

        public InsertName(IServiceProvider provider)
        {
            _serviceProvider = provider;
            _messageManager = _serviceProvider.GetRequiredService<IMessageManager>();
            _userManager = _serviceProvider.GetRequiredService<IUserManager>();
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            int numOfUsers = _userManager.GetTotalAmountOfUsers();
            if (numOfUsers < 0)
            {
                MessageBox.Show("ERROR WITH CONNECTION WITH SERVER");
            }
            if (numOfUsers >= 2)
            {
                MessageBox.Show("ROOM IS FULL, TRY AGAIN");
                return;
            }

            bool flag = _userManager.InsertUser(username);
            if (!flag)
            {
                MessageBox.Show("USER EXISTS");
                return;
            }

            if (numOfUsers == 0)
            {
                MessageBox.Show("You should wait for other user to connect to enter the chat room...");
            }
            var window = new Forms.Chat(_serviceProvider, username);
            window.ShowDialog();
        }
    }
}

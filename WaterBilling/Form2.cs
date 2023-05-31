using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaterBilling
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnBilling_Click(object sender, EventArgs e)
        {
            BillingForm billing = new BillingForm();
            billing.Show();
            this.Hide();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            UsersForm users = new UsersForm();
            users.Show();
            this.Hide();
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            ClientsForm clients = new ClientsForm();
            clients.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm back = new LoginForm();
            back.Show();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            lbluser.Text = InfoStorage.infoSet.username;
        }
    }
}

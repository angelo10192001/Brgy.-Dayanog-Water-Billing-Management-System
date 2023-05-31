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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbxUsername.Text == "admin" && tbxPassword.Text == "12345")
            {
                MainMenu login = new MainMenu();
                login.Show();
                this.Hide();
            }
            else
            {
                Boolean checker = APIConnect.GetUserPassword(tbxUsername, tbxPassword);

                if (checker)
                {
                    MainMenu login = new MainMenu();
                    login.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect password or username.");
                }
            }
        }

        private void tbxUsername_Click(object sender, EventArgs e)
        {
            tbxUsername.SelectAll();
        }

        private void tbxPassword_Click(object sender, EventArgs e)
        {
            tbxPassword.SelectAll();
        }
    }
}

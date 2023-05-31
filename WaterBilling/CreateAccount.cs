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
    public partial class CreateAccountForm : Form
    {
        public CreateAccountForm()
        {
            InitializeComponent();
        }

        

       
        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            string[] column = { "Name", "Username", "Password" };
            string[] data = { tbxName.Text, tbxUsername.Text, tbxPassword.Text };
            APIConnect.saveData("Create Account", column, data);
            MessageBox.Show("CREATED SUCCESSFULLY");
        }

        private void CreateAccountForm_Load(object sender, EventArgs e)
        {

        }
    }
}

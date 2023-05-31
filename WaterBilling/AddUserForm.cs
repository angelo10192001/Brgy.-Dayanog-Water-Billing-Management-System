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
    public partial class AddUserForm : Form
    {
        public AddUserForm()
        {
            InitializeComponent();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            string[] column = { "Name", "Username", "Password" };
            string[] data = { tbxName.Text, tbxUsername.Text, tbxPassword.Text };
            APIConnect.saveData("Create Account", column, data);
            MessageBox.Show("CREATED SUCCESSFULLY");
            LoginForm form = new LoginForm();
            this.Hide();
            form.Show();
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            UsersForm back = new UsersForm();
            back.Show();
        }
    }
}

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
    public partial class UsersForm : Form
    {
        public UsersForm()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            MainMenu back = new MainMenu();
            back.Show();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            AddUserForm addUser = new AddUserForm();
            addUser.Show();
            
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            lblAccountID.Hide();
            APIConnect.viewData("Account", dgvUsers);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            APIConnect.deleteData("Account", int.Parse(lblAccountID.Text));
            MessageBox.Show("DELETED SUCCESSFULLY");
            APIConnect.viewData("Account", dgvUsers);
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
             lblAccountID.Text = dgvUsers.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch { }
        }
    }
}

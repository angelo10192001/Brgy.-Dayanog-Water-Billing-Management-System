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
    public partial class ClientsForm : Form
    {
        public ClientsForm()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            MainMenu back = new MainMenu();
            back.Show();
        }

        private void ClientsForm_Load(object sender, EventArgs e)
        {
            btnUpdate.Hide();
            btnDelete.Hide();
            lblID.Hide();
            APIConnect.viewData("Client Information", dgvClients);
        }

        private void btnAddClient_Click(object sender, EventArgs e)
        {
            string[] column = { "First_Name", "Middle_Name", "Last_Name", "Contact_Number", "Purok" };
            string[] data = { tbxFname.Text, tbxMname.Text, tbxLname.Text, tbxContactNumber.Text, tbxPurok.Text };
            APIConnect.createAccount("Client Information", column, data);
            MessageBox.Show("ADDED SUCCESSFULLY");
            APIConnect.viewData("Client Information", dgvClients);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            APIConnect.deleteData("Billing Information", int.Parse(lblID.Text));
            APIConnect.deleteData("Client Information", int.Parse(lblID.Text));
            MessageBox.Show("DELETED SUCCESSFULLY");
            APIConnect.viewData("Client Information", dgvClients);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            APIConnect.searchData("Search Client", tbxSearch.Text, dgvClients);

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            btnUpdate.Hide();
            btnDelete.Hide();
            btnAddClient.Show();
            lblID.Text = "";
            tbxFname.Text = "";
            tbxMname.Text = "";
            tbxLname.Text = "";
            tbxContactNumber.Text = "";
            tbxPurok.Text = "";

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            string[] column = { "First_Name", "Middle_Name", "Last_Name", "Contact_Number", "Purok" };
            string[] data = { tbxFname.Text, tbxMname.Text, tbxLname.Text, tbxContactNumber.Text, tbxPurok.Text };
            APIConnect.updateData("Client Information", column, data, int.Parse(lblID.Text));
            MessageBox.Show("DATA UPDATED");
            APIConnect.viewData("Client Information", dgvClients);
        }

        private void dgvClients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnAddClient.Hide();
                btnUpdate.Show();
                btnDelete.Show();
                lblID.Text = dgvClients.Rows[e.RowIndex].Cells[0].Value.ToString();
                tbxLname.Text = dgvClients.Rows[e.RowIndex].Cells[1].Value.ToString();
                tbxFname.Text = dgvClients.Rows[e.RowIndex].Cells[2].Value.ToString();
                tbxMname.Text = dgvClients.Rows[e.RowIndex].Cells[3].Value.ToString();
                tbxContactNumber.Text = dgvClients.Rows[e.RowIndex].Cells[4].Value.ToString();
                tbxPurok.Text = dgvClients.Rows[e.RowIndex].Cells[5].Value.ToString();

            }
            catch { }
        }
    }
}

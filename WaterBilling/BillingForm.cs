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
    public partial class BillingForm : Form
    {
        public BillingForm()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            MainMenu back = new MainMenu();
            back.Show();
        }

        int client_id;
        private void BillingForm_Load(object sender, EventArgs e)
        {
            lblunpaidId.Hide();
            APIConnect.viewData("autoUpdate", dgvBilling);
            APIConnect.viewData("Client Information", dgvBilling);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            APIConnect.searchData("Search Billing Information", tbxSearch.Text, dgvBilling);
        }

        private void dgvBilling_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                client_id = int.Parse(dgvBilling.Rows[e.RowIndex].Cells[0].Value.ToString());
                APIConnect.getUnpaid(client_id, dgvUnpaid, lblBalance);
                lblName.Text = dgvBilling.Rows[e.RowIndex].Cells[1].Value.ToString();
                APIConnect.searchData("Paid History", client_id.ToString() , dgvPaid);

            }
            catch { }
            
        }
        private void btnPaid_Click(object sender, EventArgs e)
        {
            try
            {
                string[] column = {};
                string[] data = {};
                APIConnect.updateData("Paid",column,data,int.Parse(lblunpaidId.Text));
                APIConnect.getUnpaid(client_id, dgvUnpaid, lblBalance);
                rtbReceipt.Clear();
                rtbReceipt.Text += "**********************************\n";
                rtbReceipt.Text += "          BARANGAY DAYANOG        \n";
                rtbReceipt.Text += "\t      WATER BILLING             \n";
                rtbReceipt.Text += "          MANAGEMENT SYSTEM       \n";
                rtbReceipt.Text += "**********************************\n";
                rtbReceipt.Text += "Dayanog, San Juan, Southern Leyte\n";
                rtbReceipt.Text += "Date: " + DateTime.Now + "\n\n";

                rtbReceipt.Text += "Name: " + lblName.Text + "\n";
                rtbReceipt.Text += "Month Paid: " + lblMonth.Text + "\n";
                rtbReceipt.Text += "Year: " + lblYear.Text + "\n";
                rtbReceipt.Text += "Amount: 20\n";
                rtbReceipt.Text += "Balance: " + lblBalance.Text + "\n";

                rtbReceipt.Text += "\n                   Signature:";
                APIConnect.searchData("Paid History", client_id.ToString(), dgvPaid);

            }
            catch { }
        }
        private void dgvUnpaid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblunpaidId.Text = dgvUnpaid.Rows[e.RowIndex].Cells[0].Value.ToString();
                lblMonth.Text = dgvUnpaid.Rows[e.RowIndex].Cells[1].Value.ToString();
                lblYear.Text = dgvUnpaid.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            catch { }
           
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(rtbReceipt.Text,new Font("Century Gothic",12,FontStyle.Bold),Brushes.Black,new Point(10,10));
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.Show();
        }
    }
}
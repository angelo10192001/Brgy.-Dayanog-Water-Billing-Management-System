using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using Newtonsoft.Json;
using RestSharp;
using Newtonsoft.Json.Linq;


namespace WaterBilling
{
    class APIConnect
    {
        public static Boolean GetUserPassword(TextBox username, TextBox password)
        {
            bool verify = false;
            try
            {
                RestClient client = new RestClient("http://localhost:3000");
                var request = new RestRequest("login", Method.Post);
                request.AddQueryParameter("username", username.Text);
                request.AddQueryParameter("password", password.Text);

                var response = client.Execute(request);
                if (response.ContentLength > 2)
                {
                    JArray jsonArray = JArray.Parse(response.Content);
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());
                    verify = true;
                    InfoStorage.infoSet.token = (dt.Rows[0]["token"]).ToString();
                    InfoStorage.infoSet.username = username.Text;

                }
                else
                {
                    MessageBox.Show("LOGIN FAILED");
                }
            }
            catch
            {

            }
            return verify;
        }

        public static void viewData(string displayData, DataGridView dgv)
        {
            try
            {
                RestClient client = new RestClient("http://localhost:3000");
                var request = new RestRequest("waterbilling", Method.Get);
                request.AddHeader("authorization", InfoStorage.infoSet.token);
                request.AddParameter("displayData", displayData);

                var response = client.Execute(request);

                if (response.IsSuccessful)
                {

                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(response.Content);
                    dgv.DataSource = dt;
                    dgv.Columns[0].Visible = false;


                }
                else
                {
                    MessageBox.Show("Error fetching data");
                }
            }
            catch
            {

            }
        }
        public static void searchData(string displayData, string searchData, DataGridView dgv)
        {
            try
            {
                RestClient client = new RestClient("http://localhost:3000");
                var request = new RestRequest("search", Method.Get);
                request.AddHeader("authorization", InfoStorage.infoSet.token);
                request.AddParameter("displayData", displayData);
                request.AddParameter("searchData", searchData);

                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(response.Content);
                    dgv.DataSource = dt;
                    dgv.Columns[0].Visible = false;
                }
                else
                {
                    MessageBox.Show("Error fetching data");
                }
            }
            catch
            {

            }
        }
        public static void createAccount(string tableName, string[] columnName, string[] data)
        {
            try
            {
                RestClient client = new RestClient("http://localhost:3000");
                var request = new RestRequest("Create Account", Method.Post);
                for (int i = 0; i < data.Length; i++)
                {
                    request.AddQueryParameter(columnName[i], data[i]);
                }
                var response = client.Execute(request);
                if (response.IsSuccessful)
                {

                }
                else
                {
                    MessageBox.Show("Fill up incomplete" + response.ErrorMessage);
                }
            }
            catch
            {
            }
        }
        public static void saveData(string tableName, string[] columnName, string[] data)
        {
            try
            {
                RestClient client = new RestClient("http://localhost:3000");
                var request = new RestRequest("waterbilling", Method.Post);
                request.AddHeader("authorization", InfoStorage.infoSet.token);

                request.AddQueryParameter("tableName", tableName);
                for (int i = 0; i < data.Length; i++)
                {
                    request.AddQueryParameter(columnName[i], data[i]);
                }
                var response = client.Execute(request);
                if (response.IsSuccessful)
                {

                }
                else
                {
                    MessageBox.Show("Fill up incomplete" + response.ErrorMessage);
                }
            }
            catch
            {
            }
        }
        public static void updateData(string tableName, string[] columnName, string[] data, int id)
        {
            try
            {
                RestClient client = new RestClient("http://localhost:3000");
                var request = new RestRequest("waterbilling", Method.Put);
                request.AddHeader("authorization", InfoStorage.infoSet.token);

                request.AddQueryParameter("tableName", tableName);
                request.AddQueryParameter("id", id);
                for (int i = 0; i < columnName.Length; i++)
                {
                    request.AddQueryParameter(columnName[i], data[i]);
                }
                var response = client.Execute(request);
                if (response.IsSuccessful)
                {

                }
                else
                {
                    MessageBox.Show("Fill up incomplete!!" + response.ErrorMessage);
                }
            }
            catch
            {
            }
        }
        public static void deleteData(string tableName, int id)
        {
            try
            {
                RestClient client = new RestClient("http://localhost:3000");
                var request = new RestRequest("waterbilling", Method.Delete);
                request.AddHeader("authorization", InfoStorage.infoSet.token);
                request.AddQueryParameter("tableName", tableName);
                request.AddQueryParameter("id", id);
                var response = client.Execute(request);
                if (response.IsSuccessful)
                {
                }
                else
                {
                    MessageBox.Show("Error" + response.ErrorMessage);
                }
            }
            catch
            {
            }
        }
        public static void getUnpaid( int client_id, DataGridView dgv, Label lbl)
        {
            try
            {
                {
                    RestClient client = new RestClient("http://localhost:3000");
                    var request = new RestRequest("search", Method.Get);
                    request.AddHeader("authorization", InfoStorage.infoSet.token);
                    request.AddParameter("displayData", "Unpaid Balance");
                    request.AddParameter("searchData", client_id);
                    var response = client.Execute(request);

                    if (response.IsSuccessful)
                    {

                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(response.Content);
                        dgv.DataSource = dt;
                        dgv.Columns[0].Visible = false;

                    }
                   
                }
                {
                    RestClient client = new RestClient("http://localhost:3000");
                    var request = new RestRequest("search", Method.Get); var request1 = new RestRequest("search", Method.Get);
                    request.AddHeader("authorization", InfoStorage.infoSet.token);
                    request.AddParameter("displayData", "Balance");
                    request.AddParameter("searchData", client_id);
                    var response = client.Execute(request);

                    if (response.IsSuccessful)
                    {

                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(response.Content);
                        
                        lbl.Text=((int.Parse(dt.Rows[0]["Balance"].ToString())) * 20).ToString();

                    }
                    else
                    {
                        MessageBox.Show("Error fetching data");
                    }
                }
            }
            catch
            {

            }
        }
    }
}

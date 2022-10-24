// Assignment 16: PoC
// Program to get data from Chuck Norris API
// Group Project: Ryan Riccio, Ridge Wada, Young Gi Hong, Koa Afusia

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;

namespace APIUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_Get_Click(object sender, EventArgs e)
        {
            var client = new RestClient("https://api.chucknorris.io/jokes/random"); // store the client to perform GET request with
            var request = new RestRequest();    // the request does not need any data
            var response = client.Get(request); // GET request from client

            // make sure we got successful data
            if (!response.IsSuccessStatusCode) return;

            // display raw data
            string rawResponse = response.Content;
            rtb_Output.Text = rawResponse;
        }
    }
}

// Sprint 1: PoC
// Program to get stock data from stock symbol and Yahoo Finance API
// Group Project: Ryan Riccio, Ridge Wada, Young Gi Hong, Koa Afusia
// Nov 16th, 2022

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using RestSharp;

namespace FinanceManager
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonGetData_Click(object sender, EventArgs e)
        {   
            // get the stock symbol and the data to go along with it
            var symbol = textBoxSymbol.Text;
            var client = new RestClient("https://query1.finance.yahoo.com/v7/finance/quote?symbols=" + symbol.ToUpper());
            var request = new RestRequest();    // the request does not need any data
            RestResponse response = client.Get(request);

            // make sure we got successful data
            if (!response.IsSuccessStatusCode) return;

            // store the content so we can use it in the parsing
            string data = response.Content;

            // parse the Json
            using JsonDocument doc = JsonDocument.Parse(data);
            JsonElement root = doc.RootElement;

            // get the data from the response and display it to the text boxes
            textBoxPrice.Text = '$' + Convert.ToString(root.GetProperty("quoteResponse").GetProperty("result")[0].GetProperty("regularMarketPrice"));
            textBoxChange.Text = Convert.ToString(root.GetProperty("quoteResponse").GetProperty("result")[0]
                .GetProperty("regularMarketChangePercent")) + '%';
        }
    }
}

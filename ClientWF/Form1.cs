using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientWF
{
    public partial class Form1 : Form
    {
        HttpClient client;
        string userName = "noname";
        string ip;
        Thread thread;

        public Form1()
        {
            InitializeComponent();
            ip = @"http://" + ipTB.Text + "//";
            client = new HttpClient();
            thread = new Thread(Start);
            ConnectBtn.Click += ConnectClick;
            sendBtn.Click += SendClick;

        }

        private async void Start()
        {
            while (true)
            {
                try
                {
                    string response = await client.GetStringAsync(ip + "?check=" + userName);
                    string[] param = response.Split('=');
                    if (param[0] == "message")
                        listBox1.BeginInvoke(new Action(() => listBox1.Items.Add("Server: " + param[1])));
                    else if (param[0] == "name")
                        userName = param[1];
                }
                catch (Exception e)
                {
                    listBox1.BeginInvoke(new Action(() => listBox1.Items.Add("Cannot connect!")));
                }
                Thread.Sleep(500);
            }
        }

        private async void SendClick(object sender, EventArgs e)
        {
            //thread.Interrupt();
            try
            {
                var response = await client.PostAsync(ip, new StringContent("message=" + inputTB.Text));
                string content = await response.Content.ReadAsStringAsync();
                if (content == "received")
                    listBox1.Items.Add("You: " + inputTB.Text);
            }
            catch
            {
                listBox1.Items.Add("Cannot connect!");
            }
            //thread.Resume();
        }

        private void ConnectClick(object sender, EventArgs e)
        {
            if (thread.IsAlive == false)
                thread.Start();
            listBox1.Items.Add("You've connected to server!");
        }
    }
}

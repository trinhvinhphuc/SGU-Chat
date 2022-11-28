using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sign_In_Form
{
    public partial class Login : Form
    {
        bool active = true;
        IPEndPoint ipe;
        Socket server;
        Form main;

        public Login()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            new Signin().Show();
            this.Hide();
        }

        private void signinbtn_Click(object sender, EventArgs e)
        {
            ipe = new IPEndPoint(IPAddress.Parse(txtIP.Text), 2009);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect(ipe);

            Common.Account account = new Account(txtUsername.Text, txtPassword.Text);

            String loginJson = JsonSerializer.Serialize(account);
            Common.Json json = new Common.Json("LOGIN", loginJson);
            sendJson(json, server);
            var threadLog = new Thread(() => waitForLoginFeedback());
            threadLog.Start(); 
        }

        private void waitForLoginFeedback()
        {
            do
            {
                try
                {
                    byte[] data = new byte[1024];
                    int recv = server.Receive(data);
                    if (recv == 0) continue;
                    String feedbackJson = Encoding.ASCII.GetString(data, 0, recv);
                    Common.Json? feedback = JsonSerializer.Deserialize<Common.Json?>(feedbackJson);
                    if (feedback != null)
                    {
                        switch (feedback.type)
                        {
                            case "LOGIN_FEEDBACK":
                                if (feedback.content == "TRUE")
                                {
                                    MessageBox.Show("Login successes!!", "Notification");
                                    this.Invoke((MethodInvoker)delegate () {
                                        main = new MainForm(server, txtUsername.Text);
                                        main.Show();
                                    });
                                    this.Invoke(new MethodInvoker(this.Close));
                                    break;
                                }
                                if (feedback.content == "FALSE")
                                {
                                    MessageBox.Show("Login failed!!", "Notification");
                                }
                                break;
                            case "SHUTDOWN_FEEDBACK":
                                active = false;
                                server.Shutdown(SocketShutdown.Both);
                                server.Close();
                                //Environment.Exit(0);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                
            }
            while (active && server == null);
        }

        private void sendJson(Common.Json json, Socket server)
        {
            String message = JsonSerializer.Serialize(json);
            byte[] data = new byte[1024];
            data = Encoding.ASCII.GetBytes(message);

            server.Send(data, data.Length, SocketFlags.None);
        }
    }
}

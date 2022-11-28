using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using Common;
using System.Text;
using System.Windows.Forms;

namespace Sign_In_Form
{
    public partial class Signin : Form
    {
        bool active = true;
        IPEndPoint ipe;
        Socket server;

        public Signin()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Hide();
        }

        private void signinbtn_Click(object sender, EventArgs e)
        {
            ipe = new IPEndPoint(IPAddress.Parse(txtIP.Text), 2009);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect(ipe);

            Common.Account newAccount = new Account(txtUsername.Text, txtPassword.Text);

            String signinJson = JsonSerializer.Serialize(newAccount);
            Common.Json json = new Common.Json("SIGNIN", signinJson);
            sendJson(json, server);
            var threadSign = new Thread(() => waitForFeedback());
            threadSign.Start();
        }

        private void waitForFeedback()
        {
            do
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
                        case "SIGNIN_FEEDBACK":
                            if (feedback.content == "TRUE")
                            {
                                MessageBox.Show("Signin successes!!", "Notification");                                
                                this.Invoke((MethodInvoker)delegate () {
                                    new MainForm(server, txtUsername.Text).Show();
                                });
                                this.Invoke(new MethodInvoker(this.Hide));
                                break;
                            }
                            if (feedback.content == "FALSE")
                            {
                                MessageBox.Show("Signin failed!!", "Notification");
                            }
                            break;
                        case "SHUTDOWN_FEEDBACK":
                            active = false;
                            server.Shutdown(SocketShutdown.Both);
                            server.Close();
                            Environment.Exit(0);
                            break;
                    }
                }
            }
            while (active);
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
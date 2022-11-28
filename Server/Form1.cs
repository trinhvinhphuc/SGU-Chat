using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Text;
using System.Windows.Forms;
using Common;
using System.Security.Principal;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Server
{
    public partial class Form1 : Form
    {
        bool active = true;
        String IP = null, message;
        IPEndPoint iep;
        Socket server, client;
        Dictionary<string, string> USER;
        Dictionary<string, Socket> CLIENT;
        Dictionary<string, List<string>> GROUP;

        public Form1()
        {
            InitializeComponent();
        }

        private void startbtn_Click(object sender, EventArgs e)
        {
            var startThread = new Thread(() => startServer());
            startThread.Start();
        }

        private void startServer()
        {
            iep = new IPEndPoint(IPAddress.Parse(IP), Int32.Parse(txtPort.Text));
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            message = "Start accept connect from client!";
            appendInTextBox(txtMessage, message);
            changeButtonEnable(startbtn, false);
            changeButtonEnable(stopbtn, true);

            server.Bind(iep);
            server.Listen(10);

            do
            {
                try
                {
                    client = server.Accept();
                    byte[] data = new byte[1024];
                    int recv = client.Receive(data);
                    if (recv == 0) continue;
                    String s = Encoding.ASCII.GetString(data, 0, recv);

                    Common.Json infoJson = JsonSerializer.Deserialize<Common.Json>(s);

                    if (infoJson != null)
                    {
                        switch (infoJson.type)
                        {
                            case "SIGNIN":
                                reponseSignin(infoJson, client);
                                break;
                            case "LOGIN":
                                reponseLogin(infoJson, client);
                                break;
                        }
                    }
                }
                catch (Exception)
                {
                    active = false;
                }
            }
            while(active);
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        private void reponseSignin(Json infoJson, Socket client)
        {
            Account newAccount = JsonSerializer.Deserialize<Account>(infoJson.content);
            if (newAccount != null && newAccount.userName != null && !USER.ContainsKey(newAccount.userName) && !CLIENT.ContainsKey(newAccount.userName))
            {
                Json notification = new Json("SIGNIN_FEEDBACK", "TRUE");
                sendJson(notification, client);
                appendInTextBox(txtMessage, newAccount.userName + " signed in!");

                CLIENT.Add(newAccount.userName, client);
                USER.Add(newAccount.userName, newAccount.password);
                clientService(client);
            }
            else
            {
                Json notification = new Json("SIGNIN_FEEDBACK", "FALSE");
                sendJson(notification, client);
                appendInTextBox(txtMessage, newAccount.userName + " can not signin!");
            }
        }

        private void reponseLogin(Json infoJson, Socket client)
        {
            Account account = JsonSerializer.Deserialize<Account>(infoJson.content);
            if (account != null && account.userName != null && USER.ContainsKey(account.userName) && !CLIENT.ContainsKey(account.userName) && USER[account.userName] == account.password)
            {
                Json notification = new Json("LOGIN_FEEDBACK", "TRUE");
                sendJson(notification, client);
                appendInTextBox(txtMessage, account.userName + " logged in!");

                CLIENT.Add(account.userName, client);
                clientService(client);
            }
            else
            {
                Json notification = new Json("LOGIN_FEEDBACK", "FALSE");
                sendJson(notification, client);
                appendInTextBox(txtMessage, account.userName + " can not login!");
            }
        }

        private void clientService(Socket socket)
        {
            var clientThread = new Thread(() =>
            {
                bool threadActive = true;
                do
                {
                    byte[] data = new byte[1024];
                    int recv = client.Receive(data);
                    if (recv == 0) continue;
                    String s = Encoding.ASCII.GetString(data, 0, recv);
                    Common.Json infoJson = JsonSerializer.Deserialize<Common.Json>(s);

                    switch (infoJson.type)
                    {
                        case "MESSAGE":
                            reponseMessage(infoJson, socket);
                            break;
                        case "SHUTDOWN":
                            if (infoJson.content != null && CLIENT.ContainsKey(infoJson.content))
                            {
                                Json close = new Json("SHUTDOWN_FEEDBACK", "TRUE");
                                sendJson(close, socket);

                                CLIENT.Remove(infoJson.content);
                                appendInTextBox(txtMessage, infoJson.content + " logged out!");

                                client.Shutdown(SocketShutdown.Both);
                                client.Close();
                                threadActive = false;
                            }
                            else
                            {
                                Json close = new Json("SHUTDOWN_FEEDBACK", "FALSE");
                                sendJson(close, socket);
                                appendInTextBox(txtMessage, infoJson.content + " can not logged out!");
                            }                           
                            break;
                    }
                }
                while (threadActive);
            });
            clientThread.Start();
        }

        private void reponseMessage(Json infoJson, Socket client)
        {
            byte[] data = new byte[1024];
            Common.Messages? message = JsonSerializer.Deserialize<Common.Messages?>(infoJson.content);
            if (message != null && CLIENT.ContainsKey(message.receiver))
            {
                appendInTextBox(txtMessage, message.sender + " to " + message.receiver + ": " + message.message);
                Socket receiver = CLIENT[message.receiver];
                sendJson(infoJson, receiver);
            }
            else if (message != null && GROUP.ContainsKey(message.receiver))
            {
                if (GROUP[message.receiver].Contains(message.sender))
                {
                    appendInTextBox(txtMessage, message.sender + " to " + message.receiver + ": " + message.message);
                    foreach (String account in GROUP[message.receiver])
                    {
                        if (CLIENT.ContainsKey(account))
                        {
                            Socket receiver = CLIENT[account];
                            sendJson(infoJson, receiver);
                        }
                    }
                }
                else
                {
                    Common.Json notification = new Common.Json("MESSAGE_FEEDBACK", "SEND_FAILED");
                    sendJson(notification, CLIENT[message.sender]);
                    appendInTextBox(txtMessage, message.sender + " unsuccessfully send a message!");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var host = Dns.GetHostByName(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.ToString().Contains('.'))
                {
                    IP = ip.ToString();
                }
            }
            if (IP == null)
            {
                message = "No network adapters with an IPv4 address in the system!";
                string title = "Error";
                MessageBox.Show(message, title);
                return;
            }
            txtIP.Text = IP;
            txtPort.Text = "2009";

            userInitialize();
        }

        private void userInitialize()
        {
            CLIENT = new Dictionary<string, Socket>();
            GROUP = new Dictionary<string, List<string>>();
            USER = new Dictionary<string, string>();

            for (char uName = 'A'; uName <= 'Z'; uName++)
            {
                String pass = "123";
                USER.Add(uName.ToString(), pass);
            }

            for (int i = 0; i < 5; i++)
            {
                List<string> groupUser = new List<string>();
                for (byte j = 0; j < 3; j++)
                {
                    char u = (Char)('A' + 3 * i + j);
                    groupUser.Add(u.ToString());
                }
                GROUP.Add("G" + i.ToString(), groupUser);
            }
        }

        private void appendInTextBox(TextBox textBox, String text)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new Action<TextBox, String>(appendInTextBox), new object[] { textBox, text });
                return;
            }
            textBox.AppendText(text);
            textBox.AppendText(Environment.NewLine);
        }

        private void stopbtn_Click(object sender, EventArgs e)
        {
            if (CLIENT.Count != 0)
            {
                MessageBox.Show("The server has " + CLIENT.Count + " user(s) logged in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            active = false;
            //server.Close();
            Environment.Exit(0);
        }

        private void changeButtonEnable(Button btn, bool enable)
        {
            btn.BeginInvoke(new MethodInvoker(() =>
            {
                btn.Enabled = enable;
            }));
        }

        private void sendJson(Common.Json json, Socket client)
        {
            String message = JsonSerializer.Serialize(json);
            byte[] data = new byte[1024];
            data = Encoding.ASCII.GetBytes(message);

            client.Send(data, data.Length, SocketFlags.None);
        }
    }
}
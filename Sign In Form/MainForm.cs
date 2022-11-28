using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Sign_In_Form
{
    public partial class MainForm : Form
    {
        public Socket server;
        public String username;
        public ImageList imgs = new ImageList();
        public String imgLink;
        public int count;
        public bool active = true;

        public MainForm(Socket server, String username)
        {
            this.username = username;
            this.server = server;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtReceiver.Text == "" || txtmessage.Text == "")
            {
                MessageBox.Show("Empty Fields", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Messages message = new Messages(username, txtReceiver.Text, txtmessage.Text);
            String messageJson = JsonSerializer.Serialize(message);
            Json json = new Json("MESSAGE", messageJson);
            sendJson(json, server);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All files(*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    imgLink = ofd.FileName;

                    listView1.Items.Add("test", count);
                    imgs.Images.Add(Bitmap.FromFile(@imgLink));
                    listView1.SmallImageList = imgs;

                    count++;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.Columns.Add("Message", 200);

            imgs.ImageSize = new Size(50, 50);

            var mainThread = new Thread(() => receiveTheard(server));
            mainThread.Start();
        }

        private void receiveTheard(Socket server)
        {
            do
            {
                byte[] data = new byte[1024];
                int recv = server.Receive(data);
                if (recv == 0) continue;
                String s = Encoding.ASCII.GetString(data, 0, recv);
                Common.Json? infoJson = JsonSerializer.Deserialize<Common.Json?>(s);

                switch (infoJson.type)
                {
                    case "MESSAGE":
                        Common.Messages? message = JsonSerializer.Deserialize<Common.Messages?>(infoJson.content);
                        if (message != null)
                        {
                            appendInListView(listView1, message.sender + ": " + message.message);
                        }
                        break;
                    case "SHUTDOWN_FEEDBACK":
                        if (infoJson.content != null && infoJson.content == "TRUE")
                        {
                            this.Invoke((MethodInvoker)delegate () {
                                new Login().Show();
                            });
                            this.Invoke(new MethodInvoker(this.Close));

                            active = false;
                            server.Shutdown(SocketShutdown.Both);
                            server.Close();
                        }
                        if (infoJson.content == null || infoJson.content == "FALSE")
                        {
                            MessageBox.Show("Logout failed!!", "Notification");
                        }
                        break;
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

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            if (server != null)
            {
                Json close = new Json("SHUTDOWN", username);
                sendJson(close, server);
            }         

            active = false;
            //server.Shutdown(SocketShutdown.Both);
            //server.Close();
            //Environment.Exit(0);
        }

        private void appendInListView(System.Windows.Forms.ListView listView, String text)
        {
            if (listView.InvokeRequired)
            {
                listView.Invoke(new MethodInvoker(delegate
                {
                    listView.Items.Add(text);

                }));
            }
            else
            {
                listView.Items.Add(text);
            }
        }
    }
}

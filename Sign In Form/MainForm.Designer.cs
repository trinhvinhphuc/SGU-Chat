namespace Sign_In_Form
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.sendbtn = new System.Windows.Forms.Button();
            this.txtmessage = new System.Windows.Forms.TextBox();
            this.picturebtn = new System.Windows.Forms.PictureBox();
            this.createGroupBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtgroupname = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtmember = new System.Windows.Forms.TextBox();
            this.logoutBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReceiver = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.picturebtn)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(149, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 5;
            // 
            // sendbtn
            // 
            this.sendbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(139)))), ((int)(((byte)(227)))));
            this.sendbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendbtn.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.sendbtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.sendbtn.Location = new System.Drawing.Point(713, 330);
            this.sendbtn.Name = "sendbtn";
            this.sendbtn.Size = new System.Drawing.Size(75, 29);
            this.sendbtn.TabIndex = 2;
            this.sendbtn.Text = "Send";
            this.sendbtn.UseVisualStyleBackColor = false;
            this.sendbtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtmessage
            // 
            this.txtmessage.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtmessage.Location = new System.Drawing.Point(57, 330);
            this.txtmessage.Name = "txtmessage";
            this.txtmessage.Size = new System.Drawing.Size(650, 29);
            this.txtmessage.TabIndex = 1;
            // 
            // picturebtn
            // 
            this.picturebtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picturebtn.Image = ((System.Drawing.Image)(resources.GetObject("picturebtn.Image")));
            this.picturebtn.Location = new System.Drawing.Point(12, 330);
            this.picturebtn.Name = "picturebtn";
            this.picturebtn.Size = new System.Drawing.Size(39, 29);
            this.picturebtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picturebtn.TabIndex = 5;
            this.picturebtn.TabStop = false;
            this.picturebtn.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // createGroupBtn
            // 
            this.createGroupBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(139)))), ((int)(((byte)(227)))));
            this.createGroupBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createGroupBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.createGroupBtn.ForeColor = System.Drawing.Color.White;
            this.createGroupBtn.Location = new System.Drawing.Point(676, 24);
            this.createGroupBtn.Name = "createGroupBtn";
            this.createGroupBtn.Size = new System.Drawing.Size(94, 29);
            this.createGroupBtn.TabIndex = 2;
            this.createGroupBtn.Text = "Create";
            this.createGroupBtn.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(139)))), ((int)(((byte)(227)))));
            this.label3.Location = new System.Drawing.Point(6, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 21);
            this.label3.TabIndex = 1;
            this.label3.Text = "Group Name";
            // 
            // txtgroupname
            // 
            this.txtgroupname.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtgroupname.Location = new System.Drawing.Point(115, 24);
            this.txtgroupname.Name = "txtgroupname";
            this.txtgroupname.Size = new System.Drawing.Size(185, 29);
            this.txtgroupname.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.createGroupBtn);
            this.groupBox1.Controls.Add(this.txtmember);
            this.groupBox1.Controls.Add(this.txtgroupname);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(139)))), ((int)(((byte)(227)))));
            this.groupBox1.Location = new System.Drawing.Point(12, 365);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 74);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create a New Group";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(139)))), ((int)(((byte)(227)))));
            this.label4.Location = new System.Drawing.Point(322, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 21);
            this.label4.TabIndex = 1;
            this.label4.Text = "Member";
            // 
            // txtmember
            // 
            this.txtmember.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtmember.Location = new System.Drawing.Point(401, 24);
            this.txtmember.Name = "txtmember";
            this.txtmember.Size = new System.Drawing.Size(269, 29);
            this.txtmember.TabIndex = 1;
            // 
            // logoutBtn
            // 
            this.logoutBtn.BackColor = System.Drawing.Color.OrangeRed;
            this.logoutBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logoutBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.logoutBtn.ForeColor = System.Drawing.Color.White;
            this.logoutBtn.Location = new System.Drawing.Point(12, 456);
            this.logoutBtn.Name = "logoutBtn";
            this.logoutBtn.Size = new System.Drawing.Size(776, 32);
            this.logoutBtn.TabIndex = 4;
            this.logoutBtn.Text = "Log out";
            this.logoutBtn.UseVisualStyleBackColor = false;
            this.logoutBtn.Click += new System.EventHandler(this.logoutBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(139)))), ((int)(((byte)(227)))));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Receiver:";
            // 
            // txtReceiver
            // 
            this.txtReceiver.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtReceiver.Location = new System.Drawing.Point(96, 6);
            this.txtReceiver.Name = "txtReceiver";
            this.txtReceiver.Size = new System.Drawing.Size(300, 29);
            this.txtReceiver.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(12, 41);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(776, 260);
            this.listView1.TabIndex = 8;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picturebtn);
            this.Controls.Add(this.txtmessage);
            this.Controls.Add(this.txtReceiver);
            this.Controls.Add(this.logoutBtn);
            this.Controls.Add(this.sendbtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturebtn)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Button sendbtn;
        private TextBox txtmessage;
        private PictureBox picturebtn;
        private Button createGroupBtn;
        private Label label3;
        private TextBox txtgroupname;
        private GroupBox groupBox1;
        private Label label4;
        private TextBox txtmember;
        private Button logoutBtn;
        private Label label2;
        private TextBox txtReceiver;
        private ListView listView1;
    }
}
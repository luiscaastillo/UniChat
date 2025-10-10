namespace UniChat
{
    partial class FormChat
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
            this.pictureUser = new System.Windows.Forms.PictureBox();
            this.labelSalas = new System.Windows.Forms.Label();
            this.panelSalas = new System.Windows.Forms.Panel();
            this.BDeleteChat = new System.Windows.Forms.PictureBox();
            this.BNewChat = new System.Windows.Forms.PictureBox();
            this.treeViewChats = new System.Windows.Forms.TreeView();
            this.labelChats = new System.Windows.Forms.Label();
            this.pictureSala = new System.Windows.Forms.PictureBox();
            this.panelSalasName = new System.Windows.Forms.Panel();
            this.panelUser = new System.Windows.Forms.Panel();
            this.panelName = new System.Windows.Forms.Panel();
            this.BLogOut = new System.Windows.Forms.PictureBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.BEmoji = new System.Windows.Forms.PictureBox();
            this.BEnviarMsj = new System.Windows.Forms.PictureBox();
            this.RichMessage = new System.Windows.Forms.RichTextBox();
            this.panelEmoji = new System.Windows.Forms.Panel();
            this.happy = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUser)).BeginInit();
            this.panelSalas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BDeleteChat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BNewChat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSala)).BeginInit();
            this.panelSalasName.SuspendLayout();
            this.panelUser.SuspendLayout();
            this.panelName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BLogOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BEmoji)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BEnviarMsj)).BeginInit();
            this.panelEmoji.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.happy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureUser
            // 
            this.pictureUser.Location = new System.Drawing.Point(22, 9);
            this.pictureUser.Name = "pictureUser";
            this.pictureUser.Size = new System.Drawing.Size(36, 36);
            this.pictureUser.TabIndex = 0;
            this.pictureUser.TabStop = false;
            this.pictureUser.Click += new System.EventHandler(this.pictureUser_Click);
            // 
            // labelSalas
            // 
            this.labelSalas.AutoSize = true;
            this.labelSalas.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSalas.Location = new System.Drawing.Point(15, 17);
            this.labelSalas.Name = "labelSalas";
            this.labelSalas.Size = new System.Drawing.Size(136, 23);
            this.labelSalas.TabIndex = 4;
            this.labelSalas.Text = "Salas de chat";
            this.labelSalas.Click += new System.EventHandler(this.label2_Click);
            // 
            // panelSalas
            // 
            this.panelSalas.Controls.Add(this.BDeleteChat);
            this.panelSalas.Controls.Add(this.BNewChat);
            this.panelSalas.Controls.Add(this.treeViewChats);
            this.panelSalas.Controls.Add(this.labelChats);
            this.panelSalas.Controls.Add(this.pictureSala);
            this.panelSalas.Controls.Add(this.panelSalasName);
            this.panelSalas.Location = new System.Drawing.Point(12, 13);
            this.panelSalas.Name = "panelSalas";
            this.panelSalas.Size = new System.Drawing.Size(200, 350);
            this.panelSalas.TabIndex = 6;
            this.panelSalas.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // BDeleteChat
            // 
            this.BDeleteChat.Location = new System.Drawing.Point(156, 143);
            this.BDeleteChat.Name = "BDeleteChat";
            this.BDeleteChat.Size = new System.Drawing.Size(25, 24);
            this.BDeleteChat.TabIndex = 11;
            this.BDeleteChat.TabStop = false;
            this.BDeleteChat.Click += new System.EventHandler(this.BDeleteChat_Click);
            // 
            // BNewChat
            // 
            this.BNewChat.Location = new System.Drawing.Point(156, 113);
            this.BNewChat.Name = "BNewChat";
            this.BNewChat.Size = new System.Drawing.Size(25, 24);
            this.BNewChat.TabIndex = 10;
            this.BNewChat.TabStop = false;
            this.BNewChat.Click += new System.EventHandler(this.BNewChat_Click);
            // 
            // treeViewChats
            // 
            this.treeViewChats.Location = new System.Drawing.Point(19, 113);
            this.treeViewChats.Name = "treeViewChats";
            this.treeViewChats.Size = new System.Drawing.Size(121, 215);
            this.treeViewChats.TabIndex = 10;
            this.treeViewChats.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewChats_AfterSelect);
            // 
            // labelChats
            // 
            this.labelChats.AutoSize = true;
            this.labelChats.Font = new System.Drawing.Font("OCR A Extended", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelChats.Location = new System.Drawing.Point(61, 79);
            this.labelChats.Name = "labelChats";
            this.labelChats.Size = new System.Drawing.Size(63, 17);
            this.labelChats.TabIndex = 9;
            this.labelChats.Text = "Chats";
            // 
            // pictureSala
            // 
            this.pictureSala.Location = new System.Drawing.Point(19, 70);
            this.pictureSala.Name = "pictureSala";
            this.pictureSala.Size = new System.Drawing.Size(36, 36);
            this.pictureSala.TabIndex = 9;
            this.pictureSala.TabStop = false;
            this.pictureSala.Click += new System.EventHandler(this.pictureSala_Click);
            // 
            // panelSalasName
            // 
            this.panelSalasName.Controls.Add(this.labelSalas);
            this.panelSalasName.Location = new System.Drawing.Point(0, 0);
            this.panelSalasName.Name = "panelSalasName";
            this.panelSalasName.Size = new System.Drawing.Size(200, 55);
            this.panelSalasName.TabIndex = 5;
            // 
            // panelUser
            // 
            this.panelUser.Controls.Add(this.panelEmoji);
            this.panelUser.Controls.Add(this.RichMessage);
            this.panelUser.Controls.Add(this.panelName);
            this.panelUser.Controls.Add(this.BEmoji);
            this.panelUser.Controls.Add(this.BEnviarMsj);
            this.panelUser.Location = new System.Drawing.Point(218, 13);
            this.panelUser.Name = "panelUser";
            this.panelUser.Size = new System.Drawing.Size(458, 350);
            this.panelUser.TabIndex = 8;
            this.panelUser.Paint += new System.Windows.Forms.PaintEventHandler(this.panelUser_Paint);
            // 
            // panelName
            // 
            this.panelName.Controls.Add(this.BLogOut);
            this.panelName.Controls.Add(this.pictureUser);
            this.panelName.Controls.Add(this.labelUsername);
            this.panelName.Location = new System.Drawing.Point(0, 0);
            this.panelName.Name = "panelName";
            this.panelName.Size = new System.Drawing.Size(458, 55);
            this.panelName.TabIndex = 11;
            // 
            // BLogOut
            // 
            this.BLogOut.Location = new System.Drawing.Point(406, 9);
            this.BLogOut.Name = "BLogOut";
            this.BLogOut.Size = new System.Drawing.Size(36, 36);
            this.BLogOut.TabIndex = 9;
            this.BLogOut.TabStop = false;
            this.BLogOut.Click += new System.EventHandler(this.BLogOut_Click);
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Font = new System.Drawing.Font("OCR A Extended", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsername.Location = new System.Drawing.Point(64, 17);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(63, 17);
            this.labelUsername.TabIndex = 8;
            this.labelUsername.Text = "@user";
            this.labelUsername.Click += new System.EventHandler(this.labelUsername_Click);
            // 
            // BEmoji
            // 
            this.BEmoji.Location = new System.Drawing.Point(388, 323);
            this.BEmoji.Name = "BEmoji";
            this.BEmoji.Size = new System.Drawing.Size(24, 21);
            this.BEmoji.TabIndex = 10;
            this.BEmoji.TabStop = false;
            this.BEmoji.Click += new System.EventHandler(this.BEmoji_Click);
            // 
            // BEnviarMsj
            // 
            this.BEnviarMsj.Location = new System.Drawing.Point(418, 323);
            this.BEnviarMsj.Name = "BEnviarMsj";
            this.BEnviarMsj.Size = new System.Drawing.Size(24, 21);
            this.BEnviarMsj.TabIndex = 9;
            this.BEnviarMsj.TabStop = false;
            this.BEnviarMsj.Click += new System.EventHandler(this.BEnviarMsj_Click);
            // 
            // RichMessage
            // 
            this.RichMessage.Location = new System.Drawing.Point(22, 297);
            this.RichMessage.Name = "RichMessage";
            this.RichMessage.Size = new System.Drawing.Size(420, 20);
            this.RichMessage.TabIndex = 12;
            this.RichMessage.Text = "";
            this.RichMessage.TextChanged += new System.EventHandler(this.RichMessage_TextChanged);
            // 
            // panelEmoji
            // 
            this.panelEmoji.Controls.Add(this.pictureBox6);
            this.panelEmoji.Controls.Add(this.pictureBox5);
            this.panelEmoji.Controls.Add(this.pictureBox4);
            this.panelEmoji.Controls.Add(this.pictureBox3);
            this.panelEmoji.Controls.Add(this.pictureBox2);
            this.panelEmoji.Controls.Add(this.happy);
            this.panelEmoji.Location = new System.Drawing.Point(262, 164);
            this.panelEmoji.Name = "panelEmoji";
            this.panelEmoji.Size = new System.Drawing.Size(180, 100);
            this.panelEmoji.TabIndex = 13;
            this.panelEmoji.Paint += new System.Windows.Forms.PaintEventHandler(this.panelEmoji_Paint);
            // 
            // happy
            // 
            this.happy.Location = new System.Drawing.Point(3, 13);
            this.happy.Name = "happy";
            this.happy.Size = new System.Drawing.Size(22, 23);
            this.happy.TabIndex = 0;
            this.happy.TabStop = false;
            this.happy.Click += new System.EventHandler(this.happy_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(3, 42);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(22, 23);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(3, 71);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(22, 23);
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(31, 13);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(22, 23);
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Location = new System.Drawing.Point(31, 42);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(22, 23);
            this.pictureBox5.TabIndex = 4;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Location = new System.Drawing.Point(31, 71);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(22, 23);
            this.pictureBox6.TabIndex = 5;
            this.pictureBox6.TabStop = false;
            // 
            // FormChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 375);
            this.Controls.Add(this.panelSalas);
            this.Controls.Add(this.panelUser);
            this.Name = "FormChat";
            this.Text = "FormChat";
            this.Load += new System.EventHandler(this.FormChat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureUser)).EndInit();
            this.panelSalas.ResumeLayout(false);
            this.panelSalas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BDeleteChat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BNewChat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSala)).EndInit();
            this.panelSalasName.ResumeLayout(false);
            this.panelSalasName.PerformLayout();
            this.panelUser.ResumeLayout(false);
            this.panelName.ResumeLayout(false);
            this.panelName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BLogOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BEmoji)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BEnviarMsj)).EndInit();
            this.panelEmoji.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.happy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureUser;
        private System.Windows.Forms.Label labelSalas;
        private System.Windows.Forms.Panel panelSalas;
        private System.Windows.Forms.Panel panelUser;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.PictureBox BEnviarMsj;
        private System.Windows.Forms.PictureBox BEmoji;
        private System.Windows.Forms.Panel panelName;
        private System.Windows.Forms.Panel panelSalasName;
        private System.Windows.Forms.PictureBox pictureSala;
        private System.Windows.Forms.Label labelChats;
        private System.Windows.Forms.PictureBox BLogOut;
        private System.Windows.Forms.TreeView treeViewChats;
        private System.Windows.Forms.PictureBox BNewChat;
        private System.Windows.Forms.PictureBox BDeleteChat;
        private System.Windows.Forms.RichTextBox RichMessage;
        private System.Windows.Forms.Panel panelEmoji;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox happy;
    }
}
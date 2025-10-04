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
            this.BNewChat = new System.Windows.Forms.PictureBox();
            this.treeViewChats = new System.Windows.Forms.TreeView();
            this.labelChats = new System.Windows.Forms.Label();
            this.pictureSala = new System.Windows.Forms.PictureBox();
            this.panelSalasName = new System.Windows.Forms.Panel();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.panelUser = new System.Windows.Forms.Panel();
            this.panelName = new System.Windows.Forms.Panel();
            this.BLogOut = new System.Windows.Forms.PictureBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.BEmoji = new System.Windows.Forms.PictureBox();
            this.BEnviarMsj = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUser)).BeginInit();
            this.panelSalas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BNewChat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSala)).BeginInit();
            this.panelSalasName.SuspendLayout();
            this.panelUser.SuspendLayout();
            this.panelName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BLogOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BEmoji)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BEnviarMsj)).BeginInit();
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
            // textBoxMessage
            // 
            this.textBoxMessage.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMessage.Location = new System.Drawing.Point(22, 296);
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(420, 21);
            this.textBoxMessage.TabIndex = 7;
            this.textBoxMessage.Text = "Escribe un mensaje";
            this.textBoxMessage.TextChanged += new System.EventHandler(this.textBoxMessage_TextChanged);
            // 
            // panelUser
            // 
            this.panelUser.Controls.Add(this.panelName);
            this.panelUser.Controls.Add(this.BEmoji);
            this.panelUser.Controls.Add(this.BEnviarMsj);
            this.panelUser.Controls.Add(this.textBoxMessage);
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
            this.labelUsername.Location = new System.Drawing.Point(64, 20);
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
            ((System.ComponentModel.ISupportInitialize)(this.BNewChat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSala)).EndInit();
            this.panelSalasName.ResumeLayout(false);
            this.panelSalasName.PerformLayout();
            this.panelUser.ResumeLayout(false);
            this.panelUser.PerformLayout();
            this.panelName.ResumeLayout(false);
            this.panelName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BLogOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BEmoji)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BEnviarMsj)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureUser;
        private System.Windows.Forms.Label labelSalas;
        private System.Windows.Forms.Panel panelSalas;
        private System.Windows.Forms.TextBox textBoxMessage;
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
    }
}
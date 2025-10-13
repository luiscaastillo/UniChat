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
            this.panelEmoji = new System.Windows.Forms.Panel();
            this.happy = new System.Windows.Forms.PictureBox();
            this.RichMessage = new System.Windows.Forms.RichTextBox();
            this.panelName = new System.Windows.Forms.Panel();
            this.BLogOut = new System.Windows.Forms.PictureBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.BEmoji = new System.Windows.Forms.PictureBox();
            this.BEnviarMsj = new System.Windows.Forms.PictureBox();
            this.close = new System.Windows.Forms.PictureBox();
            this.sad = new System.Windows.Forms.PictureBox();
            this.cry = new System.Windows.Forms.PictureBox();
            this.eww = new System.Windows.Forms.PictureBox();
            this.like = new System.Windows.Forms.PictureBox();
            this.angry = new System.Windows.Forms.PictureBox();
            this.corazon = new System.Windows.Forms.PictureBox();
            this.lover = new System.Windows.Forms.PictureBox();
            this.kiss = new System.Windows.Forms.PictureBox();
            this.pray = new System.Windows.Forms.PictureBox();
            this.ajajaja = new System.Windows.Forms.PictureBox();
            this.cool = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUser)).BeginInit();
            this.panelSalas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BDeleteChat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BNewChat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSala)).BeginInit();
            this.panelSalasName.SuspendLayout();
            this.panelUser.SuspendLayout();
            this.panelEmoji.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.happy)).BeginInit();
            this.panelName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BLogOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BEmoji)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BEnviarMsj)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eww)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.like)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.angry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.corazon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lover)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kiss)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ajajaja)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cool)).BeginInit();
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
            // panelEmoji
            // 
            this.panelEmoji.Controls.Add(this.cool);
            this.panelEmoji.Controls.Add(this.ajajaja);
            this.panelEmoji.Controls.Add(this.pray);
            this.panelEmoji.Controls.Add(this.kiss);
            this.panelEmoji.Controls.Add(this.lover);
            this.panelEmoji.Controls.Add(this.corazon);
            this.panelEmoji.Controls.Add(this.angry);
            this.panelEmoji.Controls.Add(this.like);
            this.panelEmoji.Controls.Add(this.eww);
            this.panelEmoji.Controls.Add(this.cry);
            this.panelEmoji.Controls.Add(this.sad);
            this.panelEmoji.Controls.Add(this.close);
            this.panelEmoji.Controls.Add(this.happy);
            this.panelEmoji.Location = new System.Drawing.Point(292, 154);
            this.panelEmoji.Name = "panelEmoji";
            this.panelEmoji.Size = new System.Drawing.Size(163, 137);
            this.panelEmoji.TabIndex = 13;
            this.panelEmoji.Paint += new System.Windows.Forms.PaintEventHandler(this.panelEmoji_Paint);
            // 
            // happy
            // 
            this.happy.Location = new System.Drawing.Point(9, 22);
            this.happy.Name = "happy";
            this.happy.Size = new System.Drawing.Size(30, 30);
            this.happy.TabIndex = 0;
            this.happy.TabStop = false;
            this.happy.Click += new System.EventHandler(this.happy_Click);
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
            // close
            // 
            this.close.Location = new System.Drawing.Point(147, 3);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(13, 13);
            this.close.TabIndex = 6;
            this.close.TabStop = false;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // sad
            // 
            this.sad.Location = new System.Drawing.Point(9, 59);
            this.sad.Name = "sad";
            this.sad.Size = new System.Drawing.Size(30, 30);
            this.sad.TabIndex = 7;
            this.sad.TabStop = false;
            this.sad.Click += new System.EventHandler(this.sad_Click);
            // 
            // cry
            // 
            this.cry.Location = new System.Drawing.Point(45, 22);
            this.cry.Name = "cry";
            this.cry.Size = new System.Drawing.Size(30, 30);
            this.cry.TabIndex = 8;
            this.cry.TabStop = false;
            this.cry.Click += new System.EventHandler(this.cry_Click);
            // 
            // eww
            // 
            this.eww.Location = new System.Drawing.Point(45, 59);
            this.eww.Name = "eww";
            this.eww.Size = new System.Drawing.Size(30, 30);
            this.eww.TabIndex = 9;
            this.eww.TabStop = false;
            this.eww.Click += new System.EventHandler(this.eww_Click);
            // 
            // like
            // 
            this.like.Location = new System.Drawing.Point(45, 95);
            this.like.Name = "like";
            this.like.Size = new System.Drawing.Size(30, 30);
            this.like.TabIndex = 10;
            this.like.TabStop = false;
            this.like.Click += new System.EventHandler(this.like_Click);
            // 
            // angry
            // 
            this.angry.Location = new System.Drawing.Point(9, 95);
            this.angry.Name = "angry";
            this.angry.Size = new System.Drawing.Size(30, 30);
            this.angry.TabIndex = 11;
            this.angry.TabStop = false;
            // 
            // corazon
            // 
            this.corazon.Location = new System.Drawing.Point(81, 22);
            this.corazon.Name = "corazon";
            this.corazon.Size = new System.Drawing.Size(30, 30);
            this.corazon.TabIndex = 12;
            this.corazon.TabStop = false;
            this.corazon.Click += new System.EventHandler(this.corazon_Click);
            // 
            // lover
            // 
            this.lover.Location = new System.Drawing.Point(81, 59);
            this.lover.Name = "lover";
            this.lover.Size = new System.Drawing.Size(30, 30);
            this.lover.TabIndex = 13;
            this.lover.TabStop = false;
            this.lover.Click += new System.EventHandler(this.lover_Click);
            // 
            // kiss
            // 
            this.kiss.Location = new System.Drawing.Point(81, 95);
            this.kiss.Name = "kiss";
            this.kiss.Size = new System.Drawing.Size(30, 30);
            this.kiss.TabIndex = 14;
            this.kiss.TabStop = false;
            this.kiss.Click += new System.EventHandler(this.kiss_Click);
            // 
            // pray
            // 
            this.pray.Location = new System.Drawing.Point(117, 22);
            this.pray.Name = "pray";
            this.pray.Size = new System.Drawing.Size(30, 30);
            this.pray.TabIndex = 15;
            this.pray.TabStop = false;
            this.pray.Click += new System.EventHandler(this.pray_Click);
            // 
            // ajajaja
            // 
            this.ajajaja.Location = new System.Drawing.Point(117, 59);
            this.ajajaja.Name = "ajajaja";
            this.ajajaja.Size = new System.Drawing.Size(30, 30);
            this.ajajaja.TabIndex = 16;
            this.ajajaja.TabStop = false;
            this.ajajaja.Click += new System.EventHandler(this.ajajaja_Click);
            // 
            // cool
            // 
            this.cool.Location = new System.Drawing.Point(117, 95);
            this.cool.Name = "cool";
            this.cool.Size = new System.Drawing.Size(30, 30);
            this.cool.TabIndex = 17;
            this.cool.TabStop = false;
            this.cool.Click += new System.EventHandler(this.cool_Click);
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
            this.panelEmoji.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.happy)).EndInit();
            this.panelName.ResumeLayout(false);
            this.panelName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BLogOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BEmoji)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BEnviarMsj)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eww)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.like)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.angry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.corazon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lover)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kiss)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ajajaja)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cool)).EndInit();
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
        private System.Windows.Forms.PictureBox happy;
        private System.Windows.Forms.PictureBox like;
        private System.Windows.Forms.PictureBox eww;
        private System.Windows.Forms.PictureBox cry;
        private System.Windows.Forms.PictureBox sad;
        private System.Windows.Forms.PictureBox close;
        private System.Windows.Forms.PictureBox angry;
        private System.Windows.Forms.PictureBox kiss;
        private System.Windows.Forms.PictureBox lover;
        private System.Windows.Forms.PictureBox corazon;
        private System.Windows.Forms.PictureBox cool;
        private System.Windows.Forms.PictureBox ajajaja;
        private System.Windows.Forms.PictureBox pray;
    }
}
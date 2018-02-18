namespace NiceHashMiner.Forms
{
    partial class Form_Authorization
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
            this.textInfo = new System.Windows.Forms.TextBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.textUsername = new System.Windows.Forms.TextBox();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.buttonAuth = new System.Windows.Forms.Button();
            this.linkRecoveryLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // textInfo
            // 
            this.textInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textInfo.Enabled = false;
            this.textInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textInfo.Location = new System.Drawing.Point(26, 12);
            this.textInfo.Multiline = true;
            this.textInfo.Name = "textInfo";
            this.textInfo.Size = new System.Drawing.Size(384, 48);
            this.textInfo.TabIndex = 0;
            this.textInfo.Text = "Перед началом работы необходимо авторизоваться под своим аккаунтом.\r\nЕсли у Вас е" +
    "ще нет аккаунта, обратитесь к своему супервайзеру для получения ссылки на регист" +
    "рацию.";
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(185, 63);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(55, 13);
            this.labelEmail.TabIndex = 1;
            this.labelEmail.Text = "Ваш email";
            // 
            // textUsername
            // 
            this.textUsername.Location = new System.Drawing.Point(128, 79);
            this.textUsername.Name = "textUsername";
            this.textUsername.Size = new System.Drawing.Size(166, 20);
            this.textUsername.TabIndex = 2;
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(128, 129);
            this.textPassword.Name = "textPassword";
            this.textPassword.PasswordChar = '*';
            this.textPassword.Size = new System.Drawing.Size(166, 20);
            this.textPassword.TabIndex = 3;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(185, 113);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(45, 13);
            this.labelPassword.TabIndex = 4;
            this.labelPassword.Text = "Пароль";
            // 
            // buttonAuth
            // 
            this.buttonAuth.Location = new System.Drawing.Point(153, 183);
            this.buttonAuth.Name = "buttonAuth";
            this.buttonAuth.Size = new System.Drawing.Size(106, 23);
            this.buttonAuth.TabIndex = 5;
            this.buttonAuth.Text = "Авторизоваться";
            this.buttonAuth.UseVisualStyleBackColor = true;
            this.buttonAuth.Click += new System.EventHandler(this.buttonAuth_Click);
            // 
            // linkRecoveryLink
            // 
            this.linkRecoveryLink.AutoSize = true;
            this.linkRecoveryLink.LinkColor = System.Drawing.Color.Red;
            this.linkRecoveryLink.Location = new System.Drawing.Point(37, 160);
            this.linkRecoveryLink.Name = "linkRecoveryLink";
            this.linkRecoveryLink.Size = new System.Drawing.Size(398, 13);
            this.linkRecoveryLink.TabIndex = 6;
            this.linkRecoveryLink.TabStop = true;
            this.linkRecoveryLink.Text = "Ошибка авторизации. Вы можете восстановить аккаунт, перейдя по ссылке";
            this.linkRecoveryLink.Visible = false;
            this.linkRecoveryLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRecoveryLink_LinkClicked);
            // 
            // Form_Authorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 237);
            this.Controls.Add(this.linkRecoveryLink);
            this.Controls.Add(this.buttonAuth);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.textUsername);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.textInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form_Authorization";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация в системе";
            this.Activated += new System.EventHandler(this.Form_Authorization_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Authorization_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textInfo;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox textUsername;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Button buttonAuth;
        private System.Windows.Forms.LinkLabel linkRecoveryLink;
    }
}

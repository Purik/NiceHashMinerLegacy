namespace NiceHashMiner.Forms
{
    partial class Form_DriverUpdater
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
            this.linkIOBit = new System.Windows.Forms.LinkLabel();
            this.labelInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textInfo
            // 
            this.textInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textInfo.Enabled = false;
            this.textInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textInfo.Location = new System.Drawing.Point(51, 12);
            this.textInfo.Multiline = true;
            this.textInfo.Name = "textInfo";
            this.textInfo.Size = new System.Drawing.Size(384, 48);
            this.textInfo.TabIndex = 1;
            this.textInfo.Text = "Приложение обнаружило, что не все устройства готовы к работе. Обновите свои драйв" +
    "ера, пройдя по ссылке ниже\r\n";
            // 
            // linkIOBit
            // 
            this.linkIOBit.AutoSize = true;
            this.linkIOBit.LinkColor = System.Drawing.Color.Red;
            this.linkIOBit.Location = new System.Drawing.Point(182, 100);
            this.linkIOBit.Name = "linkIOBit";
            this.linkIOBit.Size = new System.Drawing.Size(81, 13);
            this.linkIOBit.TabIndex = 7;
            this.linkIOBit.TabStop = true;
            this.linkIOBit.Text = "IObit Uninstaller";
            this.linkIOBit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRecoveryLink_LinkClicked);
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(58, 73);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(10, 13);
            this.labelInfo.TabIndex = 112;
            this.labelInfo.Text = "-";
            // 
            // Form_DriverUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 133);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.linkIOBit);
            this.Controls.Add(this.textInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_DriverUpdater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Предупреждение";
            this.Activated += new System.EventHandler(this.Form_DriverUpdater_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_DriverUpdater_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textInfo;
        private System.Windows.Forms.LinkLabel linkIOBit;
        private System.Windows.Forms.Label labelInfo;
    }
}
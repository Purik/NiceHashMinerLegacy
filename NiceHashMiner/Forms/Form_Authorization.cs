using System;
using System.Windows.Forms;
using NiceHashMiner.Configs;

namespace NiceHashMiner.Forms
{
    public partial class Form_Authorization : Form
    {
        public Form_Authorization()
        {
            InitializeComponent();
        }


        private void buttonAuth_Click(object sender, EventArgs e)
        {
            ConfigManager.GeneralConfig.Account = null;
            try
            {
                WebAPI.AccountAnswer account = WebAPI.Account(textUsername.Text, textPassword.Text);
                if (account != null)
                {
                    if (account.success)
                    {
                        ConfigManager.GeneralConfig.Account = 
                            new Configs.Data.MinerAccount(
                                account.username, 
                                account.first_name, 
                                account.last_name, 
                                account.uid
                            );
                        ConfigManager.GeneralConfig.WorkerName = account.worker_name;
                        ConfigManager.GeneralConfigFileCommit();
                        Close();
                    }
                    else
                    {
                        linkRecoveryLink.Visible = true;
                    }
                }
            }
            finally
            {
                ConfigManager.GeneralConfigFileCommit();
                ConfigManager.SecretConfig.Set(usernameKey, textUsername.Text);
            }
        }

        private void Form_Authorization_Activated(object sender, EventArgs e)
        {
            if (ConfigManager.SecretConfig.Exists(usernameKey)) {
                textUsername.Text = ConfigManager.SecretConfig.Get(usernameKey);
            }

            if (textUsername.Text.Length == 0)
            {
                textUsername.Focus();
            }
            else
            {
                textPassword.Focus();
            }
        }

        private static Guid usernameKey = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709");
        private static Guid passwordKey = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857701");

        private void Form_Authorization_FormClosed(object sender, FormClosedEventArgs e)
        {
            ConfigManager.SecretConfig.Set(usernameKey, textUsername.Text);
        }

        private void linkRecoveryLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkRecoveryLink.LinkVisited = true;
            // Navigate to a URL.
            string url = String.Format("{0}/password-recovery-init?for={1}", ConfigManager.GeneralConfig.ServerAddress, textUsername.Text);
            System.Diagnostics.Process.Start(url);
        }
    }
}

using NiceHashMiner.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NiceHashMiner.Forms
{
    public partial class Form_DriverUpdater : Form
    {
        public Form_DriverUpdater()
        {
            InitializeComponent();
        }

        private void linkRecoveryLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkIOBit.LinkVisited = true;
            System.Diagnostics.Process.Start("https://ru.iobit.com/advanceduninstaller");
        }

        private void Form_DriverUpdater_Activated(object sender, EventArgs e)
        {
            if (ComputeDeviceManager.Avaliable.AvailGpUs == 0)
            {
                labelInfo.Text = "Не обнаружено видеокарт. Обновите драйвера.";
            }
            else if (ComputeDeviceManager.Avaliable.AvailCpus == 0)
            {
                labelInfo.Text = "Не обнаружено CPU. Обновите драйвера.";
            }
            else
            {
                labelInfo.Visible = false;
            }
        }

        private void Form_DriverUpdater_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI
{
    public partial class BSSettingsView : UserControl
    {
        public BSSettingsView()
        {
            InitializeComponent();
        }

        private void BSSettingsView_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        public void LoadSettings()
        {
            String name = Settings.BarCode.Name();
            portBox.Text = name;

            int br = Settings.BarCode.BaudRate();
            baudRateBox.Text = Convert.ToString(br);

            System.IO.Ports.Parity p = Settings.BarCode.Parity();
            switch (p)
            {
                case System.IO.Ports.Parity.Even:
                    parityBox.SelectedIndex = 1;
                    break;
                case System.IO.Ports.Parity.Odd:
                    parityBox.SelectedIndex = 2;
                    break;
                case System.IO.Ports.Parity.None:
                default:
                    parityBox.SelectedIndex = 0;
                    break;
            }

            ushort db = Settings.BarCode.DataBits();
            dataBitsBox.Text = Convert.ToString(db);

            System.IO.Ports.StopBits sb = Settings.BarCode.StopBits();
            switch (sb)
            {
                case System.IO.Ports.StopBits.One:
                    stopBitsBox.SelectedIndex = 1;
                    break;
                case System.IO.Ports.StopBits.Two:
                    stopBitsBox.SelectedIndex = 2;
                    break;
                case System.IO.Ports.StopBits.None:
                default:
                    stopBitsBox.SelectedIndex = 0;
                    break;
            }
        }

        public void SaveSettings()
        {
            Settings.BarCode.SetName(portBox.Text);
            try
            {
                int br = Convert.ToInt32(baudRateBox.Text);
                Settings.BarCode.SetBaudRate(br);
            }
            catch (Exception)
            {
            }

            try
            {
                ushort db = Convert.ToUInt16(dataBitsBox.Text);
                Settings.BarCode.SetDataBits(db);
            }
            catch (Exception)
            {
            }

            System.IO.Ports.StopBits sb = System.IO.Ports.StopBits.None;
            switch (stopBitsBox.SelectedIndex)
            {
                case 0:
                    sb = System.IO.Ports.StopBits.None;
                    break;
                case 1:
                    sb = System.IO.Ports.StopBits.One;
                    break;
                case 2:
                    sb = System.IO.Ports.StopBits.Two;
                    break;
            }
            Settings.BarCode.SetStopBits(sb);

        }
    }
}

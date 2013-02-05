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
    public partial class AdvancedBarcodeSettingsView : UserControl
    {
        public AdvancedBarcodeSettingsView()
        {
            InitializeComponent();
        }

        private void AdvancedBarcodeSettingsView_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void LoadSettings()
        {
            bool isNew = Settings.BarCode.NewType();

            radioButtonHid.Checked = isNew;
            radioButtonCom.Checked = !isNew;

            RefreshControls();
            bsSettingsView.LoadSettings();
        }

        public void SaveSettings()
        {
            bool isNew = radioButtonHid.Checked;
            Settings.BarCode.SetNewType(isNew);
            bsSettingsView.SaveSettings();
        }

        private void radioButtonSwitch(object sender, EventArgs e)
        {
            RefreshControls();
        }

        private void RefreshControls()
        {
            bsSettingsView.Enabled = radioButtonCom.Checked;
        }
    }
}

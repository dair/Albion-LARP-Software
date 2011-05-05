﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientUI
{
    public partial class ClientSettings : UI.DBObjectForm
    {
        public ClientSettings()
        {
            InitializeComponent();
        }

        public ClientSettings(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public void setDeviceIdEnabled(bool b)
        {
            dbSettingsView.setDeviceIdEnabled(b);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            dbSettingsView.SaveSettings();
            bsSettingsView.SaveSettings();

            Settings.CashDesk.SetPersonId(Convert.ToUInt64(codeBox.Text));

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ClientSettings_Load(object sender, EventArgs e)
        {
            codeBox.Text = Convert.ToString(Settings.CashDesk.GetPersonId());
        }
    }
}

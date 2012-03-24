/* ***********************************************************************
 * (C) 2008-2012 Vladimir Lebedev-Schmidthof <vladimir@schmidthof.com>
 * Made for Albion Games (http://albiongames.org)
 * 
 * 
 *            DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
 *                    Version 2, December 2004

 * Copyright (C) 2004 Sam Hocevar <sam@hocevar.net>
 * 
 * Everyone is permitted to copy and distribute verbatim or modified
 * copies of this license document, and changing it is allowed as long
 * as the name is changed.

 *           DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
 *   TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION

 *  0. You just DO WHAT THE FUCK YOU WANT TO.
 * *********************************************************************** */

using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using Settings;
using Database;

namespace UI
{
    public partial class DBSettingsView : DBObjectUserControl
    {


        public DBSettingsView()
            : base()
        {
            InitializeComponent();
        }

        public DBSettingsView(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public void setDeviceIdEnabled(bool d)
        {
            deviceIdBox.Enabled = d;
            deviceIdBox.ReadOnly = !d;
        }

        private void SettingsView_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        public void LoadSettings()
        {
            RegistryKey key = Registry.CurrentUser;
            key = key.OpenSubKey("Software\\Bladerunner", true);
            hostTextBox.Text = Settings.Database.GetDBHost();
            portTextBox.Text = Convert.ToString(Settings.Database.GetDBPort());
            userTextBox.Text = Settings.Database.GetDBUser();
            passwordTextBox.Text = Settings.Database.GetDBPassword();
            dbnameTextBox.Text = Settings.Database.GetDBName();
            if (deviceIdBox.Enabled)
            {
                deviceIdBox.Text = Convert.ToString(Settings.Database.GetDeviceId());
            }
            applySettings();
        }

        public void SaveSettings()
        {
            Settings.Database.SetDBHost(hostTextBox.Text);
            Settings.Database.SetDBName(dbnameTextBox.Text);
            Settings.Database.SetDBUser(userTextBox.Text);
            Settings.Database.SetDBPassword(passwordTextBox.Text);
            Settings.Database.SetDBPort(Convert.ToUInt16(portTextBox.Text));
            if (deviceIdBox.Enabled)
            {
                Settings.Database.SetDeviceId(Convert.ToUInt64(deviceIdBox.Text));
            }
            applySettings();
        }

        private void applySettings()
        {
            Database.Connection db = getDatabase();
            if (db != null)
            {
                db.setIpAddress(hostTextBox.Text);
                db.setPort(Convert.ToUInt64(portTextBox.Text));
                db.setUserName(userTextBox.Text);
                db.setPassword(passwordTextBox.Text);
                db.setDatabase(dbnameTextBox.Text);
            }
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            applySettings();

            bool res = getDatabase().test();
            if (res)
                logBox.Text = "Success";
            else
                logBox.Text = "Failure";
        }
    }
}

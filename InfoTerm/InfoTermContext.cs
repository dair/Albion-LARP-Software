﻿/* ***********************************************************************
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Settings;
using Logger;

namespace InfoTerm
{
    class InfoTermContext: ApplicationContext
    {
        private Database.Connection database;
        ClientUI.ClientSettings settings;
        BarCode.ReaderControl RC = null;
        Logging logger = null;
        InfoTermForm mainForm;

        // ----------------------------------------------------------
        public InfoTermContext()
        {
            database = new Database.Connection();
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

            logger = new Logging();

            settings = new ClientUI.ClientSettings(database);

            if ((Settings.Settings.HasSettings()) || settings.ShowDialog() == DialogResult.OK)
            {
                showMainWindow();
            }
            else
            {
                ExitThread();
            }
        }

        void mainForm_Closed(object sender, EventArgs e)
        {
            ExitThread();
        }

        void showMainWindow()
        {
            RC = new BarCode.ReaderControl();

            database.setIpAddress(Settings.Database.GetDBHost());
            database.setDatabase(Settings.Database.GetDBName());
            database.setPassword(Settings.Database.GetDBPassword());
            database.setPort(Settings.Database.GetDBPort());
            database.setUserName(Settings.Database.GetDBUser());

            mainForm = new InfoTermForm(database, settings, RC, logger);
            mainForm.Closed += new EventHandler(mainForm_Closed);

            mainForm.Show();
        }

        // ----------------------------------------------------------
        public void OnApplicationExit(object sender, EventArgs e)
        {
            if (RC != null)
                RC.Stop();
        }
    }
}

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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Settings;
using Logger;

namespace TimeMachine
{
    class TimeMachineContext: ApplicationContext
    {
        private Database.Connection database;
        ClientUI.ClientSettings settings;
        Logging logger = null;
        TimeMachineForm mainForm;

        public static Dictionary<String, Object> data = new Dictionary<String, Object>();

        public static Object getData(string key)
        {
            if (data.ContainsKey(key))
                return data[key];
            return null;
        }

        public static void setData(string key, object v)
        {
            data[key] = v;
        }

        public static DateTime realToGame(DateTime dt)
        {
            DateTime now = DateTime.Now;
            return dt.AddYears(2565 - DateTime.Now.Year);
        }

        public static DateTime gameToReal(DateTime dt)
        {
            return dt.AddYears(DateTime.Now.Year - 2565);
        }

        // ----------------------------------------------------------
        public TimeMachineContext()
        {
            database = new Database.Connection();
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

            logger = new Logging();

            settings = new ClientUI.ClientSettings();
            settings.setDatabase(database);

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
            database.setIpAddress(Settings.Database.GetDBHost());
            database.setDatabase(Settings.Database.GetDBName());
            database.setPassword(Settings.Database.GetDBPassword());
            database.setPort(Settings.Database.GetDBPort());
            database.setUserName(Settings.Database.GetDBUser());

            mainForm = new TimeMachineForm();//database, settings, logger);
            mainForm.setSettings(settings);
            mainForm.setConnection(database);
            mainForm.Closed += new EventHandler(mainForm_Closed);

            mainForm.Show();
        }

        // ----------------------------------------------------------
        public void OnApplicationExit(object sender, EventArgs e)
        {
        }
    }
}

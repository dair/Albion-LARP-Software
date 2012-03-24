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
using System.Windows.Forms;
using System.Text;
using Settings;
using Database;
using UI;

namespace Master
{
    class Context: ApplicationContext
    {
        private UI.DBDialog settingsForm;
        private MainWindow mainForm;
        private Logger.Logging logging;

        private Database.Connection database;

        // ----------------------------------------------------------
        public Context()
        {
            database = new Database.Connection();
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

            logging = new Logger.Logging();
            logging.Show();

            settingsForm = new UI.DBDialog(database);
            settingsForm.Closed += new EventHandler(this.settingsForm_Closed);
            settingsForm.Show();

        }

        // ----------------------------------------------------------
        public void OnApplicationExit(object sender, EventArgs e)
        {
        }

        // ----------------------------------------------------------
        public void settingsForm_Closed(object sender, EventArgs e)
        {
            if (settingsForm.DialogResult == DialogResult.OK)
            {
                mainForm = new MainWindow(database);
                mainForm.Closed += new EventHandler(this.mainForm_Closed);

                mainForm.Show();
            }
            else
            {
                ExitThread();
            }
        }

        // ----------------------------------------------------------
        public void mainForm_Closed(object sender, EventArgs e)
        {
            ExitThread();
        }
    }
}

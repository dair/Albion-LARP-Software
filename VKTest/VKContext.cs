using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Settings;
using Logger;

namespace VKTest
{
    class VKContext: ApplicationContext
    {
        private Database.Connection database;
        ClientUI.ClientSettings settings;
        Logging logger = null;
        VKForm mainForm;

        // ----------------------------------------------------------
        public VKContext()
        {
            database = new Database.Connection();
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

            logger = new Logging();

            settings = new ClientUI.ClientSettings(database);
            settings.setDeviceIdEnabled(true);

            if (Settings.Settings.HasSettings() || settings.ShowDialog() == DialogResult.OK)
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

            mainForm = new VKForm(database, settings, logger);
            mainForm.Closed += new EventHandler(mainForm_Closed);

            mainForm.Show();
        }

        // ----------------------------------------------------------
        public void OnApplicationExit(object sender, EventArgs e)
        {
        }
    }
}

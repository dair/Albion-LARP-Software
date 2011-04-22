using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Settings;
using Logger;

namespace ATM
{
    class ATMContext: ApplicationContext
    {
        private Database.Connection database;
        ClientUI.ClientSettings settings;
        BarCode.ReaderControl RC = null;
        Logging logger = null;
        ATMForm mainForm;

        // ----------------------------------------------------------
        public ATMContext()
        {
            database = new Database.Connection();
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

            logger = new Logging();

            settings = new ClientUI.ClientSettings(database);

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
            RC = new BarCode.ReaderControl();

            database.setIpAddress(Settings.Database.GetDBHost());
            database.setDatabase(Settings.Database.GetDBName());
            database.setPassword(Settings.Database.GetDBPassword());
            database.setPort(Settings.Database.GetDBPort());
            database.setUserName(Settings.Database.GetDBUser());

            mainForm = new ATMForm(database, settings, RC, logger);
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

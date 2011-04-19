using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Settings;

namespace ATM
{
    class ATMContext: ApplicationContext
    {
        private Database.Connection database;
        ClientUI.ClientSettings settings;
        ATMForm mainForm;

        // ----------------------------------------------------------
        public ATMContext()
        {
            database = new Database.Connection();
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

            settings = new ClientUI.ClientSettings(database);
            mainForm = new ATMForm(database);

            if (!Settings.Settings.HasSettings())
            {
                settings.Closed += new EventHandler(settings_Closed);
                settings.Show();
            }
            else
            {
                showMainWindow();
            }
        }

        void settings_Closed(object sender, EventArgs e)
        {
            settings.Closed -= new EventHandler(settings_Closed);
            showMainWindow();
        }

        void showMainWindow()
        {
            mainForm.Show();
        }

        // ----------------------------------------------------------
        public void OnApplicationExit(object sender, EventArgs e)
        {
        }

    }
}

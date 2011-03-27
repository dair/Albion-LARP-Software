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

        private Database.Connection database;

        // ----------------------------------------------------------
        public Context()
        {
            database = new Database.Connection();

            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

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

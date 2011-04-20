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
        BarCode.ReaderControl RC = null;

        ATMForm mainForm;

        // ----------------------------------------------------------
        public ATMContext()
        {
            database = new Database.Connection();
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

            settings = new ClientUI.ClientSettings(database);

            if (settings.ShowDialog() == DialogResult.OK)
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

            mainForm = new ATMForm(database, settings, RC);
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

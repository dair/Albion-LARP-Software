using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ClientUI;

namespace TimeMachine
{
    public partial class TimeMachineForm : Form
    {
        static DateTime NULLDATETIME = new DateTime();
        protected ClientSettings settings = null;

        Timer realTimeTimer = new Timer();
        DateTime realTime;
        UInt16 realTimeTickCounter = 0;
        Database.Connection connection = null;

        Dictionary<String, TimeMachineControl> pages = new Dictionary<string, TimeMachineControl>();
        String currentPage = null;

        public void showSettings()
        {
            if (settings != null)
                settings.ShowDialog();
        }

        public TimeMachineForm()
        {
            InitializeComponent();

            pages["MAIN_MENU"] = new MainMenu();
            pages["SELECT_PROJECT"] = new SelectProject();
            pages["EDIT_EXPERIMENT"] = new ExperimentEdit();
            pages["ENERGY_REQUEST"] = new EnergyRequest();

            realTimeTimer.Interval = 1000;
            realTimeTimer.Tick += new EventHandler(realTimeTimer_Tick);
            realTimeTimer.Start();
        }

        public void setSettings(ClientSettings s)
        {
            settings = s;
        }

        public void setConnection(Database.Connection c)
        {
            connection = c;
            connection.setMessageBox(false);

            foreach (TimeMachineControl ctr in pages.Values)
            {
                ctr.setConnection(connection);
            }
        }

        public void realTimeTimer_Tick(object sender, EventArgs e)
        {
            if (realTime == NULLDATETIME || realTimeTickCounter % 10 == 0)
            {
                realTime = connection.now();

                realTimeTickCounter = 0;
            }
            else
            {
                realTime = realTime.AddSeconds(1);
            }

            ++realTimeTickCounter;

            if (realTime == NULLDATETIME)
            {
                realTimeLabel.Text = "Ошибка";
                realTimeLabel.ForeColor = Color.Red;
            }
            else
            {
                realTimeLabel.Text = Convert.ToString(TimeMachineContext.realToGame(realTime));
                realTimeLabel.ForeColor = Color.Green;
                update();
            }
        }

        public void setPage(String pageName)
        {
            if (currentPage != null)
            {
                pages[currentPage].onDisappear();
            }
            panel.Controls.Clear();
            currentPage = pageName;
            if (currentPage != null)
            {
                pages[currentPage].onAppear();
                panel.Controls.Add(pages[currentPage]);
                pages[currentPage].Dock = DockStyle.Fill;
            }
        }

        private void TimeMachineForm_Load(object sender, EventArgs e)
        {
            setPage("MAIN_MENU");
        }

        void update()
        {
            pages[currentPage].update();
        }
    }
}

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
        Color bkColor;

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
            pages["LAUNCH_EXPERIMENT"] = new ExperimentLaunch();
            pages["PROGRESS_EXPERIMENT"] = new ExperimentProgress();
            pages["ENERGY_REQUEST"] = new EnergyRequest();
            pages["BLUE_SCREEN"] = new BlueScreen();
            pages["RECOVERY_CONSOLE"] = new RecoveryConsole();

            realTimeTimer.Interval = 1000;
            realTimeTimer.Tick += new EventHandler(realTimeTimer_Tick);
            realTimeTimer.Start();

            bkColor = BackColor;
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
            Panel panel = splitContainer.Panel2;
            Panel titlePanel = splitContainer.Panel1;

            if (currentPage != null)
            {
                pages[currentPage].onDisappear();
            }
            panel.Controls.Clear();
            currentPage = pageName;
            if (currentPage != null)
            {
                panel.Controls.Add(pages[currentPage]);
                pages[currentPage].Focus();
                pages[currentPage].onAppear();
                pages[currentPage].Dock = DockStyle.Fill;
                if (pages[currentPage].BackColor != Color.Transparent)
                {
                    BackColor = pages[currentPage].BackColor;
                    splitContainer.BackColor = pages[currentPage].BackColor;
                }
                else
                {
                    BackColor = bkColor;
                    splitContainer.BackColor = bkColor;
                }

                if (pages[currentPage].isBlueScreen)
                {
                    titlePanel.Hide();
                    splitContainer.SplitterDistance = 0;
                }
                else
                {
                    splitContainer.SplitterDistance = 60;
                    titlePanel.Show();
                }
            }
        }

        private void TimeMachineForm_Load(object sender, EventArgs e)
        {
            UInt64 expId = 0;
            UInt64 launchId = 0;
            connection.unfinishedLaunch(ref launchId, ref expId);

            if (expId == 0)
            {
                setPage("MAIN_MENU");
            }
            else
            {
                DataTable table = new DataTable();
                connection.fillWithExperiments(table, expId);

                TimeMachineContext.setData("experiment_id", expId);
                TimeMachineContext.setData("project_key", table.Rows[0]["PROJECT_KEY"]);
                TimeMachineContext.setData("launch_id", launchId);
                setPage("PROGRESS_EXPERIMENT");
            }
        }

        void update()
        {
            Double bluescreen = connection.getTMStatic("BLUE_SCREEN");
            if (bluescreen == 1 && !pages[currentPage].isBlueScreen)
            {
                setPage("BLUE_SCREEN");
            }
            else if (bluescreen == 0 && pages[currentPage].isBlueScreen)
            {
                TimeMachineForm_Load(null, null);
            }

            pages[currentPage].update();
        }
    }
}

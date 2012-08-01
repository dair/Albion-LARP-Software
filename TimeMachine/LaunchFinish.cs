using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeMachine
{
    public partial class LaunchFinish : TimeMachineControl
    {
        public LaunchFinish():
            base()
        {
            InitializeComponent();
        }

        public override void onAppear()
        {
            base.onAppear();

            UInt64 launchId = id0(TimeMachineContext.getData("launch_id"));
            UInt64 projectKey = id0(TimeMachineContext.getData("project_key"));

            DataTable table = new DataTable();
            db.fillWithLaunch(table, launchId);
            UInt64 expId = id0(table.Rows[0]["EXPERIMENT_ID"]);
            DataTable experiment = new DataTable();
            db.fillWithExperiments(experiment, expId);
            title.Text = "Эксперимент " + Convert.ToString(expId) + " в рамках проекта " + Convert.ToString(projectKey);
            nameText.Text = Convert.ToString(experiment.Rows[0]["NAME"]);
            paramSpace1.Text = Convert.ToString(experiment.Rows[0]["PARAM_SPACE_1"]);
            paramSpace2.Text = Convert.ToString(experiment.Rows[0]["PARAM_SPACE_2"]);
            paramTime.Text = Convert.ToString(experiment.Rows[0]["PARAM_TIME"]);

            mass.Text = Convert.ToString(experiment.Rows[0]["PARAM_MASS"]);
            mass2.Text = Convert.ToString(table.Rows[0]["MASS2"]);
            DateTime from = Convert.ToDateTime(table.Rows[0]["STARTED_AT"]);
            DateTime to = Convert.ToDateTime(table.Rows[0]["ENDED_AT"]);
            timeLabel.Text = "Время эксперимента: с " + Convert.ToString(TimeMachineContext.realToGame(from)) +
                " по " + Convert.ToString(TimeMachineContext.realToGame(to));
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            (ParentForm as TimeMachineForm).setPage("MAIN_MENU");
        }

        private void LaunchFinish_Resize(object sender, EventArgs e)
        {
            Point p = new Point();
            p.X = (this.Width - panel.Width) / 2;
            p.Y = (this.Height - panel.Height) / 2;
            panel.Location = p;
        }
    }
}

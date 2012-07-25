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
    public partial class ExperimentProgress : TimeMachineControl
    {
        UInt64 projectKey = 0;
        int updateCounter = 0;

        public ExperimentProgress() :
            base()
        {
            InitializeComponent();
        }

        private void setError(String err)
        {
            error.Text = err;
            error.ForeColor = Color.Red;
            if (err.Length > 0)
                error.Show();
            else
                error.Hide();
        }

        private void setGood(String msg)
        {
            error.Text = msg;
            error.ForeColor = Color.Green;
            if (msg.Length > 0)
                error.Show();
            else
                error.Hide();
        }

        public override void onAppear()
        {
            base.onAppear();

            UInt64 id = id0(TimeMachineContext.getData("experiment_id"));
            projectKey = id0(TimeMachineContext.getData("project_key"));
            UInt64 launchId = id0(TimeMachineContext.getData("launch_id"));

            title.Text = "Эксперимент " + Convert.ToString(id) + " в рамках проекта " + Convert.ToString(projectKey);
            DataTable table = new DataTable();
            db.fillWithExperiments(table, id);

            if (table.Rows.Count > 0)
            {
                nameText.Text = Convert.ToString(table.Rows[0]["NAME"]);
                paramSpace1.Text = Convert.ToString(table.Rows[0]["PARAM_SPACE_1"]);
                paramSpace2.Text = Convert.ToString(table.Rows[0]["PARAM_SPACE_2"]);
                paramTime.Text = Convert.ToString(table.Rows[0]["PARAM_TIME"]);
                mass.Text = Convert.ToString(table.Rows[0]["PARAM_MASS"]);
            }

            table = new DataTable();
            db.fillWithLaunch(table, launchId);

            setError("");
        }

        public override void update()
        {
            base.update();

            if (updateCounter % 2 == 0)
            {
                alertLabel.ForeColor = Color.Red;
            }
            else
            {
                alertLabel.ForeColor = Color.Black;
            }
            updateCounter++;
        }

        private void ExperimentEdit_Resize(object sender, EventArgs e)
        {
            Point p = new Point();
            p.X = (this.Width - panel.Width) / 2;
            p.Y = (this.Height - panel.Height) / 2;
            panel.Location = p;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            (ParentForm as TimeMachineForm).setPage("EDIT_EXPERIMENT");
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            (ParentForm as TimeMachineForm).setPage("MAIN_MENU");
        }
    }
}


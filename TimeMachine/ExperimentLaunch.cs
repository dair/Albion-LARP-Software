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
    public partial class ExperimentLaunch : TimeMachineControl
    {
        UInt64 projectKey = 0;
        UInt64 currentEnergyRequestId = 0;
        UInt64 massValue = 0;
        int updateCounter = 0;

        public ExperimentLaunch() :
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

            if (id > 0)
            {
                title.Text = "Эксперимент " + Convert.ToString(id) + " в рамках проекта " + Convert.ToString(projectKey);
                DataTable table = new DataTable();
                db.fillWithExperiments(table, id);

                if (table.Rows.Count > 0)
                {
                    nameText.Text = Convert.ToString(table.Rows[0]["NAME"]);
                    paramSpace1.Text = Convert.ToString(table.Rows[0]["PARAM_SPACE_1"]);
                    paramSpace2.Text = Convert.ToString(table.Rows[0]["PARAM_SPACE_2"]);
                    paramTime.Text = Convert.ToString(table.Rows[0]["PARAM_TIME"]);
                    massValue = Convert.ToUInt64(table.Rows[0]["PARAM_MASS"]);
                    mass.Text = Convert.ToString(massValue);
                }

            }
            else
            {
                energyLabel.Text = "";
                title.Text = "Новый эксперимент в рамках проекта " + Convert.ToString(projectKey);
            }

            launchButton.Enabled = (energyLabel.Text != "");

            setError("");
        }

        public override void update()
        {
            base.update();
            if (updateCounter % 5 != 0)
            {
                updateCounter++;
                return;
            }

            DataTable table = new DataTable();
            db.fillWithProperEnergyRequests(table, projectKey, 1);
            currentEnergyRequestId = 0;

            if (table.Rows.Count > 0)
            {
                UInt64 energy = Convert.ToUInt64(table.Rows[0]["AMOUNT"]);
                DateTime from = Convert.ToDateTime(table.Rows[0]["TIME_FROM"]);
                DateTime to = Convert.ToDateTime(table.Rows[0]["TIME_TO"]);
                currentEnergyRequestId = id0(table.Rows[0]["ID"]);
                energyLabel.Text = "Доступно энергии: " + Convert.ToString(energy) + " с " + Convert.ToString(from) + " до " + Convert.ToString(to);
            }
            else
            {
                db.fillWithProperEnergyRequests(table, projectKey, 2); // будущие запросы
                if (table.Rows.Count > 0)
                {
                    UInt64 energy = Convert.ToUInt64(table.Rows[0]["AMOUNT"]);
                    DateTime from = Convert.ToDateTime(table.Rows[0]["TIME_FROM"]);
                    DateTime to = Convert.ToDateTime(table.Rows[0]["TIME_TO"]);

                    energyLabel.Text = "Будет доступно энергии: " + Convert.ToString(energy) + " с " + Convert.ToString(from) + " до " + Convert.ToString(to);
                }
                else
                {
                    energyLabel.Text = "";
                }
            }

            launchButton.Enabled = (currentEnergyRequestId != 0);
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

        private void launchButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Запустить машину времени?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
            UInt64 id = id0(TimeMachineContext.getData("experiment_id"));

            UInt64 launchId = db.createLaunch(id, currentEnergyRequestId, massValue);
            TimeMachineContext.setData("launch_id", launchId);
            (ParentForm as TimeMachineForm).setPage("PROGRESS_EXPERIMENT");
        }
    }
}

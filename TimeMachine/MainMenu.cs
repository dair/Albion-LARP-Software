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
    public partial class MainMenu : TimeMachineControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        public override void onAppear()
        {
            base.onAppear();
            DataTable table = new DataTable();

            db.fillWithExperiments(table);
            foreach (DataRow row in table.Rows)
            {
                string name = "";
                switch (Convert.ToString(row["STATUS"]))
                {
                    case "A":
                        name = "Подготовка";
                        break;
                    case "R":
                        name = "Запуск";
                        break;
                    case "H":
                        name = "Завершено";
                        break;
                }
                row["STATUS"] = name;

                row["UPDATED_AT"] = TimeMachineContext.realToGame(Convert.ToDateTime(row["UPDATED_AT"]));
            }

            experimentsView.DataSource = table;

            experimentsView.Columns["ID"].HeaderText = "ID";
            experimentsView.Columns["PROJECT_KEY"].HeaderText = "Проект";
            experimentsView.Columns["NAME"].HeaderText = "Название";
            experimentsView.Columns["PARAM_SPACE_1"].Visible = false;
            experimentsView.Columns["PARAM_SPACE_2"].Visible = false;
            experimentsView.Columns["PARAM_TIME"].Visible = false;
            experimentsView.Columns["PARAM_MASS"].Visible = false;
            experimentsView.Columns["MASTER_PARAM_A"].Visible = false;
            experimentsView.Columns["MASTER_PARAM_B"].Visible = false;
            experimentsView.Columns["CREATED_AT"].Visible = false;
            experimentsView.Columns["STATUS"].HeaderText = "Состояние";
            experimentsView.Columns["UPDATED_AT"].HeaderText = "Обновлено";

            table = new DataTable();
            db.fillWithProperEnergyRequests(table, 0, 0);

            requestsView.DataSource = table;
            requestsView.Columns["ID"].Visible = false;
            requestsView.Columns["PROJECT_KEY"].HeaderText = "Проект";
            requestsView.Columns["AMOUNT"].HeaderText = "Количество";
            requestsView.Columns["PRICE"].Visible = false;
            requestsView.Columns["TIME_FROM"].HeaderText = "С";
            requestsView.Columns["TIME_TO"].HeaderText = "По";

            foreach (DataRow row in table.Rows)
            {
                row["TIME_FROM"] = TimeMachineContext.realToGame(Convert.ToDateTime(row["TIME_FROM"]));
                row["TIME_TO"] = TimeMachineContext.realToGame(Convert.ToDateTime(row["TIME_TO"]));
            }

            experimentsView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            requestsView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
        /*
        private void experimentsView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                int rowCount = 0;
                foreach (DataGridViewRow row in experimentsView.Rows)
                {
                    if (row.Selected)
                    {
                        break;
                    }
                    rowCount++;
                }

                UInt64 id = id0(experimentsView.Rows[rowCount].Cells["ID"].Value);
                UInt64 key = id0(experimentsView.Rows[rowCount].Cells["PROJECT_KEY"].Value);
                openExperiment(id, key);
            }

        }
         * */

        private void newExperimentButton_Click(object sender, EventArgs e)
        {
            TimeMachineContext.setData("METHOD", "EXPERIMENT");
            TimeMachineContext.setData("experiment_id", 0);
            (this.FindForm() as TimeMachineForm).setPage("SELECT_PROJECT");
        }

        private void experimentsView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                UInt64 id = id0(experimentsView.Rows[e.RowIndex].Cells["ID"].Value);
                UInt64 key = id0(experimentsView.Rows[e.RowIndex].Cells["PROJECT_KEY"].Value);
//                MessageBox.Show("experimentsView_CellDoubleClick \"" + Convert.ToString(id) + "\"");
                openExperiment(id, key);
            }
        }

        void openExperiment(UInt64 id, UInt64 key)
        {
            TimeMachineContext.setData("experiment_id", id);
            TimeMachineContext.setData("project_key", key);

            UInt64 launchId = db.getLaunchForExperiment(id);
            if (launchId > 0)
            {
                TimeMachineContext.setData("launch_id", launchId);
                (ParentForm as TimeMachineForm).setPage("FINISH_EXPERIMENT");
            }
            else
            {
                (ParentForm as TimeMachineForm).setPage("EDIT_EXPERIMENT");
            }
        }

        private void newRequestButton_Click(object sender, EventArgs e)
        {
            TimeMachineContext.setData("METHOD", "ENERGY");
            (this.FindForm() as TimeMachineForm).setPage("SELECT_PROJECT");
        }
    }
}

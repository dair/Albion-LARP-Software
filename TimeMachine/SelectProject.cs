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
    public partial class SelectProject : TimeMachineControl
    {
        public SelectProject()
        {
            InitializeComponent();
        }

        public override void onAppear()
        {
            base.onAppear();

            projectKey.Text = "";
            leaderId.Text = "";
            password.Text = "";

            setError("");

            projectKey.Focus();
        }

        private void SelectProject_Resize(object sender, EventArgs e)
        {
            Point p = new Point();
            p.X = (this.Width - panel.Width) / 2;
            p.Y = (this.Height - panel.Height) / 2;
            panel.Location = p;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            (ParentForm as TimeMachineForm).setPage("MAIN_MENU");
        }

        private void setError(String err)
        {
            error.Text = err;
            if (err.Length == 0)
            {
                error.Hide();
            }
            else
            {
                error.Show();
            }
        }

        private void commit_Click(object sender, EventArgs e)
        {
            // 1. check credentials
            UInt64 id = id0(leaderId.Text);
            
            if (id == 0)
            {
                setError("Ошибочный ID лидера проекта");
                return;
            }

            UInt64 key = id0(projectKey.Text);

            if (key == 0)
            {
                setError("Ошибочный код проекта");
                return;
            }

            Dictionary<string, string> projectInfo = db.getProjectInfo(key);
            if (projectInfo == null)
            {
                setError("Ошибочный код проекта");
                return;
            }

            Database.ATMLoginInfo info = db.ATMLoginInfo(id);
            if (info.pinCode != password.Text)
            {
                setError("Такой комбинации ID и пароля не существует");
                return;
            }

            if (info.failures > 2)
            {
                setError("ID лидера проекта заблокирован");
                return;
            }

            UInt64 dbLeaderId = id0(projectInfo["leader"]);

            if (dbLeaderId != id)
            {
                setError("Недостаточно прав для доступа к проекту");
                return;
            }

            TimeMachineContext.data["project_key"] = key;

            string method = Convert.ToString(TimeMachineContext.getData("METHOD"));

            if (method == "EXPERIMENT")
            {
                (ParentForm as TimeMachineForm).setPage("EDIT_EXPERIMENT");
            }
            else if (method == "ENERGY")
            {
                (ParentForm as TimeMachineForm).setPage("ENERGY_REQUEST");
            }

        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VKTest
{
    public partial class VKQuestion : VKObject
    {
        Int16 value = 0;
        UInt64 sessionId = 0;
        UInt64 questionId = 0;
        UInt64 questionNum = 0;

        BindingSource bindingSource = new BindingSource();

        public VKQuestion()
        {
            InitializeComponent();
        }

        public VKQuestion(Database.Connection db)
            : base(db)
        {
            InitializeComponent();

            dataGridView.DataSource = bindingSource;
            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);

            sessionId = Convert.ToUInt64(args.data["SESSION_ID"]);
            value = Convert.ToInt16(args.data["VALUE"]);

            nameLabel.Text = info.name;

            DataTable table = new DataTable();
            getDatabase().fillWithQuestionsForGender(table, info.gender, sessionId);
            questionNum = getDatabase().questionNumberForSession(sessionId);
            questionNumLabel.Text = "Вопрос №" + Convert.ToString(questionNum + 1);
            bindingSource.DataSource = table;
            dataGridView.Columns[0].Visible = false;

            instantValueBar.realValue = 50;
            instantValueBar.Shaking = true;
            instantValueBar.Show();
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.None && e.KeyCode == Keys.Enter)
            {
                questionId = Convert.ToUInt64(dataGridView.SelectedRows[0].Cells["ID"].Value);
                myTimer.Interval = 300;
                myTimer.Tick += new EventHandler(myTimer_Tick);
                myTimer.Start();
            }
        }

        void myTimer_Tick(object sender, EventArgs e)
        {
            myTimer.Tick -= myTimer_Tick;

            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "WAITING";
            args.data["PERSON_INFO"] = info;
            args.data["SESSION_ID"] = sessionId;
            args.data["VALUE"] = value;
            args.data["QUESTION_ID"] = questionId;
            args.data["QUESTION_NUM"] = questionNum + 1;
            args.data["QUESTION_TEXT"] = Convert.ToString(dataGridView.SelectedRows[0].Cells["TEXT"].Value);

            RaiseNextObjectEvent(args);
        }
    }
}

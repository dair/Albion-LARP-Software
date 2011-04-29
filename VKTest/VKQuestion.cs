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
        double middle = 0;

        BindingSource bindingSource = new BindingSource();

        public VKQuestion()
        {
            InitializeComponent();
        }

        public VKQuestion(Database.Connection db, VerticalProgressBar vBar)
            : base(db, vBar)
        {
            InitializeComponent();

            dataGridView.DataSource = bindingSource;
            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);

            bar.Visible = true;

            sessionId = Convert.ToUInt64(args.data["SESSION_ID"]);
            value = Convert.ToInt16(args.data["VALUE"]);
            middle = Convert.ToDouble(args.data["MIDDLE"]);

            //bar.RealValue = Math.Abs(value);
            bar.Shaking = true;

            nameLabel.Text = info.name;

            DataTable table = new DataTable();
            getDatabase().fillWithQuestionsForGender(table, info.gender, sessionId);
            questionNum = getDatabase().questionNumberForSession(sessionId);
            questionNumLabel.Text = "Вопрос №" + Convert.ToString(questionNum + 1);
            bindingSource.DataSource = table;
            dataGridView.Columns[0].Visible = false;

        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.None)
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        questionId = Convert.ToUInt64(dataGridView.SelectedRows[0].Cells["ID"].Value);
                        myTimer.Interval = 300;
                        myTimer.Tick += new EventHandler(myTimer_TickToAnswer);
                        myTimer.Start();
                        e.Handled = true;
                        return;
                }
            }

            OnKeyDown(sender, e);
        }

        public override void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
//                    MessageBox.Show("VKQuestion::OnKeyDown");
                    myTimer.Interval = 1;
                    myTimer.Tick += new EventHandler(myTimer_TickEscape);
                    myTimer.Start();
                    e.Handled = true;
                    return;
            }

            base.OnKeyDown(sender, e);
        }

        void myTimer_TickEscape(object sender, EventArgs e)
        {
            myTimer.Tick -= myTimer_TickEscape;
            myTimer.Stop();
            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "EXITING";
            args.data["PERSON_INFO"] = info;
            args.data["SESSION_ID"] = sessionId;
            args.data["VALUE"] = value;
            args.data["MIDDLE"] = middle;
            args.data["WHERE"] = "QUESTION";

            RaiseNextObjectEvent(args);

        }

        void myTimer_TickToAnswer(object sender, EventArgs e)
        {
            myTimer.Tick -= myTimer_TickToAnswer;
            myTimer.Stop();

            getDatabase().addQuestionForSession(sessionId, questionId);

            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "WAITING";
            args.data["PERSON_INFO"] = info;
            args.data["SESSION_ID"] = sessionId;
            args.data["VALUE"] = value;
            args.data["MIDDLE"] = middle;
            args.data["QUESTION_ID"] = questionId;
            args.data["QUESTION_NUM"] = questionNum + 1;
            args.data["QUESTION_TEXT"] = Convert.ToString(dataGridView.SelectedRows[0].Cells["TEXT"].Value);

            RaiseNextObjectEvent(args);
        }
    }
}

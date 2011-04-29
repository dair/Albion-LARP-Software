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
    public partial class VKWaiting : VKObject
    {
        UInt64 sessionId = 0;
        UInt64 questionId = 0;
        Int16 value = 0;
        UInt64 questionNum = 0;
        double middle = 0;
        String qText = null;
        Database.VKAnswerInfo answerInfo = null;

        Timer waitingTimer = new Timer();

        public VKWaiting()
        {
            InitializeComponent();
        }

        public VKWaiting(Database.Connection db, VerticalProgressBar vBar)
            : base(db, vBar)
        {
            InitializeComponent();

            waitingTimer.Tick += new EventHandler(waitingTimer_Waiting);
            waitingTimer.Interval = 1000;
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);

            bar.Visible = true;

            sessionId = Convert.ToUInt64(args.data["SESSION_ID"]);
            questionId = Convert.ToUInt64(args.data["QUESTION_ID"]);
            questionNum = Convert.ToUInt64(args.data["QUESTION_NUM"]);
            value = Convert.ToInt16(args.data["VALUE"]);
            middle = Convert.ToDouble(args.data["MIDDLE"]);
            qText = Convert.ToString(args.data["QUESTION_TEXT"]);


            nameLabel.Text = info.name;
            questionNumLabel.Text = "Вопрос №" + Convert.ToString(questionNum);
            questionText.Text = "Текст вопроса: " + qText;

            waitingTimer.Start();

            statusLabel.Text = "Ждём ответа";
            answerText.Text = "";
            answerInfo = null;
        }

        void waitingTimer_Waiting(object sender, EventArgs e)
        {
            waitingTimer.Stop();

            answerInfo = getDatabase().getAnswerForSessionQuestion(sessionId, questionId);
            if (answerInfo != null)
            {
                answerText.Text = "Ответ: " + answerInfo.text;
                myTimer.Tick -= waitingTimer_Waiting;
                myTimer.Interval = 2000;
                myTimer.Tick += new EventHandler(myTimer_Success);
                myTimer.Start();
            }
            else
            {
                waitingTimer.Enabled = true;
            }
        }

        void myTimer_Success(object sender, EventArgs e)
        {
            myTimer.Stop();
            myTimer.Tick -= myTimer_Success;

            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "QUESTION";

            short localValue = 0;
            switch (info.genome)
            {
                case Database.FullPersonInfo.Genome.Human:
                    localValue = answerInfo.humanValue;
                    break;
                case Database.FullPersonInfo.Genome.Android:
                    localValue = answerInfo.androidValue;
                    break;
            }

            value += localValue;
            middle = (double)value / (double)questionNum;

            bar.RealValue = Math.Abs(Convert.ToInt32(middle * 20));

            args.data["PERSON_INFO"] = info;
            args.data["SESSION_ID"] = sessionId;
            args.data["VALUE"] = value;
            args.data["MIDDLE"] = middle;

            RaiseNextObjectEvent(args);
        }

        public override void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.None && e.KeyCode == Keys.Escape)
            {
                waitingTimer.Stop();
                myTimer.Tick += new EventHandler(myTimer_Escape);
                myTimer.Interval = 300;
                myTimer.Start();

                e.Handled = true;
                return;
            }
            base.OnKeyDown(sender, e);
        }

        void myTimer_Escape(object sender, EventArgs e)
        {
            myTimer.Stop();

            myTimer.Tick -= myTimer_Escape;

            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "EXITING";
            args.data["PERSON_INFO"] = info;
            args.data["SESSION_ID"] = sessionId;
            args.data["QUESTION_ID"] = questionId;
            args.data["QUESTION_NUM"] = questionNum;
            args.data["VALUE"] = value;
            args.data["MIDDLE"] = middle;
            args.data["QUESTION_TEXT"] = qText;
            args.data["WHERE"] = "WAITING";
            RaiseNextObjectEvent(args);
        }
    }
}

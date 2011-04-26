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
        Database.VKAnswerInfo answerInfo = null;


        public VKWaiting()
        {
            InitializeComponent();
        }

        public VKWaiting(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);

            sessionId = Convert.ToUInt64(args.data["SESSION_ID"]);
            questionId = Convert.ToUInt64(args.data["QUESTION_ID"]);
            questionNum = Convert.ToUInt64(args.data["QUESTION_NUM"]);
            value = Convert.ToInt16(args.data["VALUE"]);

            nameLabel.Text = info.name;
            questionNumLabel.Text = "Вопрос №" + Convert.ToString(questionNum);
            questionText.Text = "Текст вопроса: " + Convert.ToString(args.data["QUESTION_TEXT"]);
            getDatabase().addQuestionForSession(sessionId, questionId);

            myTimer.Interval = 1000;
            myTimer.Tick += new EventHandler(myTimer_Waiting);
            myTimer.Start();

            statusLabel.Text = "Ждём ответа";
            answerText.Text = "";
            answerInfo = null;
        }

        void myTimer_Waiting(object sender, EventArgs e)
        {
            myTimer.Stop();

            answerInfo = getDatabase().getAnswerForSessionQuestion(sessionId, questionId);
            if (answerInfo != null)
            {
                answerText.Text = "Ответ: " + answerInfo.text;
                myTimer.Tick -= myTimer_Waiting;
                myTimer.Interval = 1000;
                myTimer.Tick += new EventHandler(myTimer_Success);
                myTimer.Start();
            }
            else
            {
                myTimer.Enabled = true;
            }
        }

        void myTimer_Success(object sender, EventArgs e)
        {
            myTimer.Stop();
            myTimer.Tick -= myTimer_Success;

            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "QUESTION";

            switch (info.genome)
            {
                case Database.FullPersonInfo.Genome.Human:
                    value += answerInfo.humanValue;
                    break;
                case Database.FullPersonInfo.Genome.Android:
                    value += answerInfo.androidValue;
                    break;
            }

            args.data["PERSON_INFO"] = info;
            args.data["SESSION_ID"] = sessionId;
            args.data["VALUE"] = value;

            RaiseNextObjectEvent(args);
        }
    }
}

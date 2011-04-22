using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ClientUI;

namespace ATM
{
    public partial class ATMTransfer : ATMObject
    {
        private string welcomeString;
        private Database.ATMLoginInfo recvInfo = null;
        bool working = false;

        public ATMTransfer()
        {
            InitializeComponent();
            welcomeString = nameLabel.Text;
        }

        public ATMTransfer(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
            welcomeString = nameLabel.Text;
        }

        public override void Init(UserObjectEventArgs args)
        {
            base.Init(args);

            Init2();
        }

        void Init2()
        {
            codeBox.ReadOnly = false;
            codeBox.Text = "";
            codeBox.ForeColor = Color.White;
            recvInfo = null;
            nameLabel.Text = welcomeString.Replace("NAMEHERE", info.name);
            codeBox.KeyDown += codeBox_KeyDown;
            codeBox.Focus();

            working = false;
        }

        public override void Deinit()
        {
            
        }

        private void NextTimerProcessor(object sender, EventArgs e)
        {
            myTimer.Stop();
            myTimer.Tick -= NextTimerProcessor;

            UserObjectEventArgs args = new UserObjectEventArgs();
            args.NextObject = "AMOUNT";
            args.data["PERSON_INFO"] = info;
            args.data["RECV_INFO"] = recvInfo;
            RaiseNextObjectEvent(args);
        }

        private void ErrorTimerProcessor(object sender, EventArgs e)
        {
            myTimer.Stop();
            myTimer.Tick -= ErrorTimerProcessor;

            Init2();
        }

        private void InputEventProcessor(object sender, EventArgs e)
        {
            myTimer.Stop();
            myTimer.Tick -= InputEventProcessor;

            UInt64 recvID;

            try
            {
                recvID = Convert.ToUInt64(codeBox.Text);
            }
            catch (FormatException)
            {
                codeBox.Text = "Ошибочный код!";
                codeBox.ForeColor = Color.Red;
                myTimer.Tick += ErrorTimerProcessor;
                myTimer.Interval = 2000;
                myTimer.Start();
                return;
            }

            if (recvID == info.id)
            {
                codeBox.Text = "Перевод невозможен!";
                codeBox.ForeColor = Color.Red;
                myTimer.Tick += ErrorTimerProcessor;
                myTimer.Interval = 2000;
                myTimer.Start();
                return;
            }

            recvInfo = getDatabase().ATMLoginInfo(recvID);
            if (recvInfo.name == null)
            {
                codeBox.Text = "Неизвестный код!";
                codeBox.ForeColor = Color.Red;
                myTimer.Tick += ErrorTimerProcessor;
                myTimer.Interval = 2000;
                myTimer.Start();
                return;
            }

            codeBox.Text = recvInfo.name;
            codeBox.ForeColor = Color.Green;
            myTimer.Tick += NextTimerProcessor;
            myTimer.Interval = 1000;
            myTimer.Start();
        }

        public override void BarCodeScanned(ulong code)
        {
            myTimer.Tick += InputEventProcessor;
            myTimer.Interval = 1;
            myTimer.Start();
            codeBox.Text = Convert.ToString(code);
            codeBox.ReadOnly = true;
            codeBox.KeyDown -= codeBox_KeyDown;
        }

        private void codeBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!working)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    working = true;

                    (ParentForm as ClientForm).RecordActivity();
                    codeBox.KeyDown -= codeBox_KeyDown;
                    codeBox.ReadOnly = true;
                    //MessageBox.Show("ENTER!!!");
                    e.Handled = true;
                    myTimer.Tick += InputEventProcessor;
                    myTimer.Interval = 1;
                    myTimer.Start();
                    //InputEventProcessor();
                }
                else if (e.KeyCode == Keys.D0 ||
                    e.KeyCode == Keys.D1 ||
                    e.KeyCode == Keys.D2 ||
                    e.KeyCode == Keys.D3 ||
                    e.KeyCode == Keys.D4 ||
                    e.KeyCode == Keys.D5 ||
                    e.KeyCode == Keys.D6 ||
                    e.KeyCode == Keys.D7 ||
                    e.KeyCode == Keys.D8 ||
                    e.KeyCode == Keys.D9)
                {
                    (ParentForm as ClientForm).RecordActivity();
                    e.Handled = true;
                }
            }

            if (!e.Handled)
            {
                OnKeyDown(sender, e);
            }
        }

    }
}

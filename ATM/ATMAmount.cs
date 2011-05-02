using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClientUI;

namespace ATM
{
    public partial class ATMAmount : ATMObject
    {
        Database.ATMLoginInfo recvInfo = null;
        Int64 amount = -1;

        public ATMAmount()
        {
            InitializeComponent();
        }

        public ATMAmount(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);

            if (!args.data.ContainsKey("RECV_INFO") || !(args.data["RECV_INFO"] is Database.ATMLoginInfo))
            {
                MessageBox.Show("Args doesn't contain RECV_INFO", "ATMAmount::Init ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            recvInfo = (Database.ATMLoginInfo)(args.data["RECV_INFO"]);

            amountBox.KeyDown += new KeyEventHandler(amountBox_KeyDown);
            Init2();
        }

        void Init2()
        {
            amountBox.Focus();
            amountBox.ReadOnly = false;
            amountBox.Text = "";
            amount = -1;
        }

        public override void Deinit()
        {
            amountBox.KeyDown -= amountBox_KeyDown;
            base.Deinit();
        }

        void amountBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.None)
            {
                (ParentForm as ClientForm).RecordActivity();
                //MessageBox.Show("ENTER!!!");
                e.Handled = true;
                amountBox.ReadOnly = true;
                myTimer.Tick += InputEventProcessor;
                myTimer.Interval = 1;
                myTimer.Start();
                //InputEventProcessor();
            }
            else if ((e.KeyCode == Keys.D0 ||
                e.KeyCode == Keys.D1 ||
                e.KeyCode == Keys.D2 ||
                e.KeyCode == Keys.D3 ||
                e.KeyCode == Keys.D4 ||
                e.KeyCode == Keys.D5 ||
                e.KeyCode == Keys.D6 ||
                e.KeyCode == Keys.D7 ||
                e.KeyCode == Keys.D8 ||
                e.KeyCode == Keys.D9) && e.Modifiers == Keys.None)
            {
                (ParentForm as ClientForm).RecordActivity();
                e.Handled = true;
            }
            else
            {
                OnKeyDown(sender, e);
            }
        }

        private void InputEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            myTimer.Stop();
            myTimer.Tick -= InputEventProcessor;

            bool error = false;
            try
            {
                Decimal dollarAmount = Convert.ToDecimal(amountBox.Text);

                amount = (Int64)(dollarAmount * 100);
                if (amount < 0 || (ulong)amount > info.balance)
                    error = true;
            }
            catch (Exception)
            {
                error = true;
            }

            if (error)
            {
                amountBox.Text = "Ошибочная сумма";
                amountBox.ForeColor = Color.Red;
                myTimer.Tick += TimerEventReject;
                myTimer.Interval = 2000;
            }
            else
            {
                myTimer.Tick += TimerEventAccept;
                myTimer.Interval = 1;
            }

            myTimer.Start();
        }

        private void TimerEventAccept(Object myObject, EventArgs myEventArgs)
        {
            myTimer.Stop();
            myTimer.Tick -= TimerEventAccept;
            UserObjectEventArgs args = new UserObjectEventArgs();
            args.data["PERSON_INFO"] = info;
            args.data["RECV_INFO"] = recvInfo;
            args.data["AMOUNT"] = amount;
            args.NextObject = "VERIFY";
            RaiseNextObjectEvent(args);
        }

        private void TimerEventReject(Object myObject, EventArgs myEventArgs)
        {
            myTimer.Stop();
            myTimer.Tick -= TimerEventReject;
            Init2();
        }
    }


}

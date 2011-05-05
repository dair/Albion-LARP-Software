using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ClientUI;

namespace CashDesk
{
    public partial class CashDeskVerify : CashDeskObject
    {
        private bool waiting;
        private string welcome1, amountString, commitString;
        private UInt64 amount;

        public CashDeskVerify()
        {
            InitializeComponent();
        }

        public CashDeskVerify(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
            welcome1 = senderLabel.Text;
            amountString = amountLabel.Text;
            commitString = commitLabel.Text;
        }

        public override void Init(UserObjectEventArgs args)
        {
            base.Init(args);
            waiting = true;
            commitLabel.Text = commitString;
            commitLabel.ForeColor = Color.White;

            amount = Convert.ToUInt64(args.data["AMOUNT"]);

            senderLabel.Text = welcome1.Replace("NAMEHERE", info.name);
            amountLabel.Text = amountString.Replace("AMOUNTHERE", moneyToString(amount));
        }

        public override void Deinit()
        {
            
        }

        public override void BarCodeScanned(ulong code)
        {
            
        }

        private void NextTimerProcessor(object sender, EventArgs e)
        {
            myTimer.Stop();
            myTimer.Tick -= NextTimerProcessor;

            UserObjectEventArgs args = new UserObjectEventArgs();
            args.NextObject = "START";
            RaiseNextObjectEvent(args);
        }

        public override void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.None)
            {
                if (!waiting) return;
                waiting = false;

                UInt64 cashDeskId = Settings.CashDesk.GetPersonId();

                bool res = getDatabase().moneyTransfer(info.id, cashDeskId, amount, amount / 5);

                if (res)
                {
                    commitLabel.Text = "Платёж осуществлён";
                    commitLabel.ForeColor = Color.Green;
                }
                else
                {
                    commitLabel.Text = "Ошибка платежа";
                    commitLabel.ForeColor = Color.Red;
                }
                myTimer.Tick += NextTimerProcessor;
                myTimer.Interval = 2000;
                myTimer.Start();
            }

            base.OnKeyDown(sender, e);
        }
    }
}

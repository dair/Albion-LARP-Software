/* ***********************************************************************
 * (C) 2008-2012 Vladimir Lebedev-Schmidthof <vladimir@schmidthof.com>
 * Made for Albion Games (http://albiongames.org)
 * 
 * 
 *            DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
 *                    Version 2, December 2004

 * Copyright (C) 2004 Sam Hocevar <sam@hocevar.net>
 * 
 * Everyone is permitted to copy and distribute verbatim or modified
 * copies of this license document, and changing it is allowed as long
 * as the name is changed.

 *           DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
 *   TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION

 *  0. You just DO WHAT THE FUCK YOU WANT TO.
 * *********************************************************************** */

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
        private string welcome1, amountString, commitString;
        private UInt64 amount;

        public CashDeskVerify()
            : base()
        {
            InitializeComponent();
            welcome1 = senderLabel.Text;
            amountString = amountLabel.Text;
            commitString = commitLabel.Text;
        }

        public override void Init(UserObjectEventArgs args)
        {
            base.Init(args);
            commitLabel.Text = commitString;
            commitLabel.ForeColor = Color.White;

            amount = Convert.ToUInt64(args.data["AMOUNT"]);

            senderLabel.Text = welcome1.Replace("NAMEHERE", info.name);
            amountLabel.Text = amountString.Replace("AMOUNTHERE", moneyToString(amount));

            label1.Show();
            amountLabel.Show();
            pinBox.Show();
            pinBox.Text = "";
            commitLabel.Show();
            wrongLabel.Hide();
            remainLabel.Hide();

            pinBox.Focus();
        }

        public override void Deinit()
        {
            
        }

        public override void BarCodeScanned(ulong code)
        {
            
        }

        private String remainText(int count)
        {
            String ret = "Осталось ";
            switch (count)
            {
                case 0:
                    ret += "0 попыток";
                    break;
                case 1:
                    ret += "1 попытка";
                    break;
                default:
                    ret += Convert.ToString(count) + " попытки";
                    break;
            }
            return ret;
        }

        private void TimerEventAccept(Object myObject, EventArgs myEventArgs)
        {
            myTimer.Stop();
            myTimer.Tick -= TimerEventAccept;

            UInt64 cashDeskId = Settings.CashDesk.GetPersonId();

            bool res = getDatabase().moneyTransfer(info.id, cashDeskId, amount, amount / 2);

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

        private void TimerEventReject(Object myObject, EventArgs myEventArgs)
        {
            myTimer.Stop();
            myTimer.Tick -= TimerEventReject;
            pinBox.Text = "";
            UserObjectEventArgs args = new UserObjectEventArgs();
            if (info.failures == 2)
            {
                args.NextObject = "START";
            }
            else
            {
                args.data["AMOUNT"] = amount;
                args.NextObject = "VERIFY";
            }

            RaiseNextObjectEvent(args);
        }

        private void InputEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            myTimer.Stop();
            myTimer.Tick -= InputEventProcessor;

            string localPin = pinBox.Text.ToUpper();
            string globalPin = info.pinCode.ToUpper();

            if (localPin == globalPin)
            {
                getDatabase().CorrectPinEntered(info.id);
                myTimer.Tick += TimerEventAccept;
                myTimer.Interval = 1;
            }
            else
            {
                getDatabase().WrongPinEntered(info.id);
                myTimer.Tick += TimerEventReject;
                myTimer.Interval = 2000;
                remainLabel.Text = remainText(2 - info.failures);

                label1.Hide();
                amountLabel.Hide();
                pinBox.Hide();
                commitLabel.Hide();
                wrongLabel.Show();
                remainLabel.Show();
            }

            myTimer.Start();
        }

        public void pinBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.None)
            {
                (ParentForm as ClientForm).RecordActivity();
                //MessageBox.Show("ENTER!!!");
                e.Handled = true;
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


        private void NextTimerProcessor(object sender, EventArgs e)
        {
            myTimer.Stop();
            myTimer.Tick -= NextTimerProcessor;

            UserObjectEventArgs args = new UserObjectEventArgs();
            args.NextObject = "START";
            RaiseNextObjectEvent(args);
        }
    }
}

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

namespace ATM
{
    public partial class StockDirectPinCode : ATMObject
    {
        private string welcomeString;
        Database.StockCompanyInfo companyInfo = null;
        UInt64 qty = 0;
        UInt64 price = 0;
        Database.ATMLoginInfo receiverInfo = null;


        private void TimerEventAccept(Object myObject, EventArgs myEventArgs)
        {
            myTimer.Stop();
            myTimer.Tick -= TimerEventAccept;
            UserObjectEventArgs args = new UserObjectEventArgs();
            args.NextObject = "STOCK_DIRECT_CONFIRM";
            args.data["PERSON_INFO"] = info;
            args.data["COMPANY_INFO"] = companyInfo;
            args.data["QTY"] = qty;
            args.data["PRICE"] = price;
            args.data["RECEIVER_INFO"] = receiverInfo;

            RaiseNextObjectEvent(args);
        }
        
        private void TimerEventReject(Object myObject, EventArgs myEventArgs)
        {
            myTimer.Stop();
            myTimer.Tick -= TimerEventReject;
            pinBox.Text = "";
            UserObjectEventArgs args = new UserObjectEventArgs();
            args.NextObject = "START";
            RaiseNextObjectEvent(args);
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

        private void InputEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            myTimer.Stop();
            myTimer.Tick -= InputEventProcessor;

            string localPin = pinBox.Text.ToUpper();
            string globalPin = receiverInfo.pinCode.ToUpper();

            if (localPin == globalPin)
            {
                getDatabase().CorrectPinEntered(receiverInfo.id);
                myTimer.Tick += TimerEventAccept;
                myTimer.Interval = 1;
            }
            else
            {
                getDatabase().WrongPinEntered(receiverInfo.id);
                myTimer.Tick += TimerEventReject;
                myTimer.Interval = 2000;
                pinBox.Hide();
                wrongLabel.Show();
                remainLabel.Text = remainText(2 - receiverInfo.failures);
                remainLabel.Show();
            }

            myTimer.Start();
        }

        public StockDirectPinCode(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
            welcomeString = greetLabel.Text;
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);
            if (!args.data.ContainsKey("COMPANY_INFO") || !(args.data["COMPANY_INFO"] is Database.StockCompanyInfo))
            {
                MessageBox.Show("Args doesn't contain COMPANY_INFO", "StockDirectQty::Init ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            companyInfo = (Database.StockCompanyInfo)(args.data["COMPANY_INFO"]);

            qty = Convert.ToUInt64(args.data["QTY"]);
            price = Convert.ToUInt64(args.data["PRICE"]);
            receiverInfo = (Database.ATMLoginInfo)(args.data["RECEIVER_INFO"]);


            wrongLabel.Hide();
            remainLabel.Hide();
            pinBox.Show();
            
            greetLabel.Text = welcomeString.Replace("NAMEHERE", receiverInfo.name);

            pinBox.Text = "";

            pinBox.Focus();
            pinBox.KeyDown += pinBox_KeyDown;
        }

        public override void Deinit()
        {
            pinBox.KeyDown -= pinBox_KeyDown;
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
    }
}

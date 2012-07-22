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
    public partial class ATMVerify : ATMObject
    {
        private bool waiting;
        private string welcome1, welcome2, amountString, commitString;
        private UInt64 amount;
        Database.ATMLoginInfo recvInfo;

        public ATMVerify()
        {
            InitializeComponent();
        }

        public ATMVerify(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
            welcome1 = senderLabel.Text;
            welcome2 = receiverLabel.Text;
            amountString = amountLabel.Text;
            commitString = commitLabel.Text;
        }

        public override void Init(UserObjectEventArgs args)
        {
            base.Init(args);
            waiting = true;
            commitLabel.Text = commitString;
            commitLabel.ForeColor = Color.White;

            if (!args.data.ContainsKey("RECV_INFO") || !(args.data["RECV_INFO"] is Database.ATMLoginInfo))
            {
                MessageBox.Show("Args doesn't contain RECV_INFO", "ATMAmount::Init ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            recvInfo = (Database.ATMLoginInfo)(args.data["RECV_INFO"]);
            amount = Convert.ToUInt64(args.data["AMOUNT"]);

            senderLabel.Text = welcome1.Replace("NAMEHERE", info.name);
            receiverLabel.Text = welcome2.Replace("NAMEHERE", recvInfo.name);
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

                bool res = false;
                if (recvInfo.status == "A")
                {
                    res = getDatabase().moneyTransfer(info.id, recvInfo.id, amount);
                }
                else if (recvInfo.status == "P")
                {
                    res = getDatabase().moneyTransferToProject(info.id, recvInfo.id, amount);
                }

                if (res)
                {
                    commitLabel.Text = "Перевод средств осуществлён";
                    commitLabel.ForeColor = Color.Green;
                }
                else
                {
                    commitLabel.Text = "Ошибка перевода средств";
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

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

            base.OnKeyDown(sender, e);
        }
    }
}

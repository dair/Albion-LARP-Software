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
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ATM
{
    public partial class StockAddRequestTicker : ATMObject
    {
        Database.StockCompanyInfo stockInfo = null;
        DataTable lastCycle = null;

        public StockAddRequestTicker()
        {
            InitializeComponent();
        }

        public StockAddRequestTicker(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);
            infoLabel.Text = "";
            tickerBox.Text = "";
            stockInfo = null;
            lastCycle = (DataTable)args.data["CYCLE"];

            tickerBox.Enabled = true;
        }

        void processTicker()
        {
            stockInfo = null;
            if (tickerBox.Text.Trim() != "")
            {
                stockInfo = getDatabase().getCompanyInfo(tickerBox.Text.Trim().ToUpper());
            }

            if (stockInfo == null)
            {
                infoLabel.Text = "Неизвестный тикер";
                infoLabel.ForeColor = Color.Red;
                return;
            }
            else
            {
                infoLabel.Text = stockInfo.name;
                infoLabel.ForeColor = Color.Green;
            }

            myTimer.Tick += new EventHandler(myTimer_TickerEntered);
            myTimer.Interval = 200;
            myTimer.Start();

            tickerBox.Enabled = false;
        }

        private void tickerBox_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(sender, e);

            if (!e.Handled)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    processTicker();
                }
            }
        }

        void myTimer_TickerEntered(object sender, EventArgs e)
        {
            myTimer.Stop();
            myTimer.Tick -= myTimer_TickerEntered;

            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "STOCK_ADD_QTY";
            args.data["PERSON_INFO"] = info;
            args.data["CYCLE"] = lastCycle;
            args.data["COMPANY_INFO"] = stockInfo;

            RaiseNextObjectEvent(args);
        }
    }
}

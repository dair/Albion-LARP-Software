﻿/* ***********************************************************************
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
    public partial class StockAddQty : ATMObject
    {
        Database.StockCompanyInfo stockInfo = null;
        DataTable lastCycle = null;
        UInt64 qty = 0;


        public StockAddQty()
        {
            InitializeComponent();
        }

        public StockAddQty(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);
            infoLabel.Text = "";
            qtyBox.Text = "";
            stockInfo = null;
            lastCycle = (DataTable)args.data["CYCLE"];
            stockInfo = (Database.StockCompanyInfo)args.data["COMPANY_INFO"];

            qtyBox.Enabled = true;
        }

        void processQty()
        {
            qty = 0;
            try
            {
                qty = Convert.ToUInt64(qtyBox.Text.Trim());
            }
            catch (Exception)
            {
            }

            if (qty == 0)
            {
                infoLabel.Text = "Ошибочное значение";
                infoLabel.ForeColor = Color.Red;
                return;
            }
            else
            {
                infoLabel.Text = "";
            }

            myTimer.Tick += new EventHandler(myTimer_QtyEntered);
            myTimer.Interval = 200;
            myTimer.Start();

            qtyBox.Enabled = false;
        }

        private void tickerBox_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(sender, e);

            if (!e.Handled)
            {
                infoLabel.Text = "";
                if (e.KeyCode == Keys.Enter)
                {
                    processQty();
                }
            }
        }

        void myTimer_QtyEntered(object sender, EventArgs e)
        {
            myTimer.Stop();
            myTimer.Tick -= myTimer_QtyEntered;

            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "STOCK_ADD_OPERATION";
            args.data["PERSON_INFO"] = info;
            args.data["CYCLE"] = lastCycle;
            args.data["COMPANY_INFO"] = stockInfo;
            args.data["QTY"] = qty;

            RaiseNextObjectEvent(args);
        }
    }
}

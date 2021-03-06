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
    public partial class StockRequests : ATMObject
    {
        DataTable lastCycle;
        DataTable existingRequests;

        public StockRequests()
        {
            InitializeComponent();
        }

        public StockRequests(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }


        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);
            existingRequests = new DataTable();
            getDatabase().currentCyclePersonRequests(info.id, existingRequests);

            DataTable showTable = new DataTable();
            showTable.Columns.Add("ID", Type.GetType("System.UInt64"));
            showTable.Columns.Add("TICKER");
            showTable.Columns.Add("OPERATION");
            showTable.Columns.Add("QTY", Type.GetType("System.UInt64"));
            showTable.Columns.Add("PRICE");

            foreach (DataRow row in existingRequests.Rows)
            {
                String op;
                if (Convert.ToString(row["OPERATION"]) == "B")
                    op = "ПОКУПКА";
                else
                    op = "ПРОДАЖА";

                UInt64 qty = Convert.ToUInt64(row["QTY"]);
                UInt64 quote = Convert.ToUInt64(row["QUOTE"]);

                String price = moneyToString(qty*quote);

                showTable.Rows.Add(row["ID"], row["TICKER"], op, row["QTY"], price);
            }

            requestsView.DataSource = showTable;
            requestsView.Refresh();
            requestsView.Columns["ID"].Visible = false;


            noRequestsLabel.Visible = (existingRequests.Rows.Count == 0);

            lastCycle = new DataTable();
            getDatabase().fillWithCycleInfoAndQuotes(1, lastCycle);

            DateTime now = getDatabase().now();

            if (lastCycle.Rows.Count <= 0)
            {
                newRequestLabel.Visible = false;
                deleteRequestLabel.Visible = false;
            }
            else
            {
                DateTime start = Convert.ToDateTime(lastCycle.Rows[0]["START_TIME"]);
                DateTime border1 = Convert.ToDateTime(lastCycle.Rows[0]["BORDER1_TIME"]);
                DateTime border2 = Convert.ToDateTime(lastCycle.Rows[0]["BORDER2_TIME"]);
                DateTime finish = Convert.ToDateTime(lastCycle.Rows[0]["FINISH_TIME"]);

                newRequestLabel.Visible = (now >= start) && (now < border2);
                deleteRequestLabel.Visible = (now >= start) && (now < border1) && (showTable.Rows.Count > 0);
            }
        }

        private void requestsView_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(sender, e);

            if (!e.Handled)
            {
                if (e.Modifiers == Keys.None)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.D1:
                            if (newRequestLabel.Visible)
                                TryNewRequest();
                            e.Handled = true;
                            break;
                        case Keys.D2:
                            if (deleteRequestLabel.Visible && (requestsView.Rows.Count > 0) &&
                                requestsView.SelectedRows.Count == 1)
                                TryDeleteRequest();
                            e.Handled = true;
                            break;

                    }
                }
            }
        }

        void TryNewRequest()
        {
            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "STOCK_ADD_REQUEST_TICKER";
            args.data["PERSON_INFO"] = info;
            args.data["CYCLE"] = lastCycle;

            RaiseNextObjectEvent(args);
        }

        void TryDeleteRequest()
        {
            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "STOCK_DELETE_REQUEST_CONFURMATION";
            args.data["PERSON_INFO"] = info;
            args.data["ROW"] = existingRequests.Rows[requestsView.SelectedRows[0].Index];
            RaiseNextObjectEvent(args);
        }
    }
}

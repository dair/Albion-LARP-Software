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
            DataTable table = new DataTable();
            getDatabase().currentCyclePersonRequests(info.id, table);

            DataTable showTable = new DataTable();
            showTable.Columns.Add("ID", Type.GetType("System.UInt64"));
            showTable.Columns.Add("TICKER");
            showTable.Columns.Add("OPERATION");
            showTable.Columns.Add("QTY", Type.GetType("System.UInt64"));
            showTable.Columns.Add("PRICE");

            foreach (DataRow row in table.Rows)
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


            noRequestsLabel.Visible = (table.Rows.Count == 0);

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
            UInt64 reqId = Convert.ToUInt64(requestsView.SelectedRows[0].Cells["ID"].Value);
            args.data["REQUEST_ID"] = reqId;
            RaiseNextObjectEvent(args);
        }
    }
}

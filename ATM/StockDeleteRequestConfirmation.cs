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
    public partial class StockDeleteRequestConfirmation : ATMObject
    {
        UInt64 reqId;
        UInt64 qty;
        UInt64 quote;
        String operation;
        String ticker;

        public StockDeleteRequestConfirmation()
        {
            InitializeComponent();
        }

        public StockDeleteRequestConfirmation(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }


        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);

            DataRow row = (DataRow)args.data["ROW"];
            reqId = Convert.ToUInt64(row["ID"]);
            ticker = Convert.ToString(row["TICKER"]);
            operation = Convert.ToString(row["OPERATION"]);
            qty = Convert.ToUInt64(row["QTY"]);
            quote = Convert.ToUInt64(row["QUOTE"]);

        }

        public override void OnKeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(sender, e);

            if (e.Modifiers == Keys.None)
            {
                switch (e.KeyCode)
                {
                    case Keys.D1:
                        DeleteRequest();
                        break;
                    case Keys.D2:
                        Cancel();
                        break;
                }
            }
        }

        void DeleteRequest()
        {
            if (operation.ToUpper() == "B")
            {
                getDatabase().deleteBuyRequest(info.id, reqId, qty * quote);
            }
            else
            {
                getDatabase().deleteSellRequest(info.id, reqId, ticker, qty);
            }

            Cancel();
        }

        void Cancel()
        {
            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "START";
            RaiseNextObjectEvent(args);
        }
    }
}

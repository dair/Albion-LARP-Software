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

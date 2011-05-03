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
            reqId = Convert.ToUInt64(args.data["REQUEST_ID"]);
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
            getDatabase().deleteRequest(reqId);
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

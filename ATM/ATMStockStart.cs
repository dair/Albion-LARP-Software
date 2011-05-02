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
    public partial class ATMStockStart : ATMObject
    {
        public ATMStockStart()
        {
            InitializeComponent();
        }

        public ATMStockStart(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);
            stockWidget.Retrieve();
        }

        private void stockWidget_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(sender, e);
        }

        public override void OnKeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(sender, e);
            if (e.Modifiers == Keys.None)
            {
                myTimer.Interval = 300;

                switch (e.KeyCode)
                {
                    case Keys.D1:
                        myTimer.Tick += new EventHandler(myTimer_TickDirect);
                        myTimer.Start();
                        break;
                    case Keys.D2:
                        myTimer.Tick += new EventHandler(myTimer_TickNews);
                        myTimer.Start();
                        break;
                    case Keys.D3:
                        myTimer.Tick += new EventHandler(myTimer_TickRequests);
                        myTimer.Start();
                        break;
                }
            }
        }

        void myTimer_TickNews(object sender, EventArgs e)
        {
            myTimer.Stop();
            myTimer.Tick -= myTimer_TickNews;

            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "NEWS";
            args.data["PERSON_INFO"] = info;

            RaiseNextObjectEvent(args);
        }

        void myTimer_TickDirect(object sender, EventArgs e)
        {
            myTimer.Stop();
            myTimer.Tick -= myTimer_TickDirect;

            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "STOCK_DIRECT";
            args.data["PERSON_INFO"] = info;

            RaiseNextObjectEvent(args);
        }

        void myTimer_TickRequests(object sender, EventArgs e)
        {
            myTimer.Stop();
            myTimer.Tick -= myTimer_TickRequests;

            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "STOCK_REQUESTS";
            args.data["PERSON_INFO"] = info;

            RaiseNextObjectEvent(args);
        }
    }
}

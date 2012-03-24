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

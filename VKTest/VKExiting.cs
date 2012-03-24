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

namespace VKTest
{
    public partial class VKExiting : VKObject
    {
        ClientUI.UserObjectEventArgs args;
        public VKExiting()
            : base()
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);
            this.args = args;

            nameLabel.Text = info.name;
        }

        public override void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.None)
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        myTimer.Tick += new EventHandler(myTimer_TickEnter);
                        myTimer.Interval = 300;
                        myTimer.Start();
                        e.Handled = true;
                        return;
                    case Keys.Escape:
//                        MessageBox.Show("VKExiting::OnKeyDown");
                        myTimer.Tick += new EventHandler(myTimer_TickEscape);
                        myTimer.Interval = 300;
                        myTimer.Start();
                        e.Handled = true;
                        return;
                }
            }
            base.OnKeyDown(sender, e);
        }

        void myTimer_TickEscape(object sender, EventArgs e)
        {
            myTimer.Tick -= myTimer_TickEscape;
            myTimer.Stop();
            args.NextObject = Convert.ToString(args.data["WHERE"]);
            RaiseNextObjectEvent(args);
        }

        void myTimer_TickEnter(object sender, EventArgs e)
        {
            myTimer.Tick -= myTimer_TickEnter;
            myTimer.Stop();

            args.NextObject = "FINISH";
            RaiseNextObjectEvent(args);
        }
    }
}

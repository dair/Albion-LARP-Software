/* ***********************************************************************
 * (C) 2008-2011 Vladimir Lebedev-Schmidthof <vladimir@schmidthof.com>
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
using ClientUI;

namespace ATM
{
    public partial class ATMSelect : ATMObject
    {
        public ATMSelect()
        {
            InitializeComponent();
        }

        public ATMSelect(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);
        }

        private void NextObject(String code)
        {
            UserObjectEventArgs args = new UserObjectEventArgs();
            args.NextObject = code;
            args.data["PERSON_INFO"] = info;
            RaiseNextObjectEvent(args);
        }

        public override void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.None)
            {
                switch (e.KeyCode)
                {
                    case Keys.D1:
                        NextObject("BALANCE");
                        break;
                    case Keys.D2:
                        NextObject("TRANSFER");
                        break;
                    case Keys.D3:
                        NextObject("TRANSFER_PROJECT");
                        break;
                    case Keys.D4:
                        NextObject("HISTORY");
                        break;
                }
            }
            base.OnKeyDown(sender, e);
        }
    }
}

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

namespace InfoTerm
{
    public partial class InfoTermSearch : InfoTermObject
    {
        public InfoTermSearch()
            : base()
        {
            InitializeComponent();
        }

        private void queryBox_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(sender, e);
            if (e.Modifiers == Keys.None && e.KeyCode == Keys.Enter)
            {
                if (queryBox.Text.Trim().Length > 0)
                {
                    ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
                    args.NextObject = "TABLE";
                    args.data["PERSON_INFO"] = info;
                    args.data["SEARCH_STRING"] = queryBox.Text;
                    RaiseNextObjectEvent(args);
                }
            }
        }
    }
}

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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClientUI;

namespace CashDesk
{
    public partial class CashDeskForm : ClientUI.ClientForm
    {
        public CashDeskForm()
        {
            InitializeComponent();
        }

        public CashDeskForm(Database.Connection db, ClientSettings s, BarCode.ReaderControl r, Logger.Logging logger)
            : base(db, s, r, logger, 120000)
        {
            InitializeComponent();

            userObjects["START"] = new CashDeskStart(db);
            userObjects["AMOUNT"] = new CashDeskAmount(db);
            userObjects["VERIFY"] = new CashDeskVerify(db);

            startupObjectKey = "START";
        }

        private void CashDeskForm_Load(object sender, EventArgs e)
        {
        }
    }
}

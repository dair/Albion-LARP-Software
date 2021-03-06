﻿/* ***********************************************************************
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

namespace ATM
{
    public partial class ATMBalance : ATMObject
    {
        public ATMBalance()
        {
            InitializeComponent();
        }

        public ATMBalance(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);
            decimal inDollars = (decimal)info.balance / 100;

            balanceLabel.Text = "$" + inDollars.ToString("N");

            DataTable table = new DataTable();
            getDatabase().fillSharesByPerson(info.id, table);

            foreach (DataRow row in table.Rows)
            {
                System.Console.WriteLine(Convert.ToString(row["TICKER"]) + ": " + Convert.ToString(row["SHARE"]));
            }

            dataGridView.DataSource = table;
            dataGridView.Refresh();
            dataGridView.Columns["NAME"].Visible = false;
        }

        public override void OnKeyDown(object sender, KeyEventArgs e)
        {
            //(ParentForm as ClientUI.ClientForm).toStart();
            base.OnKeyDown(sender, e);
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(sender, e);
        }
    }
}

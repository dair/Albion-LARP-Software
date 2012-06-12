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
    public partial class ATMHistory : ATMObject
    {
        public ATMHistory()
        {
            InitializeComponent();
        }

        public ATMHistory(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);

            DataTable dt = new DataTable();
            getDatabase().fillWithHistory(info.id, dt);

            dataGridView.Rows.Clear();

            foreach (DataRow row in dt.Rows)
            {
                String smb = ">";
                if (Convert.ToString(row["INOUT"]) == "IN")
                    smb = "<";
                int rownum = dataGridView.Rows.Add(row["TIME"], smb, row["AMOUNT"], row["USER"]);
                if (Convert.ToString(row["INOUT"]) == "IN")
                {
                    dataGridView.Rows[rownum].Cells["INOUT"].Style.ForeColor = Color.Green;
                }
                else
                {
                    dataGridView.Rows[rownum].Cells["INOUT"].Style.ForeColor = Color.Red;
                }

            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(sender, e);
        }
    }
}

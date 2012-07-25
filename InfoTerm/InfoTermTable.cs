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
    public partial class InfoTermTable : InfoTermObject
    {
        String searchString;

        public InfoTermTable()
            : base()
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);
            searchString = Convert.ToString(args.data["SEARCH_STRING"]);
            DataTable table = new DataTable();
            getDatabase().searchInfo(searchString, table);
            baseTableView.DataSource = table;
            baseTableView.Columns["ID"].Width = 100;
            baseTableView.Columns["NAME"].Width = 200;
            baseTableView.Refresh();
        }

        private void baseTableView_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(sender, e);
            if (!e.Handled)
            {
                if (e.Modifiers == Keys.None && e.KeyCode == Keys.Enter)
                {
                    if (baseTableView.SelectedRows.Count == 1)
                    {
                        UInt64 id = Convert.ToUInt64(baseTableView.SelectedRows[0].Cells["ID"].Value);
                        if (id > 0)
                        {
                            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
                            args.NextObject = "FULLINFO";
                            args.data["PERSON_INFO"] = info;
                            args.data["SEARCH_STRING"] = searchString;
                            args.data["INFO_ABOUT"] = id;
                            RaiseNextObjectEvent(args);
                        }
                    }
                }
            }
        }
    }
}

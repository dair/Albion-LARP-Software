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

namespace StockMaster
{
    public partial class SharesByPersonTableView : UI.BaseTableView
    {
        public SharesByPersonTableView()
            : base()
        {
        }

        public SharesByPersonTableView(Database.Connection db)
            : base(db)
        {
        }

        public void Retrieve(UInt64 personId)
        {
            if (getDatabase() == null)
                return;

            DataTable table = new DataTable();
            getDatabase().fillSharesByPerson(personId, table);
            //            MessageBox.Show(Convert.ToString(table.Rows.Count));
//            BindingSource bindingSource = new BindingSource();
//            bindingSource.DataSource = table;
//            DataSource = bindingSource;
            DataSource = table;
            Refresh();
        }
    }
}

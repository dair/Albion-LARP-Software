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
    public partial class CycleList : UI.BaseTableView
    {
        public CycleList()
        {
        }

        public CycleList(Database.Connection db)
            : base(db)
        {
        }

        public void Retrieve()
        {
            if (getDatabase() == null)
                return;

            DataTable table = new DataTable();
            getDatabase().fillWithStockCycles(table);

            foreach (DataColumn c in table.Columns)
            {
                System.Console.Write(c.ColumnName + "\t");
            }
            System.Console.WriteLine();

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn c in table.Columns)
                {
                    System.Console.Write(row[c.ColumnName]);
                }
                System.Console.WriteLine();
            }

            DataSource = table;
            //table.Rows.Add(0, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
            Refresh();
            //this.Rows.Add();
            Columns["ID"].Visible = false;
            Columns["START_TIME"].HeaderText = "Начало цикла";
            Columns["BORDER1_TIME"].HeaderText = "Конец ОТМЕНЫ заявок";
            Columns["BORDER2_TIME"].HeaderText = "Конец ПОДАЧИ заявок";
            Columns["FINISH_TIME"].HeaderText = "Конец цикла";

            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }

        public UInt64 getCurrentCycleId()
        {
            UInt64 ret = 0;

            try
            {
                ret = Convert.ToUInt64(SelectedRows[0].Cells["ID"].Value);
            }
            catch (Exception)
            {
            }

            return ret;
        }
    }
}

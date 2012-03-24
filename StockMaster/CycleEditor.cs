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
    public partial class CycleEditor : UI.DBObjectUserControl
    {
        public CycleEditor()
        {
            InitializeComponent();
        }

        public CycleEditor(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            getDatabase().fillWithLatestStockQuotes(table);
            NewCycleForm n = new NewCycleForm();
            Database.StockCycleInfo info = new Database.StockCycleInfo();
            info.quotes = table;
            n.info = info;

            if (n.ShowDialog() == DialogResult.OK)
            {
                getDatabase().newCycle(n.info);
                cycleList.Retrieve();
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            UInt64 cycleId = cycleList.getCurrentCycleId();
            if (cycleId == 0)
            {
                MessageBox.Show("Нечего редактировать");
                return;
            }

            DataTable table = new DataTable();
            getDatabase().fillWithQuotesForCycle(cycleId, table);

            Database.StockCycleInfo info = new Database.StockCycleInfo();
            info.quotes = table;
            info.id = cycleId;
            info.start = Convert.ToDateTime(cycleList.SelectedRows[0].Cells["START_TIME"].Value);
            info.border1 = Convert.ToDateTime(cycleList.SelectedRows[0].Cells["BORDER1_TIME"].Value);
            info.border2 = Convert.ToDateTime(cycleList.SelectedRows[0].Cells["BORDER2_TIME"].Value);
            info.finish = Convert.ToDateTime(cycleList.SelectedRows[0].Cells["FINISH_TIME"].Value);

            NewCycleForm n = new NewCycleForm();
            n.info = info;

            if (n.ShowDialog() == DialogResult.OK)
            {
                getDatabase().editCycle(info);
                cycleList.Retrieve();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            UInt64 cycleId = cycleList.getCurrentCycleId();
            if (cycleId == 0)
            {
                MessageBox.Show("Нечего удалять");
                return;
            }

            if (MessageBox.Show("Удалить цикл\r\nВместе с циклом удалятся и заявки, и котировки", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                getDatabase().deleteCycle(cycleList.getCurrentCycleId());
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            cycleList.Retrieve();

/*            DataTable table = new DataTable();
            getDatabase().fillWithStockCycles(table);
            cycleList.DataSource = table;
            dataGridView1.DataSource = table;
            //cycleList.Refresh();
            dataGridView1.Refresh();*/
        }

        private void CycleEditor_Load(object sender, EventArgs e)
        {
            cycleList.Retrieve();
        }

        private void cycleList_SelectionChanged(object sender, EventArgs e)
        {
            UInt64 cycleId = cycleList.getCurrentCycleId();
            requestEditor.Retrieve(cycleId);
            quoteList1.Retrieve(cycleId);
        }
    }
}

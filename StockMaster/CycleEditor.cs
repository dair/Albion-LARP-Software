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
            n.quotes = table;
            if (n.ShowDialog() == DialogResult.OK)
            {
                Database.StockCycleInfo info = new Database.StockCycleInfo();
                info.start = n.start;
                info.border1 = n.border1;
                info.border2 = n.border2;
                info.finish = n.finish;
                info.quotes = table;
                getDatabase().newCycle(info);
                cycleList.Retrieve();
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {

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

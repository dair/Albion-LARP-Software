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
    public partial class QuoteList : UI.BaseTableView
    {
        public QuoteList()
        {
        }

        public QuoteList(Database.Connection db)
            : base(db)
        {
        }

        public void Retrieve(UInt64 cycleId)
        {
            DataTable table = new DataTable();
            getDatabase().fillWithQuotesForCycle(cycleId, table);
            this.DataSource = table;
            Columns["CYCLE_ID"].Visible = false;
            Refresh();
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // QuoteList
            // 
            this.RowHeadersVisible = false;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
    }
}

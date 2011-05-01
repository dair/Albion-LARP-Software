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
    public partial class RequestList : UI.BaseTableView
    {
        public RequestList()
        {
        }

        public RequestList(Database.Connection db)
            : base(db)
        {
        }

        public void Retrieve(UInt64 cycleId)
        {
            DataTable table = new DataTable();
            getDatabase().fillWithRequests(cycleId, table);
            DataSource = table;

            Columns["ID"].Visible = false;
            Columns["PERSON_ID"].Visible = false;
        }
    }
}

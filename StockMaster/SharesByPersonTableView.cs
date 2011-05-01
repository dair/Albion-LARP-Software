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

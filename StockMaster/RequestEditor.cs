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
    public partial class RequestEditor : UI.DBObjectUserControl
    {
        public RequestEditor()
        {
            InitializeComponent();
        }

        public RequestEditor(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public void Retrieve(UInt64 cycleId)
        {
            requestList.Retrieve(cycleId);
        }

        private void processButton_Click(object sender, EventArgs e)
        {
            DataTable table = (DataTable)requestList.DataSource;

        }
    }
}

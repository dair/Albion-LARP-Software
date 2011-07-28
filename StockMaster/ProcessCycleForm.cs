using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockMaster
{
    public partial class ProcessCycleForm : Form
    {
        public DataTable table;

        public ProcessCycleForm()
        {
            InitializeComponent();

            this.Closing += new CancelEventHandler(ProcessCycleForm_Closing);
        }

        void ProcessCycleForm_Closing(object sender, CancelEventArgs e)
        {
            Settings.UI.storeForm(this);
        }

        private void ProcessCycleForm_Load(object sender, EventArgs e)
        {
            Settings.UI.restoreForm(this);

            if (table != null)
            {
                dataGridView.DataSource = table;
                dataGridView.ReadOnly = false;
                dataGridView.Columns["ID"].ReadOnly = true;
                dataGridView.Columns["NAME"].ReadOnly = true;
                dataGridView.Columns["TICKER"].ReadOnly = true;
                dataGridView.Columns["OPERATION"].ReadOnly = true;
                dataGridView.Columns["QTY"].ReadOnly = true;
                dataGridView.Columns["BROKER"].ReadOnly = true;
                dataGridView.Columns["RESULT"].ReadOnly = false;

            }
        }
    }
}

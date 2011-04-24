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
    public partial class NewsEditor : UI.DBObjectUserControl
    {
        private DataTable table = new DataTable();
        private BindingSource bindingSource = null;

        public NewsEditor()
        {
            InitializeComponent();
        }

        public NewsEditor(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
            bindingSource = new BindingSource();
            dataGridView.DataSource = bindingSource;
            Retrieve();
        }

        private void NewsEditor_Load(object sender, EventArgs e)
        {
            if (getDatabase() == null)
                return;

            Retrieve();
        }

        public void Retrieve()
        {
            if (getDatabase() == null)
                return;

            getDatabase().fillWithNews(table);
            bindingSource.DataSource = table;

            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            dataGridView.Refresh();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            NewsEditForm nf = new NewsEditForm();
            if (nf.ShowDialog(this) == DialogResult.OK)
            {
                getDatabase().editNews(nf.info);
                Retrieve();
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (getCurrentId() != 0)
            {
                Database.NewsInfo newsInfo = new Database.NewsInfo();
                newsInfo.id = getCurrentId();
                newsInfo.date = Convert.ToDateTime(dataGridView.SelectedRows[0].Cells["TIME"].Value);
                newsInfo.title = Convert.ToString(dataGridView.SelectedRows[0].Cells["TITLE"].Value);
                newsInfo.text = Convert.ToString(dataGridView.SelectedRows[0].Cells["TEXT"].Value);
                NewsEditForm nf = new NewsEditForm();
                nf.info = newsInfo;
                if (nf.ShowDialog(this) == DialogResult.OK)
                {
                    getDatabase().editNews(nf.info);
                    Retrieve();
                }
            }
        }

        public UInt64 getCurrentId()
        {
            if (dataGridView.SelectedRows.Count != 1)
                return 0;

            return Convert.ToUInt64(dataGridView.SelectedRows[0].Cells["ID"].Value);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (getCurrentId() != 0)
            {
                if (MessageBox.Show("В самом деле удалить новость?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    getDatabase().deleteNews(getCurrentId());
                    Retrieve();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UI;

namespace Master
{
    public class VKQuestionList : DataGridView, IDBObject
    {
        private Database.Connection database = null;
        public DataTable dataTable = new DataTable();
        private BindingSource bindingSource = null;

        public VKQuestionList()
        {
            InitializeComponent();
        }

        public VKQuestionList(Database.Connection db)
        {
            database = db;
            if (database == null)
                return;

            InitializeComponent();

            bindingSource = new BindingSource();
            DataSource = bindingSource;
        }

        public Database.Connection getDatabase()
        {
            return database;
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // VKQuestionList
            // 
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToResizeRows = false;
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.BackgroundColor = System.Drawing.SystemColors.Window;
            this.MultiSelect = false;
            this.ReadOnly = true;
            this.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        public void Retrieve()
        {
            if (database == null)
                return;

            database.fillWithVKQuestions(dataTable);
            bindingSource.DataSource = dataTable;

            AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            Refresh();
        }

        public UInt16 getCurrentQuestionId()
        {
            if (SelectedRows.Count != 1)
            {
                return 0;
            }

            if (SelectedRows[0].Cells[0].Value is DBNull)
            {
                return 0;
            }

            return Convert.ToUInt16(SelectedRows[0].Cells[0].Value);
        }

        public void setCurrentQuestionId(UInt16 qid)
        {
            bool somethingSelected = false;
            foreach (DataGridViewRow row in Rows)
            {
                UInt16 cellId = Convert.ToUInt16(row.Cells[0].Value);

                if (cellId == qid)
                {
                    FirstDisplayedScrollingRowIndex = row.Index;
                    Refresh();
                    CurrentCell = row.Cells[0];
                    row.Selected = true;
                    somethingSelected = true;
                }
            }

            if (!somethingSelected)
            {
                if (Rows.Count > 0)
                {
                    Rows[0].Selected = true;
                }
            }

        }

    }
}

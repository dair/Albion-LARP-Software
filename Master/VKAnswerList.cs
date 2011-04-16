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
    public class VKAnswerList : DataGridView, IDBObject
    {
        private Database.Connection database = null;
        public DataTable dataTable = new DataTable();
        private BindingSource bindingSource = null;

        public VKAnswerList()
        {
            InitializeComponent();
        }

        public VKAnswerList(Database.Connection db)
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
            this.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }

        public void Retrieve(UInt16 questionId)
        {
            if (database == null)
                return;
            dataTable.Clear();
            database.fillWithVKAnswers(questionId, dataTable);
            bindingSource.DataSource = dataTable;

            AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

            Refresh();
       }

        public UInt16 getCurrentAnswerId()
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

        public Database.VKAnswerInfo getCurrentAnswerInfo()
        {
            if (SelectedRows.Count != 1)
            {
                return null;
            }

            if (SelectedRows[0].Cells[0].Value is DBNull)
            {
                return null;
            }

            Database.VKAnswerInfo info = new Database.VKAnswerInfo();
            info.answerId = Convert.ToUInt16(SelectedRows[0].Cells[0].Value);
            info.text = Convert.ToString(SelectedRows[0].Cells[1].Value);
            info.humanValue = Convert.ToInt16(SelectedRows[0].Cells[2].Value);
            info.androidValue = Convert.ToInt16(SelectedRows[0].Cells[3].Value);

            return info;
        }

        public UInt16 selectedIndex()
        {
            UInt16 ret = 0;
            foreach (DataGridViewRow row in Rows)
            {
                if (row.Selected)
                    break;
                ret++;
            }
            return ret;
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Database;

namespace UI
{
    public partial class PersonList : DataGridView, IDBObject
    {
        public DataTable dataTable = new DataTable();
        private Database.Connection database = null;
        private BindingSource bindingSource = null;

        public PersonList()
            : base()
        {
            InitializeComponent();
        }

        public PersonList(Database.Connection db)
            : base()
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

        public void Retrieve()
        {
            if (database == null)
                return;

            database.fillWithPersons(dataTable);
            bindingSource.DataSource = dataTable;

            AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // PersonList
            // 
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToOrderColumns = true;
            this.AllowUserToResizeRows = false;
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BackgroundColor = System.Drawing.SystemColors.Window;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MultiSelect = false;
            this.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        public Database.PersonInfo getCurrentPersonInfo()
        {
            if (SelectedRows.Count != 1)
            {
                return null;
            }

            if (SelectedRows[0].Cells[0].Value is DBNull)
            {
                return null;
            }

            Database.PersonInfo ret = new Database.PersonInfo();
            ret.id = Convert.ToUInt16(SelectedRows[0].Cells[0].Value);
            ret.name = Convert.ToString(SelectedRows[0].Cells[1].Value);
            return ret;
        }

    }
}

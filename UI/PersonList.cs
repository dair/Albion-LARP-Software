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

            DataTable table = new DataTable();
            database.fillWithPersons(table);
            bindingSource.DataSource = table;

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
            this.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        public UInt16 getId()
        {
            if (SelectedRows.Count != 1)
            {
                return 0;
            }

            return Convert.ToUInt16(SelectedRows[0].Cells[0].Value);
        }
    }
}

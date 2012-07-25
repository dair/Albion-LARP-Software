/* ***********************************************************************
 * (C) 2008-2012 Vladimir Lebedev-Schmidthof <vladimir@schmidthof.com>
 * Made for Albion Games (http://albiongames.org)
 * 
 * 
 *            DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
 *                    Version 2, December 2004

 * Copyright (C) 2004 Sam Hocevar <sam@hocevar.net>
 * 
 * Everyone is permitted to copy and distribute verbatim or modified
 * copies of this license document, and changing it is allowed as long
 * as the name is changed.

 *           DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
 *   TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION

 *  0. You just DO WHAT THE FUCK YOU WANT TO.
 * *********************************************************************** */

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
        }

        public Database.Connection getDatabase()
        {
            return database;
        }

        public void setDatabase(Database.Connection db)
        {
            database = db;
        }

        public void Retrieve()
        {
            if (database == null)
                return;

            DataTable dataTable = new DataTable();
            database.fillWithPersons(dataTable);
            DataSource = dataTable;

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
            this.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.MultiSelect = false;
            this.ReadOnly = true;
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
            ret.id = Convert.ToUInt64(SelectedRows[0].Cells[0].Value);
            ret.name = Convert.ToString(SelectedRows[0].Cells[1].Value);
            return ret;
        }

        public void setCurrentPersonId(UInt64 id)
        {
            bool somethingSelected = false;
            foreach (DataGridViewRow row in Rows)
            {
                UInt64 cellId = Convert.ToUInt64(row.Cells[0].Value);

                if (cellId == id)
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

﻿/* ***********************************************************************
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

        public void Retrieve(UInt64 questionId)
        {
            if (database == null)
                return;
            dataTable.Clear();
            database.fillWithVKAnswers(questionId, dataTable);
            bindingSource.DataSource = dataTable;

            AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

            Refresh();
       }

        public UInt64 getCurrentAnswerId()
        {
            if (SelectedRows.Count != 1)
            {
                return 0;
            }

            if (SelectedRows[0].Cells[0].Value is DBNull)
            {
                return 0;
            }

            return Convert.ToUInt64(SelectedRows[0].Cells[0].Value);
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
            info.text = Convert.ToString(SelectedRows[0].Cells["TEXT"].Value);
            info.humanValue = Convert.ToInt16(SelectedRows[0].Cells["HUMAN"].Value);
            info.androidValue = Convert.ToInt16(SelectedRows[0].Cells["ANDROID"].Value);

            return info;
        }

        public int selectedIndex()
        {
            if (SelectedRows.Count != 1)
                return 0;

            return SelectedRows[0].Index;
        }

    }
}

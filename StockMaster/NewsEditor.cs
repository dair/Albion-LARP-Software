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
using System.Text.RegularExpressions;

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
            table = new DataTable();
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

        private void refreshButton_Click(object sender, EventArgs e)
        {
            Retrieve();
        }

        private void addMultipleButton_Click(object sender, EventArgs e)
        {
            MultipleNewsAdd m = new MultipleNewsAdd();
            if (m.ShowDialog() == DialogResult.OK)
            {
                string[] lines = Regex.Split(m.text, "\r\n");

                foreach (String line in lines)
                {
                    Database.NewsInfo info = new Database.NewsInfo();
                    info.title = "";
                    info.text = line;
                    info.date = DateTime.Now;
                    getDatabase().editNews(info);
                }

                Retrieve();
            }
        }
    }
}

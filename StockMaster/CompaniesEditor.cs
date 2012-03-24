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


namespace StockMaster
{
    public partial class CompaniesEditor : UI.DBObjectUserControl
    {
        private DataTable table = new DataTable();
        private BindingSource bindingSource = null;

        public CompaniesEditor()
        {
            InitializeComponent();
        }

        public CompaniesEditor(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
            bindingSource = new BindingSource();
            dataGridView.DataSource = bindingSource;
            Retrieve();
        }

        private void CompaniesEditor_Load(object sender, EventArgs e)
        {
            if (getDatabase() == null)
                return;

            Retrieve();
        }

        public void Retrieve()
        {
            if (getDatabase() == null)
                return;

            getDatabase().fillWithCompanies(table);
            bindingSource.DataSource = table;

            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            dataGridView.Refresh();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            StockCompanyEditForm nf = new StockCompanyEditForm();
            if (nf.ShowDialog(this) == DialogResult.OK)
            {
                getDatabase().editCompany(null, nf.info);
                Retrieve();
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (getCurrentKey() != null)
            {
                Database.StockCompanyInfo info = new Database.StockCompanyInfo();
                info.key = getCurrentKey();

                info.name = Convert.ToString(dataGridView.SelectedRows[0].Cells["NAME"].Value);
                info.stockAmount = Convert.ToUInt64(dataGridView.SelectedRows[0].Cells["STOCK"].Value);
                StockCompanyEditForm nf = new StockCompanyEditForm();
                nf.info = info;
                if (nf.ShowDialog(this) == DialogResult.OK)
                {
                    getDatabase().editCompany(getCurrentKey(), nf.info);
                    Retrieve();
                }
            }
        }

        public String getCurrentKey()
        {
            if (dataGridView.SelectedRows.Count != 1)
                return null;

            return Convert.ToString(dataGridView.SelectedRows[0].Cells["TICKER"].Value);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (getCurrentKey() != null)
            {
                if (MessageBox.Show("Не компания и была?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    getDatabase().deleteCompany(getCurrentKey());
                    Retrieve();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Retrieve();
        }
    }
}

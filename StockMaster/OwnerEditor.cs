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

namespace StockMaster
{
    public partial class OwnerEditor : UI.DBObjectUserControl
    {
        DataTable companies = null;

        public OwnerEditor()
        {
            InitializeComponent();
        }

        public OwnerEditor(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        private void Retrieve()
        {
            personList.SelectionChanged -= personList_SelectionChanged;
            personList.Retrieve();
            personList.SelectionChanged += personList_SelectionChanged;
            personList_SelectionChanged(this, null);
            companies = null;
        }

        private void OwnerEditor_Load(object sender, EventArgs e)
        {
            Retrieve();
        }

        private void personList_SelectionChanged(object sender, EventArgs e)
        {
            Database.PersonInfo info = personList.getCurrentPersonInfo();
            UInt64 pid = 0;
            if (info != null)
            {
                pid = info.id;
            }
            sharesByPersonTableView.Retrieve(pid);
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            personList_SelectionChanged(sender, e);
        }



        private String selectedTicker()
        {
            if (sharesByPersonTableView.SelectedRows.Count != 1)
                return null;

            return Convert.ToString(sharesByPersonTableView.SelectedRows[0].Cells["TICKER"].Value);
        }

        private String companyString(String ticker, String name)
        {
            return ticker + " - " + name;
        }

        private void fillCompanies()
        {
            companies = new DataTable();
            getDatabase().fillWithCompanies(companies);
        }

        private IList<String> unusedCompanies()
        {
            if (companies == null)
                fillCompanies();

            List<String> c = new List<string>();

            foreach (DataRow row in companies.Rows)
            {
                c.Add(companyString(Convert.ToString(row["TICKER"]), Convert.ToString(row["NAME"])));
            }

            foreach (DataGridViewRow row in sharesByPersonTableView.Rows)
            {
                String s = companyString(Convert.ToString(row.Cells["TICKER"].Value), Convert.ToString(row.Cells["NAME"].Value));
                if (c.Contains(s))
                {
                    c.Remove(s);
                }
            }
            
            return c;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Database.PersonInfo info = personList.getCurrentPersonInfo();
            if (info == null || info.id == 0)
            {
                MessageBox.Show("Непонятно для кого акции добавлять. Надо выбрать владельца");
                return;
            }

            OwnerEditForm oForm = new OwnerEditForm();
            oForm.otherCompanies = unusedCompanies();
            if (oForm.otherCompanies.Count == 0)
            {
                MessageBox.Show("Нет больше компаний, где чувак не был бы (со)владельцем");
                return;
            }

            if (oForm.ShowDialog() == DialogResult.OK)
            {
                String ticker = oForm.company.Substring(0, oForm.company.IndexOf(' '));
                UInt64 share = oForm.share;

                getDatabase().editPersonShare(info.id, null, ticker, share);
                sharesByPersonTableView.Retrieve(info.id);
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            Database.PersonInfo info = personList.getCurrentPersonInfo();
            if (info == null || info.id == 0)
            {
                MessageBox.Show("Непонятно для кого акции добавлять. Надо выбрать владельца");
                return;
            }

            if (selectedTicker() == null)
            {
                MessageBox.Show("Нечего редактировать");
                return;
            }

            String s = companyString(selectedTicker(), Convert.ToString(sharesByPersonTableView.SelectedRows[0].Cells["NAME"].Value));

            OwnerEditForm oForm = new OwnerEditForm();
            oForm.otherCompanies = unusedCompanies();
            oForm.company = s;
            oForm.share = Convert.ToUInt64(sharesByPersonTableView.SelectedRows[0].Cells["SHARE"].Value);
            if (oForm.ShowDialog() == DialogResult.OK)
            {
                String ticker = oForm.company.Substring(0, oForm.company.IndexOf(' '));
                UInt64 share = oForm.share;

                getDatabase().editPersonShare(info.id, selectedTicker(), ticker, share);
                sharesByPersonTableView.Retrieve(info.id);
            }

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            Database.PersonInfo info = personList.getCurrentPersonInfo();
            if (info == null || info.id == 0)
            {
                MessageBox.Show("Непонятно для кого акции удалять. Надо выбрать владельца");
                return;
            }

            if (selectedTicker() == null)
            {
                MessageBox.Show("Нечего удалять");
                return;
            }

            if (MessageBox.Show("В самом деле убрать чувака из (со)владельцев?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                getDatabase().deletePersonShare(info.id, selectedTicker());
                sharesByPersonTableView.Retrieve(info.id);
            }
        }

        private void refreshPersonButton_Click(object sender, EventArgs e)
        {
            Retrieve();
        }

    }
}

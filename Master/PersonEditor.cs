﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Master
{
    public partial class PersonEditor : UI.DBObjectUserControl
    {
        public PersonEditor()
        {
            InitializeComponent();
        }

        public PersonEditor(Database.Connection db)
            : base(db)
        {
            InitializeComponent();

            this.personList.SelectionChanged += new EventHandler(personList_SelectionChanged);
        }

        private void PersonEditor_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            Database.PersonInfo pInfo = personList.getCurrentPersonInfo();
            personList.SelectionChanged -= personList_SelectionChanged;
            personList.Retrieve();
            personList.SelectionChanged += personList_SelectionChanged;
            if (pInfo != null)
                personList.setCurrentPersonId(pInfo.getId());
//            personList_SelectionChanged(personList, null);
        }

        void personList_SelectionChanged(object sender, EventArgs e)
        {
            Database.PersonInfo pInfo = personList.getCurrentPersonInfo();
            UInt64 id = 0;
            if (pInfo != null)
                id = pInfo.getId();
            personInfo.setId(id);
            moneyInfo.setId(id);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            DataRow row = personList.dataTable.Rows.Add();
            personList.Rows[personList.Rows.Count - 1].Selected = true;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            Database.PersonInfo pInfo = personList.getCurrentPersonInfo();
            if (pInfo == null)
            {
                MessageBox.Show("Некого удалять", "Тишина", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String name = "без имени";
            if (pInfo.getName() != null || pInfo.getName().Trim().Equals(""))
                name = "по имени " + pInfo.getName();

            if (MessageBox.Show("Удалить неудачника " + name + "?", "Удалить?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                getDatabase().deletePerson(pInfo.getId());
                
                RefreshData();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Database.FullPersonInfo fpInfo = personInfo.getFullPersonInfo();
            Database.MoneyInfo mInfo = moneyInfo.getMoneyInfo();

            if (fpInfo != null &&
                mInfo != null)
            {
                if (fpInfo.id == 0)
                {
                    MessageBox.Show("Надо обязательно указать номер!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Database.PersonInfo pInfo = personList.getCurrentPersonInfo();
                UInt64 oldId = 0;
                if (pInfo != null)
                    oldId = pInfo.getId();

                getDatabase().updatePerson(oldId, fpInfo);
                getDatabase().updateMoney(fpInfo.getId(), mInfo);

                RefreshData();
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}

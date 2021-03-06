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

namespace UI
{
    public partial class PersonInfo : DBObjectUserControl
    {
        private Database.FullPersonInfo fullPersonInfo = null;
        private BindingSource bindingSource = null;

        public PersonInfo()
        {
            InitializeComponent();
        }

        public PersonInfo(Database.Connection db)
            : base(db)
        {
            InitializeComponent();

            bindingSource = new BindingSource();
            propertiesGridView.DataSource = bindingSource;
            propertiesGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        public void setId(UInt64 id)
        {
            if (id == 0)
            {
                fullPersonInfo = new Database.FullPersonInfo();
                this.idBox.Text = "";
                this.nameBox.Text = "";
                genderBox.SelectedIndex = 0;
                genomeBox.SelectedIndex = 0;
            }
            else
            {
                this.Enabled = true;
                fullPersonInfo = getDatabase().getPersonInfo(id);

                this.idBox.Text = Convert.ToString(fullPersonInfo.id);
                this.nameBox.Text = fullPersonInfo.name;

                switch (fullPersonInfo.gender)
                {
                    case Database.FullPersonInfo.Gender.Female:
                        genderBox.SelectedIndex = 2;
                        break;
                    case Database.FullPersonInfo.Gender.Male:
                        genderBox.SelectedIndex = 1;
                        break;
                    case Database.FullPersonInfo.Gender.Unknown:
                        genderBox.SelectedIndex = 0;
                        break;
                }

                switch (fullPersonInfo.genome)
                {
                    case Database.FullPersonInfo.Genome.Android:
                        genomeBox.SelectedIndex = 1;
                        break;
                    case Database.FullPersonInfo.Genome.Human:
                        genomeBox.SelectedIndex = 0;
                        break;
                }


            }
            bindingSource.DataSource = fullPersonInfo.properties;
            propertiesGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            propertiesGridView.Columns[2].Width = propertiesGridView.Width * 4 / 5;
            propertiesGridView.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }

        public Database.FullPersonInfo getFullPersonInfo()
        {
            Database.FullPersonInfo ret = fullPersonInfo;

            ret.id = Convert.ToUInt64(idBox.Text);

            ret.name = nameBox.Text.Trim();
            switch (genderBox.SelectedIndex)
            {
                case 0:
                    ret.gender = Database.FullPersonInfo.Gender.Unknown;
                    break;
                case 1:
                    ret.gender = Database.FullPersonInfo.Gender.Male;
                    break;
                case 2:
                    ret.gender = Database.FullPersonInfo.Gender.Female;
                    break;
            }

            switch (genomeBox.SelectedIndex)
            {
                case 0:
                    ret.genome = Database.FullPersonInfo.Genome.Human;
                    break;
                case 1:
                    ret.genome = Database.FullPersonInfo.Genome.Android;
                    break;
            }

            return ret;
        }

        private IList<Database.PropertyInfo> getUnusedProperties(bool excludeSelected)
        {
            IList<Database.PropertyInfo> list = getDatabase().getPropertyList();

            UInt64 selectedPropId = 0;
            if (propertiesGridView.SelectedRows.Count == 1)
                selectedPropId = Convert.ToUInt64(propertiesGridView.SelectedRows[0].Cells[0].Value);

            foreach (DataRow row in fullPersonInfo.properties.Rows)
            {
                UInt64 propId = Convert.ToUInt64(row[0]);
                if (excludeSelected && propId == selectedPropId)
                    continue;

                for (int i = 0; i < list.Count; ++i)
                {
                    if (list[i].id == propId)
                    {
                        list.RemoveAt(i);
                        break;
                    }
                }
            }

            return list;
        }

        private void addPropButton_Click(object sender, EventArgs e)
        {
            IList<Database.PropertyInfo> propList = getUnusedProperties(false);
            if (propList.Count == 0)
            {
                MessageBox.Show("Все свойства заполнены, больше добавлять нечего", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PersonPropEdit edit = new PersonPropEdit(getDatabase());
            edit.list = propList;
            DialogResult result = edit.ShowDialog();
            if (result == DialogResult.OK)
            {
                Database.PersonProperty prop = edit.personProperty;
                fullPersonInfo.properties.Rows.Add(prop.propertyId, prop.propName, prop.value);
                propertiesGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
        }

        private Database.PersonProperty selectedPersonProperty()
        {
            if (propertiesGridView.SelectedRows.Count != 1)
            {
                return null;
            }
            Database.PersonProperty ret = new Database.PersonProperty();
            ret.propertyId = Convert.ToUInt64(propertiesGridView.SelectedRows[0].Cells[0].Value);
            ret.propName = Convert.ToString(propertiesGridView.SelectedRows[0].Cells[1].Value);
            ret.value = Convert.ToString(propertiesGridView.SelectedRows[0].Cells[2].Value);
            return ret;
        }

        private int selectedIndex()
        {
            int ret = 0;
            foreach (DataGridViewRow row in propertiesGridView.Rows)
            {
                if (row.Selected)
                    break;
                ret++;
            }
            return ret;
        }

        private void editPropButton_Click(object sender, EventArgs e)
        {
            if (propertiesGridView.SelectedRows.Count != 1)
            {
                MessageBox.Show("нечего редактировать", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            IList<Database.PropertyInfo> propList = getUnusedProperties(true);

            PersonPropEdit edit = new PersonPropEdit(getDatabase());
            edit.list = propList;
            edit.personProperty = selectedPersonProperty();
            DialogResult result = edit.ShowDialog();
            if (result == DialogResult.OK)
            {
                Database.PersonProperty prop = edit.personProperty;
                fullPersonInfo.properties.Rows[selectedIndex()][0] = prop.propertyId;
                fullPersonInfo.properties.Rows[selectedIndex()][1] = prop.propName;
                fullPersonInfo.properties.Rows[selectedIndex()][2] = prop.value;
                propertiesGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
        }

        private void deletePropButton_Click(object sender, EventArgs e)
        {
            if (propertiesGridView.SelectedRows.Count != 1)
            {
                MessageBox.Show("Нечего удалять", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Удалить свойство?", "В самом деле?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                fullPersonInfo.properties.Rows.RemoveAt(selectedIndex());
                propertiesGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
        }
    }
}

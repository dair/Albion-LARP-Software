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

namespace Master
{
    public partial class PropertyEditor : UI.DBObjectUserControl
    {
        BindingSource bindingSource;
        // ----------------------------------------------------------
        public PropertyEditor()
            : base()
        {
            InitializeComponent();

            bindingSource = new BindingSource();
            dataGridView.DataSource = bindingSource;
        }

        // ----------------------------------------------------------
        private void PropertyEditor_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        // ----------------------------------------------------------
        private void RefreshData()
        {
            if (getDatabase() == null)
                return;

            DataTable table = new DataTable();
            getDatabase().fillPropList(table);
            bindingSource.DataSource = table;
        }

        // ----------------------------------------------------------
        private void addButton_Click(object sender, EventArgs e)
        {
            PropertyEditWindow editWindow = new PropertyEditWindow();
            //editWindow.Closed += new EventHandler(editWindow_Closed);
            DialogResult res = editWindow.ShowDialog();
            if (res == DialogResult.OK)
            {
                saveProperty(editWindow.propertyInfo);
            }
        }

        // ----------------------------------------------------------
        private void saveProperty(Database.PropertyInfo propInfo)
        {
            getDatabase().editProperty(propInfo);
            RefreshData();
        }

        // ----------------------------------------------------------
        private Database.PropertyInfo getCurrentProperty()
        {
            if (dataGridView.SelectedRows.Count != 1)
            {
                return null;
            }

            Database.PropertyInfo propertyInfo = new Database.PropertyInfo();
            propertyInfo.id = Convert.ToUInt64(dataGridView.SelectedRows[0].Cells[0].Value);
            propertyInfo.name = Convert.ToString(dataGridView.SelectedRows[0].Cells[1].Value);
            propertyInfo.policeVisibility = Convert.ToBoolean(dataGridView.SelectedRows[0].Cells[2].Value);

            return propertyInfo;
        }

        // ----------------------------------------------------------
        private void editButton_Click(object sender, EventArgs e)
        {
            Database.PropertyInfo propertyInfo = getCurrentProperty();
            if (propertyInfo == null)
            {
                MessageBox.Show("Непонятно, что редактировать");
                return;
            }

            PropertyEditWindow w = new PropertyEditWindow();
            w.propertyInfo = propertyInfo;
            DialogResult res = w.ShowDialog();
            if (res == DialogResult.OK)
            {
                saveProperty(w.propertyInfo);
            }
        }

        // ----------------------------------------------------------
        private void deleteButton_Click(object sender, EventArgs e)
        {
            Database.PropertyInfo propertyInfo = getCurrentProperty();
            if (propertyInfo == null)
            {
                MessageBox.Show("Непонятно, что удалять");
                return;
            }

            if (MessageBox.Show("В самом деле удалить свойство \"" + propertyInfo.name + "\"?", "Да ну?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                getDatabase().deleteProperty(propertyInfo);
                RefreshData();
            }
        }

        private void refreshBitton_Click(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}

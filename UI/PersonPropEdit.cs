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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI
{
    public partial class PersonPropEdit : DBObjectForm
    {
        public Database.PersonProperty personProperty;
        public IList<Database.PropertyInfo> list = null;

        public PersonPropEdit()
        {
            InitializeComponent();
        }

        private void PersonPropEdit_Load(object sender, EventArgs e)
        {
            if (getDatabase() == null)
                return;

            if (list == null)
                list = getDatabase().getPropertyList();

            foreach (Database.PropertyInfo info in list)
            {
                propNameBox.Items.Add(info);
                if (personProperty != null &&
                    info.id == personProperty.propertyId)
                {
                    propNameBox.SelectedItem = info;
                }
            }

            if (personProperty != null)
            {
                textBox.Text = personProperty.value;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Database.PropertyInfo info = (Database.PropertyInfo)propNameBox.SelectedItem;
            if (info == null)
            {
                MessageBox.Show("Свойство надо-таки выбрать", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (personProperty == null)
                personProperty = new Database.PersonProperty();

            personProperty.propertyId = info.id;
            personProperty.propName = info.name;
            personProperty.value = textBox.Text;
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Master
{
    public partial class PropertyEditWindow : Form
    {
        public Database.PropertyInfo propertyInfo = null;

        public PropertyEditWindow()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (propertyInfo == null)
                propertyInfo = new Database.PropertyInfo();

            propertyInfo.name = nameBox.Text;
            propertyInfo.policeVisibility = policeVisibility.Checked;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void PropertyEditWindow_Load(object sender, EventArgs e)
        {
            if (propertyInfo != null)
            {
                nameBox.Text = propertyInfo.name;
                policeVisibility.Checked = propertyInfo.policeVisibility;
            }
        }
    }
}

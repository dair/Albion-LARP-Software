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

namespace StockMaster
{
    public partial class NewsEditForm : Form
    {
        public Database.NewsInfo info = null;

        public NewsEditForm()
        {
            InitializeComponent();
        }

        private void NewsEditForm_Load(object sender, EventArgs e)
        {
            Settings.UI.restoreForm(this);

            if (info == null)
                info = new Database.NewsInfo();
            else
            {
                dateTimePicker.Value = info.date;
                titleBox.Text = info.title;
                textBox.Text = info.text;
            }
            //DialogResult = DialogResult.Cancel;

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            info.date = dateTimePicker.Value;
            info.title = titleBox.Text;
            info.text = textBox.Text;
            DialogResult = DialogResult.OK;
            Settings.UI.storeForm(this);
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Settings.UI.storeForm(this);
            Close();
        }

        private void nowButton_Click(object sender, EventArgs e)
        {
            dateTimePicker.Value = DateTime.Now;
        }

    }
}

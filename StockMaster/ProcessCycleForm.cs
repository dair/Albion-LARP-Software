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
    public partial class ProcessCycleForm : Form
    {
        public DataTable table;

        public ProcessCycleForm()
        {
            InitializeComponent();

            this.Closing += new CancelEventHandler(ProcessCycleForm_Closing);
        }

        void ProcessCycleForm_Closing(object sender, CancelEventArgs e)
        {
            Settings.UI.storeForm(this);
        }

        private void ProcessCycleForm_Load(object sender, EventArgs e)
        {
            Settings.UI.restoreForm(this);

            if (table != null)
            {
                dataGridView.DataSource = table;
                dataGridView.ReadOnly = false;
                dataGridView.Columns["ID"].ReadOnly = true;
                dataGridView.Columns["NAME"].ReadOnly = true;
                dataGridView.Columns["TICKER"].ReadOnly = true;
                dataGridView.Columns["OPERATION"].ReadOnly = true;
                dataGridView.Columns["QTY"].ReadOnly = true;
                dataGridView.Columns["BROKER"].ReadOnly = true;
                dataGridView.Columns["RESULT"].ReadOnly = false;

            }
        }
    }
}

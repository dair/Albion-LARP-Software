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
    public partial class NewCycleForm : Form
    {
        public Database.StockCycleInfo info = null;

        public NewCycleForm()
        {
            InitializeComponent();
            this.Closing += new CancelEventHandler(NewCycleForm_Closing);
        }

        void NewCycleForm_Closing(object sender, CancelEventArgs e)
        {
            Settings.UI.storeForm(this);
        }

        private void NewCycleForm_Load(object sender, EventArgs e)
        {
            Settings.UI.restoreForm(this);
            if (info == null || info.quotes == null || info.quotes.Rows.Count == 0)
            {
                MessageBox.Show("Нету компаний!");
                return;
            }

            dataGridView.DataSource = info.quotes;

            dataGridView.Columns["CYCLE_ID"].Visible = false;

            dataGridView.Columns["TICKER"].HeaderText = "Тикер";
            dataGridView.Columns["TICKER"].ReadOnly = true;

            dataGridView.Columns["NAME"].HeaderText = "Название";
            dataGridView.Columns["NAME"].ReadOnly = true;

            dataGridView.Columns["QUOTE"].HeaderText = "Котировка, ЦЕНТЫ, целое число";
            dataGridView.Columns["QUOTE"].ReadOnly = false;

            dataGridView.Columns["TRADE_LIMIT"].HeaderText = "Ограничение торговли, шт.акций";
            dataGridView.Columns["TRADE_LIMIT"].ReadOnly = false;

            dataGridView.Columns["NPCS_BUY"].HeaderText = "NPC готовы купить акций в этот цикл, шт";
            dataGridView.Columns["NPCS_BUY"].ReadOnly = false;

            if (info.id != 0)
            {
                startDatePicker.Value = info.start;
                border1Picker.Value = info.border1;
                border2Picker.Value = info.border2;
                finishPicker.Value = info.finish;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (border1Picker.Value < startDatePicker.Value)
            {
                MessageBox.Show(label2.Text + " раньше чем " + label1.Text);
                return;
            }

            if (border2Picker.Value < border1Picker.Value)
            {
                MessageBox.Show(label3.Text + " раньше чем " + label2.Text);
                return;
            }

            if (finishPicker.Value < border2Picker.Value)
            {
                MessageBox.Show(label3.Text + " раньше чем " + label3.Text);
                return;
            }

            info.start = startDatePicker.Value;
            info.border1 = border1Picker.Value;
            info.border2 = border2Picker.Value;
            info.finish = finishPicker.Value;

            foreach (DataRow row in info.quotes.Rows)
            {
                UInt64 quote = 0;
                try
                {
                    quote = Convert.ToUInt64(row["QUOTE"]);
                }
                catch (Exception)
                {
                    MessageBox.Show("Непонятно что написано в котировке для " + row["TICKER"]);
                    return;
                }
                if (quote == 0)
                {
                    MessageBox.Show("Что-то странное (0?) написано в котировке для " + row["TICKER"]);
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void calcButton_Click(object sender, EventArgs e)
        {
            border1Picker.Value = startDatePicker.Value.AddMinutes(10);
            border2Picker.Value = startDatePicker.Value.AddHours(1);
            finishPicker.Value = startDatePicker.Value.AddMinutes(90);
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Такого значения быть не может!");
        }
    }
}

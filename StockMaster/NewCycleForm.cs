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
        public DataTable quotes = null;
        public DateTime start, border1, border2, finish;

        public NewCycleForm()
        {
            InitializeComponent();
        }

        private void NewCycleForm_Load(object sender, EventArgs e)
        {
            if (quotes == null)
            {
                MessageBox.Show("Нету компаний!");
                return;
            }

            dataGridView.DataSource = quotes;

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

            start = startDatePicker.Value;
            border1 = border1Picker.Value;
            border2 = border2Picker.Value;
            finish = finishPicker.Value;

            foreach (DataRow row in quotes.Rows)
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

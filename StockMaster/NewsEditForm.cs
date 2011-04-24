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

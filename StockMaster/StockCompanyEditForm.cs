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
    public partial class StockCompanyEditForm : Form
    {
        public Database.StockCompanyInfo info = null;

        public StockCompanyEditForm()
        {
            InitializeComponent();
        }

        private void StockCompanyEditForm_Load(object sender, EventArgs e)
        {
            Settings.UI.restoreForm(this);

            if (info == null)
                info = new Database.StockCompanyInfo();
            else
            {
                keyBox.Text = info.key;
                nameBox.Text = info.name;
                stockAmountBox.Text = Convert.ToString(info.stockAmount);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            info.key = keyBox.Text.Trim();
            info.name = nameBox.Text.Trim();
            try
            {
                info.stockAmount = Convert.ToUInt64(stockAmountBox.Text.Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("Херня в поле \"" + label3.Text + "\"");
                return;
            }

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

    }
}

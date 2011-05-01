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
    public partial class OwnerEditForm : Form
    {
        public String company;
        public UInt64 share = 0;
        public IList<String> otherCompanies;

        public OwnerEditForm()
        {
            InitializeComponent();
        }

        private void OwnerEditForm_Load(object sender, EventArgs e)
        {
            if (otherCompanies != null && company != null)
                otherCompanies.Add(company);

            companiesBox.Items.Clear();
            foreach (String c in otherCompanies)
            {
                int idx = companiesBox.Items.Add(c);
                if (c == company)
                    companiesBox.SelectedIndex = idx;
            }

            shareBox.Text = Convert.ToString(share);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            company = Convert.ToString(companiesBox.SelectedItem);

            try
            {
                share = Convert.ToUInt64(shareBox.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Фигня в поле " + label2.Text);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }


    }
}

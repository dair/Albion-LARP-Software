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

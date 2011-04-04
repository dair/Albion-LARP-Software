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
        public Database.PropertyInfo propertyInfo = new Database.PropertyInfo();

        public PropertyEditWindow()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            propertyInfo.name = nameBox.Text;
            propertyInfo.policeVisibility = policeVisibility.Checked;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

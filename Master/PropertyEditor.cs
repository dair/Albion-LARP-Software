using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Master
{
    public partial class PropertyEditor : UI.DBObjectUserControl
    {
        BindingSource bindingSource;

        // ----------------------------------------------------------
        public PropertyEditor()
        {
        }

        // ----------------------------------------------------------
        public PropertyEditor(Database.Connection db)
            : base(db)
        {
            InitializeComponent();

            bindingSource = new BindingSource();
            dataGridView.DataSource = bindingSource;
        }

        // ----------------------------------------------------------
        private void PropertyEditor_Load(object sender, EventArgs e)
        {
            if (getDatabase() == null)
                return;

            DataTable table = new DataTable();
            getDatabase().fillPropList(table);
            bindingSource.DataSource = table;
        }

        // ----------------------------------------------------------
        private void addButton_Click(object sender, EventArgs e)
        {
            PropertyEditWindow editWindow = new PropertyEditWindow();
            //editWindow.Closed += new EventHandler(editWindow_Closed);
            DialogResult res = editWindow.ShowDialog();
            if (res == DialogResult.OK)
            {
                saveProperty(editWindow.propertyInfo);
            }
        }

        // ----------------------------------------------------------
        private void saveProperty(Database.PropertyInfo propInfo)
        {
            getDatabase().editProperty(propInfo);
        }
    }
}

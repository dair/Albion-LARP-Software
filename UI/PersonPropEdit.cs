using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI
{
    public partial class PersonPropEdit : DBObjectForm
    {
        public Database.PersonProperty personProperty;
        public IList<Database.PropertyInfo> list = null;

        public PersonPropEdit()
        {
            InitializeComponent();
        }

        public PersonPropEdit(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        private void PersonPropEdit_Load(object sender, EventArgs e)
        {
            if (getDatabase() == null)
                return;

            if (list == null)
                list = getDatabase().getPropertyList();

            foreach (Database.PropertyInfo info in list)
            {
                propNameBox.Items.Add(info);
                if (personProperty != null &&
                    info.id == personProperty.propertyId)
                {
                    propNameBox.SelectedItem = info;
                }
            }

            if (personProperty != null)
            {
                textBox.Text = personProperty.value;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Database.PropertyInfo info = (Database.PropertyInfo)propNameBox.SelectedItem;
            if (info == null)
            {
                MessageBox.Show("Свойство надо-таки выбрать", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (personProperty == null)
                personProperty = new Database.PersonProperty();

            personProperty.propertyId = info.id;
            personProperty.propName = info.name;
            personProperty.value = textBox.Text;
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

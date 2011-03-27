using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI
{
    public partial class PersonInfo : DBObjectUserControl
    {
        private Database.FullPersonInfo fullPersonInfo = null;
        private BindingSource bindingSource = null;

        public PersonInfo()
        {
            InitializeComponent();
        }

        public PersonInfo(Database.Connection db)
            : base(db)
        {
            InitializeComponent();

            bindingSource = new BindingSource();
            propertiesGridView.DataSource = bindingSource;
        }

        public void setId(UInt16 id)
        {
            if (id == 0)
            {
                fullPersonInfo = null;
                this.Enabled = false;
            }
            else
            {
                this.Enabled = true;
                fullPersonInfo = getDatabase().getPersonInfo(id);

                this.idBox.Text = Convert.ToString(fullPersonInfo.id);
                this.nameBox.Text = fullPersonInfo.name;
                this.moneyBox.Text = Convert.ToString(fullPersonInfo.balance);

                bindingSource.DataSource = fullPersonInfo.properties;
            }
        }
    }
}

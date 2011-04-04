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

                switch (fullPersonInfo.gender)
                {
                    case Database.FullPersonInfo.Gender.Female:
                        genderBox.SelectedIndex = 2;
                        break;
                    case Database.FullPersonInfo.Gender.Male:
                        genderBox.SelectedIndex = 1;
                        break;
                    case Database.FullPersonInfo.Gender.Unknown:
                        genderBox.SelectedIndex = 0;
                        break;
                }

                switch (fullPersonInfo.genome)
                {
                    case Database.FullPersonInfo.Genome.Android:
                        genomeBox.SelectedIndex = 1;
                        break;
                    case Database.FullPersonInfo.Genome.Human:
                        genomeBox.SelectedIndex = 0;
                        break;
                }

                bindingSource.DataSource = fullPersonInfo.properties;
            }
        }
    }
}

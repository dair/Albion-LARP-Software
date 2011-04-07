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
                this.idBox.Text = "";
                this.nameBox.Text = "";
                genderBox.SelectedIndex = 0;
                genomeBox.SelectedIndex = 0;
                bindingSource.DataSource = null;
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

        public Database.FullPersonInfo getFullPersonInfo()
        {
            Database.FullPersonInfo ret = new Database.FullPersonInfo();

            ret.id = Convert.ToUInt16(idBox.Text);

            ret.name = nameBox.Text.Trim();
            switch (genderBox.SelectedIndex)
            {
                case 0:
                    ret.gender = Database.FullPersonInfo.Gender.Unknown;
                    break;
                case 1:
                    ret.gender = Database.FullPersonInfo.Gender.Male;
                    break;
                case 2:
                    ret.gender = Database.FullPersonInfo.Gender.Female;
                    break;
            }

            switch (genomeBox.SelectedIndex)
            {
                case 0:
                    ret.genome = Database.FullPersonInfo.Genome.Human;
                    break;
                case 1:
                    ret.genome = Database.FullPersonInfo.Genome.Android;
                    break;
            }

            return ret;
        }
    }
}

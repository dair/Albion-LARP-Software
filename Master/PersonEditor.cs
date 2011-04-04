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
    public partial class PersonEditor : UI.DBObjectUserControl
    {
        public PersonEditor()
        {
            InitializeComponent();
        }

        public PersonEditor(Database.Connection db)
            : base(db)
        {
            InitializeComponent();

            this.personList.SelectionChanged += new EventHandler(personList_SelectionChanged);
        }

        private void PersonEditor_Load(object sender, EventArgs e)
        {
            personList.Retrieve();
        }

        void personList_SelectionChanged(object sender, EventArgs e)
        {
            UInt16 id = personList.getId();
            personInfo.setId(id);
            moneyInfo.setId(id);
        }
    }
}

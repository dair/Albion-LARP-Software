using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InfoTerm
{
    public partial class InfoTermSearch : InfoTermObject
    {
        public InfoTermSearch()
        {
            InitializeComponent();
        }

        public InfoTermSearch(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        private void queryBox_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(sender, e);
            if (e.Modifiers == Keys.None && e.KeyCode == Keys.Enter)
            {
                if (queryBox.Text.Trim().Length > 0)
                {
                    ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
                    args.NextObject = "TABLE";
                    args.data["PERSON_INFO"] = info;
                    args.data["SEARCH_STRING"] = queryBox.Text;
                    RaiseNextObjectEvent(args);
                }
            }
        }
    }
}

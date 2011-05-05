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
    public partial class InfoTermTable : InfoTermObject
    {
        String searchString;

        public InfoTermTable()
        {
            InitializeComponent();
        }

        public InfoTermTable(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);
            searchString = Convert.ToString(args.data["SEARCH_STRING"]);
            DataTable table = new DataTable();
            getDatabase().searchInfo(searchString, table);
            baseTableView.DataSource = table;
            baseTableView.Columns["ID"].Width = 100;
            baseTableView.Columns["NAME"].Width = 200;
            baseTableView.Refresh();
        }

        private void baseTableView_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(sender, e);
            if (!e.Handled)
            {
                if (e.Modifiers == Keys.None && e.KeyCode == Keys.Enter)
                {
                    if (baseTableView.SelectedRows.Count == 1)
                    {
                        UInt64 id = Convert.ToUInt64(baseTableView.SelectedRows[0].Cells["ID"].Value);
                        if (id > 0)
                        {
                            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
                            args.NextObject = "FULLINFO";
                            args.data["PERSON_INFO"] = info;
                            args.data["SEARCH_STRING"] = searchString;
                            args.data["INFO_ABOUT"] = id;
                            RaiseNextObjectEvent(args);
                        }
                    }
                }
            }
        }
    }
}

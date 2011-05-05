using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClientUI;

namespace InfoTerm
{
    public partial class InfoTermForm : ClientUI.ClientForm
    {
        public InfoTermForm()
        {
            InitializeComponent();
        }

        public InfoTermForm(Database.Connection db, ClientSettings s, BarCode.ReaderControl r, Logger.Logging logger)
            : base(db, s, r, logger, 120000)
        {
            InitializeComponent();

            userObjects["START"] = new InfoTermStart(db);
            userObjects["PINCODE"] = new InfoTermPinCode(db);
            userObjects["SEARCH"] = new InfoTermSearch(db);
            userObjects["TABLE"] = new InfoTermTable(db);
            userObjects["FULLINFO"] = new InfoTermFullInfo(db);

            startupObjectKey = "START";
        }

        private void InfoTermForm_Load(object sender, EventArgs e)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClientUI;

namespace CashDesk
{
    public partial class CashDeskForm : ClientUI.ClientForm
    {
        public CashDeskForm()
        {
            InitializeComponent();
        }

        public CashDeskForm(Database.Connection db, ClientSettings s, BarCode.ReaderControl r, Logger.Logging logger)
            : base(db, s, r, logger, 120000)
        {
            InitializeComponent();

            userObjects["START"] = new CashDeskStart(db);
            userObjects["AMOUNT"] = new CashDeskAmount(db);
            userObjects["VERIFY"] = new CashDeskVerify(db);

            startupObjectKey = "START";
        }

        private void CashDeskForm_Load(object sender, EventArgs e)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClientUI;

namespace ATM
{
    public partial class ATMForm : ClientUI.ClientForm
    {
        public ATMForm()
        {
            InitializeComponent();
        }

        public ATMForm(Database.Connection db, ClientSettings s, BarCode.ReaderControl r, Logger.Logging logger)
            : base(db, s, r, logger, 120000)
        {
            InitializeComponent();

            userObjects["START"] = new ATMStart(db);
            userObjects["PINCODE"] = new ATMPinCode(db);
            userObjects["SELECT"] = new ATMSelect(db);
            userObjects["BALANCE"] = new ATMBalance(db);
            userObjects["TRANSFER"] = new ATMTransfer(db);
            userObjects["AMOUNT"] = new ATMAmount(db);
            userObjects["VERIFY"] = new ATMVerify(db);

            startupObjectKey = "START";
        }

        private void ATMForm_Load(object sender, EventArgs e)
        {
        }
    }
}

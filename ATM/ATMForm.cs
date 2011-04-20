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

        public ATMForm(Database.Connection db, ClientSettings s, BarCode.ReaderControl r)
            : base(db, s, r)
        {
            InitializeComponent();

            userObjects["START"] = new ATMStart(db);
            startupObjectKey = "START";
        }

        private void ATMForm_Load(object sender, EventArgs e)
        {
        }
    }
}

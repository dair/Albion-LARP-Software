using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ATM
{
    public partial class ATMBalance : ATMObject
    {
        public ATMBalance()
        {
            InitializeComponent();
        }

        public ATMBalance(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);
            decimal inDollars = (decimal)info.balance / 100;

            balanceLabel.Text = "$" + inDollars.ToString("N");
        }

        public override void OnKeyDown(object sender, KeyEventArgs e)
        {
            (ParentForm as ClientUI.ClientForm).toStart();
            base.OnKeyDown(sender, e);
        }
    }
}

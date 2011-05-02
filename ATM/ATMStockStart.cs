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
    public partial class ATMStockStart : ATMObject
    {
        public ATMStockStart()
        {
            InitializeComponent();
        }

        public ATMStockStart(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);
            stockWidget.Retrieve();
        }

        private void stockWidget_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(sender, e);
        }
    }
}

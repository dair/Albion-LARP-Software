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
    public partial class ATMStart : ATMObject
    {
        public ATMStart(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public override void OnBarCodeEvent(BarCode.BarCodeEventArgs e)
        {
            infoLabel.Text = Convert.ToString(e.Code);
        }
    }
}

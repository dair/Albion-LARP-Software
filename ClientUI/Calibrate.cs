using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientUI
{
    public partial class Calibrate : UserObject
    {
        private string startKey;

        public Calibrate(string sKey):
            base()
        {
            InitializeComponent();
            startKey = sKey;
        }

        public override void BarCodeScanned(ulong code)
        {
            (ParentForm as ClientForm).toStart();
            base.BarCodeScanned(code);
        }
    }
}

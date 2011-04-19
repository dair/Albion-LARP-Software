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
    public partial class ATMObject : ClientUI.UserObject
    {
        public ATMObject(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

    }
}

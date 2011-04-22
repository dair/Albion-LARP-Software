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
        protected Database.ATMLoginInfo info = null;

        public ATMObject()
        {
            InitializeComponent();
        }

        public ATMObject(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            if (!args.data.ContainsKey("PERSON_INFO") || !(args.data["PERSON_INFO"] is Database.ATMLoginInfo))
            {
                MessageBox.Show("Args doesn't contain PERSON_INFO", "ATMPinCode::Init ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            info = (Database.ATMLoginInfo)(args.data["PERSON_INFO"]);
        }
    }
}

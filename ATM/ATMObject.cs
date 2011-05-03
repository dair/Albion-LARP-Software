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

        public static String moneyToString(UInt64 money)
        {
            Decimal d = (Decimal)money / 100;
            return d.ToString("N");
        }

        public static UInt64 stringToMoney(String s)
        {
            Decimal d = 0;
            try
            {
                d = Convert.ToDecimal(s);
            }
            catch (Exception)
            {
            }

            return (UInt64)(d * 100);
        }

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

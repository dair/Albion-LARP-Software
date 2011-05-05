using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InfoTerm
{
    public partial class InfoTermObject : ClientUI.UserObject
    {
        protected Database.ATMLoginInfo info;

        public static String moneyToString(UInt64 money)
        {
            Decimal d = (Decimal)money / 100;
            return d.ToString("N");
        }

        public InfoTermObject()
        {
            InitializeComponent();
        }

        public InfoTermObject(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);

            if (args.data.ContainsKey("PERSON_INFO"))
            {
                info = (Database.ATMLoginInfo)args.data["PERSON_INFO"];
            }
        }
    }
}

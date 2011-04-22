using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClientUI;

namespace ATM
{
    public partial class ATMSelect : ATMObject
    {
        public ATMSelect()
        {
            InitializeComponent();
        }

        public ATMSelect(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);
        }

        private void NextObject(String code)
        {
            UserObjectEventArgs args = new UserObjectEventArgs();
            args.NextObject = code;
            args.data["PERSON_INFO"] = info;
            RaiseNextObjectEvent(args);
        }

        public override void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.None)
            {
                switch (e.KeyCode)
                {
                    case Keys.D1:
                        NextObject("BALANCE");
                        break;
                    case Keys.D2:
                        NextObject("TRANSFER");
                        break;
                    case Keys.D3:
                        NextObject("STOCK_START");
                        break;
                }
            }
            base.OnKeyDown(sender, e);
        }
    }
}

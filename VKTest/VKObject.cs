using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VKTest
{
    public partial class VKObject : ClientUI.UserObject
    {
        protected Database.FullPersonInfo info;

        public VKObject()
        {
            InitializeComponent();
        }

        public VKObject(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            if (!args.data.ContainsKey("PERSON_INFO") || !(args.data["PERSON_INFO"] is Database.FullPersonInfo))
            {
                MessageBox.Show("Args doesn't contain PERSON_INFO", "ATMPinCode::Init ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            info = (Database.FullPersonInfo)(args.data["PERSON_INFO"]);

        }

    }
}

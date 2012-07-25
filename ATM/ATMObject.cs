/* ***********************************************************************
 * (C) 2008-2011 Vladimir Lebedev-Schmidthof <vladimir@schmidthof.com>
 * Made for Albion Games (http://albiongames.org)
 * 
 * 
 *            DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
 *                    Version 2, December 2004

 * Copyright (C) 2004 Sam Hocevar <sam@hocevar.net>
 * 
 * Everyone is permitted to copy and distribute verbatim or modified
 * copies of this license document, and changing it is allowed as long
 * as the name is changed.

 *           DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
 *   TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION

 *  0. You just DO WHAT THE FUCK YOU WANT TO.
 * *********************************************************************** */

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
            Decimal d = (Decimal)money;
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

            return (UInt64)(d);
        }

        public ATMObject()
            : base()
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

﻿/* ***********************************************************************
 * (C) 2008-2012 Vladimir Lebedev-Schmidthof <vladimir@schmidthof.com>
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

namespace VKTest
{
    public partial class VKObject : ClientUI.UserObject
    {
        protected Database.FullPersonInfo info;
        protected VerticalProgressBar bar;

        public VKObject()
        {
            InitializeComponent();
        }

        public VKObject(Database.Connection db, VerticalProgressBar vBar)
            : base(db)
        {
            bar = vBar;
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

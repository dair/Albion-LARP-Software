/* ***********************************************************************
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
    public partial class VKFinish : VKObject
    {
        public VKFinish()
        {
            InitializeComponent();
        }

        public VKFinish(VerticalProgressBar vb)
            : base(null, vb)
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);
            bar.Shaking = false;
            bar.Visible = false;

            int value = Convert.ToInt16(args.data["VALUE"]);
            if (value > 0)
            {
                if (value > 50)
                    value = 50;
                progressBarA.Value = 0;
                progressBarH.Value = value * 2;
            }
            else
            {
                if (value < -50)
                    value = -50;
                progressBarA.Value = -value * 2;
                progressBarH.Value = 0;
            }

            nameLabel.Text = info.name;
        }

        public override void OnKeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(sender, e);
        }

    }
}

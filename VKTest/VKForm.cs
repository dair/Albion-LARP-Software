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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClientUI;

namespace VKTest
{
    public partial class VKForm : ClientUI.ClientForm
    {
        public VKForm()
        {
            InitializeComponent();
        }

        public VKForm(Database.Connection db, ClientSettings s, Logger.Logging logger)
            : base(db, s, null, logger, 0)
        {
            InitializeComponent();

            userObjects["START"] = new VKStart(db, verticalProgressBar);
            userObjects["QUESTION"] = new VKQuestion(db, verticalProgressBar);
            userObjects["WAITING"] = new VKWaiting(db, verticalProgressBar);
            userObjects["EXITING"] = new VKExiting();
            userObjects["FINISH"] = new VKFinish(verticalProgressBar);

            startupObjectKey = "START";
        }


        protected override void HandleNextObjectEvent(object sender, UserObjectEventArgs e)
        {
            base.HandleNextObjectEvent(sender, e);
            if (e.data.ContainsKey("VALUE"))
            {
                score.Text = Convert.ToString(e.data["VALUE"]);
            }
            else
            {
                score.Text = "нет";
            }

            if (e.data.ContainsKey("MIDDLE"))
            {
                middle.Text = Convert.ToString(e.data["MIDDLE"]);
            }
            else
            {
                middle.Text = "нет";
            }
        }
/*        public override void positionObject(UserObject obj)
        {
            //obj.Location = new Point(0, 0);
            obj.Dock = DockStyle.Fill;
        }*/
    }
}

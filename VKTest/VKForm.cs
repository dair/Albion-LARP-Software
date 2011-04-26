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
            : base(db, s, null, logger)
        {
            InitializeComponent();

            userObjects["START"] = new VKStart(db);
            userObjects["QUESTION"] = new VKQuestion(db);
            userObjects["WAITING"] = new VKWaiting(db);

            startupObjectKey = "START";
        }

/*        public override void positionObject(UserObject obj)
        {
            //obj.Location = new Point(0, 0);
            obj.Dock = DockStyle.Fill;
        }*/
    }
}

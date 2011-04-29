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

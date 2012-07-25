using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeMachine
{
    public partial class BlueScreen : TimeMachineControl
    {
        public BlueScreen()
        {
            InitializeComponent();
            isBlueScreen = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (ParentForm as TimeMachineForm).setPage("RECOVERY_CONSOLE");
        }

    }
}

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
    public partial class RecoveryConsole : TimeMachineControl
    {

        public RecoveryConsole()
        {
            InitializeComponent();
            isBlueScreen = true;
        }

        private void shellControl1_CommandEntered(object sender, UILibrary.CommandEnteredEventArgs e)
        {
            shellControl1.WriteText("MWAHAHA");
        }
    }
}

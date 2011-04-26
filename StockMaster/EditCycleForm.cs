using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockMaster
{
    public partial class EditCycleForm : Form
    {
        public EditCycleForm()
        {
            InitializeComponent();
        }

        private void syncButton_Click(object sender, EventArgs e)
        {
            DateTime start = startTime.Value;
            border1Time.Value = start.AddMinutes(10);
            border2Time.Value = start.AddHours(1);
            endTime.Value = start.AddHours(1.5);
        }
    }
}

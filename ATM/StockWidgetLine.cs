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
    public partial class StockWidgetLine : UserControl
    {
        public StockWidgetLine()
        {
            InitializeComponent();
        }

        static String upArrow = "▲";
        static String downArrow = "▼";

        public void SetData(String t, UInt64 value, Int64 diff)
        {
            ticker.Text = t;
            decimal inDollars = (decimal)value / 100;
            decimal diffInDollars = Math.Abs((decimal)diff / 100);

            String arrow = "";
            quote.ForeColor = Color.LightGray;

            if (diff > 0)
            {
                arrow = upArrow;
                quote.ForeColor = Color.Green;
            }
            else
            {
                arrow = downArrow;
                quote.ForeColor = Color.Red;
            }
            quote.Text = Convert.ToString(inDollars.ToString("N")) + arrow + diffInDollars.ToString("N");
        }

        private void StockWidgetLine_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show("StockWidgetLine_KeyDown");
            e.Handled = false;
        }
    }
}

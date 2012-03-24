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
    }
}

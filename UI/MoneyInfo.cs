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

namespace UI
{
    public partial class MoneyInfo : DBObjectUserControl
    {
        Database.MoneyInfo moneyInfo = null;

        public MoneyInfo()
        {
            InitializeComponent();
        }

        public void setId(UInt64 id)
        {
            if (getDatabase() == null)
                return;

            moneyInfo = getDatabase().getMoneyInfo(id);
            if (moneyInfo == null)
            {
                moneyBox.Text = "";
                pinCodeBox.Text = "";
                failuresBox.Text = "";
            }
            else
            {
                Decimal inDollars = moneyInfo.balance;
                //inDollars = inDollars / 100;
                moneyBox.Text = Convert.ToString(inDollars);
                pinCodeBox.Text = moneyInfo.pinCode;
                failuresBox.Text = Convert.ToString(moneyInfo.failures);
            }
        }

        public Database.MoneyInfo getMoneyInfo()
        {
            Database.MoneyInfo ret = new Database.MoneyInfo();
            if (moneyInfo != null)
                ret.id = moneyInfo.id;
            try
            {
                decimal inDollars = Convert.ToDecimal(moneyBox.Text);
                ret.balance = (UInt64)(inDollars);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Фигня в поле \"" + label1.Text + "\"\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            ret.pinCode = pinCodeBox.Text;

            try
            {
                ret.failures = Convert.ToUInt16(failuresBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Фигня в поле \"" + label3.Text + "\"\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return ret;
        }

    }
}

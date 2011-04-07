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

        public MoneyInfo(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public void setId(UInt16 id)
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
                moneyBox.Text = Convert.ToString(moneyInfo.balance);
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
                ret.balance = Convert.ToUInt32(moneyBox.Text);
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

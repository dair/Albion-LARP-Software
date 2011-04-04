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
    }
}

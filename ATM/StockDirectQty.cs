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
    public partial class StockDirectQty : ATMObject
    {
        Database.StockCompanyInfo companyInfo = null;

        public StockDirectQty()
        {
            InitializeComponent();
        }

        public StockDirectQty(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);

            if (!args.data.ContainsKey("COMPANY_INFO") || !(args.data["COMPANY_INFO"] is Database.StockCompanyInfo))
            {
                MessageBox.Show("Args doesn't contain COMPANY_INFO", "StockDirectQty::Init ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            companyInfo = (Database.StockCompanyInfo)(args.data["COMPANY_INFO"]);
            companyLabel.Text = companyInfo.key + ": " + companyInfo.name;

            qtyBox.Text = "";
            infoLabel.Text = "";
        }

        private void qtyBox_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(sender, e);

            if (!e.Handled)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    processQty();
                }
            }
        }

        void processQty()
        {
            UInt64 qty = 0;
            try
            {
                qty = Convert.ToUInt64(qtyBox.Text);
                infoLabel.Text = "";
            }
            catch (Exception)
            {
            }
            if (qty == 0)
            {
                infoLabel.Text = "Количество введено неправильно";
                infoLabel.ForeColor = Color.Red;
                return;
            }

            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "STOCK_DIRECT_PRICE";
            args.data["PERSON_INFO"] = info;
            args.data["COMPANY_INFO"] = companyInfo;
            args.data["QTY"] = qty;

            RaiseNextObjectEvent(args);
        }
    }
}

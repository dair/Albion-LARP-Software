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
    public partial class StockDirectPrice : ATMObject
    {
        Database.StockCompanyInfo companyInfo = null;
        UInt64 qty;


        public StockDirectPrice()
        {
            InitializeComponent();
        }

        public StockDirectPrice(Database.Connection db)
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

            qty = Convert.ToUInt64(args.data["QTY"]);
            qtyLabel.Text = "Количество: " + Convert.ToString(qty);

            priceBox.Text = "";
            infoLabel.Text = "";
        }

        private void priceBox_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(sender, e);

            if (!e.Handled)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    processPrice();
                }
            }
        }

        void processPrice()
        {
            UInt64 price = 0;
            try
            {
                Decimal dollarPrice = Convert.ToDecimal(priceBox.Text);
                price = (UInt64)(dollarPrice * 100);
                infoLabel.Text = "";
            }
            catch (Exception)
            {
            }
            if (price == 0)
            {
                infoLabel.Text = "Стоимость введена неправильно";
                infoLabel.ForeColor = Color.Red;
                return;
            }

            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "STOCK_DIRECT_RECIPIENT";
            args.data["PERSON_INFO"] = info;
            args.data["COMPANY_INFO"] = companyInfo;
            args.data["QTY"] = qty;
            args.data["PRICE"] = price;

            RaiseNextObjectEvent(args);
        }
    }
}

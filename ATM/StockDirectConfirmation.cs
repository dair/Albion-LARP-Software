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
    public partial class StockDirectConfirmation : ATMObject
    {
        Database.ATMLoginInfo receiverInfo;
        Database.StockCompanyInfo companyInfo;
        UInt64 qty;
        UInt64 price;

        String infoString;
        bool ready = true;

        public StockDirectConfirmation()
        {
            InitializeComponent();
        }

        public StockDirectConfirmation(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
            infoString = infoLabel.Text;
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

            qty = Convert.ToUInt64(args.data["QTY"]);
            price = Convert.ToUInt64(args.data["PRICE"]);
            receiverInfo = (Database.ATMLoginInfo)(args.data["RECEIVER_INFO"]);

            senderLabel.Text = info.name;
            receiverLabel.Text = receiverInfo.name;
            companyLabel.Text = companyInfo.key + " " + companyInfo.name;
            qtyLabel.Text = Convert.ToString(qty);
            decimal dollarPrice = (decimal)price / 100;
            priceLabel.Text = dollarPrice.ToString("N");

            infoLabel.Text = infoString;
            infoLabel.ForeColor = Color.White;

            ready = true;
        }

        public override void OnKeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(sender, e);
            if (e.Modifiers == Keys.None)
            {
                if ((int)companyInfo.key[0] == e.KeyValue && ready)
                {
                    ProcessOperation();
                }
            }
        }

        void ProcessOperation()
        {
            ready = false;
            DataTable shares = new DataTable();
            getDatabase().fillSharesByPerson(info.id, shares);

            bool enough = false;
            foreach (DataRow row in shares.Rows)
            {
                if (Convert.ToString(row["TICKER"]) == companyInfo.key)
                {
                    if (Convert.ToUInt64(row["SHARE"]) >= qty)
                    {
                        enough = true;
                    }
                }
            }

            if (!enough)
            {
                infoLabel.Text = "Недостаточно акций у продавца для проведения сделки";
                infoLabel.ForeColor = Color.Red;

                myTimer.Interval = 2000;
                myTimer.Tick += new EventHandler(myTimer_TickFail);
                myTimer.Start();
                return;
            }

            if (price > receiverInfo.balance)
            {
                infoLabel.Text = "Недостаточно денег на счету покупателя для проведения сделки";
                infoLabel.ForeColor = Color.Red;

                myTimer.Interval = 2000;
                myTimer.Tick += new EventHandler(myTimer_TickFail);
                myTimer.Start();
                return;
            }

            infoLabel.Text = "Операция успешно завершена";
            infoLabel.ForeColor = Color.Green;

            getDatabase().directStockSale(info.id, receiverInfo.id, companyInfo.key, qty, price);
            myTimer.Interval = 2000;
            myTimer.Tick += new EventHandler(myTimer_TickSuccess);
            myTimer.Start();
        }

        void myTimer_TickSuccess(object sender, EventArgs e)
        {
            myTimer.Stop();
            myTimer.Tick -= myTimer_TickSuccess;
            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "START";
            RaiseNextObjectEvent(args);
        }

        void myTimer_TickFail(object sender, EventArgs e)
        {
            myTimer.Stop();
            myTimer.Tick -= myTimer_TickFail;

            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "START";
            RaiseNextObjectEvent(args);
        }
    }
}

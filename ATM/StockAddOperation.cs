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
    public partial class StockAddOperation : ATMObject
    {
        DataTable lastCycle;
        Database.StockCompanyInfo stockInfo;
        UInt64 qty;
        UInt64 quote = 0;

        public StockAddOperation()
        {
            InitializeComponent();
        }

        public StockAddOperation(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);

            lastCycle = (DataTable)args.data["CYCLE"];
            stockInfo = (Database.StockCompanyInfo)args.data["COMPANY_INFO"];
            qty = Convert.ToUInt64(args.data["QTY"]);

            
            foreach (DataRow row in lastCycle.Rows)
            {
                String ticker = Convert.ToString(row["TICKER"]);
                if (ticker == stockInfo.key)
                {
                    quote = Convert.ToUInt64(row["QUOTE"]);
                    break;
                }
            }

            infoLabel.Text = stockInfo.key + ", " + Convert.ToString(qty) + "\rкотировка $" +
                moneyToString(quote) + "\rСумма: $" + moneyToString(quote*qty);

            resultLabel.Text = "";
        }

        public override void OnKeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(sender, e);
            if (e.Modifiers == Keys.None)
            {
                switch (e.KeyCode)
                {
                    case Keys.D1:
                        Buy();
                        break;
                    case Keys.D2:
                        Sell();
                        break;
                }
            }
        }

        void Buy()
        {
            if (quote * qty > info.balance)
            {
                resultLabel.Text = "Недостаточно средств для подачи заявки";
                resultLabel.ForeColor = Color.Red;
                Fail();

                return;
            }

            UInt64 pid = info.id;
            UInt64 cid = Convert.ToUInt64(lastCycle.Rows[0]["CYCLE_ID"]);
            String ticker = stockInfo.key;

            getDatabase().newBuyRequest(pid, cid, ticker, qty);

            resultLabel.Text = "Заявка успешно размещена";
            resultLabel.ForeColor = Color.Green;

            Success();
        }

        void Sell()
        {
            UInt64 pid = info.id;
            UInt64 cid = Convert.ToUInt64(lastCycle.Rows[0]["CYCLE_ID"]);
            String ticker = stockInfo.key;
            DataTable table = new DataTable();

            getDatabase().fillSharesByPerson(pid, table);

            UInt64 share = 0;

            foreach (DataRow row in table.Rows)
            {
                if (Convert.ToString(row["TICKER"]) == ticker.ToUpper())
                {
                    share = Convert.ToUInt64(row["SHARE"]);
                    break;
                }
            }

            if (share < qty)
            {
                resultLabel.Text = "Количество акций недостаточно для подачи заявки";
                resultLabel.ForeColor = Color.Red;
                Fail();
                return;
            }

            getDatabase().newSellRequest(pid, cid, ticker, qty);

            resultLabel.Text = "Заявка успешно размещена";
            resultLabel.ForeColor = Color.Green;

            Success();
        }

        void Fail()
        {
            myTimer.Interval = 2000;
            myTimer.Tick += new EventHandler(myTimer_TickFail);
            myTimer.Start();
        }

        void myTimer_TickFail(object sender, EventArgs e)
        {
            myTimer.Stop();
            myTimer.Tick -= myTimer_TickFail;

            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "START";

            RaiseNextObjectEvent(args);
        }

        void Success()
        {
            myTimer.Interval = 1000;
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
    }
}

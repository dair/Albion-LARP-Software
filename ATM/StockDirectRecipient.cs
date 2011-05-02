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
    public partial class StockDirectRecipient : ATMObject
    {
        Database.StockCompanyInfo companyInfo = null;
        UInt64 qty = 0;
        UInt64 price = 0;
        bool ready;
        Database.ATMLoginInfo receiverInfo = null;

        public StockDirectRecipient()
        {
            InitializeComponent();
        }

        public StockDirectRecipient(Database.Connection db)
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
            //companyLabel.Text = companyInfo.key + ": " + companyInfo.name;

            qty = Convert.ToUInt64(args.data["QTY"]);
            price = Convert.ToUInt64(args.data["PRICE"]);

            ready = true;
            infoLabel.Text = "";
            infoLabel.ForeColor = Color.White;
        }

        public override void BarCodeScanned(ulong code)
        {
            if (!ready)
                return;
            ready = false;
            receiverInfo = getDatabase().ATMLoginInfo(code);
            if (receiverInfo == null || receiverInfo.name == null)
            {
                infoLabel.Text = "Ошибка!";
                infoLabel.ForeColor = Color.Red;
                myTimer.Tick += new EventHandler(TickReject);
                myTimer.Interval = 2000;
                myTimer.Start();
            }
            else
            {
                if (info.failures > 2)
                {
                    infoLabel.Text = "Код заблокирован!";
                    infoLabel.ForeColor = Color.Red;
                    myTimer.Tick += new EventHandler(TickReject);
                    myTimer.Interval = 2000;
                    myTimer.Start();
                }
                else
                {
                    if (info.id == receiverInfo.id)
                    {
                        infoLabel.Text = "Самому себе что-то продавать смысла не имеет";
                        infoLabel.ForeColor = Color.Red;
                        myTimer.Tick += new EventHandler(TickReject);
                        myTimer.Interval = 2000;
                        myTimer.Start();
                    }
                    else
                    {
                        infoLabel.Text = receiverInfo.name;
                        infoLabel.ForeColor = Color.Green;
                        myTimer.Tick += new EventHandler(TickAccept);
                        myTimer.Interval = 2000;
                        myTimer.Start();
                    }
                }
            }
        }

        void TickReject(object sender, EventArgs e)
        {
            myTimer.Stop();
            myTimer.Tick -= TickReject;
            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "START";
            RaiseNextObjectEvent(args);
        }

        void TickAccept(object sender, EventArgs e)
        {
            myTimer.Stop();
            myTimer.Tick -= TickAccept;

            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "STOCK_DIRECT_RECEIVER_PINCODE";
            args.data["PERSON_INFO"] = info;
            args.data["COMPANY_INFO"] = companyInfo;
            args.data["QTY"] = qty;
            args.data["PRICE"] = price;
            args.data["RECEIVER_INFO"] = receiverInfo;

            RaiseNextObjectEvent(args);
        }

        void infoLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D0 ||
                e.KeyCode == Keys.D1 ||
                e.KeyCode == Keys.D2 ||
                e.KeyCode == Keys.D3 ||
                e.KeyCode == Keys.D4 ||
                e.KeyCode == Keys.D5 ||
                e.KeyCode == Keys.D6 ||
                e.KeyCode == Keys.D7 ||
                e.KeyCode == Keys.D8 ||
                e.KeyCode == Keys.D9)
            {
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                UInt64 code = 0;
                try
                {
                    code = Convert.ToUInt64(infoLabel.Text);
                    BarCodeScanned(code);
                }
                catch (Exception)
                {
                    MessageBox.Show("Херня какая-то");
                }
            }
            if (e.Handled == false)
            {
                OnKeyDown(sender, e);
            }
        }


    }
}

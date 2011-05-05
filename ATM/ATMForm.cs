using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClientUI;

namespace ATM
{
    public partial class ATMForm : ClientUI.ClientForm
    {
        public ATMForm()
        {
            InitializeComponent();
        }

        public ATMForm(Database.Connection db, ClientSettings s, BarCode.ReaderControl r, Logger.Logging logger)
            : base(db, s, r, logger, 120000)
        {
            InitializeComponent();

            userObjects["START"] = new ATMStart(db);
            userObjects["PINCODE"] = new ATMPinCode(db);
            userObjects["SELECT"] = new ATMSelect(db);
            userObjects["BALANCE"] = new ATMBalance(db);
            userObjects["TRANSFER"] = new ATMTransfer(db);
            userObjects["AMOUNT"] = new ATMAmount(db);
            userObjects["VERIFY"] = new ATMVerify(db);
            userObjects["STOCK_START"] = new ATMStockStart(db);

            userObjects["STOCK_DIRECT"] = new StockDirectShare(db);
            userObjects["STOCK_DIRECT_QTY"] = new StockDirectQty(db);
            userObjects["STOCK_DIRECT_PRICE"] = new StockDirectPrice(db);
            userObjects["STOCK_DIRECT_RECIPIENT"] = new StockDirectRecipient(db);
            userObjects["STOCK_DIRECT_RECEIVER_PINCODE"] = new StockDirectPinCode(db);
            userObjects["STOCK_DIRECT_CONFIRM"] = new StockDirectConfirmation(db);

            userObjects["NEWS"] = new StockNews(db);

            userObjects["STOCK_REQUESTS"] = new StockRequests(db);
            userObjects["STOCK_ADD_REQUEST_TICKER"] = new StockAddRequestTicker(db);
            userObjects["STOCK_ADD_QTY"] = new StockAddQty(db);
            userObjects["STOCK_ADD_OPERATION"] = new StockAddOperation(db);

            userObjects["STOCK_DELETE_REQUEST_CONFURMATION"] = new StockDeleteRequestConfirmation(db);
            startupObjectKey = "START";
        }

        private void ATMForm_Load(object sender, EventArgs e)
        {
            CycleInfoBar bar = new CycleInfoBar(getDatabase());
            bar.Location = new Point(0, 0);
            bar.Width = Width;
            bar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(bar);
            bar.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CashDesk
{
    public partial class CashDeskStart : CashDeskObject
    {
        public CashDeskStart()
        {
            InitializeComponent();
        }

        public CashDeskStart(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            //base.Init(args);
            infoLabel.Text = "";
        }

        public override void BarCodeScanned(ulong code)
        {
            base.BarCodeScanned(code);

            codeBox.Text = code.ToString();

            info = getDatabase().ATMLoginInfo(code);
            if (info == null || info.name == null)
            {
                infoLabel.Text = "Ошибка!";
                infoLabel.ForeColor = Color.Red;
                myTimer.Tick += new EventHandler(TickReject);
                myTimer.Interval = 2000;
                myTimer.Start();
            }
            else
            {
                infoLabel.Text = "Удачно";
                infoLabel.ForeColor = Color.Green;
                myTimer.Tick += new EventHandler(TickAccept);
                myTimer.Interval = 2000;
                myTimer.Start();
            }
        }

        void TickReject(object sender, EventArgs e)
        {
            myTimer.Stop();
            myTimer.Tick -= TickReject;
            Init(null);
        }

        void TickAccept(object sender, EventArgs e)
        {
            myTimer.Stop();
            myTimer.Tick -= TickAccept;

            ClientUI.UserObjectEventArgs args = new ClientUI.UserObjectEventArgs();
            args.NextObject = "AMOUNT";
            args.data["PERSON_INFO"] = info;

            RaiseNextObjectEvent(args);
        }

        private void codeBox_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(sender, e);
        }

    }
}

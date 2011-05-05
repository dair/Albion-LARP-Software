using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ClientUI;

namespace ATM
{
    public partial class ATMStart : ATMObject
    {
        private bool ready = false;

        public ATMStart(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
            escLabel.Hide();
            infoLabel.KeyDown += new KeyEventHandler(infoLabel_KeyDown);
        }


        override public void Init(ClientUI.UserObjectEventArgs e)
        {
            infoLabel.Text = "";
            infoLabel.ForeColor = Color.White;
            info = null;
            ready = true;
        }

        override public void Deinit()
        {
        }

        public override void BarCodeScanned(ulong code)
        {
            if (!ready)
                return;
            ready = false;
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
                    infoLabel.Text = info.name;
                    infoLabel.ForeColor = Color.Green;
                    myTimer.Tick += new EventHandler(TickAccept);
                    myTimer.Interval = 2000;
                    myTimer.Start();
                }
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

            UserObjectEventArgs args = new UserObjectEventArgs();
            args.NextObject = "PINCODE";
            args.data["PERSON_INFO"] = info;

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
                    MessageBox.Show("Неправильно введённый код");
                }
            }
            if (e.Handled == false)
            {
                OnKeyDown(sender, e);
            }
        }
    }
}

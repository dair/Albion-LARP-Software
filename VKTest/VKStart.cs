using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ClientUI;

namespace VKTest
{
    public partial class VKStart : VKObject
    {
        UInt64 sessionId = 0;

        //-------------------------------------------------
        public VKStart()
            : base()
        {
            InitializeComponent();
        }

        //-------------------------------------------------
        public VKStart(Database.Connection db, VerticalProgressBar vBar)
            : base(db, vBar)
        {
            InitializeComponent();

            //insta
        }

        //-------------------------------------------------
        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            infoLabel.Text = "";
            bar.Visible = false;
        }

        //-------------------------------------------------
        private void okButton_Click(object sender, EventArgs e)
        {
            ProcessId();
        }

        //-------------------------------------------------
        void ProcessId()
        {
            infoLabel.Text = "";
            UInt64 id = 0;
            try
            {
                id = Convert.ToUInt64(idBox.Text.Trim());
            }
            catch (Exception)
            {
                infoLabel.Text = "Неправильно введённый ID";
                infoLabel.ForeColor = Color.Red;
                return;
            }

            info = getDatabase().getPersonInfo(id);
            if (info == null || info.id == 0)
            {
                if (getDatabase().getLastException() != null)
                {
                    infoLabel.Text = "Ошибка соединения с БД";
                    infoLabel.ForeColor = Color.Red;
                    return;
                }
                else
                {
                    infoLabel.Text = "Пользователя с ID " + idBox.Text + " нет в БД";
                    infoLabel.ForeColor = Color.Red;
                    return;
                }
            }
            else
            {
                infoLabel.Text = info.name;
                infoLabel.ForeColor = Color.Green;
            }

            sessionId = getDatabase().CreateVKSession(id, Settings.Database.GetDeviceId());

            if (sessionId > 0)
            {
                myTimer.Tick += new EventHandler(TickAccept);
                myTimer.Interval = 500;
                myTimer.Start();
            }
            else
            {
                infoLabel.Text = "Ошибка создания сессии";
                infoLabel.ForeColor = Color.Red;
            }
        }

        private void TickAccept(object sender, EventArgs e)
        {
            myTimer.Tick -= TickAccept;
            myTimer.Stop();

            UserObjectEventArgs args = new UserObjectEventArgs();
            args.NextObject = "QUESTION";
            args.data["PERSON_INFO"] = info;
            args.data["SESSION_ID"] = sessionId;
            args.data["VALUE"] = 0;
            args.data["MIDDLE"] = 0.0;

            RaiseNextObjectEvent(args);
        }

        private void idBox_KeyDown(object sender, KeyEventArgs e)
        {
            infoLabel.Text = "";

            bool h = false;
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    ProcessId();
                    h = true;
                    break;
                case Keys.Escape:
                    idBox.Text = "";
                    h = true;
                    break;
                case Keys.O:
                    if (e.Modifiers == Keys.Control)
                    {
                        this.OnKeyDown(sender, e);
                        h = true;
                    }
                    break;
            }

            e.Handled = h;
        }

    }
}

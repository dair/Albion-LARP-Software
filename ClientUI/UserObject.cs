﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ClientUI
{
    public partial class UserObject : UI.DBObjectUserControl
    {
        protected Timer myTimer = new Timer();

        public UserObject()
        {
            InitializeComponent();
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
        }

        public UserObject(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
        }

        public event EventHandler<UserObjectEventArgs> NextObjectEvent;

        protected void RaiseNextObjectEvent(UserObjectEventArgs args)
        {
            EventHandler<UserObjectEventArgs> handler = NextObjectEvent;
            if(handler != null)
                handler(this, args);
        }


        private void UserObject_Load(object sender, EventArgs e)
        {
            
        }

        public virtual void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            MessageBox.Show("UserObject::OnKeyDown");

            (ParentForm as ClientForm).RecordActivity();
            if (e.KeyCode == Keys.O && e.Modifiers == Keys.Control)
            {
                e.Handled = true;
                if (ParentForm is ClientForm)
                {
                    (ParentForm as ClientForm).showSettings();
                }
            }
            else if (e.KeyCode == Keys.L && (e.Modifiers == (Keys.Control | Keys.Shift)))
            {
                e.Handled = true;
                if (ParentForm is ClientForm)
                {
                    (ParentForm as ClientForm).toggleLog();
                }
            }
            else if (e.KeyCode == Keys.Escape && e.Modifiers == Keys.None)
            {
                e.Handled = true;
                if (ParentForm is ClientForm)
                {
                    (ParentForm as ClientForm).toStart();
                }
            }
        }

        // Common methods
        public virtual void Init(UserObjectEventArgs args)
        {
        }

        public virtual void Deinit()
        {
        }

        public virtual void BarCodeScanned(UInt64 code)
        {
            (ParentForm as ClientForm).RecordActivity();
        }
    }

    public class UserObjectEventArgs : EventArgs
    {
        public string NextObject;
        public Dictionary<string, object> data;

        public UserObjectEventArgs()
        {
            data = new Dictionary<string, object>();
        }
    }

}

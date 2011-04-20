using System;
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
            if (e.KeyCode == Keys.O && e.Modifiers == Keys.Control)
            {
                e.Handled = true;
                if (ParentForm is ClientForm)
                {
                    (ParentForm as ClientForm).showSettings();
                }
            }
        }

        public virtual void OnBarCodeEvent(BarCode.BarCodeEventArgs e)
        {
        }

        // Common methods
        public virtual void Init( UserObjectEventArgs args )
        {
            MessageBox.Show("UserObject::Init");
        }

        public virtual void Deinit()
        {
            MessageBox.Show("UserObject::Deinit");
        }

        public virtual void BarCodeScanned(UInt64 code)
        {
            MessageBox.Show("UserObject::BarCodeScanned");
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

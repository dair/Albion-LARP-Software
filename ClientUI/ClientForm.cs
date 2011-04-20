using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientUI
{
    public partial class ClientForm : UI.DBObjectForm
    {
        protected IDictionary<String, UserObject> userObjects = null;
        protected String startupObjectKey = null;
        protected String currentObjectKey = null;
        protected ClientSettings settings = null;
        protected BarCode.ReaderControl RC = null;



        public ClientForm()
        {
            InitializeComponent();
        }

        public ClientForm(Database.Connection db, ClientSettings s, BarCode.ReaderControl r)
            : base(db)
        {
            userObjects = new Dictionary<String, UserObject>();
            settings = s;
            RC = r;
            InitializeComponent();
            if (RC != null)
            {
                RC.BarCodeObject.BarCodeEvent += new EventHandler<BarCode.BarCodeEventArgs>(HandleBarCodeEvent);
            }
        }

        void HandleBarCodeEvent(object sender, BarCode.BarCodeEventArgs e)
        {
            if (currentObjectKey != null &&
                userObjects[currentObjectKey] != null)
            {
                userObjects[currentObjectKey].OnBarCodeEvent(e);
            }
        }

        public void showSettings()
        {
            if (settings.ShowDialog() == DialogResult.OK)
            {
                if (RC != null)
                    RC.Reload();
            }
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            if (startupObjectKey == null || userObjects == null ||
                !userObjects.ContainsKey(startupObjectKey))
                return;

            foreach (String key in userObjects.Keys)
            {
                UserObject obj = userObjects[key];
                this.Controls.Add(obj);

                obj.Location = new Point((Size.Width - obj.Size.Width) / 2,
                                         (Size.Height - obj.Size.Height) / 2);
                if (key.Equals(startupObjectKey))
                {
                    currentObjectKey = startupObjectKey;
                    obj.Show();
                }
                else
                {
                    obj.Hide();
                }

                if (RC != null)
                {
                    RC.Reload();
                }
            }
        }
    }
}

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

        public ClientForm()
        {
            InitializeComponent();
        }

        public ClientForm(Database.Connection db)
            : base(db)
        {
            userObjects = new Dictionary<String, UserObject>();
            InitializeComponent();
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
                    obj.Show();
                }
                else
                {
                    obj.Hide();
                }
            }
        }
    }
}

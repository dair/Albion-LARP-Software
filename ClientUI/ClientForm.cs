/* ***********************************************************************
 * (C) 2008-2012 Vladimir Lebedev-Schmidthof <vladimir@schmidthof.com>
 * Made for Albion Games (http://albiongames.org)
 * 
 * 
 *            DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
 *                    Version 2, December 2004

 * Copyright (C) 2004 Sam Hocevar <sam@hocevar.net>
 * 
 * Everyone is permitted to copy and distribute verbatim or modified
 * copies of this license document, and changing it is allowed as long
 * as the name is changed.

 *           DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
 *   TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION

 *  0. You just DO WHAT THE FUCK YOU WANT TO.
 * *********************************************************************** */

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
        protected BarCode.HIDScanner HID = null;

        protected Logger.Logging logger = null;
        protected Timer inactivityTimer = null;

        protected Calibrate calibrateObject = null;

        public ClientForm()
        {
            InitializeComponent();
        }

        public ClientForm(Database.Connection db, ClientSettings s, BarCode.ReaderControl r, Logger.Logging l, int inactTime)
            : base(db)
        {
            userObjects = new Dictionary<String, UserObject>();
            settings = s;
            RC = r;
            logger = l;
            if (inactTime > 0)
            {
                inactivityTimer = new Timer();
                inactivityTimer.Tick += new EventHandler(inactivityTimer_Tick);
                inactivityTimer.Interval = inactTime;
            }

            InitializeComponent();
            if (RC != null)
            {
                RC.BarCodeObject.BarCodeEvent += new EventHandler<BarCode.BarCodeEventArgs>(HandleBarCodeEvent);
            }
        }

        void inactivityTimer_Tick(object sender, EventArgs e)
        {
            toStart();
        }

        void HandleBarCodeEvent(object sender, BarCode.BarCodeEventArgs e)
        {
            if (currentObjectKey != null &&
                userObjects[currentObjectKey] != null)
            {
                UserObject uo = userObjects[currentObjectKey];
                if (uo.InvokeRequired)
                {
                    uo.Invoke((MethodInvoker)delegate
                    {
                        uo.BarCodeScanned(e.Code);
                    });
                }
                else
                {
                    uo.BarCodeScanned(e.Code);
                }
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

        public void toggleLog()
        {
            if (logger != null && !logger.Visible)
            {
                logger.Show();
            }
        }

        virtual public void positionObject(UserObject obj)
        {
            obj.Location = new Point((Size.Width - obj.Size.Width) / 2,
                                     (Size.Height - obj.Size.Height) / 2);
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            if (startupObjectKey == null || userObjects == null ||
                !userObjects.ContainsKey(startupObjectKey))
                return;

            string startKey;

            if (RC != null)
            {
                RC.Reload();
                startKey = startupObjectKey;
                RC.BarCodeObject.BarCodeEvent += new EventHandler<BarCode.BarCodeEventArgs>(HandleBarCodeEvent);
            }
            else
            {
                HID = BarCode.HIDScanner.getHIDScanner();
                calibrateObject = new Calibrate(startupObjectKey);
                userObjects["BASE_CALIBRATE"] = calibrateObject;
                startKey = "BASE_CALIBRATE";
                HID.BarCodeEvent += new EventHandler<BarCode.BarCodeEventArgs>(HandleBarCodeEvent);
            }

            foreach (String key in userObjects.Keys)
            {
                UserObject obj = userObjects[key];
                this.Controls.Add(obj);

                positionObject(obj);

                obj.Hide();

            }

            if (RC != null)
            {
                RC.Reload();
            }

            SetCurrentKey(startKey, null);
        }

        void SetCurrentKey(String newKey, UserObjectEventArgs e)
        {
            if (currentObjectKey != null && userObjects[currentObjectKey] != null)
            {
                userObjects[currentObjectKey].NextObjectEvent -= new EventHandler<UserObjectEventArgs>(HandleNextObjectEvent);
                userObjects[currentObjectKey].Deinit();
                userObjects[currentObjectKey].Hide();
            }
            currentObjectKey = newKey;
            if (currentObjectKey != null && userObjects[currentObjectKey] != null)
            {
                userObjects[currentObjectKey].Init(e);
                userObjects[currentObjectKey].Show();
                userObjects[currentObjectKey].NextObjectEvent += new EventHandler<UserObjectEventArgs>(HandleNextObjectEvent);
                userObjects[currentObjectKey].Focus();

                RecordActivity();
            }
        }

        virtual protected void HandleNextObjectEvent(object sender, UserObjectEventArgs e)
        {
            SetCurrentKey(e.NextObject, e);
        }

        public void toStart()
        {
            SetCurrentKey(startupObjectKey, null);
        }

        public void RecordActivity()
        {
            if (inactivityTimer != null)
            {
                inactivityTimer.Stop();
                if (currentObjectKey != startupObjectKey)
                {
                    inactivityTimer.Start();
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            bool processed = false;
            if (HID != null)
            {
                processed = HID.ProcessMessage(m);
            }

            if (!processed)
            {
                base.WndProc(ref m);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Interop;
using System.Windows.Forms;

namespace BarCode
{
    public class HIDScanner
    {
        private static HIDScanner _sharedHIDScanner = null;
        private string scannerDeviceName = null;
        private string buffer = "";
        private DateTime bufferStart = DateTime.MinValue;
        private DateTime bufferLast = DateTime.MinValue;
        private string bufferDeviceName = null;
        private bool bufferReady = false;
        private KeysConverter cvt = new KeysConverter();
        private bool processed;

        public event EventHandler<BarCodeEventArgs> BarCodeEvent;

        public static HIDScanner getHIDScanner()
        {
            if (_sharedHIDScanner == null)
            {
                _sharedHIDScanner = new HIDScanner();
            }

            return _sharedHIDScanner;
        }

        RawStuff.InputDevice inputDevice = null;
        int NumberOfKeyboards;


        public bool ProcessMessage(Message m)
        {
            processed = false;
            inputDevice.ProcessMessage(m);

            return processed;
        }

        void initInputDevice()
        {
            System.Windows.Forms.Form mainForm = null;
            foreach (System.Windows.Forms.Form f in System.Windows.Forms.Application.OpenForms)
            {
                if (f.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                {
                    mainForm = f;
                    break;
                }
            }

            if (mainForm == null)
                return;

            IntPtr hwnd = mainForm.Handle;

            inputDevice = new RawStuff.InputDevice(hwnd);
            NumberOfKeyboards = inputDevice.EnumerateDevices();
            inputDevice.KeyPressed += new RawStuff.InputDevice.DeviceEventHandler(_KeyPressed);
        }

        private void addToBuffer(RawStuff.InputDevice.KeyControlEventArgs e)
        {
            string s = cvt.ConvertToString(e.Keyboard.key);
            Keys k = (Keys)cvt.ConvertFromString(s);

            if (e.Keyboard.deviceName != bufferDeviceName)
            {
                bufferDeviceName = e.Keyboard.deviceName;
                buffer = "";
            }

            switch (k)
            {
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                    s = k.ToString().Substring(1);
                    if (buffer.Length == 0)
                    {
                        bufferStart = DateTime.Now;
                    }
                    buffer += s;
                    bufferLast = DateTime.Now;
                    break;
                case Keys.Enter:
                    bufferLast = DateTime.Now;
                    TimeSpan ts = bufferLast.Subtract(bufferStart);

                    if (buffer.Length > 4 &&
                        ts.TotalSeconds < 1)
                    {
                        // hurray!
                        bufferReady = true;
                    }
                    break;
                default:
                    buffer = "";
                    bufferStart = bufferLast = DateTime.MinValue;
                    break;
            }
        }

        private string getBuffer()
        {
            string ret = (string)buffer.Clone();
            buffer = "";
            bufferStart = bufferLast = DateTime.MinValue;
            bufferReady = false;

            return ret;
        }

        private void _KeyPressed(object sender, RawStuff.InputDevice.KeyControlEventArgs e)
        {
            if (scannerDeviceName == null)
            {
                addToBuffer(e);
                if (bufferReady)
                {
                    scannerDeviceName = bufferDeviceName;
                    callEvent(getBuffer());
                }
                processed = true;
            }
            else
            {
                if (e.Keyboard.deviceName == scannerDeviceName)
                {
                    processed = true;
                    addToBuffer(e);
                    if (bufferReady)
                    {
                        callEvent(getBuffer());
                    }
                }
            }
        }

        private void callEvent(string s)
        {
            EventHandler<BarCodeEventArgs> handler = BarCodeEvent;
            if (handler != null)
            {
                handler(this, new BarCodeEventArgs(BarCode.StringCodeToInt(s)));
            }
        }

        public HIDScanner()
        {
            if (inputDevice == null)
            {
                initInputDevice();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using Settings;

namespace BarCode
{
    public class BarCodeEventArgs : EventArgs
    {
        public BarCodeEventArgs(UInt64 c)
        {
            code = c;
        }
        private UInt64 code;

        public UInt64 Code
        {
            get { return code; }
            set { code = value; }
        }
    }


    public class BarCode
    {
        ~BarCode()
        {
//            Deinit();
        }

        public bool Continue = false;
        public bool Ready = false;

        private SerialPort portObject;
        private string buffer = "";
        private string lastCode = "";

        public event EventHandler<BarCodeEventArgs> BarCodeEvent;

        static private UInt64 StringCodeToInt(string s)
        {
            UInt64 ret = 0;
            try
            {
                ret = Convert.ToUInt64(s);
            }
            catch (Exception)
            {
            }

            //string s1 = s.Substring(0, s.Length - 1);
            return ret;
        }

        protected virtual void OnRaiseBarCodeEvent()
        {
//            Program.Log("Code scanned: " + lastCode);
            EventHandler<BarCodeEventArgs> handler = BarCodeEvent;
            if (handler != null)
            {
                handler(this, new BarCodeEventArgs(StringCodeToInt(lastCode)));
            }
        }

        private void ProcessBuffer()
        {
            int dotPos = buffer.IndexOf('.');
            if (dotPos < 0) return;
            lastCode = buffer.Substring(0, dotPos);
            buffer = buffer.Substring(dotPos+1);
//            MessageBox.Show(lastCode, "Read");
            OnRaiseBarCodeEvent();
        }

        private void Read()
        {
            if (!Ready) return;

            int ch;
            try
            {
                ch = portObject.ReadChar();
            }
            catch (TimeoutException)
            {
                return;
            }
//            if (data.Length > 0)
            buffer += Convert.ToChar(ch);
            ProcessBuffer();
        }

        public void Start()
        {
            Init();
            if (!Ready) return;

            try
            {
                while (true)
                {
                    if (Continue)
                    {
                        lock (this)
                        {
                            if (Continue)
                                Read();
                        }
                    }
                    else
                    {
                        break;
                    }
                }

            }
            catch (ThreadAbortException)
            {
            }

            Deinit();
        }


        public void Init()
        {
//            Program.Log("BarCode::Init()");
            Ready = false;

            portObject = new SerialPort(
                Settings.BarCode.Name(),
                Settings.BarCode.BaudRate(),
                Settings.BarCode.Parity(),
                Settings.BarCode.DataBits(),
                Settings.BarCode.StopBits());
            portObject.ReadTimeout = 300;

            try
            {
                //Program.Log("BarCode::Init(): open");
                portObject.Open();
            }
            catch (IOException)
            {
                MessageBox.Show("Cannot open " + Settings.BarCode.Name(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Ready = true;
            Continue = true;
        }

        public void Deinit()
        {
            if (portObject.IsOpen)
            {
                portObject.ReadExisting();
//                Program.Log("BarCode::Deinit(): Close");
                portObject.Close();
            }
            Ready = false;
        }
    }
}

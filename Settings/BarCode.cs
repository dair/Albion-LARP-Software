using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Settings
{
    public class BarCode: Settings
    {
        private static String SUBKEY_BARCODE = "barcode";

        public static bool HasBarCodeSettings()
        {
            return HasSettings(SUBKEY_BARCODE);
        }

        public static string Name()
        {
            return GetData(SUBKEY_BARCODE, "COMName", "COM5");
        }

        public static void SetName(string n)
        {
            SetData(SUBKEY_BARCODE, "COMName", n);
        }

        public static Int32 BaudRate()
        {
            return Convert.ToInt32(GetData(SUBKEY_BARCODE, "COMBaud", "9600"));
        }

        public static void SetBaudRate(Int32 r)
        {
            SetData( SUBKEY_BARCODE, "COMBaud", Convert.ToString(r));
        }

        public static Parity Parity()
        {
            UInt16 n = Convert.ToUInt16(GetData(SUBKEY_BARCODE, "COMParity", "0"));
            Parity p;
            switch (n)
            {
                case 0:
                    p = System.IO.Ports.Parity.None;
                    break;
                case 1:
                    p = System.IO.Ports.Parity.Even;
                    break;
                case 2:
                    p = System.IO.Ports.Parity.Odd;
                    break;
                default:
                    p = System.IO.Ports.Parity.None;
                    break;
            }
            return p;
        }

        public static void SetParity(Parity p)
        {
            UInt16 n;
            switch (p)
            {
                case System.IO.Ports.Parity.None:
                    n = 0;
                    break;
                case System.IO.Ports.Parity.Even:
                    n = 1;
                    break;
                case System.IO.Ports.Parity.Odd:
                    n = 2;
                    break;
                default:
                    n = 0;
                    break;
            }
            SetData(SUBKEY_BARCODE, "COMParity", Convert.ToString(n));
        }

        public static UInt16 DataBits()
        {
            UInt16 n = Convert.ToUInt16(GetData(SUBKEY_BARCODE, "COMDataBits", "8"));
            if (n > 9) n = 9;
            if (n < 7) n = 7;
            return n;
        }

        public static void SetDataBits(UInt16 nn)
        {
            UInt16 n = nn;
            if (n > 9) n = 9;
            if (n < 7) n = 7;

            SetData(SUBKEY_BARCODE, "COMDataBits", Convert.ToString(n));
        }

        public static StopBits StopBits()
        {
            UInt16 n = Convert.ToUInt16(GetData(SUBKEY_BARCODE, "COMStopBits", "1"));
            StopBits b;
            switch (n)
            {
                case 0:
                    b = System.IO.Ports.StopBits.None;
                    break;
                case 1:
                    b = System.IO.Ports.StopBits.One;
                    break;
                case 2:
                    b = System.IO.Ports.StopBits.Two;
                    break;
                default:
                    b = System.IO.Ports.StopBits.None;
                    break;
            }
            return b;

        }

        public static void SetStopBits(StopBits b)
        {
            UInt16 n = 0;
            switch (b)
            {
                case System.IO.Ports.StopBits.None:
                    n = 0;
                    break;
                case System.IO.Ports.StopBits.One:
                    n = 1;
                    break;
                case System.IO.Ports.StopBits.Two:
                    n = 2;
                    break;
            }

            SetData(SUBKEY_BARCODE, "COMStopBits", Convert.ToString(n));
        }
    }
}

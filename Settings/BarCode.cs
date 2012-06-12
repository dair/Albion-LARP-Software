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

        public static bool NewType()
        {
            UInt64 d;
            try
            {
                d = Convert.ToUInt64(GetData(SUBKEY_BARCODE, "COMName", "COM5").Trim());
            }
            catch (Exception)
            {
                d = 1;
            }

            return d == 1;
        }

        public static void SetNewType(bool t)
        {
            UInt64 d;
            if (t)
                d = 1;
            else
                d = 0;

            SetData(SUBKEY_BARCODE, "NEW", Convert.ToString(d));
        }

        public static string Name()
        {
            String str = GetData(SUBKEY_BARCODE, "COMName", "COM5").Trim();
            if (str == "")
                return "COM5";
            else
                return str;
        }

        public static void SetName(string n)
        {
            SetData(SUBKEY_BARCODE, "COMName", n);
        }

        public static Int32 BaudRate()
        {
            String str = GetData(SUBKEY_BARCODE, "COMBaud", "9600");
            Int32 ret = 9600;
            try
            {
                ret = Convert.ToInt32(str);
            }
            catch (Exception)
            {
                ret = 9600;
            }

            return ret;
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
            String str = GetData(SUBKEY_BARCODE, "COMDataBits", "8");
            UInt16 ret = 8;
            try
            {
                ret = Convert.ToUInt16(str);
            }
            catch (Exception)
            {
                ret = 8;
            }
            
            if (ret > 9) ret = 9;
            if (ret < 7) ret = 7;
            return ret;
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
            String str = GetData(SUBKEY_BARCODE, "COMStopBits", "1");

            UInt16 n = 1;
            try
            {
                n = Convert.ToUInt16(str);
            }
            catch (Exception)
            {
            }

            StopBits b;
            switch (n)
            {
                case 2:
                    b = System.IO.Ports.StopBits.Two;
                    break;
                case 1:
                default:
                    b = System.IO.Ports.StopBits.One;
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

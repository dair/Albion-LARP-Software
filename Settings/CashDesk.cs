using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Settings
{
    public class CashDesk: Settings
    {
        private static String SUBKEY_DATABASE = "CashDesk";
        public static UInt64 GetPersonId()
        {
            return Convert.ToUInt64(GetData(SUBKEY_DATABASE, "code", "0"));
        }

        public static void SetPersonId(UInt64 code)
        {
            SetData(SUBKEY_DATABASE, "code", code.ToString());
        }
    }
}

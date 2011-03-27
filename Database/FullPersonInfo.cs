using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Database
{
    public class FullPersonInfo: PersonInfo
    {
        public UInt32 balance;
        public DataTable properties;

        public FullPersonInfo()
            : base()
        {
            properties = new DataTable();
        }

        public bool Equals(FullPersonInfo p)
        {
            bool ret = base.Equals(p);
            if (!ret)
                return ret;

            if (balance != p.balance)
                return false;

            if (!properties.Equals(p.properties))
                return false;

            return true;
        }
    }
}

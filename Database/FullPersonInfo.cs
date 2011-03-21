using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class FullPersonInfo: PersonInfo
    {
        public UInt32 balance;
        public IDictionary<String, String> properties;

        public FullPersonInfo()
            : base()
        {
            properties = new Dictionary<String, String>();
        }

        public bool Equals(FullPersonInfo p)
        {
            bool ret = base.Equals(p);
            if (!ret)
                return ret;

            if (balance != p.balance)
                return false;

            if (properties.Count != p.properties.Count)
                return false;

            foreach (String k in properties.Keys)
            {
                if (!p.properties.ContainsKey(k))
                    return false;
                if (!properties[k].Equals(p.properties[k]))
                    return false;
            }

            return true;
        }
    }
}

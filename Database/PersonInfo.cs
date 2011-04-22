using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class PersonInfo: IPerson
    {
        public UInt64 id;
        public String name;

        public PersonInfo()
        {
            id = 0;
            name = null;
        }

        public PersonInfo(UInt64 i, String n)
        {
            id = i;
            name = n;
        }

        public UInt64 getId()
        {
            return id;
        }

        public String getName()
        {
            return name;
        }

        public bool Equals(IPerson p)
        {
            return id == p.getId() &&
                name.Equals(p.getName());
        }
    }
}

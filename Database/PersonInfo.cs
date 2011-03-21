using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class PersonInfo: IPerson
    {
        public UInt16 id;
        public String name;

        public PersonInfo()
        {
            id = 0;
            name = null;
        }

        public PersonInfo(UInt16 i, String n)
        {
            id = i;
            name = n;
        }

        public UInt16 getId()
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

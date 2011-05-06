using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database
{
    public class PropertyInfo
    {
        public UInt64 id = 0;
        public String name = null;
        public bool policeVisibility = false;

        override public String ToString()
        {
            String police = "";
            if (policeVisibility)
                police = " (видно полиции)";
            return name + police;
        }
    }
}

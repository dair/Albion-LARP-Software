using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public interface IPerson
    {
        UInt64 getId();
        String getName();

        bool Equals(IPerson p);
    }
}

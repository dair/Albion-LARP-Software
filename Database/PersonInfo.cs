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

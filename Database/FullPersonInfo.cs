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
using System.Data;

namespace Database
{
    public class FullPersonInfo: PersonInfo
    {
        public enum Gender
        {
            Unknown,
            Male,
            Female
        };
        public Gender gender;

        public enum Genome
        {
            Human,
            Android
        };
        public Genome genome;

        public DataTable properties;

        public FullPersonInfo()
            : base()
        {
            properties = new DataTable();
            properties.Columns.Add("ID");
            properties.Columns.Add("Название");
            properties.Columns.Add("Значение");
        }

        public bool Equals(FullPersonInfo p)
        {
            bool ret = base.Equals(p);
            if (!ret)
                return ret;

            if (gender != p.gender)
                return false;

            if (genome != p.genome)
                return false;

            if (!properties.Equals(p.properties))
                return false;

            return true;
        }
    }
}

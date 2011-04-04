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

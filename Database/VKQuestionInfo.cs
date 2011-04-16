using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database
{
    public class VKQuestionInfo
    {
        public enum Gender
        {
            All,
            Male,
            Female
        };

        public UInt16 id;
        public String text;
        public Gender gender;
    }
}

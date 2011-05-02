using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Database
{
    public class StockCycleInfo
    {
        public DateTime start;
        public DateTime border1;
        public DateTime border2;
        public DateTime finish;

        public DataTable quotes;
    }
}

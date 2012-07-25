using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TimeMachine
{
    class TimeMachineMath
    {
        private Database.Connection db = null;

        public void setDatabase(Database.Connection d)
        {
            db = d;
        }

        public Double startCost(UInt64 launchId)
        {
            DataTable table = new DataTable();
            db.fillWithLaunch(table, launchId);

            Double D = db.getTMStatic("COEF_D");
            Double H = db.getTMStatic("COEF_H");
            Double J = db.getTMStatic("COEF_J");
            Double K = db.getTMStatic("COEF_K");

            DataTable last = new DataTable();
            db.fillWithLastCompleteLaunch(last);
            if (last.Rows.Count == 0)
            {
                
            }
            return 0;
        }

        public Double currentRunCost(UInt64 launchId)
        {
            return 0;
        }

        public Double returnCost(UInt64 launchId)
        {
            return 0;
        }
    }
}

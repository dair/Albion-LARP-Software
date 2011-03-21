using System;
using System.Collections.Generic;
using System.Text;
using Database;

namespace UI
{
    public interface IDBObject
    {
        Database.Connection getDatabase();
    }
}

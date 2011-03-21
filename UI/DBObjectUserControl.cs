using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UI
{
    public class DBObjectUserControl : UserControl, IDBObject
    {
        private Database.Connection database = null;

        protected DBObjectUserControl()
        {
            //MessageBox.Show("This method shouldn't be called: UI.DBObjectUserControl.DBObjectUserControl()");
        }

        public DBObjectUserControl(Database.Connection db)
        {
            database = db;
        }

        public void setDatabase(Database.Connection db)
        {
            database = db;
        }

        public Database.Connection getDatabase()
        {
            return database;
        }
    }
}

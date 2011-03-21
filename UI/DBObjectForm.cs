using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UI
{
    public class DBObjectForm : Form, IDBObject
    {
        private Database.Connection database = null;

        public DBObjectForm()
        {
            MessageBox.Show("This method shouldn't be called: UI.DBObjectForm.DBObjectForm()");
        }

        public DBObjectForm(Database.Connection db)
        {
            database = db;
        }

        public Database.Connection getDatabase()
        {
            return database;
        }
    }
}

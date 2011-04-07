using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Logger
{
    public partial class Logging : Form
    {
        public static Logging logging;

        public Logging()
        {
            InitializeComponent();
            logging = this;
        }

        public static void log(String str)
        {
            lock (logging)
            {
                DateTime dt = DateTime.Now;
                logging.textBox.Text += dt.ToString() + ": " + str + "\r\n";
            }
        }
    }
}

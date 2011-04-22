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

        private void Logging_Load(object sender, EventArgs e)
        {
            this.Closing += new CancelEventHandler(Logging_Closing);
        }

        void Logging_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }
    }
}

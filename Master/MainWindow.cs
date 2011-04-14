using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Master
{
    public partial class MainWindow : UI.DBObjectForm
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Settings.UI.restoreForm(this);

            this.FormClosing += new FormClosingEventHandler(MainWindow_Closing);
        }

        private void MainWindow_Closing(object sender, FormClosingEventArgs e)
        {
            Settings.UI.storeForm(this);
        }
    }
}

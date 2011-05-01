using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockMaster
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

            this.Closing += new CancelEventHandler(MainWindow_Closing);
        }

        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            Settings.UI.storeForm(this);
        }
    }
}

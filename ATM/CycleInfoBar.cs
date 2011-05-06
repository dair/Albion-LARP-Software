using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ATM
{
    public partial class CycleInfoBar : UI.DBObjectUserControl
    {
        Timer myTimer = new Timer();
        Database.StockCycleInfo info = null;
        DateTime now;
        TimeSpan untilBorder1;
        TimeSpan untilBorder2;
        TimeSpan untilFinish;

        int counter = 0;

        public CycleInfoBar()
        {
            InitializeComponent();
        }

        public CycleInfoBar(Database.Connection db)
            : base(db)
        {
            InitializeComponent();

            serverTimeLabel.Text = "";
            untilBorder1Label.Text = "";
            untilBorder2Label.Text = "";
            untilFinishLabel.Text = "";
        }

        private void CycleInfoBar_SizeChanged(object sender, EventArgs e)
        {
            int x = 0;
            serverTimeLabel.Location = new Point(x, 0);
            serverTimeLabel.Width = Width / 4;
            x += serverTimeLabel.Width;

            untilBorder1Label.Location = new Point(x, 0);
            untilBorder1Label.Width = Width / 4;
            x += untilBorder1Label.Width;

            untilBorder2Label.Location = new Point(x, 0);
            untilBorder2Label.Width = Width / 4;
            x += untilBorder2Label.Width;

            untilFinishLabel.Location = new Point(x, 0);
            untilFinishLabel.Width = Width / 4;
        }

        private void CycleInfoBar_Load(object sender, EventArgs e)
        {
            counter = 1;
            myTimer.Interval = 1000;
            myTimer.Tick += new EventHandler(myTimer_Tick);
            myTimer.Start();
            Retrieve();
        }

        String timeSpanToString(TimeSpan s)
        {
            String ret = s.ToString().Substring(0, 8);
            return ret;
        }

        void myTimer_Tick(object sender, EventArgs e)
        {
            if (counter == 0)
            {
                System.Console.WriteLine("Retrieve");
                Retrieve();
            }
            else
            {
                System.Console.WriteLine("Adding");
                now = now.AddSeconds(1);

                untilBorder1 = untilBorder1.Subtract(TimeSpan.FromSeconds(1));

                untilBorder2 = untilBorder2.Subtract(TimeSpan.FromSeconds(1));

                untilFinish = untilFinish.Subtract(TimeSpan.FromSeconds(1));
            }
            serverTimeLabel.Text = now.ToLongTimeString();
            serverTimeLabel.Refresh();

            untilBorder1Label.Visible = untilBorder1 > TimeSpan.FromSeconds(0);
            untilBorder1Label.Text = "Отмена заявок: " + timeSpanToString(untilBorder1);
            untilBorder1Label.Refresh();

            untilBorder2Label.Visible = untilBorder2 > TimeSpan.FromSeconds(0);
            untilBorder2Label.Text = "Подача заявок: " + timeSpanToString(untilBorder2);

            untilFinishLabel.Visible = untilFinish > TimeSpan.FromSeconds(0);
            untilFinishLabel.Text = "Проведение операций: " + timeSpanToString(untilFinish);

            System.Console.WriteLine(now.ToLongTimeString());

            counter++;
            if (counter == 20)
                counter = 0;
        }

        void Retrieve()
        {
            if (getDatabase() == null)
                return;

            DataTable table = new DataTable();
            now = getDatabase().now();
            getDatabase().fillWithCycleInfoAndQuotes(1, table);
            info = new Database.StockCycleInfo();

            foreach (DataRow row in table.Rows)
            {
                info.start = Convert.ToDateTime(row["START_TIME"]);
                info.border1 = Convert.ToDateTime(row["BORDER1_TIME"]);
                info.border2 = Convert.ToDateTime(row["BORDER2_TIME"]);
                info.finish = Convert.ToDateTime(row["FINISH_TIME"]);
            }

            untilBorder1 = info.border1 - now;
            untilBorder2 = info.border2 - now;
            untilFinish = info.finish - now;
        }
        
    }
}

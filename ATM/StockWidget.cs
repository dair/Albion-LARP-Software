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
    public partial class StockWidget : UI.DBObjectUserControl
    {
        IList<StockWidgetLine> lines = new List<StockWidgetLine>();

        public StockWidget()
        {
            InitializeComponent();
        }

        public StockWidget(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        private void StockWidget_Load(object sender, EventArgs e)
        {
        }


        public void Retrieve()
        {
            DataTable table = new DataTable();
            getDatabase().fillWithCycleInfoAndQuotes(2, table);
            //dataGridView.DataSource = table;

            IDictionary<String, UInt64> quoteLast = new Dictionary<String, UInt64>();
            IDictionary<String, UInt64> quotePrev = new Dictionary<String, UInt64>();
            IDictionary<String, DateTime> dateLast = new Dictionary<String, DateTime>();
            IDictionary<String, DateTime> datePrev = new Dictionary<String, DateTime>();

            foreach (DataRow row in table.Rows)
            {
                DateTime dt = Convert.ToDateTime(row["START_TIME"]);
                String ticker = Convert.ToString(row["TICKER"]);

                if (!dateLast.ContainsKey(ticker))
                    dateLast[ticker] = new DateTime(1, 1, 1);
                if (!datePrev.ContainsKey(ticker))
                    datePrev[ticker] = new DateTime(9999, 1, 1);
                bool isLast = dt >= dateLast[ticker];
                bool isPrev = dt <= datePrev[ticker];

                if (isLast)
                {
                    dateLast[ticker] = dt;
                    quoteLast[ticker] = Convert.ToUInt64(row["QUOTE"]);
                }

                if (isPrev)
                {
                    datePrev[ticker] = dt;
                    quotePrev[ticker] = Convert.ToUInt64(row["QUOTE"]);
                }
            }

            foreach (String k in quoteLast.Keys)
            {
                StockWidgetLine line = new StockWidgetLine();
                line.SetData(k, quoteLast[k], (Int64)(quoteLast[k]) - (Int64)quotePrev[k]);
                lines.Add(line);

                line.KeyDown += new KeyEventHandler(line_KeyDown);
            }

            // initial position
            int y = 0;
            foreach (StockWidgetLine line in lines)
            {
                line.Location = new Point(0, y);
                y += line.Height;
                line.Width = this.Width;
                this.Controls.Add(line);
                line.Show();
            }

            if (y < Height)
            {
                int pan = (Height - y) / 2;
                foreach (StockWidgetLine line in lines)
                {
                    Point l = line.Location;
                    l.Y += pan;
                    line.Location = l;
                }
            }
        }

        void line_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }

    }
}

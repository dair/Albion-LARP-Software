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

        public void Retrieve()
        {
            foreach (StockWidgetLine line in lines)
            {
                Controls.Remove(line);
            }
            lines.Clear();

            DataTable table = new DataTable();
            getDatabase().fillWithCycleInfoAndQuotes(2, table);
            //dataGridView.DataSource = table;

            Dictionary<String, List<UInt64>> quotes = new Dictionary<string, List<UInt64>>();
            Dictionary<String, List<DateTime>> times = new Dictionary<string, List<DateTime>>();
            
            foreach (DataRow row in table.Rows)
            {
                String ticker = Convert.ToString(row["TICKER"]);
                DateTime dt = Convert.ToDateTime(row["START_TIME"]);
                UInt64 quote = Convert.ToUInt64(row["QUOTE"]);

                if (times.ContainsKey(ticker))
                {
                    for (int i = 0; i < times[ticker].Count; ++i)
                    {
                        if (times[ticker][i] < dt)
                        {
                            times[ticker].Insert(i, dt);
                            quotes[ticker].Insert(i, quote);
                            break;
                        }
                    }
                }
                else
                {
                    times[ticker] = new List<DateTime>();
                    times[ticker].Add(dt);
                    quotes[ticker] = new List<ulong>();
                    quotes[ticker].Add(quote);
                }
            }

            List<String> keys = new List<string>(quotes.Keys);
            keys.Sort();

            foreach (String k in keys)
            {
                StockWidgetLine line = new StockWidgetLine();
                line.SetData(k, quotes[k][0], (Int64)(quotes[k][0]) - (Int64)quotes[k][quotes[k].Count-1]);
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

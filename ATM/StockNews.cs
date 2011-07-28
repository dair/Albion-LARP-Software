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
    public partial class StockNews : ATMObject
    {
        DataTable newsTable = null;
        //List<NewsItem> itemList = new List<NewsItem>();
        Dictionary<UInt64, NewsItem> itemDict = new Dictionary<ulong, NewsItem>();
        bool waitToDisappear = false;
        int NewsX;
        Timer moveTimer = new Timer();

        public StockNews()
        {
            InitializeComponent();
        }

        public StockNews(Database.Connection db)
            : base(db)
        {
            InitializeComponent();

            NewsX = pictureLogo.Width + pictureLogo.Location.X;
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);

            newsTable = new DataTable();
            moveTimer.Interval = 50;
            moveTimer.Tick += new EventHandler(moveTimer_Tick);
            moveTimer.Start();

            InitiallyCreateItems();
            InitiallyShowItems();
        }

        public override void Deinit()
        {
            moveTimer.Stop();

            foreach (UInt64 id in itemDict.Keys)
            {
                if (Controls.Contains(itemDict[id]))
                {
                    itemDict[id].KeyDown -= StockNews_KeyDown;
                    Controls.Remove(itemDict[id]);
                }
            }

            itemDict.Clear();
        }

        UInt64 idGet(UInt64 elem)
        {
            return elem;
        }

        void moveTimer_Tick(object sender, EventArgs e)
        {
            UInt64 lastId = 0;
            UInt64 firstId = 0;
            List<UInt64> keys = new List<UInt64>(itemDict.Keys);
            keys.Sort();

            (ParentForm as ClientUI.ClientForm).RecordActivity();

            foreach (UInt64 id in keys)
            {
                if (itemDict[id].Visible)
                {
                    Point l = itemDict[id].Location;
                    l.Y -= 1;
                    itemDict[id].Location = l;

                    int lowY = l.Y + itemDict[id].Height;

                    if (lowY < 0)
                    {
                        itemDict[id].Visible = false;
                        if (waitToDisappear)
                        {
                            firstId = id;
                        }
                    }
                    else if (lowY == Height)
                    {
                        lastId = id;
                    }
                }
            }

            if (lastId != 0)
            {
                int idx = keys.IndexOf(lastId) + 1;
                if (idx == keys.Count)
                    idx = 0;

                if (!itemDict[keys[idx]].Visible)
                {
                    itemDict[keys[idx]].Location = new Point(NewsX, Height);
                    itemDict[keys[idx]].Visible = true;
                }
                else
                {
                    waitToDisappear = true;
                }
            }

            if (firstId != 0)
            {
                itemDict[firstId].Location = new Point(NewsX, Height);
                itemDict[firstId].Visible = true;

                waitToDisappear = false;
            }
        }

        void InitiallyCreateItems()
        {
            getDatabase().fillWithLastNews(newsTable);

            foreach (DataRow row in newsTable.Rows)
            {
                UInt64 id = Convert.ToUInt64(row["ID"]);
                itemDict[id] = new NewsItem();
                Database.NewsInfo info = new Database.NewsInfo();
                info.title = Convert.ToString(row["TITLE"]);
                info.text = Convert.ToString(row["TEXT"]);
                itemDict[id].NewsInfo = info;
                itemDict[id].KeyDown += new KeyEventHandler(StockNews_KeyDown);
            }
        }

        void StockNews_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(sender, e);
        }

        void InitiallyShowItems()
        {
            int y = 0;
            waitToDisappear = false;

            List<UInt64> keys = new List<UInt64>(itemDict.Keys);
            keys.Sort();

            foreach (UInt64 id in keys)
            {
                Controls.Add(itemDict[id]);
                if (y <= Height)
                {
                    itemDict[id].Location = new Point(NewsX, y);
                    y += itemDict[id].Height;
                    itemDict[id].Show();
                }
                else
                {
                    itemDict[id].Visible = false;
                }
            }
            if (y < Height)
            {
                waitToDisappear = true;
            }
        }


    }
}

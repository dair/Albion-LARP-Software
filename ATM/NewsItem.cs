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
    public partial class NewsItem : UserControl
    {
        public NewsItem()
        {
            InitializeComponent();
        }

        private Database.NewsInfo newsInfo = null;
        public Database.NewsInfo NewsInfo
        {
            set
            {
                newsInfo = value;
                headerLabel.Text = newsInfo.title;
                headerLabel.Refresh();
                textLabel.Text = newsInfo.text;
                textLabel.Location = new Point(0, headerLabel.Location.Y + headerLabel.Height);
                textLabel.Refresh();

                this.Height = headerLabel.Height + textLabel.Height + 7;
            }
            get
            {
                return newsInfo;
            }
        }
    }
}

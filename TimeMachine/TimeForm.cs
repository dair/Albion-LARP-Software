using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeMachine
{
    public partial class TimeForm : UserControl
    {
        public TimeForm()
        {
            InitializeComponent();
        }

        private void TimeForm_Load(object sender, EventArgs e)
        {
        }

        public void setDate(DateTime t)
        {
            year.Text = String.Format("{0:0000}", t.Year);
            month.Text = String.Format("{0:00}", t.Month);
            day.Text = String.Format("{0:00}", t.Day);

            hour.Text = String.Format("{0:00}", t.Hour);
            minute.Text = String.Format("{0:00}", t.Minute);

            yearError.Hide();
            monthError.Hide();
            dayError.Hide();
            hourError.Hide();
            minuteError.Hide();
        }

        private int checkPos(TextBox tb, int min, int max, Panel error, ref bool ok)
        {
            ok = false;
            int value = 0;
            try
            {
                value = Convert.ToInt16(tb.Text);

                ok = (value >= min && value <= max);
            }
            catch (FormatException)
            {
                ok = false;
            }

            error.Visible = !ok;
            return value;
        }

        public DateTime validate(ref bool all_ok)
        {
            all_ok = true;
            bool ok = false;
            int i_year = checkPos(year, 0, 9999, yearError, ref ok);
            all_ok = all_ok && ok;
            int i_month = checkPos(month, 1, 12, monthError, ref ok);
            all_ok = all_ok && ok;
            int i_day = checkPos(day, 1, 31, dayError, ref ok);
            all_ok = all_ok && ok;
            int i_hour = checkPos(hour, 0, 23, hourError, ref ok);
            all_ok = all_ok && ok;
            int i_minute = checkPos(minute, 0, 59, minuteError, ref ok);
            all_ok = all_ok && ok;

            DateTime dt = new DateTime();

            if (all_ok)
            {
                try
                {
                    dt = new DateTime(i_year, i_month, i_day, i_hour, i_minute, 0);
                }
                catch (ArgumentOutOfRangeException)
                {
                    all_ok = false;
                }
                catch (ArgumentException)
                {
                    all_ok = false;
                }
            }

            return dt;
        }
    }
}

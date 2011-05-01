using System.Text;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Settings
{
    public class UI: Settings
    {
        private static String SUBKEY_WINDOW = "ui";

        private static String fullControlName(Control control)
        {
            Control c = control;
            String ret = "";
            while (c != null)
            {
                if (ret != "")
                    ret = "." + ret;
                ret = c.Name + ret;
                c = c.Parent;
            }

            return ret;
        }

        public static void storeSplit(SplitContainer sc)
        {
            String name = fullControlName(sc);
            String key = SUBKEY_WINDOW + "\\" + name;
            SetData(key, "splitter", Convert.ToString(sc.SplitterDistance));
        }

        public static void restoreSplit(SplitContainer sc)
        {
            String name = fullControlName(sc);
            String key = SUBKEY_WINDOW + "\\" + name;

            String d = GetData(key, "splitter");

            try
            {
                int c = Convert.ToInt32(d);
                if (c > 0)
                {
                    sc.SplitterDistance = c;
                }
            }
            catch (Exception)
            {
            }
        }



        public static void storeForm(Form form)
        {
            String name = fullControlName(form);
            String key = SUBKEY_WINDOW + "\\" + name;
            SetData(key, "state", Convert.ToString(form.WindowState));
            SetData(key, "x", Convert.ToString(form.Location.X));
            SetData(key, "y", Convert.ToString(form.Location.Y));
            SetData(key, "w", Convert.ToString(form.Size.Width));
            SetData(key, "h", Convert.ToString(form.Size.Height));
        }

        public static void restoreForm(Form form)
        {
            String name = form.Name;
            String key = SUBKEY_WINDOW + "\\" + name;
            Point l = form.Location;
            String value = GetData(key, "x");
            if (value != null)
                l.X = Convert.ToInt32(value);
            value = GetData(key, "y");
            if (value != null)
                l.Y = Convert.ToInt32(value);

            form.Location = l;

            Size s = form.Size;
            value = GetData(key, "w");
            if (value != null)
                s.Width = Convert.ToInt32(value);
            value = GetData(key, "h");
            if (value != null)
                s.Height = Convert.ToInt32(value);

            form.Size = s;
        }
    }
}
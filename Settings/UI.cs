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

        public static void storeForm(Form form)
        {
            String name = form.Name;
            String key = SUBKEY_WINDOW + "\\" + name;
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
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace Settings
{
    public class Settings
    {
        protected static void SetData(String subKey, String name, String value)
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey("Software\\Bladerunner\\" + subKey, true);
            if (myKey == null)
            {
                myKey = Registry.CurrentUser.CreateSubKey("Software\\Bladerunner\\" + subKey);
            }
            myKey.SetValue(name, value);
        }

        protected static String GetData(String subKey, String name)
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey("Software\\Bladerunner\\" + subKey, true);
            if (myKey == null)
                return null;

            return Convert.ToString(myKey.GetValue(name));
        }

        protected static bool HasSettings(String subKey)
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey("Software\\Bladerunner\\" + subKey, false);
            return myKey != null;
        }


    }
}

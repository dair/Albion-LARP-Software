/* ***********************************************************************
 * (C) 2008-2012 Vladimir Lebedev-Schmidthof <vladimir@schmidthof.com>
 * Made for Albion Games (http://albiongames.org)
 * 
 * 
 *            DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
 *                    Version 2, December 2004

 * Copyright (C) 2004 Sam Hocevar <sam@hocevar.net>
 * 
 * Everyone is permitted to copy and distribute verbatim or modified
 * copies of this license document, and changing it is allowed as long
 * as the name is changed.

 *           DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
 *   TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION

 *  0. You just DO WHAT THE FUCK YOU WANT TO.
 * *********************************************************************** */

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

        protected static String GetData(String subKey, String name, String defValue)
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey("Software\\Bladerunner\\" + subKey, true);
            if (myKey == null)
                return defValue;
            object obj = myKey.GetValue(name);
            if (obj == null)
                return defValue;
            return Convert.ToString(obj);
        }

        protected static String GetData(String subKey, String name)
        {
            return GetData(subKey, name, null);
        }

        protected static bool HasSettings(String subKey)
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey("Software\\Bladerunner\\" + subKey, false);
            return myKey != null;
        }

        public static bool HasSettings()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey("Software\\Bladerunner");
            return myKey != null;
        }
    }
}

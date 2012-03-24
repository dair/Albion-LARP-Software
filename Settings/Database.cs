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
using System.Linq;
using System.Text;

namespace Settings
{
    public class Database: Settings
    {
        private static String SUBKEY_DATABASE = "db";

        public static bool HasDBSettings()
        {
            return HasSettings(SUBKEY_DATABASE);
        }

        public static void SetDBHost(String host)
        {
            SetData(SUBKEY_DATABASE, "DBHost", host);
        }

        public static String GetDBHost()
        {
            return GetData(SUBKEY_DATABASE, "DBHost");
        }

        public static void SetDBUser(String user)
        {
            SetData(SUBKEY_DATABASE, "DBUser", user);
        }

        public static String GetDBUser()
        {
            return GetData(SUBKEY_DATABASE, "DBUser");
        }

        public static void SetDBPassword(String passwd)
        {
            SetData(SUBKEY_DATABASE, "DBPassword", passwd);
        }

        public static String GetDBPassword()
        {
            return GetData(SUBKEY_DATABASE, "DBPassword");
        }

        public static void SetDBName(String name)
        {
            SetData(SUBKEY_DATABASE, "DBName", name);
        }

        public static String GetDBName()
        {
            return GetData(SUBKEY_DATABASE, "DBName");
        }

        public static void SetDBPort(UInt16 port)
        {
            SetData(SUBKEY_DATABASE, "DBPort", Convert.ToString(port));
        }

        public static UInt16 GetDBPort()
        {
            String portString = GetData(SUBKEY_DATABASE, "DBPort");
            UInt16 port = 0;
            try
            {
                port = Convert.ToUInt16(portString);
            }
            catch (Exception)
            {
                // do nothing
            }
            return port;
        }

        public static void SetDeviceId(UInt64 did)
        {
            SetData(SUBKEY_DATABASE, "deviceId", Convert.ToString(did));
        }

        public static UInt64 GetDeviceId()
        {
            String didString = GetData(SUBKEY_DATABASE, "deviceId");
            UInt64 deviceId = 0;
            try
            {
                deviceId = Convert.ToUInt64(didString);
            }
            catch (Exception)
            {
                // do nothing
            }
            return deviceId;
        }

    }
}

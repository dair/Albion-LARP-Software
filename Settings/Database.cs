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

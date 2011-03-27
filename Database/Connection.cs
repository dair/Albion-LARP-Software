using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;

namespace Database
{
    public class Connection
    {
        private String ipAddress = null;
        private UInt16 port = 0;
        private String userName = null;
        private String password = null;
        private String database = null;
        private Exception lastException = null;
        NpgsqlConnection connection = null;

        public Connection()
        {
        }

        public void setIpAddress(String ip)
        {
            ipAddress = (String)ip.Clone();
        }

        public void setPort(UInt16 p)
        {
            port = p;
        }

        public void setUserName(String uname)
        {
            userName = (String)uname.Clone();
        }

        public void setPassword(String pwd)
        {
            password = (String)pwd.Clone();
        }

        public void setDatabase(String db)
        {
            database = (String)db.Clone();
        }

        private String connectionString()
        {
            return "Server=" + ipAddress + ";" +
                "Port=" + port.ToString() + ";" +
                "User Id=" + userName + ";" +
                "Password=" + password + ";" +
                "Database=" + database + ";";
        }

        protected bool connect()
        {
            connection = new NpgsqlConnection(connectionString());
            bool ret = true;
            try
            {
                lastException = null;
                connection.Open();
            }
            catch (NpgsqlException ex)
            {
                lastException = ex;
                ret = false;
            }

            return ret;
        }

        protected void disconnect()
        {
            connection.Close();
            connection = null;
        }

        public bool test()
        {
            if (!connect())
                return false;

            NpgsqlCommand command = new NpgsqlCommand("select version()", connection);
            String serverVersion = null;
            try
            {
                serverVersion = (String)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                lastException = ex;
            }
            finally
            {
                disconnect();
            }

            return serverVersion != null;
        }

        public void fillWithPersons(DataTable table)
        {
            table.Columns.Clear();
            table.Columns.Add("ID");
            table.Columns.Add("NAME");
            table.Rows.Clear();

            if (!connect())
                return;

            NpgsqlCommand command = new NpgsqlCommand("select ID, NAME from PERSON", connection);
            try
            {

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    table.Rows.Add(Convert.ToString(rd["ID"]), Convert.ToString(rd["NAME"]));
                }
            }
            catch (Exception ex)
            {
                lastException = ex;
            }
            finally
            {
                disconnect();
            }
        }

        // ----------------------------------------------------------
        public ICollection<IPerson> getPersons()
        {
            if (!connect())
                return null;

            ICollection<IPerson> ret = null;

            NpgsqlCommand command = new NpgsqlCommand("select ID, NAME from PERSON", connection);
            try
            {
                List<IPerson> list = new List<IPerson>();
                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    UInt16 id = Convert.ToUInt16(rd["ID"]);
                    String name = Convert.ToString(rd["NAME"]);
                    PersonInfo info = new PersonInfo(id, name);
                    list.Add(info);
                }

                ret = list;
            }
            catch (Exception ex)
            {
                lastException = ex;
            }
            finally
            {
                disconnect();
            }

            return ret;
        }

        // ----------------------------------------------------------
        public FullPersonInfo getPersonInfo(UInt16 id)
        {
            if (!connect())
                return null;

            FullPersonInfo ret = new FullPersonInfo();

            NpgsqlCommand command = new NpgsqlCommand("select NAME, BALANCE from PERSON where ID = :value1", connection);
            try
            {
                command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters[0].Value = id;

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ret.id = id;
                    ret.name = Convert.ToString(rd["NAME"]);
                    ret.balance = Convert.ToUInt32(rd["BALANCE"]);
                }

                command = new NpgsqlCommand("select PROPERTY.NAME, PERSON_PROP.VALUE from PERSON_PROP, PROPERTY where PERSON_PROP.PERS_ID = :value1 and PERSON_PROP.PROP_ID = PROPERTY.ID", connection);
                command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters[0].Value = id;

                ret.properties.Clear();
                ret.properties.Columns.Add("Название");
                ret.properties.Columns.Add("Значение");

                rd = command.ExecuteReader();



                while (rd.Read())
                {
                    String key = Convert.ToString(rd[0]);
                    String value = Convert.ToString(rd[1]);

                    ret.properties.Rows.Add(key, value);
                }
            }
            catch (Exception ex)
            {
                lastException = ex;
            }
            finally
            {
                disconnect();
            }

            return ret;
        }

        // ----------------------------------------------------------
        public void deletePerson(UInt16 personId)
        {
            if (!connect())
                return;

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("delete from person_prop where pers_id = :value", connection);
                command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters[0].Value = personId;
                command.ExecuteNonQuery();

                command = new NpgsqlCommand("delete from person where id = :value", connection);
                command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters[0].Value = personId;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                lastException = ex;
            }
            finally
            {
                disconnect();
            }
        }
    }
}

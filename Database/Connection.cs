using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
            bool ret = true;
            connection = new NpgsqlConnection(connectionString());
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

        public Boolean dbToBoolean(object o)
        {
            if (!(o is String))
            {
                throw new InvalidCastException("Invalid result from DB");
            }

            String s = Convert.ToString(o);
            if (s.ToLower()[0] == 'y')
                return true;

            return false;
        }

        public String booleanToDb(bool b)
        {
            if (b)
                return "Y";
            else
                return "N";
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

            NpgsqlCommand command = new NpgsqlCommand("select ID, NAME from PERSON ORDER BY ID ASC", connection);
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

            NpgsqlCommand command = new NpgsqlCommand("select NAME, GENDER, RACE from PERSON where ID = :value1", connection);
            try
            {
                command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters[0].Value = id;

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ret.id = id;
                    ret.name = Convert.ToString(rd["NAME"]);
                    String sGender = Convert.ToString(rd["GENDER"]);
                    String sGenome = Convert.ToString(rd["RACE"]);

                    switch (sGender)
                    {
                        case "M":
                            ret.gender = FullPersonInfo.Gender.Male;
                            break;
                        case "F":
                            ret.gender = FullPersonInfo.Gender.Female;
                            break;
                    }

                    switch (sGenome)
                    {
                        case "H":
                            ret.genome = FullPersonInfo.Genome.Human;
                            break;
                        case "A":
                            ret.genome = FullPersonInfo.Genome.Android;
                            break;
                    }
                }

                command = new NpgsqlCommand("select PROPERTY.NAME, PERSON_PROP.VALUE from PERSON_PROP, PROPERTY where PERSON_PROP.PERS_ID = :value1 and PERSON_PROP.PROP_ID = PROPERTY.ID ORDER BY PROPERTY.NAME ASC", connection);
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
        public MoneyInfo getMoneyInfo(UInt16 id)
        {
            if (!connect())
                return null;

            MoneyInfo ret = new MoneyInfo();

            NpgsqlCommand command = new NpgsqlCommand("select BALANCE, PIN, FAILURES from MONEY where ID = :value1 ORDER BY ID ASC", connection);
            try
            {
                command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters[0].Value = id;

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ret.balance = Convert.ToUInt32(rd["BALANCE"]);
                    ret.pinCode = Convert.ToString(rd["PIN"]);
                    ret.failures = Convert.ToUInt16(rd["FAILURES"]);
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

        // ----------------------------------------------------------
        public void fillPropList(DataTable table)
        {
            table.Columns.Clear();
            table.Columns.Add("ID", Type.GetType("System.UInt16"));
            table.Columns.Add("Название");
            table.Columns.Add("Видно полиции", Type.GetType("System.Boolean", false, true));
            table.Rows.Clear();

            if (!connect())
                return;

            NpgsqlCommand command = new NpgsqlCommand("select ID, NAME, POLICE from PROPERTY order by ID ASC", connection);
            try
            {
                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    object o0 = rd["ID"];
                    object o1 = rd["NAME"];
                    object o2 = rd["POLICE"];

                    table.Rows.Add(Convert.ToUInt16(o0), Convert.ToString(o1), dbToBoolean(o2));
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
        public void editProperty(PropertyInfo pInfo)
        {
            if (pInfo == null)
                return;

            if (pInfo.name == null ||
                pInfo.name.Length == 0)
                throw new ArgumentNullException("pInfo.name", "name shoudn't be null or empty");

            String req;
            if (pInfo.id == 0)
            {
                req = "INSERT INTO PROPERTY (NAME, POLICE) VALUES (:value1, :value2)";
            }
            else
            {
                req = "UPDATE PROPERTY SET NAME = :value1, POLICE = :value2 WHERE ID = :value3";
            }

            if (!connect())
                return;

            NpgsqlCommand command = new NpgsqlCommand(req, connection);
            try
            {
                command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters["value1"].Value = pInfo.name;
                command.Parameters.Add(new NpgsqlParameter("value2", NpgsqlTypes.NpgsqlDbType.Char));
                command.Parameters["value2"].Value = booleanToDb(pInfo.policeVisibility);
                command.Parameters.Add(new NpgsqlParameter("value3", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["value3"].Value = pInfo.id;

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

        // ----------------------------------------------------------
        public void deleteProperty(PropertyInfo pInfo)
        {
            if (pInfo == null)
                return;
            if (!connect())
                return;

            String req;
            req = "DELETE FROM PROPERTY WHERE ID = :value1";

            NpgsqlCommand command = new NpgsqlCommand(req, connection);
            try
            {
                command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["value1"].Value = pInfo.id;

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

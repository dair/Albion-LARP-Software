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
using System.Data;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using Npgsql;
using Logger;
using System.Text.RegularExpressions;

namespace Database
{
    public class Connection
    {
        private String ipAddress = null;
        private UInt64 port = 0;
        private String userName = null;
        private String password = null;
        private String database = null;
        private Exception lastException = null;
        NpgsqlConnection connection = null;
        private bool invalid = false;
        System.Windows.Forms.Timer validateTimer = new System.Windows.Forms.Timer();

        public Connection()
        {
            invalid = false;
        }

        public Exception getLastException()
        {
            return lastException;
        }

        public void setIpAddress(String ip)
        {
            ipAddress = (String)ip.Clone();
        }

        public void setPort(UInt64 p)
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
            bool ret = false;
            connection = new NpgsqlConnection(connectionString());
            try
            {
                lastException = null;
                connection.Open();
                ret = true;
                invalid = false;
            }
            catch (NpgsqlException ex)
            {
                HandleException(ex);
                ret = false;
            }

            return ret;
        }

        protected void disconnect()
        {
            connection.Close();
            connection = null;
        }

        protected void HandleException(Exception ex)
        {
            lastException = ex;
            if (lastException != null)
            {
                Logger.Logging.log(ex.ToString());

                if (!invalid)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Ошибка!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    invalid = true;
                }
            }
        }

        protected void begin()
        {
            NpgsqlCommand command = new NpgsqlCommand("begin transaction", connection);
            command.ExecuteNonQuery();
        }

        protected void commit()
        {
            NpgsqlCommand command = new NpgsqlCommand("commit", connection);
            command.ExecuteNonQuery();
        }

        protected void rollback()
        {
            NpgsqlCommand command = new NpgsqlCommand("rollback", connection);
            command.ExecuteNonQuery();
        }

        public static Boolean dbToBoolean(object o)
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

        public static String booleanToDb(bool b)
        {
            if (b)
                return "Y";
            else
                return "N";
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }

            return serverVersion != null;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void fillWithPersons(DataTable table)
        {
            table.Clear();
            table.Columns.Add("ID", Type.GetType("System.UInt64"));
            table.Columns.Add("NAME");

            Logging.log("fillWithPersons\n");
            if (!connect())
                return;

            NpgsqlCommand command = new NpgsqlCommand("select ID, NAME from PERSON ORDER BY ID ASC", connection);
            try
            {
                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    table.Rows.Add(rd["ID"], rd["NAME"]);
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }
            Logging.log("!fillWithPersons\n");
        }

        // ----------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
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
                    UInt64 id = Convert.ToUInt64(rd["ID"]);
                    String name = Convert.ToString(rd["NAME"]);
                    PersonInfo info = new PersonInfo(id, name);
                    list.Add(info);
                }

                ret = list;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }

            return ret;
        }

        // ----------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public FullPersonInfo getPersonInfo(UInt64 id)
        {
            Logging.log("getPersonInfo\n");

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
                        default:
                            ret.gender = FullPersonInfo.Gender.Unknown;
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

                command = new NpgsqlCommand("select PROPERTY.ID, PROPERTY.NAME, PERSON_PROP.VALUE from PERSON_PROP, PROPERTY where PERSON_PROP.PERS_ID = :value1 and PERSON_PROP.PROP_ID = PROPERTY.ID ORDER BY PROPERTY.NAME ASC", connection);
                command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters[0].Value = id;

                rd = command.ExecuteReader();

                while (rd.Read())
                {
                    UInt64 propId = Convert.ToUInt64(rd[0]);
                    String key = Convert.ToString(rd[1]);
                    String value = Convert.ToString(rd[2]);

                    ret.properties.Rows.Add(propId, key, value);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }
            Logging.log("!getPersonInfo\n");

            return ret;
        }

        // ----------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public MoneyInfo getMoneyInfo(UInt64 id)
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
                    ret.balance = Convert.ToUInt64(rd["BALANCE"]);
                    ret.pinCode = Convert.ToString(rd["PIN"]);
                    ret.failures = Convert.ToUInt16(rd["FAILURES"]);
                }
            }

            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }

            return ret;
        }

        // ----------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deletePerson(UInt64 personId)
        {
            if (!connect())
                return;

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("delete from person_prop where pers_id = :value", connection);
                command.Parameters.Add(new NpgsqlParameter("value", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters[0].Value = personId;
                command.ExecuteNonQuery();

                command = new NpgsqlCommand("delete from person where id = :value", connection);
                command.Parameters.Add(new NpgsqlParameter("value", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters[0].Value = personId;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }
        }

        // ----------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void insertPerson(FullPersonInfo fpInfo)
        {
            updatePerson(0, fpInfo);
        }

        // ----------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updatePerson(UInt64 id, FullPersonInfo fpInfo)
        {
            if (!connect())
                return;

            try
            {
                begin();

                String query;
                if (id == 0)
                    query = "insert into person (id, name, gender, race) values (:newid, :name, :g, :r)";
                else
                    query = "update person set id = :newid, name = :name, gender = :g, race = :r where id = :id";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("newid", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["newid"].Value = fpInfo.getId();
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["id"].Value = id;
                command.Parameters.Add(new NpgsqlParameter("name", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters["name"].Value = fpInfo.name;
                command.Parameters.Add(new NpgsqlParameter("g", NpgsqlTypes.NpgsqlDbType.Char));
                String g = "?";
                switch (fpInfo.gender)
                {
                    case FullPersonInfo.Gender.Unknown:
                        g = "?";
                        break;
                    case FullPersonInfo.Gender.Male:
                        g = "M";
                        break;
                    case FullPersonInfo.Gender.Female:
                        g = "F";
                        break;
                }
                command.Parameters["g"].Value = g;
                command.Parameters.Add(new NpgsqlParameter("r", NpgsqlTypes.NpgsqlDbType.Varchar));
                String r = null;
                switch (fpInfo.genome)
                {
                    case FullPersonInfo.Genome.Android:
                        r = "A";
                        break;
                    case FullPersonInfo.Genome.Human:
                        r = "H";
                        break;
                }
                command.Parameters["r"].Value = r;
                command.ExecuteNonQuery();

                command = new NpgsqlCommand("delete from person_prop where pers_id = :persId", connection);
                command.Parameters.Add(new NpgsqlParameter("persId", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["persId"].Value = fpInfo.getId();
                command.ExecuteNonQuery();

                foreach (DataRow row in fpInfo.properties.Rows)
                {
                    command = new NpgsqlCommand("insert into person_prop (prop_id, pers_id, value) values (:propId, :persId, :value)", connection);
                    command.Parameters.Add(new NpgsqlParameter("persId", NpgsqlTypes.NpgsqlDbType.Numeric));
                    command.Parameters["persId"].Value = fpInfo.getId();
                    command.Parameters.Add(new NpgsqlParameter("propId", NpgsqlTypes.NpgsqlDbType.Numeric));
                    command.Parameters["propId"].Value = Convert.ToUInt64(row[0]);
                    command.Parameters.Add(new NpgsqlParameter("value", NpgsqlTypes.NpgsqlDbType.Varchar));
                    command.Parameters["value"].Value = Convert.ToString(row[2]);

                    command.ExecuteNonQuery();
                }

                commit();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }
        }

        // ----------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateMoney(UInt64 id, MoneyInfo mInfo)
        {
            if (!connect())
                return;

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("select count(*) from money where id = :id", connection);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["id"].Value = id;
                NpgsqlDataReader rd = command.ExecuteReader();
                UInt64 count = 0;
                while (rd.Read())
                {
                    count = Convert.ToUInt64(rd[0]);
                }

                String query;
                if (count == 0)
                {
                    query = "insert into money (id, balance, pin, failures) values (:id, :b, :p, :f)";
                }
                else
                {
                    query = "update money set balance = :b, pin = :p, failures = :f where id = :id";
                }

                command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["id"].Value = id;
                command.Parameters.Add(new NpgsqlParameter("b", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["b"].Value = mInfo.balance;
                command.Parameters.Add(new NpgsqlParameter("p", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters["p"].Value = mInfo.pinCode;
                command.Parameters.Add(new NpgsqlParameter("f", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["f"].Value = mInfo.failures;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }

        }

        // ----------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IList<PropertyInfo> getPropertyList()
        {
            List<PropertyInfo> ret = new List<PropertyInfo>();

            if (!connect())
                return null;

            NpgsqlCommand command = new NpgsqlCommand("select ID, NAME, POLICE from PROPERTY order by NAME ASC", connection);
            try
            {
                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    PropertyInfo info = new PropertyInfo();
                    info.id = Convert.ToUInt64(rd["ID"]);
                    info.name = Convert.ToString(rd["NAME"]);
                    info.policeVisibility = dbToBoolean(rd["POLICE"]);
                    ret.Add(info);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }

            return ret;
        }

        // ----------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void setPersonProperty(UInt64 personId, UInt64 oldPropId, PersonProperty prop)
        {
            if (!connect())
                return;

            NpgsqlCommand command = new NpgsqlCommand("select count(*) from person_prop where pers_id = :persId and prop_id = :prop_id", connection);
            try
            {
                command.Parameters.Add(new NpgsqlParameter("persId", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["persId"].Value = personId;
                command.Parameters.Add(new NpgsqlParameter("propId", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["propId"].Value = oldPropId;

                NpgsqlDataReader rd = command.ExecuteReader();
                UInt64 count = 0;
                while (rd.Read())
                {
                    count = Convert.ToUInt64(rd[0]);
                }
                String query;
                if (count == 0)
                    query = "insert into person_prop (prop_id, pers_id, value) values (:propId, :persId, :value)";
                else
                    query = "update person_prop set prop_id = :propId, value = :value where pers_id = :persId and prop_id = :oldPropId";

                command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("propId", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["propId"].Value = prop.propertyId;
                command.Parameters.Add(new NpgsqlParameter("oldPropId", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["oldPropId"].Value = oldPropId;
                command.Parameters.Add(new NpgsqlParameter("persId", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["persId"].Value = personId;
                command.Parameters.Add(new NpgsqlParameter("value", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters["value"].Value = prop.value;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }
        }

        // ----------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void fillPropList(DataTable table)
        {
            table.Columns.Clear();
            table.Columns.Add("ID", Type.GetType("System.UInt64"));
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

                    table.Rows.Add(Convert.ToUInt64(o0), Convert.ToString(o1), dbToBoolean(o2));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }
        }

        // ----------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
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
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }
        }

        // ----------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
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
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }
        }

        public ATMLoginInfo ATMLoginInfo(UInt64 id)
        {
            ATMLoginInfo ret = new ATMLoginInfo();

            if (id == 0)
                return ret;

            if (!connect())
                return ret;

            String query = "SELECT PERSON.NAME AS NAME, MONEY.BALANCE AS BALANCE, MONEY.PIN AS PIN, MONEY.FAILURES AS FAILURES from PERSON, MONEY where PERSON.ID = :id and PERSON.ID = MONEY.ID";
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            try
            {
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters[0].Value = id;

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ret.id = id;
                    ret.name = Convert.ToString(rd["NAME"]);
                    ret.balance = Convert.ToUInt64(rd["BALANCE"]);
                    ret.pinCode = Convert.ToString(rd["PIN"]);
                    ret.failures = Convert.ToUInt16(rd["FAILURES"]);
                }
            }

            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }

            return ret;
        }

        public String ATMProjectName(UInt64 key)
        {
            String ret = null;
            if (key == 0)
                return ret;

            if (!connect())
                return ret;

            String query = "SELECT NAME FROM PROJECT WHERE KEY = :key AND STATUS = 'A'";

            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            try
            {
                command.Parameters.Add(new NpgsqlParameter("key", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters[0].Value = key;

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ret = Convert.ToString(rd["NAME"]);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }

            return ret;
        }

        public void CorrectPinEntered(UInt64 id)
        {
            if (id == 0)
                return;

            if (!connect())
                return;

            String query = "UPDATE MONEY SET FAILURES = 0 WHERE ID = :id";

            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            try
            {
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters[0].Value = id;

                command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }

            return;
        }

        public void WrongPinEntered(UInt64 id)
        {
            if (id == 0)
                return;

            if (!connect())
                return;

            String query = "UPDATE MONEY SET FAILURES = FAILURES + 1 WHERE ID = :id";

            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            try
            {
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters[0].Value = id;

                command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }

            return;
        }

        public bool moneyTransfer(UInt64 senderId, UInt64 recvId, UInt64 amount)
        {
            return moneyTransfer(senderId, recvId, amount, amount);
        }

        public bool moneyTransfer(UInt64 senderId, UInt64 recvId, UInt64 amountToRemove, UInt64 amountToAdd)
        {
            if (!connect())
                return false;
            bool ret = false;

            String query = "UPDATE MONEY SET BALANCE = BALANCE - :amount WHERE ID = :id";
            try
            {
                begin();

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["id"].Value = senderId;
                command.Parameters.Add(new NpgsqlParameter("amount", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["amount"].Value = amountToRemove;
                command.ExecuteNonQuery();

                query = "UPDATE MONEY SET BALANCE = BALANCE + :amount WHERE ID = :id";
                command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["id"].Value = recvId;
                command.Parameters.Add(new NpgsqlParameter("amount", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["amount"].Value = amountToAdd;
                command.ExecuteNonQuery();

                query = "INSERT INTO MONEY_HISTORY (SENDER_ID, RECEIVER_ID, VALUE, TDATE) VALUES (:sid, :rid, :amount, now())";
                command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("sid", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["sid"].Value = senderId;
                command.Parameters.Add(new NpgsqlParameter("rid", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["rid"].Value = recvId;
                command.Parameters.Add(new NpgsqlParameter("amount", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["amount"].Value = amountToRemove;
                command.ExecuteNonQuery();

                commit();
                ret = true;
            }
            catch (Exception ex)
            {
                rollback();
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }

            return ret;
        }

        public bool moneyTransferToProject(UInt64 senderId, UInt64 recvId, UInt64 amount)
        {
            if (!connect())
                return false;
            bool ret = false;

            String query = "UPDATE MONEY SET BALANCE = BALANCE - :amount WHERE ID = :id";
            try
            {
                begin();

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["id"].Value = senderId;
                command.Parameters.Add(new NpgsqlParameter("amount", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["amount"].Value = amount;
                command.ExecuteNonQuery();

                query = "UPDATE PROJECT SET MONEY = MONEY + :amount WHERE KEY = :id AND STATUS='A'";
                command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["id"].Value = recvId;
                command.Parameters.Add(new NpgsqlParameter("amount", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["amount"].Value = amount;
                command.ExecuteNonQuery();

                query = "INSERT INTO MONEY_HISTORY (SENDER_ID, RECEIVER_PROJECT_KEY, VALUE, TDATE) VALUES (:sid, :rid, :amount, now())";
                command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("sid", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["sid"].Value = senderId;
                command.Parameters.Add(new NpgsqlParameter("rid", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["rid"].Value = recvId;
                command.Parameters.Add(new NpgsqlParameter("amount", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["amount"].Value = amount;
                command.ExecuteNonQuery();

                commit();
                ret = true;
            }
            catch (Exception ex)
            {
                rollback();
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }

            return ret;
        }

        public DateTime now()
        {
            DateTime ret = new DateTime();
            if (!connect())
                return ret;

            try
            {
                String query = "SELECT NOW()";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ret = Convert.ToDateTime(rd["NOW"]);             
                }

                rd.Close();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }

            return ret;
        }

        public void searchInfo(String searchStr, DataTable table)
        {
            table.Clear();
            table.Columns.Add("ID", Type.GetType("System.UInt64"));
            table.Columns.Add("NAME", Type.GetType("System.String"));
            table.Columns.Add("FOUND", Type.GetType("System.String"));

            if (!connect())
                return;


            String s = searchStr.Trim();
            if (s == "")
                s = "%";
            else
                s = "%" + s + "%";

            try
            {
                String query = "SELECT ID, NAME, TXT FROM ((SELECT ID, NAME, 'Код' AS TXT FROM PERSON WHERE cast(ID as varchar(10)) LIKE :str) UNION (SELECT ID, NAME, 'Имя' AS TXT FROM PERSON WHERE UPPER(NAME) LIKE UPPER(:str)) UNION (SELECT P.ID, P.NAME, PROP.NAME AS TXT FROM PERSON P, PERSON_PROP PP, PROPERTY PROP WHERE P.ID = PP.PERS_ID AND UPPER(PP.VALUE) LIKE UPPER(:str) AND PP.PROP_ID = PROP.ID AND PROP.POLICE = 'Y')) AS S";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("str", NpgsqlTypes.NpgsqlDbType.Varchar);
                command.Parameters["str"].Value = s;

                Dictionary<UInt64, String> text = new Dictionary<ulong, string>();

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    UInt64 id = Convert.ToUInt64(rd["ID"]);
                    String name = Convert.ToString(rd["NAME"]);
                    String txt = Convert.ToString(rd["TXT"]);
                    if (text.ContainsKey(id))
                    {
                        text[id] += Environment.NewLine + txt;
                    }
                    else
                    {
                        text[id] = txt;
                        table.Rows.Add(id, name, "");
                    }
                }
                rd.Close();

                foreach (DataRow row in table.Rows)
                {
                    UInt64 id = Convert.ToUInt64(row["ID"]);
                    row["FOUND"] = text[id];
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }

        }

        public void fillWithPoliceProperties(UInt64 personId, DataTable table)
        {
            table.Clear();
            table.Columns.Add("ID", Type.GetType("System.UInt64"));
            table.Columns.Add("NAME");
            table.Columns.Add("VALUE");

            if (!connect())
                return;

            try
            {
                String query = "SELECT P.ID, P.NAME, PP.VALUE FROM PERSON_PROP PP, PROPERTY P WHERE PP.PERS_ID = :pid AND PP.PROP_ID = P.ID AND P.POLICE = 'Y' ORDER BY P.NAME ASC";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("pid", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["pid"].Value = personId;

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    table.Rows.Add(rd["ID"], rd["NAME"], rd["VALUE"]);
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }
        }

        public void fillWithHistory(UInt64 personId, DataTable table)
        {
            table.Clear();
            table.Columns.Add("TIME", Type.GetType("System.DateTime"));
            table.Columns.Add("INOUT");
            table.Columns.Add("AMOUNT", Type.GetType("System.UInt64"));
            table.Columns.Add("USER");

            if (!connect())
                return;

            try
            {

                String query = "SELECT TDATE, SID, RID, SNAME, RNAME, VALUE FROM " +
"((SELECT H.TDATE, S.ID AS SID, R.ID AS RID, S.NAME AS SNAME, R.NAME AS RNAME, H.VALUE FROM MONEY_HISTORY H, PERSON S, PERSON R " + 
"    WHERE (H.SENDER_ID = :pid OR H.RECEIVER_ID = :pid) AND H.SENDER_ID = S.ID AND H.RECEIVER_ID = R.ID) " +
"    UNION " +
"(SELECT H.TDATE, S.ID AS SID, R.KEY AS RID, S.NAME AS SNAME, R.NAME AS RNAME, H.VALUE FROM MONEY_HISTORY H, PERSON S, PROJECT R " +
"    WHERE " +
"    S.ID = :pid AND " +
"    S.ID = H.SENDER_ID AND R.KEY = H.RECEIVER_PROJECT_KEY AND R.STATUS = 'A' " +
"    ORDER BY H.TDATE ASC) " +
"    UNION " +
"(SELECT H.TDATE, S.KEY AS SID, R.ID AS RID, S.NAME AS SNAME, R.NAME AS RNAME, H.VALUE FROM MONEY_HISTORY H, PROJECT S, PERSON R " +
"    WHERE " +
"    R.ID = :pid AND " +
"    R.ID = H.RECEIVER_ID AND S.KEY = H.SENDER_PROJECT_KEY AND S.STATUS = 'A')) AS FOO ORDER BY TDATE ASC";

//                String query = "SELECT H.TDATE, S.ID AS SID, R.ID AS RID, S.NAME AS SNAME, R.NAME AS RNAME, H.VALUE FROM MONEY_HISTORY H, PERSON S, PERSON R WHERE (H.SENDER_ID = :pid OR H.RECEIVER_ID = :pid) AND H.SENDER_ID = S.ID AND H.RECEIVER_ID = R.ID ORDER BY H.TDATE ASC";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("pid", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["pid"].Value = personId;
                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    String inout = "OUT";
                    String user = Convert.ToString(rd["RNAME"]);

                    if (Convert.ToUInt64(rd["RID"]) == personId)
                    {
                        inout = "IN";
                        user = Convert.ToString(rd["SNAME"]);
                    }
                    
                    table.Rows.Add(rd["TDATE"], inout, rd["VALUE"], user);
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                disconnect();
            }


        }

    }

}

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
        private bool doMessageBox = true;

        public Connection()
        {
            invalid = false;
        }

        public void setMessageBox(bool m)
        {
            doMessageBox = m;
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
                    if (doMessageBox)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.ToString(), "Ошибка!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
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

        public bool moneyTransferFromProject(UInt64 senderId, UInt64 recvId, UInt64 amount)
        {
            if (!connect())
                return false;
            bool ret = false;

            try
            {
                begin();
                String query = "UPDATE PROJECT SET MONEY = MONEY - :amount WHERE KEY = :id AND STATUS='A'";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Integer));
                command.Parameters["id"].Value = senderId;
                command.Parameters.Add(new NpgsqlParameter("amount", NpgsqlTypes.NpgsqlDbType.Bigint));
                command.Parameters["amount"].Value = amount;
                command.ExecuteNonQuery();

                query = "UPDATE MONEY SET BALANCE = BALANCE + :amount WHERE ID = :id";
                command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Integer));
                command.Parameters["id"].Value = recvId;
                command.Parameters.Add(new NpgsqlParameter("amount", NpgsqlTypes.NpgsqlDbType.Bigint));
                command.Parameters["amount"].Value = amount;
                command.ExecuteNonQuery();

                query = "INSERT INTO MONEY_HISTORY (SENDER_PROJECT_KEY, RECEIVER_ID, VALUE, TDATE) VALUES (:sid, :rid, :amount, now())";
                command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("sid", NpgsqlTypes.NpgsqlDbType.Integer));
                command.Parameters["sid"].Value = senderId;
                command.Parameters.Add(new NpgsqlParameter("rid", NpgsqlTypes.NpgsqlDbType.Integer));
                command.Parameters["rid"].Value = recvId;
                command.Parameters.Add(new NpgsqlParameter("amount", NpgsqlTypes.NpgsqlDbType.Bigint));
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

        public Dictionary<String, String> getProjectInfo(UInt64 key)
        {
            if (!connect())
                return null;

            Dictionary<String, String> ret = null;

            try
            {
                String query = "SELECT p.name, p.money, t.person_id from project p, project_team t where p.key = t.project_key and p.key = :key and t.status = 'L' and p.status = 'A'";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("key", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["key"].Value = key;

                NpgsqlDataReader rd = command.ExecuteReader();

                ret = new Dictionary<string, string>();
                while (rd.Read())
                {
                    ret["name"] = Convert.ToString(rd["name"]);
                    ret["money"] = Convert.ToString(rd["money"]);
                    ret["leader"] = Convert.ToString(rd["person_id"]);
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

        public uint count6(UInt64 n)
        {
            UInt64 num = n;

            uint count = 0;

            while (num > 0)
            {
                if (num % 10 == 6)
                    count++;
                num = num / 10;
            }

            return count;
        }

        public UInt64 generateExperimentId()
        {
            if (!connect())
            {
                return 0;
            }

            UInt64 ret = 0;
            Random random = new Random();

            try
            {
                UInt64 qty = 1;
                do
                {
                    ret = Convert.ToUInt64(random.Next(10000000, 99999999));
                    qty = 1;
                    if (count6(ret) < 2)
                    {
                        String query = "SELECT count(*) from tm_experiment where id = :id";
                        NpgsqlCommand command = new NpgsqlCommand(query, connection);
                        command.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Numeric);
                        command.Parameters["id"].Value = ret;

                        NpgsqlDataReader rd = command.ExecuteReader();

                        while (rd.Read())
                        {
                            qty = Convert.ToUInt64(rd["count"]);
                        }

                        rd.Close();
                    }
                }
                while (qty > 0);
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

        public UInt64 editTimeMachineExperiment(UInt64 id, UInt64 projectKey, string name, UInt64 param_space_1,
            UInt64 param_space_2, UInt64 param_time, UInt64 param_mass)
        {
            bool doInsert = false;
            if (id == 0)
            {
                id = generateExperimentId();
                doInsert = true;
            }

            if (!connect())
                return 0;

            try
            {
                String query;
                if (doInsert)
                    query = "INSERT INTO tm_experiment (id, project_key, name, param_space_1, param_space_2, param_time, param_mass) " +
                        "values (:id, :project_key, :name, :param_space_1, :param_space_2, :param_time, :param_mass)";
                else
                    query = "UPDATE tm_experiment set name = :name, param_space_1 = :param_space_1, param_space_2 = :param_space_2, param_time = :param_time, param_mass = :param_mass, updated_at = now() " +
                        "WHERE ID = :id";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer);
                command.Parameters["id"].Value = id;

                command.Parameters.Add("project_key", NpgsqlTypes.NpgsqlDbType.Integer);
                command.Parameters["project_key"].Value = projectKey;

                command.Parameters.Add("name", NpgsqlTypes.NpgsqlDbType.Varchar);
                command.Parameters["name"].Value = name;

                command.Parameters.Add("param_space_1", NpgsqlTypes.NpgsqlDbType.Bigint);
                command.Parameters["param_space_1"].Value = param_space_1;

                command.Parameters.Add("param_space_2", NpgsqlTypes.NpgsqlDbType.Bigint);
                command.Parameters["param_space_2"].Value = param_space_2;

                command.Parameters.Add("param_time", NpgsqlTypes.NpgsqlDbType.Bigint);
                command.Parameters["param_time"].Value = param_time;

                command.Parameters.Add("param_mass", NpgsqlTypes.NpgsqlDbType.Bigint);
                command.Parameters["param_mass"].Value = param_mass;

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

            return id;
        }

        public void fillWithExperiments(DataTable table, UInt64 id = 0)
        {
            table.Clear();
            table.Columns.Clear();
            table.Columns.Add("ID", Type.GetType("System.UInt64"));
            table.Columns.Add("PROJECT_KEY", Type.GetType("System.UInt64"));
            table.Columns.Add("NAME");
            table.Columns.Add("PARAM_SPACE_1", Type.GetType("System.UInt64"));
            table.Columns.Add("PARAM_SPACE_2", Type.GetType("System.UInt64"));
            table.Columns.Add("PARAM_TIME", Type.GetType("System.UInt64"));
            table.Columns.Add("PARAM_MASS", Type.GetType("System.UInt64"));
            table.Columns.Add("MASTER_PARAM_A", Type.GetType("System.Double"));
            table.Columns.Add("MASTER_PARAM_B", Type.GetType("System.Double"));
            table.Columns.Add("STATUS");
            table.Columns.Add("CREATED_AT", Type.GetType("System.DateTime"));
            table.Columns.Add("UPDATED_AT", Type.GetType("System.DateTime"));

            if (!connect())
                return;

            try
            {
                string query = "SELECT id, project_key, name, param_space_1, param_space_2, param_time, param_mass, master_param_a, master_param_b, status, created_at, updated_at from tm_experiment ";
                if (id > 0)
                {
                    query += "where id = :id ";
                }

                query += " order by id asc";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                if (id > 0)
                {
                    command.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer);
                    command.Parameters["id"].Value = id;
                }

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    table.Rows.Add(rd["id"], rd["project_key"], rd["name"], rd["param_space_1"], rd["param_space_2"], rd["param_time"], rd["param_mass"], rd["master_param_a"], rd["master_param_b"], rd["status"], rd["created_at"], rd["updated_at"]);
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

        public void fillWithProperEnergyRequests(DataTable table, UInt64 key = 0, int timeFilter = 0)
        {
            table.Clear();
            table.Columns.Clear();
            table.Columns.Add("ID", Type.GetType("System.UInt64"));
            table.Columns.Add("PROJECT_KEY", Type.GetType("System.UInt64"));
            table.Columns.Add("AMOUNT", Type.GetType("System.UInt64"));
            table.Columns.Add("PRICE", Type.GetType("System.UInt64"));
            table.Columns.Add("TIME_FROM", Type.GetType("System.DateTime"));
            table.Columns.Add("TIME_TO", Type.GetType("System.DateTime"));

            if (!connect())
                return;

            try
            {
                string query = "SELECT id, project_key, amount, price, time_from, time_to from tm_energy where 1 = 1 ";
                if (key > 0)
                {
                    query += "and project_key = :key ";
                }
                switch (timeFilter)
                {
                    case 0:
                        query += "and time_to > now() ";
                        break;
                    case 1:
                        query += "and now() between time_from and time_to ";
                        break;
                    case 2:
                        query += "and time_from > now() ";
                        break;
                }

                query += "ORDER BY project_key asc, time_from asc";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                if (key > 0)
                {
                    command.Parameters.Add("key", NpgsqlTypes.NpgsqlDbType.Integer);
                    command.Parameters["key"].Value = key;
                }

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    table.Rows.Add(rd["id"], rd["project_key"], rd["amount"], rd["price"], rd["time_from"], rd["time_to"]);
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

        public Double getEnergyPrice(Double amount)
        {
            if (!connect())
                return 0;

            Double ret = 0;
            try
            {
                string query = "select energy_price(:amount)";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("amount", NpgsqlTypes.NpgsqlDbType.Double);
                command.Parameters["amount"].Value = amount;
                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ret = Convert.ToDouble(rd[0]);
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

        public Double getTMStatic(String name)
        {
            if (!connect())
                return 0;

            Double ret = 0;
            try
            {
                string query = "select value from tm_static where name = :name";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("name", NpgsqlTypes.NpgsqlDbType.Varchar);
                command.Parameters["name"].Value = name;
                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ret = Convert.ToDouble(rd[0]);
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

        public void setTMStatic(String name, double value)
        {
            if (!connect())
                return;

            try
            {
                string query = "UPDATE tm_static set value = :value where name = :name";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("name", NpgsqlTypes.NpgsqlDbType.Varchar);
                command.Parameters["name"].Value = name;
                command.Parameters.Add("value", NpgsqlTypes.NpgsqlDbType.Double);
                command.Parameters["value"].Value = value;
                command.ExecuteNonQuery();

                query = "INSERT INTO tm_static (name, value) select :name, :value where not exists (select 1 from tm_static where name = :name)";
                command.Parameters.Add("name", NpgsqlTypes.NpgsqlDbType.Varchar);
                command.Parameters["name"].Value = name;
                command.Parameters.Add("value", NpgsqlTypes.NpgsqlDbType.Double);
                command.Parameters["value"].Value = value;

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

        public void addEnergyRequest(UInt64 pkey, UInt64 amount, UInt64 price, DateTime from, DateTime to)
        {
            if (!connect())
                return;

            try
            {
                String query;
                query = "INSERT INTO tm_energy (project_key, amount, price, time_from, time_to) " +
                    "values (:project_key, :amount, :price, :time_from, :time_to)";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("project_key", NpgsqlTypes.NpgsqlDbType.Integer);
                command.Parameters["project_key"].Value = pkey;

                command.Parameters.Add("amount", NpgsqlTypes.NpgsqlDbType.Bigint);
                command.Parameters["amount"].Value = amount;

                command.Parameters.Add("price", NpgsqlTypes.NpgsqlDbType.Bigint);
                command.Parameters["price"].Value = price;

                command.Parameters.Add("time_from", NpgsqlTypes.NpgsqlDbType.Timestamp);
                command.Parameters["time_from"].Value = from;

                command.Parameters.Add("time_to", NpgsqlTypes.NpgsqlDbType.Timestamp);
                command.Parameters["time_to"].Value = to;

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

        public void unfinishedLaunch(ref UInt64 launchId, ref UInt64 expId, ref UInt64 energyId)
        {
            launchId = 0;
            expId = 0;
            if (!connect())
                return;

            try
            {
                String query;
                query = "select id, experiment_id, energy_id from tm_launch where ended_at is null order by started_at desc limit 1";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    launchId = Convert.ToUInt64(rd["ID"]);
                    expId = Convert.ToUInt64(rd["EXPERIMENT_ID"]);
                    energyId = Convert.ToUInt64(rd["ENERGY_ID"]);
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

            return;
        }

        public UInt64 createLaunch(UInt64 expId, UInt64 energyId, UInt64 mass2)
        {
            if (!connect())
                return 0;

            UInt64 ret = 0;

            try
            {
                String query;
                query = "INSERT INTO tm_launch (experiment_id, energy_id, mass2) " +
                    "values (:experiment_id, :energy_id, :mass2) returning id";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("experiment_id", NpgsqlTypes.NpgsqlDbType.Integer);
                command.Parameters["experiment_id"].Value = expId;

                command.Parameters.Add("energy_id", NpgsqlTypes.NpgsqlDbType.Integer);
                command.Parameters["energy_id"].Value = energyId;

                command.Parameters.Add("mass2", NpgsqlTypes.NpgsqlDbType.Bigint);
                command.Parameters["mass2"].Value = mass2;

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ret = Convert.ToUInt64(rd[0]);
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

        public void fillWithLaunch(DataTable table, UInt64 launchId = 0)
        {
            table.Clear();
            table.Columns.Clear();
            table.Columns.Add("ID", Type.GetType("System.UInt64"));
            table.Columns.Add("EXPERIMENT_ID", Type.GetType("System.UInt64"));
            table.Columns.Add("ENERGY_ID", Type.GetType("System.UInt64"));
            table.Columns.Add("MASS2", Type.GetType("System.UInt64"));
            table.Columns.Add("STARTED_AT", Type.GetType("System.DateTime"));
            table.Columns.Add("ENDED_AT", Type.GetType("System.DateTime"));

            if (!connect())
                return;

            try
            {
                String query;
                query = "select id, experiment_id, energy_id, mass2, started_at, ended_at from tm_launch";
                if (launchId > 0)
                {
                    query += " where id = :id";
                }
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                if (launchId > 0)
                {
                    command.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer);
                    command.Parameters["id"].Value = launchId;
                }

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    table.Rows.Add(rd["id"], rd["experiment_id"], rd["energy_id"], rd["mass2"], rd["started_at"], rd["ended_at"]);
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

        public void fillWithLastCompleteLaunch(DataTable table)
        {
            if (!connect())
                return;

            UInt64 id = 0;

            try
            {
                String query;
                query = "select id from tm_launch where ended_at is not null order by ended_at desc limit 1";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    id = Convert.ToUInt64(rd[0]);
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

            if (id > 0)
            {
                fillWithLaunch(table, id);
            }
        }

        public void finishLaunch(UInt64 id)
        {
            if (!connect())
                return;

            try
            {
                String query;
                query = "UPDATE tm_launch set ended_at = now() where id = :id";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer);
                command.Parameters["id"].Value = id;
                command.ExecuteNonQuery();

                query = "UPDATE tm_experiment set status = 'H' where id in (select experiment_id from tm_launch where id = :id)";

                command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer);
                command.Parameters["id"].Value = id;
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

        public UInt64 getLaunchForExperiment(UInt64 expId)
        {
            if (!connect())
                return 0;

            UInt64 id = 0;

            try
            {
                String query;
                query = "select id from tm_launch where experiment_id = :exp_id";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("exp_id", NpgsqlTypes.NpgsqlDbType.Integer);
                command.Parameters["exp_id"].Value = expId;

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    id = Convert.ToUInt64(rd[0]);
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

            return id;
        }

        public void fillWithConsoleLines(DataTable table)
        {
            table.Clear();
            table.Columns.Clear();
            table.Columns.Add("ID", Type.GetType("System.UInt64"));
            table.Columns.Add("LINE");
            table.Columns.Add("RESPONSE");

            if (!connect())
                return;

            try
            {
                String query;
                query = "select id, line, response from tm_console order by id asc";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    table.Rows.Add(rd["id"], rd["line"], rd["response"]);
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

        public UInt64 addConsoleLine(String line)
        {
            if (!connect())
                return 0;

            UInt64 ret = 0;

            try
            {
                String query;
                query = "INSERT INTO tm_console (line) values (:line) returning id";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("line", NpgsqlTypes.NpgsqlDbType.Varchar);
                command.Parameters["line"].Value = line;

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ret = Convert.ToUInt64(rd[0]);
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
    }
}

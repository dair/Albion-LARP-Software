using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using Npgsql;
using Logger;

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
            bool ret = false;
            connection = new NpgsqlConnection(connectionString());
            try
            {
                lastException = null;
                connection.Open();
                ret = true;
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
            table.Columns.Clear();
            table.Columns.Add("ID");
            table.Columns.Add("NAME");
            table.Rows.Clear();

            Logging.log("fillWithPersons\n");
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
                    UInt16 propId = Convert.ToUInt16(rd[0]);
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
                    command.Parameters["propId"].Value = Convert.ToUInt16(row[0]);
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
                UInt32 count = 0;
                while (rd.Read())
                {
                    count = Convert.ToUInt32(rd[0]);
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
                    info.id = Convert.ToUInt16(rd["ID"]);
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
                UInt32 count = 0;
                while (rd.Read())
                {
                    count = Convert.ToUInt32(rd[0]);
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

        // ----------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void fillWithVKQuestions(DataTable table)
        {
            table.Columns.Clear();
            table.Columns.Add("ID", Type.GetType("System.UInt16"));
            table.Columns.Add("TEXT");
            table.Rows.Clear();

            Logging.log("fillWithVKQuestions\n");
            if (!connect())
                return;

            NpgsqlCommand command = new NpgsqlCommand("select ID, TEXT from VK_QUESTION ORDER BY ID ASC", connection);
            try
            {
                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    table.Rows.Add(Convert.ToUInt16(rd["ID"]), Convert.ToString(rd["TEXT"]));
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
            Logging.log("!fillWithVKQuestions\n");
        }

        // ----------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void fillWithVKAnswers(UInt16 questionId, DataTable table)
        {
            table.Columns.Clear();
            table.Columns.Add("TEXT");
            table.Columns.Add("HUMAN");
            table.Columns.Add("ANDROID");
            table.Rows.Clear();

            Logging.log("fillWithVKAnswers\n");
            if (!connect())
                return;

            NpgsqlCommand command = new NpgsqlCommand("select TEXT, HUMAN_VALUE, ANDROID_VALUE from VK_ANSWER WHERE question_id = :qid ORDER BY ID ASC", connection);
            command.Parameters.Add(new NpgsqlParameter("qid", NpgsqlTypes.NpgsqlDbType.Numeric));
            command.Parameters["qid"].Value = questionId;

            try
            {
                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    table.Rows.Add(Convert.ToString(rd["TEXT"]), Convert.ToInt16(rd["HUMAN_VALUE"]), Convert.ToInt16(rd["ANDROID_VALUE"]));
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
            Logging.log("!fillWithVKAnswers\n");
        }

        // ----------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public VKQuestionInfo getQuestionInfo(UInt16 questionId)
        {
            Logging.log("getQuestionInfo\n");
            if (!connect())
                return null;

            NpgsqlCommand command = new NpgsqlCommand("select ID, TEXT, GENDER from VK_QUESTION WHERE id = :qid", connection);
            command.Parameters.Add(new NpgsqlParameter("qid", NpgsqlTypes.NpgsqlDbType.Numeric));
            command.Parameters["qid"].Value = questionId;

            VKQuestionInfo ret = null;

            try
            {
                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ret = new VKQuestionInfo();
                    ret.id = Convert.ToUInt16(rd["ID"]);
                    ret.text = Convert.ToString(rd["TEXT"]);
                    String g = Convert.ToString(rd["GENDER"]);
                    switch (g)
                    {
                        case "M":
                            ret.gender = VKQuestionInfo.Gender.Male;
                            break;
                        case "F":
                            ret.gender = VKQuestionInfo.Gender.Female;
                            break;
                        default:
                            ret.gender = VKQuestionInfo.Gender.All;
                            break;
                    }
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
            Logging.log("!getQuestionInfo\n");
            return ret;
        }

        // ----------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public UInt16 editQuestion(VKQuestionInfo info)
        {
            if (!connect())
                return 0;
            String query = null;
            if (info.id == 0)
                query = "insert into VK_QUESTION (text, gender) values (:text, :gender) RETURNING id";
            else
                query = "update VK_QUESTION set text = :text, gender = :gender where id = :qid";

            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.Parameters.Add(new NpgsqlParameter("text", NpgsqlTypes.NpgsqlDbType.Varchar));
            command.Parameters["text"].Value = info.text;
            String gender;
            switch (info.gender)
            {
                case VKQuestionInfo.Gender.Female:
                    gender = "F";
                    break;
                case VKQuestionInfo.Gender.Male:
                    gender = "M";
                    break;
                default:
                    gender = "A";
                    break;
            }
            command.Parameters.Add(new NpgsqlParameter("gender", NpgsqlTypes.NpgsqlDbType.Char));
            command.Parameters["gender"].Value = gender;
            if (info.id != 0)
            {
                command.Parameters.Add(new NpgsqlParameter("qid", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["qid"].Value = info.id;
            }

            UInt16 retId = 0;

            try
            {
                if (info.id == 0)
                {
                    NpgsqlDataReader rd = command.ExecuteReader();
                    while (rd.Read())
                    {
                        retId = Convert.ToUInt16(rd["ID"]);
                    }
                }
                else
                {
                    command.ExecuteNonQuery();
                    retId = info.id;
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

            return retId;
        }

        public void deleteQuestion(UInt16 qid)
        {
            if (!connect())
                return;
            String query = "delete from VK_QUESTION where id = :qid";

            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.Parameters.Add(new NpgsqlParameter("qid", NpgsqlTypes.NpgsqlDbType.Numeric));
            command.Parameters["qid"].Value = qid;
            try
            {
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

        public void setAnswers(UInt16 qid, DataTable table)
        {
            if (qid == 0)
            {
                return;
            }

            if (!connect())
                return;

            try
            {
                begin();

                String query = "delete from VK_ANSWER where question_id = :qid";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("qid", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["qid"].Value = qid;
                command.ExecuteNonQuery();

                query = "insert into VK_ANSWER (question_id, id, text, human_value, android_value) values (:qid, :aid, :text, :human, :android)";

                UInt16 aid = 0;
                foreach (DataRow row in table.Rows)
                {
                    aid++;

                    command = new NpgsqlCommand(query, connection);
                    command.Parameters.Add(new NpgsqlParameter("qid", NpgsqlTypes.NpgsqlDbType.Numeric));
                    command.Parameters["qid"].Value = qid;
                    command.Parameters.Add(new NpgsqlParameter("aid", NpgsqlTypes.NpgsqlDbType.Numeric));
                    command.Parameters["aid"].Value = aid;
                    command.Parameters.Add(new NpgsqlParameter("text", NpgsqlTypes.NpgsqlDbType.Varchar));
                    command.Parameters["text"].Value = row["TEXT"];
                    command.Parameters.Add(new NpgsqlParameter("human", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters["human"].Value = row["HUMAN"];
                    command.Parameters.Add(new NpgsqlParameter("android", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters["android"].Value = row["ANDROID"];
                    command.ExecuteNonQuery();
                }

                commit();
            }
            catch (Exception ex)
            {
                HandleException(ex);
                rollback();
            }
            finally
            {
                disconnect();
            }
            Logging.log("!fillWithVKAnswers\n");
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
                    ret.balance = Convert.ToUInt32(rd["BALANCE"]);
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
            if (!connect())
                return false;
            bool ret = false;

            begin();

            String query = "UPDATE MONEY SET BALANCE = BALANCE - :amount WHERE ID = :id";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["id"].Value = senderId;
                command.Parameters.Add(new NpgsqlParameter("amount", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["amount"].Value = amount;
                command.ExecuteNonQuery();

                query = "UPDATE MONEY SET BALANCE = BALANCE + :amount WHERE ID = :id";
                command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["id"].Value = recvId;
                command.Parameters.Add(new NpgsqlParameter("amount", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["amount"].Value = amount;
                command.ExecuteNonQuery();

                query = "INSERT INTO MONEY_HISTORY (SENDER_ID, RECEIVER_ID, VALUE) VALUES (:sid, :rid, :amount)";
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
    }
}

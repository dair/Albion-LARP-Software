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

        public Exception getLastException()
        {
            return lastException;
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


                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Ошибка!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
            table.Columns.Add("ID", Type.GetType("System.UInt64"));
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
                    table.Rows.Add(Convert.ToUInt64(rd["ID"]), Convert.ToString(rd["NAME"]));
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

        public void fillWithNews(DataTable table)
        {
            table.Clear();
            table.Columns.Clear();
            
            table.Columns.Add("ID", Type.GetType("System.UInt64"));
            table.Columns.Add("TIME", Type.GetType("System.DateTime"));
            table.Columns.Add("TITLE", Type.GetType("System.String"));
            table.Columns.Add("TEXT", Type.GetType("System.String"));

            if (!connect())
                return;

            try
            {
                String query = "select ID, PUBLISH_TIME, TITLE, NTEXT from STOCK_NEWS ORDER BY PUBLISH_TIME DESC";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    table.Rows.Add(rd["ID"], rd["PUBLISH_TIME"], rd["TITLE"], rd["NTEXT"]);
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


        public void fillWithLastNews(DataTable table)
        {
            table.Clear();
            table.Columns.Clear();

            table.Columns.Add("ID", Type.GetType("System.UInt64"));
            table.Columns.Add("TIME", Type.GetType("System.DateTime"));
            table.Columns.Add("TITLE", Type.GetType("System.String"));
            table.Columns.Add("TEXT", Type.GetType("System.String"));

            if (!connect())
                return;

            try
            {
                String query = "SELECT START_TIME FROM STOCK_CYCLE ORDER BY START_TIME DESC LIMIT 2";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                NpgsqlDataReader rd = command.ExecuteReader();
                List<DateTime> dates = new List<DateTime>();
                while (rd.Read())
                {
                    dates.Add(Convert.ToDateTime(rd["START_TIME"]));
                }
                rd.Close();

                bool select = false;
                switch (dates.Count)
                {
                    case 0:
                        break;
                    case 1:
                        select = true;
                        query = "select ID, PUBLISH_TIME, TITLE, NTEXT from STOCK_NEWS WHERE PUBLISH_TIME < :last_time ORDER BY PUBLISH_TIME DESC";
                        command = new NpgsqlCommand(query, connection);
                        command.Parameters.Add("last_time", NpgsqlTypes.NpgsqlDbType.Timestamp);
                        command.Parameters["last_time"].Value = dates[0];
                        break;
                    case 2:
                        select = true;
                        query = "select ID, PUBLISH_TIME, TITLE, NTEXT from STOCK_NEWS WHERE PUBLISH_TIME < :last_time AND PUBLISH_TIME >= :prev_time ORDER BY PUBLISH_TIME DESC";
                        command = new NpgsqlCommand(query, connection);
                        command.Parameters.Add("last_time", NpgsqlTypes.NpgsqlDbType.Timestamp);
                        command.Parameters["last_time"].Value = dates[0];
                        command.Parameters.Add("prev_time", NpgsqlTypes.NpgsqlDbType.Timestamp);
                        command.Parameters["prev_time"].Value = dates[1];
                        break;
                }
                if (select)
                {
                    rd = command.ExecuteReader();
                    while (rd.Read())
                    {
                        table.Rows.Add(rd["ID"], rd["PUBLISH_TIME"], rd["TITLE"], rd["NTEXT"]);
                    }

                    rd.Close();
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

        public void editNews(NewsInfo info)
        {
            if (!connect())
                return;

            try
            {
                String query;
                if (info.id == 0)
                {
                    query = "INSERT INTO STOCK_NEWS (PUBLISH_TIME, TITLE, NTEXT) VALUES (:dt, :title, :txt)";
                }
                else
                {
                    query = "UPDATE STOCK_NEWS SET PUBLISH_TIME = :dt, TITLE = :title, NTEXT = :txt WHERE ID = :id";
                }

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["id"].Value = info.id;
                command.Parameters.Add(new NpgsqlParameter("dt", NpgsqlTypes.NpgsqlDbType.Timestamp));
                command.Parameters["dt"].Value = info.date;
                command.Parameters.Add(new NpgsqlParameter("title", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters["title"].Value = info.title;
                command.Parameters.Add(new NpgsqlParameter("txt", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters["txt"].Value = info.text;
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

        public void deleteNews(UInt64 nid)
        {
            if (!connect())
                return;

            try
            {
                String query = "DELETE FROM STOCK_NEWS WHERE ID = :nid";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("nid", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["nid"].Value = nid;
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

        public void fillWithCompanies(DataTable table)
        {
            table.Clear();
            table.Columns.Clear();

            table.Columns.Add("TICKER", Type.GetType("System.String"));
            table.Columns.Add("NAME", Type.GetType("System.String"));
            table.Columns.Add("STOCK", Type.GetType("System.UInt64"));

            if (!connect())
                return;

            try
            {
                String query = "select KEY, NAME, TOTAL_STOCK from STOCK_COMPANY ORDER BY KEY ASC";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    table.Rows.Add(rd["KEY"], rd["NAME"], rd["TOTAL_STOCK"]);
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

        public StockCompanyInfo getCompanyInfo(String ticker)
        {
            StockCompanyInfo info = null;

            if (!connect())
                return null;

            try
            {
                String query = "SELECT KEY, NAME, TOTAL_STOCK, MARKET_STOCK FROM STOCK_COMPANY WHERE KEY = :ticker";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                command.Parameters.Add(new NpgsqlParameter("ticker", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters[0].Value = ticker;

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    info = new StockCompanyInfo();
                    info.key = Convert.ToString(rd["KEY"]);
                    info.name = Convert.ToString(rd["NAME"]);
                    info.stockAmount = Convert.ToUInt64(rd["TOTAL_STOCK"]);
                    info.marketAmount = Convert.ToUInt64(rd["MARKET_STOCK"]);
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

            return info;
        }

        public void editCompany(String oldKey, StockCompanyInfo info)
        {
            if (!connect())
                return;

            try
            {
                String query;
                if (oldKey == null || oldKey.Trim().Length == 0)
                {
                    query = "INSERT INTO STOCK_COMPANY (KEY, NAME, TOTAL_STOCK) VALUES (:key, :name, :amount)";
                }
                else
                {
                    query = "UPDATE STOCK_COMPANY SET KEY = :key, NAME = :name, TOTAL_STOCK = :amount WHERE KEY = :old_key";
                }

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("key", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters["key"].Value = info.key;
                command.Parameters.Add(new NpgsqlParameter("name", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters["name"].Value = info.name;
                command.Parameters.Add(new NpgsqlParameter("amount", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["amount"].Value = info.stockAmount;
                command.Parameters.Add(new NpgsqlParameter("old_key", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters["old_key"].Value = oldKey;
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

        public void deleteCompany(String key)
        {
            if (!connect())
                return;

            try
            {
                String query = "DELETE FROM STOCK_COMPANY WHERE KEY = :nid";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("nid", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters["nid"].Value = key;
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

        public UInt64 CreateVKSession(UInt64 personId, UInt64 deviceId)
        {
            if (!connect())
                return 0;
            UInt64 ret = 0;
            try
            {
                String query = "INSERT INTO VK_SESSION (PERSON_ID, DEVICE_ID) VALUES (:pid, :did) RETURNING id";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("pid", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["pid"].Value = personId;
                command.Parameters.Add(new NpgsqlParameter("did", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["did"].Value = deviceId;

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ret = Convert.ToUInt16(rd["id"]);
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

        public void fillWithQuestionsForGender(DataTable table, FullPersonInfo.Gender gender, UInt64 sessionId)
        {
            table.Clear();

            table.Columns.Add("ID", Type.GetType("System.UInt64"));
            table.Columns.Add("TEXT", Type.GetType("System.String"));

            String g = "('A'";
            switch (gender)
            {
                case FullPersonInfo.Gender.Female:
                    g += ", 'F'";
                    break;
                case FullPersonInfo.Gender.Male:
                    g += ", 'M'";
                    break;
            }
            g += ")";

            if (!connect())
                return;

            String query = "SELECT ID, TEXT FROM VK_QUESTION WHERE ID NOT IN " +
                "(SELECT QUESTION_ID FROM VK_SESSION_QUESTION WHERE SESSION_ID = :sid) AND " +
                "GENDER IN " + g;

            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("sid", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["sid"].Value = sessionId;

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    table.Rows.Add(rd["ID"], rd["TEXT"]);
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

        public void addQuestionForSession(UInt64 sessionId, UInt64 questionId)
        {
            if (!connect())
                return;

            try
            {
                String query = "INSERT INTO VK_SESSION_QUESTION (SESSION_ID, QUESTION_ID) VALUES (:sid, :qid)";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("sid", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["sid"].Value = sessionId;
                command.Parameters.Add(new NpgsqlParameter("qid", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["qid"].Value = questionId;

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

        public UInt64 questionNumberForSession(UInt64 sessionId)
        {
            if (!connect())
                return 0;
            UInt64 ret = 0;

            try
            {
                String query = "SELECT COUNT(*) AS C FROM VK_SESSION_QUESTION WHERE SESSION_ID = :sid";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("sid", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["sid"].Value = sessionId;

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ret = Convert.ToUInt64(rd["C"]);
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

        public VKAnswerInfo getAnswerForSessionQuestion(UInt64 sessionId, UInt64 questionId)
        {
            VKAnswerInfo ret = null;

            if (!connect())
                return null;

            try
            {
                String query = "SELECT A.ID, A.TEXT, A.HUMAN_VALUE, A.ANDROID_VALUE FROM " +
                    "VK_ANSWER A, VK_SESSION_ANSWER SA WHERE " +
                    "A.ID = SA.ANSWER_ID AND " +
                    "A.QUESTION_ID = SA.QUESTION_ID AND " +
                    "SA.SESSION_ID = :sid AND " +
                    "A.QUESTION_ID = :qid";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("sid", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["sid"].Value = sessionId;
                command.Parameters.Add(new NpgsqlParameter("qid", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["qid"].Value = questionId;

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ret = new VKAnswerInfo();

                    ret.questionId = questionId;
                    ret.answerId = Convert.ToUInt64(rd["ID"]);
                    ret.text = Convert.ToString(rd["TEXT"]);
                    ret.humanValue = Convert.ToInt16(rd["HUMAN_VALUE"]);
                    ret.androidValue = Convert.ToInt16(rd["ANDROID_VALUE"]);
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

        public void fillSharesByPerson(UInt64 personId, DataTable table)
        {
            table.Clear();

            if (!connect())
            {
                return;
            }

            table.Columns.Add("TICKER");
            table.Columns.Add("NAME");
            table.Columns.Add("SHARE", Type.GetType("System.UInt64"));

            try
            {
                String query = "select C.KEY AS KEY, C.NAME AS NAME, O.QUANTITY AS SHARE " +
                    "FROM STOCK_COMPANY C, STOCK_OWNER O WHERE " +
                    "O.PERSON_ID = :pid AND " +
                    "C.STATUS = 'A' AND " +
                    "O.KEY = C.KEY AND " +
                    "O.QUANTITY > 0 ORDER BY C.KEY ASC";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("pid", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["pid"].Value = personId;
                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    table.Rows.Add(rd["KEY"], rd["NAME"], rd["SHARE"]);
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

        protected void editPersonShareRaw(UInt64 personId, String oldTicker, String newTicker, UInt64 share)
        {
            String query;
            NpgsqlCommand command;
            if (oldTicker != null)
            {
                query = "DELETE FROM STOCK_OWNER WHERE PERSON_ID = :pid AND KEY = :ticker";
                command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("pid", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["pid"].Value = personId;
                command.Parameters.Add(new NpgsqlParameter("ticker", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters["ticker"].Value = oldTicker;
                command.ExecuteNonQuery();
            }

            if (share > 0)
            {
                query = "INSERT INTO STOCK_OWNER (PERSON_ID, KEY, QUANTITY) VALUES (:pid, :ticker, :share)";
                command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("pid", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["pid"].Value = personId;
                command.Parameters.Add(new NpgsqlParameter("ticker", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters["ticker"].Value = newTicker;
                command.Parameters.Add(new NpgsqlParameter("share", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["share"].Value = share;
                command.ExecuteNonQuery();
            }
        }

        public void editPersonShare(UInt64 personId, String oldTicker, String newTicker, UInt64 share)
        {
            if (!connect())
                return;

            try
            {
                begin();
                editPersonShareRaw(personId, oldTicker, newTicker, share);
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

        public void deletePersonShare(UInt64 personId, String ticker)
        {
            if (!connect())
                return;

            try
            {
                begin();
                String query;
                NpgsqlCommand command;
                query = "DELETE FROM STOCK_OWNER WHERE PERSON_ID = :pid AND KEY = :ticker";
                command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("pid", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["pid"].Value = personId;
                command.Parameters.Add(new NpgsqlParameter("ticker", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters["ticker"].Value = ticker;
                command.ExecuteNonQuery();
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

        public void fillWithStockCycles(DataTable table)
        {
            table.Clear();
            table.Columns.Add("ID", Type.GetType("System.UInt64"));
            table.Columns.Add("START_TIME", Type.GetType("System.DateTime"));
            table.Columns.Add("BORDER1_TIME", Type.GetType("System.DateTime"));
            table.Columns.Add("BORDER2_TIME", Type.GetType("System.DateTime"));
            table.Columns.Add("FINISH_TIME", Type.GetType("System.DateTime"));

            if (!connect())
                return;

            try
            {
                String query = "SELECT ID, START_TIME, BORDER1_TIME, BORDER2_TIME, FINISH_TIME FROM STOCK_CYCLE ORDER BY START_TIME ASC";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    table.Rows.Add(rd["ID"], rd["START_TIME"], rd["BORDER1_TIME"], rd["BORDER2_TIME"], rd["FINISH_TIME"]);
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

        public void fillWithLatestStockQuotes(DataTable table)
        {
            table.Clear();
            table.Columns.Add("TICKER");
            table.Columns.Add("NAME");
            table.Columns.Add("QUOTE", Type.GetType("System.UInt64"));

            if (!connect())
                return;

            try
            {
                String query = "SELECT C.KEY AS TICKER, C.NAME AS NAME, Q.PRICE AS QUOTE FROM STOCK_COMPANY C LEFT OUTER JOIN STOCK_QUOTE Q ON (C.KEY = Q.COMPANY_KEY AND Q.CYCLE_ID IN (SELECT MAX(ID) FROM STOCK_CYCLE))";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    Object obj = rd["QUOTE"];
                    table.Rows.Add(rd["TICKER"], rd["NAME"], obj);
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

        public void newCycle(StockCycleInfo info)
        {
            if (info == null || !connect())
                return;

            try
            {
                begin();
                String query;
                NpgsqlCommand command;
                query = "INSERT INTO STOCK_CYCLE (START_TIME, BORDER1_TIME, BORDER2_TIME, FINISH_TIME) VALUES (:s, :b1, :b2, :f) RETURNING ID";
                command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("s", NpgsqlTypes.NpgsqlDbType.Timestamp));
                command.Parameters["s"].Value = info.start;
                command.Parameters.Add(new NpgsqlParameter("b1", NpgsqlTypes.NpgsqlDbType.Timestamp));
                command.Parameters["b1"].Value = info.border1;
                command.Parameters.Add(new NpgsqlParameter("b2", NpgsqlTypes.NpgsqlDbType.Timestamp));
                command.Parameters["b2"].Value = info.border2;
                command.Parameters.Add(new NpgsqlParameter("f", NpgsqlTypes.NpgsqlDbType.Timestamp));
                command.Parameters["f"].Value = info.finish;

                NpgsqlDataReader rd = command.ExecuteReader();
                UInt64 cycleId = 0;
                while (rd.Read())
                {
                    cycleId = Convert.ToUInt64(rd[0]);
                    break;
                }
                rd.Close();

                query = "INSERT INTO STOCK_QUOTE (CYCLE_ID, COMPANY_KEY, PRICE) VALUES (:cid, :ticker, :quote)";
                foreach (DataRow row in info.quotes.Rows)
                {
                    String ticker = Convert.ToString(row["TICKER"]);
                    UInt64 quote = Convert.ToUInt64(row["QUOTE"]);
                    command = new NpgsqlCommand(query, connection);
                    command.Parameters.Add(new NpgsqlParameter("cid", NpgsqlTypes.NpgsqlDbType.Numeric));
                    command.Parameters["cid"].Value = cycleId;
                    command.Parameters.Add(new NpgsqlParameter("ticker", NpgsqlTypes.NpgsqlDbType.Varchar));
                    command.Parameters["ticker"].Value = ticker;
                    command.Parameters.Add(new NpgsqlParameter("quote", NpgsqlTypes.NpgsqlDbType.Numeric));
                    command.Parameters["quote"].Value = quote;
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

        public void fillWithRequests(UInt64 cycleId, DataTable table)
        {
            table.Clear();
            table.Columns.Add("ID", Type.GetType("System.UInt64"));
            table.Columns.Add("PERSON_ID", Type.GetType("System.UInt64"));
            table.Columns.Add("NAME");
            table.Columns.Add("TICKER");
            table.Columns.Add("OPERATION");
            table.Columns.Add("QUANTITY", Type.GetType("System.UInt64"));
            table.Columns.Add("RTIME", Type.GetType("System.DateTime"));

            if (!connect())
                return;

            try
            {
                String query = "SELECT S.ID, S.PERSON_ID, P.NAME, S.COMPANY_KEY, S.RTIME, S.OPERATION, S.QUANTITY " +
                    "FROM STOCK_REQUEST S, PERSON P " +
                    "WHERE S.CYCLE_ID = :cid AND " +
                    "S.PERSON_ID = P.ID AND " +
                    "STATUS = 'A' " +
                    "ORDER BY S.RTIME ASC";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add(new NpgsqlParameter("cid", NpgsqlTypes.NpgsqlDbType.Numeric));
                command.Parameters["cid"].Value = cycleId;

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    table.Rows.Add(rd["ID"], rd["PERSON_ID"], rd["NAME"], rd["COMPANY_KEY"], rd["OPERATION"], rd["QUANTITY"], rd["RTIME"]);
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

        public void fillWithCycleInfoAndQuotes(UInt64 last, DataTable table)
        {
            table.Clear();
            table.Columns.Add("CYCLE_ID", Type.GetType("System.UInt64"));
            table.Columns.Add("START_TIME", Type.GetType("System.DateTime"));
            table.Columns.Add("BORDER1_TIME", Type.GetType("System.DateTime"));
            table.Columns.Add("BORDER2_TIME", Type.GetType("System.DateTime"));
            table.Columns.Add("FINISH_TIME", Type.GetType("System.DateTime"));
            table.Columns.Add("TICKER");
            table.Columns.Add("NAME");
            table.Columns.Add("QUOTE", Type.GetType("System.UInt64"));

            if (!connect())
                return;

            try
            {
                // выбираем последние N сессий с котировками. Т.о., кол-во строк = N * кол-во компаний.
                String query = "SELECT C.ID, C.START_TIME, C.BORDER1_TIME, C.BORDER2_TIME, C.FINISH_TIME, SQ.TICKER, SQ.NAME, SQ.QUOTE" +
                    " FROM STOCK_CYCLE C, "+
                    "(SELECT Q.CYCLE_ID, C.KEY AS TICKER, C.NAME AS NAME, Q.PRICE AS QUOTE "+ 
                        "FROM STOCK_COMPANY C LEFT OUTER JOIN STOCK_QUOTE Q ON "+
                            "(C.KEY = Q.COMPANY_KEY AND Q.CYCLE_ID IN (SELECT ID FROM STOCK_CYCLE ORDER BY START_TIME DESC LIMIT :last))) as SQ WHERE C.ID = SQ.CYCLE_ID";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("last", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["last"].Value = last;

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    table.Rows.Add(rd["ID"], rd["START_TIME"], rd["BORDER1_TIME"], rd["BORDER2_TIME"], rd["FINISH_TIME"], rd["TICKER"], rd["NAME"], rd["QUOTE"]);
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

        public void directStockSale(UInt64 seller, UInt64 buyer, String ticker, UInt64 qty, UInt64 price)
        {
            if (!connect())
                return;

            try
            {
                UInt64 buyerQty = 0;
                UInt64 sellerQty = 0;

                begin();

                String query = "UPDATE MONEY SET BALANCE = BALANCE - :price WHERE ID = :pid";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("pid", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["pid"].Value = buyer;
                command.Parameters.Add("price", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["price"].Value = price;
                command.ExecuteNonQuery();

                query = "UPDATE MONEY SET BALANCE = BALANCE + :price WHERE ID = :pid";
                command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("pid", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["pid"].Value = seller;
                command.Parameters.Add("price", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["price"].Value = price;
                command.ExecuteNonQuery();

                query = "SELECT PERSON_ID, QUANTITY FROM STOCK_OWNER WHERE PERSON_ID = :pid1 OR PERSON_ID = :pid2";
                command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("pid1", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["pid1"].Value = seller;
                command.Parameters.Add("pid2", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["pid2"].Value = buyer;

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    UInt64 pid = Convert.ToUInt64(rd["PERSON_ID"]);
                    if (pid == seller)
                        sellerQty = Convert.ToUInt64(rd["QUANTITY"]);
                    if (pid == buyer)
                        buyerQty = Convert.ToUInt64(rd["QUANTITY"]);
                }

                sellerQty -= qty;
                buyerQty += qty;

                editPersonShareRaw(seller, ticker, ticker, sellerQty);
                editPersonShareRaw(buyer, ticker, ticker, buyerQty);

                query = "INSERT INTO STOCK_HISTORY (SENDER_ID, RECEIVER_ID, COMPANY_KEY, QTY, PRICE) " +
                    "VALUES (:sid, :bid, :ticker, :qty, :price)";
                command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("sid", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["sid"].Value = seller;
                command.Parameters.Add("bid", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["bid"].Value = buyer;
                command.Parameters.Add("ticker", NpgsqlTypes.NpgsqlDbType.Varchar);
                command.Parameters["ticker"].Value = ticker;
                command.Parameters.Add("qty", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["qty"].Value = qty;
                command.Parameters.Add("price", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["price"].Value = price;
                command.ExecuteNonQuery();

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

        public void currentCyclePersonRequests(UInt64 pid, DataTable table)
        {
            table.Clear();
            table.Columns.Add("ID", Type.GetType("System.UInt64"));
            table.Columns.Add("CYCLE_ID", Type.GetType("System.UInt64"));
            table.Columns.Add("TICKER", Type.GetType("System.String"));
            table.Columns.Add("OPERATION", Type.GetType("System.String"));
            table.Columns.Add("QTY", Type.GetType("System.UInt64"));
            table.Columns.Add("QUOTE", Type.GetType("System.UInt64"));

            if (!connect())
                return;

            try
            {
                String query = "SELECT R.ID, R.CYCLE_ID, R.COMPANY_KEY, R.OPERATION, R.QUANTITY, Q.PRICE FROM " +
                    "STOCK_REQUEST R, STOCK_QUOTE Q WHERE " +
                    "R.PERSON_ID = :pid AND " +
                    "R.CYCLE_ID = Q.CYCLE_ID AND " +
                    "R.COMPANY_KEY = Q.COMPANY_KEY AND " +
                    "R.STATUS = 'A' " +
                    "ORDER BY R.RTIME ASC";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("pid", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["pid"].Value = pid;

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    table.Rows.Add(rd["ID"], rd["CYCLE_ID"], rd["COMPANY_KEY"], rd["OPERATION"], rd["QUANTITY"], rd["PRICE"]);
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

        public void newBuyRequest(UInt64 personId, UInt64 cycleId, String ticker, UInt64 qty)
        {
            if (!connect())
            {
                return;
            }

            try
            {
                begin();

                String query = "SELECT PRICE FROM STOCK_QUOTE WHERE CYCLE_ID = :cid AND COMPANY_KEY = :ticker";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("cid", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["cid"].Value = cycleId;
                command.Parameters.Add("ticker", NpgsqlTypes.NpgsqlDbType.Varchar);
                command.Parameters["ticker"].Value = ticker;
                UInt64 quote = 0;

                NpgsqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    quote = Convert.ToUInt64(rd["PRICE"]);
                }

                rd.Close();

                query = "UPDATE MONEY SET BALANCE = BALANCE - :price WHERE ID = :pid";
                command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("pid", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["pid"].Value = personId;
                command.Parameters.Add("price", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["price"].Value = quote * qty;
                command.ExecuteNonQuery();

                query = "INSERT INTO STOCK_REQUEST " +
                    "(PERSON_ID, CYCLE_ID, COMPANY_KEY, OPERATION, QUANTITY) " +
                    "VALUES (:pid, :cid, :ticker, :op, :qty)";
                command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("pid", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["pid"].Value = personId;
                command.Parameters.Add("cid", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["cid"].Value = cycleId;
                command.Parameters.Add("ticker", NpgsqlTypes.NpgsqlDbType.Varchar);
                command.Parameters["ticker"].Value = ticker;
                command.Parameters.Add("op", NpgsqlTypes.NpgsqlDbType.Varchar);
                command.Parameters["op"].Value = "B";
                command.Parameters.Add("qty", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["qty"].Value = qty;
                command.ExecuteNonQuery();

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

        public void newSellRequest(UInt64 personId, UInt64 cycleId, String ticker, UInt64 qty)
        {
            if (!connect())
            {
                return;
            }

            try
            {
                begin();

                String query = "UPDATE STOCK_OWNER SET QUANTITY = QUANTITY - :qty WHERE PERSON_ID = :pid AND KEY = :ticker";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("pid", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["pid"].Value = personId;
                command.Parameters.Add("ticker", NpgsqlTypes.NpgsqlDbType.Varchar);
                command.Parameters["ticker"].Value = ticker;
                command.Parameters.Add("qty", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["qty"].Value = qty;
                command.ExecuteNonQuery();


                query = "INSERT INTO STOCK_REQUEST " +
                    "(PERSON_ID, CYCLE_ID, COMPANY_KEY, OPERATION, QUANTITY) " +
                    "VALUES (:pid, :cid, :ticker, :op, :qty)";
                command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("pid", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["pid"].Value = personId;
                command.Parameters.Add("cid", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["cid"].Value = cycleId;
                command.Parameters.Add("ticker", NpgsqlTypes.NpgsqlDbType.Varchar);
                command.Parameters["ticker"].Value = ticker;
                command.Parameters.Add("op", NpgsqlTypes.NpgsqlDbType.Varchar);
                command.Parameters["op"].Value = "S";
                command.Parameters.Add("qty", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["qty"].Value = qty;
                command.ExecuteNonQuery();

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

        public void deleteRequest(UInt64 reqId)
        {
            if (!connect())
            {
                return;
            }

            try
            {
                begin();

                String query = "UPDATE STOCK_REQUEST SET STATUS = 'D' WHERE ID = :req_id";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.Add("req_id", NpgsqlTypes.NpgsqlDbType.Numeric);
                command.Parameters["req_id"].Value = reqId;
                command.ExecuteNonQuery();

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
    }
}

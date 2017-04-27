using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DAL
{
    public class MeetingDB : IDataAccess<Meeting>
    {
        static string connectionstring = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;

        /*
         * Created (26/04) 
         * Need to consider concurrency
         * Exception Handling?
         * Consider Adding contacts etc by ID? sth like dat
         * RowsAffected btw sucks? shouldnt throw exception
         */
        public Meeting Create(Meeting t)
        {
            string query = "INSERT INTO Meeting(StartTime, EndTime, Title, Description, ContactID, CreatedByID) VALUES(@StartTime, @EndTime, @Title, @Description, @ContactID, @CreatedByID);";
            SqlParameter[] ArrayOfParams =
            {
                new SqlParameter { ParameterName = "@StartTime", SqlValue = t.StartTime, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@EndTime", SqlValue = t.EndTime, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@Title", SqlValue = t.Title, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Description", SqlValue = t.Description, SqlDbType = SqlDbType.Text },
                new SqlParameter { ParameterName = "@ContactID", SqlValue = t.ContactId, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@CreatedByID", SqlValue = t.CreatedById, SqlDbType = SqlDbType.NVarChar },
           };
            
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                using (SqlCommand sqlcommand = new SqlCommand(query, con))
                {
                    try
                    {
                        sqlcommand.Parameters.AddRange(ArrayOfParams);
                        con.Open();
                        int rowsaffected = sqlcommand.ExecuteNonQuery();
                        if (rowsaffected < 1)
                        {
                            throw new System.Exception("No rows affected. Insert failed - Are you a fag?");
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception();
                    }
                }
            }
            return t;
        }

        public bool Delete(Meeting t)
        {
            throw new NotImplementedException();
        }

        public Meeting Read(Meeting t)
        {
            string sql = "SELECT * FROM AspNetUsers WHERE ID = @Id"; // search by ID, see below.

            SqlParameter idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = t.Id, SqlDbType = SqlDbType.NVarChar };

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(idParameter);
                    command.Connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            int IdCol = reader.GetOrdinal("ID"); // probably don't want ID at all... but how to search then boss?
                            int TitleCol = reader.GetOrdinal("Title");
                            int StartTimeCol = reader.GetOrdinal("StartTime");
                            int EndTimeCol = reader.GetOrdinal("EndTime");
                            int DescCol = reader.GetOrdinal("Description");
                            int CreatedByIDCol = reader.GetOrdinal("CreatedByID");
                            int ContactIDCol = reader.GetOrdinal("ContactID");

                            if (reader.Read())
                            {
                                t.Id = GetDataSafe(reader, IdCol, reader.GetInt32);
                                t.Title = GetDataSafe(reader, TitleCol, reader.GetString);
                                t.StartTime = (DateTime)GetDataSafe(reader, StartTimeCol, reader.GetSqlDateTime); // Needed explicit cast because wat?
                                t.EndTime = (DateTime)GetDataSafe(reader, EndTimeCol, reader.GetSqlDateTime);
                                t.Description = GetDataSafe(reader, DescCol, reader.GetString);
                                t.CreatedById = GetDataSafe(reader, CreatedByIDCol, reader.GetString);
                                t.ContactId = GetDataSafe(reader, ContactIDCol, reader.GetString);

                            }
                        }
                    }
                }
            }
            return t;
        }

        public List<Meeting> ReadAll()
        {
            List<Meeting> users = new List<Meeting>();

            string sql = "SELECT StartTime, EndTime, Title, Description, User1.UserName CreatedBy, User1.Id CreatedByID, User2.UserName Contact, User2.Id ContactID FROM Meeting JOIN AspNetUsers User1 ON Meeting.CreatedByID = User1.Id JOIN AspNetUsers User2 ON Meeting.ContactID = User2.Id;";

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            int TitleCol = reader.GetOrdinal("Title");
                            int StartTimeCol = reader.GetOrdinal("StartTime");
                            int EndTimeCol = reader.GetOrdinal("EndTime");
                            int DescCol = reader.GetOrdinal("Description");
                            int CreatedByIDCol = reader.GetOrdinal("CreatedByID");
                            int CreatedByNameCol = reader.GetOrdinal("CreatedBy");
                            int ContactIDCol = reader.GetOrdinal("ContactID");
                            int ContactCol = reader.GetOrdinal("Contact");

                            while (reader.Read())
                            {
                                users.Add(new Meeting
                                {
                                    Title = GetDataSafe(reader, TitleCol, reader.GetString),
                                    StartTime = (DateTime)GetDataSafe(reader, StartTimeCol, reader.GetSqlDateTime),
                                    EndTime = (DateTime)GetDataSafe(reader, EndTimeCol, reader.GetSqlDateTime),
                                    Description = GetDataSafe(reader, DescCol, reader.GetString),
                                    CreatedById = GetDataSafe(reader, CreatedByIDCol, reader.GetString),
                                    CreatedBy = GetDataSafe(reader, CreatedByNameCol, reader.GetString),
                                    ContactId = GetDataSafe(reader, ContactIDCol, reader.GetString),
                                    Contact = GetDataSafe(reader, ContactCol, reader.GetString)
                                });
                            }
                        }
                    }
                }
            }
            return users;
        }

        public Meeting AddUserToMeeting(Meeting m, User u)
        {
            string sql = "INSERT INTO Meeting_Users VALUES(@MeetingID, @UserID);";

            SqlParameter[] arrayofparams =
            {
            new SqlParameter { ParameterName = "@UserID", SqlValue = u.Id, SqlDbType = SqlDbType.NVarChar },
            new SqlParameter { ParameterName = "@MeetingID", SqlValue = m.Id, SqlDbType = SqlDbType.NVarChar },
            };
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(arrayofparams);
                    command.Connection.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    if (rowsaffected < 1)
                    {
                        throw new Exception();
                    }
                }
            }
            return m;
        }

        public Meeting Update(Meeting t)
        {
            throw new NotImplementedException();
        }

        public Meeting ReadDetails(Meeting m)
        {
            throw new NotImplementedException();
        }

        public T GetDataSafe<T>(SqlDataReader reader, int columnIndex, Func<int, T> getData)
        {
            if (!reader.IsDBNull(columnIndex))
            {
                return getData(columnIndex);
            }
            return default(T);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.ServiceModel;
using Model;

namespace DAL
{
	public class MeetingDb : IDataAccess<Meeting>
	{
		private static readonly string Connectionstring = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;

		/*
		 * Created (26/04) 
		 * Need to consider concurrency
		 * Exception Handling?
		 * Consider Adding contacts etc by ID? sth like dat
		 * RowsAffected btw sucks? shouldnt throw exception
		 */
		public Meeting Create(Meeting meeting)
		{
			const string sql = "INSERT INTO Meetings(StartTime, EndTime, Title, Description, ContactID, CreatedByID) VALUES(@StartTime, @EndTime, @Title, @Description, @ContactID, @CreatedByID);";
			SqlParameter[] arrayOfParams =
			{
				new SqlParameter {ParameterName = "@StartTime", SqlValue = meeting.StartTime, SqlDbType = SqlDbType.DateTime},
				new SqlParameter {ParameterName = "@EndTime", SqlValue = meeting.EndTime, SqlDbType = SqlDbType.DateTime},
				new SqlParameter {ParameterName = "@Title", SqlValue = meeting.Title, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@Description", SqlValue = meeting.Description, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@ContactID", SqlValue = meeting.Contact.Id, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@CreatedByID", SqlValue = meeting.CreatedBy.Id, SqlDbType = SqlDbType.NVarChar}
			};

			using (var con = new SqlConnection(Connectionstring))
			{
				using (var sqlcommand = new SqlCommand(sql, con))
				{
					try
					{
						sqlcommand.Parameters.AddRange(arrayOfParams);
						con.Open();
						int rowsaffected = sqlcommand.ExecuteNonQuery();
						if (rowsaffected < 1)
							throw new Exception("No rows affected. Insert failed");
					}
					catch (Exception)
					{
						throw new Exception();
					}
				}
			}
			return meeting;
		}

		public Meeting Delete(Meeting meeting)
		{
			const string sql = "UPDATE Meetings SET Deleted = 1 WHERE ID = @Id";

			var idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = meeting.Id, SqlDbType = SqlDbType.NVarChar };

			using (var connection = new SqlConnection(Connectionstring))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add(idParameter);
					command.Connection.Open();
					int rowsaffected = command.ExecuteNonQuery();
					if (rowsaffected < 1)
						throw new Exception();
				}
			}
			return meeting;
		}

		public Meeting Read(Meeting meeting)
		{
			const string sql = "SELECT User1 CreatedByUserName, User2 ContactUserName, Meetings.Id mId, StartTime, EndTime, Title, Description, User1.UserName CreatedBy, User1.Id CreatedByID, User2.UserName Contact, User2.Id ContactID FROM Meetings JOIN AspNetUsers User1 ON Meetings.CreatedByID = User1.Id JOIN AspNetUsers User2 ON Meetings.ContactID = User2.Id WHERE Meetings.ID = @Id";

			var idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = meeting.Id, SqlDbType = SqlDbType.NVarChar };

			using (var connection = new SqlConnection(Connectionstring))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add(idParameter);
					command.Connection.Open();
					using (var reader = command.ExecuteReader())
					{
						if (!reader.HasRows) return meeting;
						int idCol = reader.GetOrdinal("MId");
						int titleCol = reader.GetOrdinal("Title");
						int startTimeCol = reader.GetOrdinal("StartTime");
						int endTimeCol = reader.GetOrdinal("EndTime");
						int descCol = reader.GetOrdinal("Description");
						int createdByNameCol = reader.GetOrdinal("CreatedBy");
						int contactCol = reader.GetOrdinal("Contact");
						int createdByUserNameCol = reader.GetOrdinal("CreatedByUserName");
						int contactUserNameCol = reader.GetOrdinal("ContactUserName");

						if (!reader.Read()) return meeting;
						meeting.Id = GetDataSafe(reader, idCol, reader.GetInt32);
						meeting.Title = GetDataSafe(reader, titleCol, reader.GetString);
						meeting.StartTime =
							(DateTime)GetDataSafe(reader, startTimeCol, reader.GetSqlDateTime);
						meeting.EndTime = (DateTime)GetDataSafe(reader, endTimeCol, reader.GetSqlDateTime);
						meeting.Description = GetDataSafe(reader, descCol, reader.GetString);
						meeting.CreatedBy = new User
						{
							Id = GetDataSafe(reader, createdByNameCol, reader.GetString),
							UserName = GetDataSafe(reader, createdByUserNameCol, reader.GetString)
						};
						meeting.Contact = new User
						{
							Id = GetDataSafe(reader, contactCol, reader.GetString),
							UserName = GetDataSafe(reader, contactUserNameCol, reader.GetString)
						};
					}
				}
			}
			return meeting;
		}

		public List<Meeting> ReadAll()
		{
			var users = new List<Meeting>();

			const string sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; BEGIN TRANSACTION SELECT User1.UserName CreatedByUserName, User2.UserName ContactUserName, Meetings.Id mId, Meetings.Deleted, StartTime, EndTime, Title, Description, User1.UserName CreatedBy, User1.Id CreatedByID, User2.UserName Contact, User2.Id ContactID FROM Meetings JOIN AspNetUsers User1 ON Meetings.CreatedByID = User1.Id JOIN AspNetUsers User2 ON Meetings.ContactID = User2.Id; COMMIT";

			using (var connection = new SqlConnection(Connectionstring))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Connection.Open();
					using (var reader = command.ExecuteReader())
					{
						if (!reader.HasRows) return users;
						int titleCol = reader.GetOrdinal("Title");
						int startTimeCol = reader.GetOrdinal("StartTime");
						int endTimeCol = reader.GetOrdinal("EndTime");
						int descCol = reader.GetOrdinal("Description");
						int createdByNameCol = reader.GetOrdinal("CreatedBy");
						int contactCol = reader.GetOrdinal("Contact");
						int deletedCol = reader.GetOrdinal("Deleted");
						int idCol = reader.GetOrdinal("mId");
						int createdByUserNameCol = reader.GetOrdinal("CreatedByUserName");
						int contactUserNameCol = reader.GetOrdinal("ContactUserName");

						while (reader.Read())
							users.Add(new Meeting
							{
								Id = GetDataSafe(reader, idCol, reader.GetInt32),
								Title = GetDataSafe(reader, titleCol, reader.GetString),
								StartTime = (DateTime)GetDataSafe(reader, startTimeCol, reader.GetSqlDateTime),
								EndTime = (DateTime)GetDataSafe(reader, endTimeCol, reader.GetSqlDateTime),
								Description = GetDataSafe(reader, descCol, reader.GetString),
								CreatedBy = new User
								{
									Id = GetDataSafe(reader, createdByNameCol, reader.GetString),
									UserName = GetDataSafe(reader, createdByUserNameCol, reader.GetString)
								},
								Deleted = GetDataSafe(reader, deletedCol, reader.GetBoolean),
								Contact = new User
								{
									Id = GetDataSafe(reader, contactCol, reader.GetString),
									UserName = GetDataSafe(reader, contactUserNameCol, reader.GetString)
								}
							});
					}
				}
			}
			return users;
		}

		public Meeting Update(Meeting meeting)
		{
			const string sql = "UPDATE Meetings SET Title = @title, Description = @description, StartTime = @starttime, EndTime = @endtime, CreatedByID = @createdbyid, ContactID = @contactid, Deleted = @deleted WHERE ID = @id";
			SqlParameter[] sqlparams =
			{
				new SqlParameter {ParameterName = "@id", SqlValue = meeting.Id, SqlDbType = SqlDbType.Int},
				new SqlParameter {ParameterName = "@title", SqlValue = meeting.Title, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@description", SqlValue = meeting.Description, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@starttime", SqlValue = meeting.StartTime, SqlDbType = SqlDbType.DateTime},
				new SqlParameter {ParameterName = "@endtime", SqlValue = meeting.EndTime, SqlDbType = SqlDbType.DateTime},
				new SqlParameter {ParameterName = "@createdbyid", SqlValue = meeting.CreatedBy.Id, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@contactid", SqlValue = meeting.Contact.Id, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@deleted", SqlValue = meeting.Deleted, SqlDbType = SqlDbType.Bit}
			};

			var con = new SqlConnection(Connectionstring);
			var sqlcommand = new SqlCommand(sql, con);

			con.Open();
			var myTrans = con.BeginTransaction(IsolationLevel.RepeatableRead);
			sqlcommand.Parameters.AddRange(sqlparams);
			sqlcommand.Transaction = myTrans;

			int rowsaffected = sqlcommand.ExecuteNonQuery();
			if (rowsaffected < 1)
				throw new Exception();

			myTrans.Commit();
			return meeting;
		}

		public List<Meeting> ReadAllForUser(User user)
		{
			var userMeetings = new List<Meeting>();

			const string sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; BEGIN TRANSACTION SELECT DISTINCT Meetings.Id mId, StartTime, EndTime, Title, Description, User1.UserName CreatedBy, User1.Id CreatedByID, User2.UserName Contact, User2.Id ContactID  FROM Meetings JOIN AspNetUsers User1 ON Meetings.CreatedByID = User1.Id JOIN AspNetUsers User2 ON Meetings.ContactID = User2.Id WHERE Meetings.CreatedByID = @Id OR Meetings.ContactID = @Id; COMMIT";

			var idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = user.Id, SqlDbType = SqlDbType.NVarChar };

			using (var connection = new SqlConnection(Connectionstring))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add(idParameter);
					command.Connection.Open();
					using (var reader = command.ExecuteReader())
					{
						if (!reader.HasRows) return userMeetings;
						int titleCol = reader.GetOrdinal("Title");
						int startTimeCol = reader.GetOrdinal("StartTime");
						int endTimeCol = reader.GetOrdinal("EndTime");
						int descCol = reader.GetOrdinal("Description");
						int createdByCol = reader.GetOrdinal("CreatedByID");
						int contactCol = reader.GetOrdinal("ContactID");
						int idCol = reader.GetOrdinal("mId");
						int contactUserNameCol = reader.GetOrdinal("Contact");
						int createdByUserNameCol = reader.GetOrdinal("CreatedBy");

						while (reader.Read())
							userMeetings.Add(new Meeting
							{
								Title = GetDataSafe(reader, titleCol, reader.GetString),
								StartTime = (DateTime)GetDataSafe(reader, startTimeCol, reader.GetSqlDateTime),
								EndTime = (DateTime)GetDataSafe(reader, endTimeCol, reader.GetSqlDateTime),
								Description = GetDataSafe(reader, descCol, reader.GetString),
								CreatedBy = new User
								{
									Id = GetDataSafe(reader, createdByCol, reader.GetString),
									UserName = GetDataSafe(reader, createdByUserNameCol, reader.GetString)
								},
								Contact = new User
								{
									Id = GetDataSafe(reader, contactCol, reader.GetString),
									UserName = GetDataSafe(reader, contactUserNameCol, reader.GetString)
								},
								Id = GetDataSafe(reader, idCol, reader.GetInt32)
							});
					}
				}
			}
			return userMeetings;
		}

		public Meeting AddUserToMeeting(Meeting meeting, User user)
		{
			const string sql = "INSERT INTO Meetings_Users VALUES(@MeetingID, @UserID);";

			SqlParameter[] arrayofparams =
			{
				new SqlParameter {ParameterName = "@UserID", SqlValue = user.Id, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@MeetingID", SqlValue = meeting.Id, SqlDbType = SqlDbType.NVarChar}
			};
			using (var connection = new SqlConnection(Connectionstring))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddRange(arrayofparams);
					command.Connection.Open();
					int rowsaffected = command.ExecuteNonQuery();
					if (rowsaffected < 1)
						throw new Exception();
				}
			}
			return meeting;
		}

		public Meeting ReadDetails(Meeting m)
		{
			Meeting result = null;

			const string sql = "SELECT Contact.UserName ContactUserName, CreatedBy.UserName CreatedByUserName, Meetings.id MeetingId, Contact.Id ContactId, CreatedBy.Id CreatedById, Meetings.starttime MeetingStartTime, Meetings.EndTime MeetingEndTime, Meetings.Title MeetingTitle, Meetings.Description, Meetings.Deleted, Contact.FirstName ContactFirstName, Contact.LastName ContactLastName, CreatedBy.FirstName CreatedByFirstName, CreatedBy.LastName CreatedByLastName FROM Meetings JOIN AspNetUsers Contact ON Meetings.ContactID = Contact.Id JOIN AspNetUsers CreatedBy ON Meetings.CreatedByID = CreatedBy.Id WHERE Meetings.id = @id";

			try
			{
				var midparam = new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.Int, Value = m.Id };
				using (var connection = new SqlConnection(Connectionstring))
				{
					using (var command = new SqlCommand(sql, connection))
					{
						command.Parameters.Add(midparam);
						command.Connection.Open();
						using (var reader = command.ExecuteReader())
						{
							while (reader.Read())
								result = new Meeting
								{
									Id = (int)reader["MeetingId"],
									Deleted = (bool)reader["Deleted"],
									Title = reader["MeetingTitle"].ToString(),
									StartTime = (DateTime)reader["MeetingStartTime"],
									EndTime = (DateTime)reader["MeetingEndTime"],
									Description = reader["Description"].ToString(),
									CreatedBy = new User
									{
										Id = reader["CreatedById"].ToString(),
										FirstName = reader["CreatedByFirstName"].ToString(),
										LastName = reader["CreatedByLastName"].ToString(),
										UserName = reader["CreatedByUserName"].ToString()
									},
									Contact = new User
									{
										FirstName = reader["ContactFirstName"].ToString(),
										LastName = reader["ContactLastName"].ToString(),
										Id = reader["ContactId"].ToString(),
										UserName = reader["ContactUserName"].ToString()
									}
								};
						}
					}
				}
			}
			catch (SqlException e)
			{
				throw new FaultException<SqlException>(e, new FaultReason(e.Message), new FaultCode("Sender"));
			}
			catch (DbException e)
			{
				throw new FaultException<DbException>(e, new FaultReason(e.Message), new FaultCode("Sender"));
			}
			catch (Exception e)
			{
				throw new FaultException<Exception>(e, new FaultReason(e.Message), new FaultCode("Sender"));
			}
			return result;
		}

		public Meeting RemoveMeeting(Meeting meeting)
		{
			const string sql = "DELETE FROM Meetings WHERE id = @id";
			var theparam = new SqlParameter { ParameterName = "@id", SqlValue = meeting.Id, SqlDbType = SqlDbType.Int };
			using (var connection = new SqlConnection(Connectionstring))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add(theparam);
					command.Connection.Open();
					using (command.ExecuteReader())
					{
					}
				}
			}
			return meeting;
		}
		public T GetDataSafe<T>(SqlDataReader reader, int columnIndex, Func<int, T> getData)
		{
			return !reader.IsDBNull(columnIndex) ? getData(columnIndex) : default(T);
		}

		public Page<Meeting> ReadMeetingPage(int? page, int? pageSize)
		{
			page = page ?? 1;
			pageSize = pageSize ?? 10;
			var meetings = new List<Meeting>();
			var totalRows = 0;
			var rowStart = ((page - 1) * pageSize);

			const string sql = "SELECT DISTINCT Meetings.Id mId, StartTime, EndTime, Title, Description, User1.UserName CreatedBy, User1.Id CreatedByID, User2.UserName Contact, User2.Id ContactID  FROM Meetings JOIN AspNetUsers User1 ON Meetings.CreatedByID = User1.Id JOIN AspNetUsers User2 ON Meetings.ContactID = User2.Id WHERE Deleted = 0 ORDER BY Meetings.Id OFFSET @page ROWS FETCH NEXT @pageSize ROWS ONLY";
			const string sql2 = "SELECT COUNT(*) AS total FROM Meetings WHERE Deleted = 0";
			SqlParameter[] sqlparams =
			{
				new SqlParameter { ParameterName = "@page", SqlValue = rowStart, SqlDbType = SqlDbType.Int },
				new SqlParameter { ParameterName = "@pageSize", SqlValue = pageSize, SqlDbType = SqlDbType.Int }
			};


			var con = new SqlConnection(Connectionstring);
			var command = new SqlCommand(sql, con);
			try
			{
				con.Open();
				var myTrans = con.BeginTransaction(IsolationLevel.ReadUncommitted);
				command.Parameters.AddRange(sqlparams);
				command.Transaction = myTrans;
				using (var reader = command.ExecuteReader())
				{
					int titleCol = reader.GetOrdinal("Title");
					int startTimeCol = reader.GetOrdinal("StartTime");
					int endTimeCol = reader.GetOrdinal("EndTime");
					int descCol = reader.GetOrdinal("Description");
					int createdByCol = reader.GetOrdinal("CreatedByID");
					int contactCol = reader.GetOrdinal("ContactID");
					int idCol = reader.GetOrdinal("mId");
					int contactUserNameCol = reader.GetOrdinal("Contact");
					int createdByUserNameCol = reader.GetOrdinal("CreatedBy");

					while (reader.Read())
						meetings.Add(new Meeting
						{
							Title = GetDataSafe(reader, titleCol, reader.GetString),
							StartTime = (DateTime)GetDataSafe(reader, startTimeCol, reader.GetSqlDateTime),
							EndTime = (DateTime)GetDataSafe(reader, endTimeCol, reader.GetSqlDateTime),
							Description = GetDataSafe(reader, descCol, reader.GetString),
							CreatedBy = new User
							{
								Id = GetDataSafe(reader, createdByCol, reader.GetString),
								UserName = GetDataSafe(reader, createdByUserNameCol, reader.GetString)
							},
							Contact = new User
							{
								Id = GetDataSafe(reader, contactCol, reader.GetString),
								UserName = GetDataSafe(reader, contactUserNameCol, reader.GetString)
							},
							Id = GetDataSafe(reader, idCol, reader.GetInt32)
						});
				}

				command.CommandText = sql2;
				using (var reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						totalRows = (int)reader["total"];
					}
				}
				myTrans.Commit();
			}
			catch (Exception)
			{
				con.Close();
				throw new Exception();
			}
			finally
			{
				con.Close();
			}
			return new Page<Meeting>(totalRows, meetings, page, pageSize);
		}

		public Page<Meeting> ReadMeetingPageForUser(string userId, int? page, int? pageSize)
		{
			page = page ?? 1;
			pageSize = pageSize ?? 10;
			var meetings = new List<Meeting>();
			var totalRows = 0;
			var rowStart = ((page - 1) * pageSize);

			const string sql = "SELECT DISTINCT Meetings.Id mId, StartTime, EndTime, Title, Description, User1.UserName CreatedBy, User1.Id CreatedByID, User2.UserName Contact, User2.Id ContactID  FROM Meetings JOIN AspNetUsers User1 ON Meetings.CreatedByID = User1.Id JOIN AspNetUsers User2 ON Meetings.ContactID = User2.Id WHERE Deleted = 0 AND (Meetings.CreatedByID = @Id OR Meetings.ContactID = @Id) ORDER BY Meetings.Id OFFSET @page ROWS FETCH NEXT @pageSize ROWS ONLY";
			const string sql2 = "SELECT COUNT(*) AS total FROM Meetings WHERE Deleted = 0 AND (Meetings.CreatedByID = @Id OR Meetings.ContactID = @Id)";
			SqlParameter[] sqlparams =
			{
				new SqlParameter { ParameterName =  "@Id", SqlValue = userId, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter { ParameterName = "@page", SqlValue = rowStart, SqlDbType = SqlDbType.Int },
				new SqlParameter { ParameterName = "@pageSize", SqlValue = pageSize, SqlDbType = SqlDbType.Int }
			};


			var con = new SqlConnection(Connectionstring);
			var command = new SqlCommand(sql, con);
			try
			{
				con.Open();
				var myTrans = con.BeginTransaction(IsolationLevel.ReadUncommitted);
				command.Parameters.AddRange(sqlparams);
				command.Transaction = myTrans;
				using (var reader = command.ExecuteReader())
				{
					int titleCol = reader.GetOrdinal("Title");
					int startTimeCol = reader.GetOrdinal("StartTime");
					int endTimeCol = reader.GetOrdinal("EndTime");
					int descCol = reader.GetOrdinal("Description");
					int createdByCol = reader.GetOrdinal("CreatedByID");
					int contactCol = reader.GetOrdinal("ContactID");
					int idCol = reader.GetOrdinal("mId");
					int contactUserNameCol = reader.GetOrdinal("Contact");
					int createdByUserNameCol = reader.GetOrdinal("CreatedBy");

					while (reader.Read())
						meetings.Add(new Meeting
						{
							Title = GetDataSafe(reader, titleCol, reader.GetString),
							StartTime = (DateTime)GetDataSafe(reader, startTimeCol, reader.GetSqlDateTime),
							EndTime = (DateTime)GetDataSafe(reader, endTimeCol, reader.GetSqlDateTime),
							Description = GetDataSafe(reader, descCol, reader.GetString),
							CreatedBy = new User
							{
								Id = GetDataSafe(reader, createdByCol, reader.GetString),
								UserName = GetDataSafe(reader, createdByUserNameCol, reader.GetString)
							},
							Contact = new User
							{
								Id = GetDataSafe(reader, contactCol, reader.GetString),
								UserName = GetDataSafe(reader, contactUserNameCol, reader.GetString)
							},
							Id = GetDataSafe(reader, idCol, reader.GetInt32)
						});
				}

				command.CommandText = sql2;
				using (var reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						totalRows = (int)reader["total"];
					}
				}
				myTrans.Commit();
			}
			catch (Exception)
			{
				con.Close();
				throw new Exception();
			}
			finally
			{
				con.Close();
			}
			return new Page<Meeting>(totalRows, meetings, page, pageSize);
		}
	}
}
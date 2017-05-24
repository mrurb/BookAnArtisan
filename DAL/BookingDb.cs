using System;
using System.Collections.Generic;
using Model;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using static System.Int32;

namespace DAL
{
	public class BookingDb : IDataAccess<Booking>
	{
		private static readonly string Connectionstring = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
		public Booking Create(Booking t)
		{
			SqlParameter[] arrayOfParams =
			{
				new SqlParameter { ParameterName = "@starttime", SqlValue = t.StartTime, SqlDbType = SqlDbType.DateTime },
				new SqlParameter { ParameterName = "@endtime", SqlValue = t.EndTime, SqlDbType = SqlDbType.DateTime },
				new SqlParameter { ParameterName = "@userID", SqlValue = t.User.Id, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@materialID" , SqlValue = t.Item.Id, SqlDbType = SqlDbType.Int }
		   };
			var con = new SqlConnection(Connectionstring);
			const string query = "if not exists(SELECT StartTime, EndTime FROM Bookings WHERE (@starttime <= EndTime AND @endtime >= StartTime) AND @starttime < @endtime AND MaterialID = @materialID AND Bookings.Deleted = 0) BEGIN INSERT INTO Bookings(StartTime, EndTime, UserID, MaterialID) VALUES(@starttime, @endtime, @userID, @materialID) SELECT SCOPE_IDENTITY() END";
			var sqlcommand = new SqlCommand(query, con);


			try
			{
				con.Open();
				var myTrans = con.BeginTransaction(IsolationLevel.Serializable);
				sqlcommand.Transaction = myTrans;
				sqlcommand.Parameters.AddRange(arrayOfParams);
				t.Id = Parse(sqlcommand.ExecuteScalar().ToString());
				if (t.Id == 0)
				{
					throw new ApplicationException("Booking not created");
				}
				myTrans.Commit();
			}
			finally
			{
				con.Close();
			}
			return t;
		}

		public Booking Delete(Booking t)
		{
			const string sql = "UPDATE Bookings SET Deleted = 1 WHERE id = @id";
			var theparam = new SqlParameter { ParameterName = "@id", SqlValue = t.Id, SqlDbType = SqlDbType.Int };
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
			return null;
		}

		public Booking Read(Booking t)
		{
			const string sql = "SELECT Bookings.ID bookingID, Bookings.updated, Bookings.StartTime starttime, Bookings.Deleted deleted, Bookings.EndTime endtime, materials.ID materialID, materials.Name materialsname, materials.Description description,materials.Condition condition, users.ID userID, users.Email email, users.PhoneNumber phonenumber, users.UserName username, users.FirstName firstname, users.LastName lastname, users.Address address, materialOwner.Id ownerId, materialOwner.UserName ownerUserName, Bookings.Created FROM Bookings JOIN Materials_Unique materials ON Bookings.MaterialID = materials.ID JOIN AspNetUsers materialOwner ON materialOwner.Id = materials.OwnerID JOIN AspNetUsers users ON Bookings.UserID = users.Id WHERE bookings.ID = @id";
			Booking material = null;
			var theparam = new SqlParameter { ParameterName = "@id", SqlValue = t.Id, SqlDbType = SqlDbType.Int };
			using (var connection = new SqlConnection(Connectionstring))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add(theparam);
					command.Connection.Open();
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							material = new Booking
							{
								Id = (int)reader["bookingID"],
								EndTime = (DateTime)reader["endtime"],
								StartTime = (DateTime)reader["starttime"],
								Deleted = (bool)reader["deleted"],
								Updated = (DateTime)reader["Updated"],
								Created = (DateTime)reader["Created"],
								User = new User()
								{
									Id = reader["userID"].ToString(),
									FirstName = reader["firstname"].ToString(),
									LastName = reader["lastname"].ToString(),
									Email = reader["email"].ToString(),
									PhoneNumber = reader["phonenumber"].ToString(),
									Address = reader["address"].ToString(),
									UserName = reader["username"].ToString()
								},
								Item = new Material()
								{
									Id = (int)reader["materialID"],
									Name = reader["materialsname"].ToString(),
									Description = reader["description"].ToString(),
									Condition = reader["condition"].ToString(),
									Owner = new User()
									{
										Id = reader["ownerId"].ToString(),
										UserName = reader["ownerUsername"].ToString()
									}
								}
							};
						}
					}
				}
			}
			return material;
		}

		public List<Booking> ReadAll()
		{
			var materials = new List<Booking>();

			const string sql = "SELECT booking.updated, booking.ID bookingID, booking.StartTime starttime, booking.Deleted deleted, booking.EndTime endtime, materials.ID materialID, materials.Name materialsname, materials.Description description, materials.Condition condition, users.ID userID, users.Email email, users.PhoneNumber phonenumber, users.UserName username, users.FirstName firstname, users.LastName lastname, users.Address address FROM Bookings booking JOIN Materials_Unique materials ON booking.MaterialID = materials.ID JOIN AspNetUsers users ON booking.UserID = users.Id WHERE booking.deleted = 0";
			var con = new SqlConnection(Connectionstring);
			var command = new SqlCommand(sql, con);
			try
			{
				con.Open();
				var myTrans = con.BeginTransaction(IsolationLevel.ReadUncommitted);
				command.Transaction = myTrans;
				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						materials.Add(new Booking
						{
							Updated = (DateTime)reader["updated"],
							Id = (int)reader["bookingID"],
							EndTime = (DateTime)reader["endtime"],
							StartTime = (DateTime)reader["starttime"],
							Deleted = (bool)reader["deleted"],
							User = new User()
							{
								Id = reader["userID"].ToString(),
								FirstName = reader["firstname"].ToString(),
								LastName = reader["lastname"].ToString(),
								Email = reader["email"].ToString(),
								PhoneNumber = reader["phonenumber"].ToString(),
								Address = reader["address"].ToString(),
								UserName = reader["username"].ToString()
							},
							Item = new Material()
							{
								Id = (int)reader["materialID"],
								Name = reader["materialsname"].ToString(),
								Description = reader["description"].ToString(),
								Condition = reader["condition"].ToString(),
							}
						});
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
			return materials;
		}

		public Booking Update(Booking t)
		{
			const string sql = "if not exists(SELECT StartTime, EndTime FROM Bookings WHERE (@starttime <= EndTime AND @endtime >= StartTime) AND @starttime < @endtime AND MaterialID = @materialID AND Deleted = 0 AND Bookings.ID <> @id) BEGIN UPDATE Bookings SET StartTime = @starttime, EndTime = @endtime, UserID = @userID, Updated = GETUTCDATE() WHERE Bookings.ID = @id AND Bookings.Updated = @Updated END";

			SqlParameter[] sqlparams =
			{
				new SqlParameter { ParameterName = "@userID", SqlValue = t.User.Id, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@starttime", SqlValue = t.StartTime, SqlDbType = SqlDbType.DateTime },
				new SqlParameter { ParameterName = "@endtime", SqlValue = t.EndTime, SqlDbType = SqlDbType.DateTime },
				new SqlParameter { ParameterName = "@materialID", SqlValue = t.Item.Id, SqlDbType = SqlDbType.Int },
				new SqlParameter { ParameterName = "@updated", SqlValue = t.Updated, SqlDbType = SqlDbType.DateTime },
				new SqlParameter { ParameterName = "@id", SqlValue = t.Id, SqlDbType = SqlDbType.Int }
			};
			var con = new SqlConnection(Connectionstring);
			var sqlcommand = new SqlCommand(sql, con);
			con.Open();
			var myTrans = con.BeginTransaction(IsolationLevel.ReadCommitted);
			try
			{

				sqlcommand.Transaction = myTrans;
				sqlcommand.Parameters.AddRange(sqlparams);

				int rowsaffected = sqlcommand.ExecuteNonQuery();
				if (rowsaffected < 1)
				{
					throw new Exception("Concurrency problem");
				}
				myTrans.Commit();
			}
			catch (Exception ex)
			{
				myTrans.Rollback();
				throw new Exception("failed", ex);

			}
			finally
			{
				con.Close();
			}
			return t;
		}

		public Booking RemoveBooking(Booking booking)
		{
			const string sql = "DELETE FROM Bookings WHERE id = @id";
			var theparam = new SqlParameter { ParameterName = "@id", SqlValue = booking.Id, SqlDbType = SqlDbType.Int };
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
			return booking;
		}

		public Page<Booking> ReadPage(string userId, int? page, int? pageSize)
		{
			page = page ?? 1;
			pageSize = pageSize ?? 10;
			var materials = new List<Booking>();
			var totalRows = 0;
			var rowStart = (((page - 1) * pageSize) + 1);

			const string sql = "SELECT booking.updated, booking.ID bookingID, booking.StartTime starttime, booking.Deleted deleted, booking.EndTime endtime, materials.ID materialID, materials.Name materialsname, materials.Description description, materials.Condition condition, users.ID userID, users.Email email, users.PhoneNumber phonenumber, users.UserName username, users.FirstName firstname, users.LastName lastname, users.Address address FROM Bookings booking JOIN Materials_Unique materials ON booking.MaterialID = materials.ID JOIN AspNetUsers users ON booking.UserID = users.Id WHERE booking.deleted = 0 AND booking.UserId = @UserId ORDER BY booking.starttime OFFSET @page ROWS FETCH NEXT @pageSize ROWS ONLY";
			const string sql2 = "SELECT COUNT(*) AS total FROM Bookings WHERE Deleted = 0 AND UserId = @UserId";
			SqlParameter[] sqlparams =
			{
				new SqlParameter { ParameterName =  "@UserId", SqlValue = userId, SqlDbType = SqlDbType.NVarChar},
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
					while (reader.Read())
					{
						materials.Add(new Booking
						{
							Updated = (DateTime)reader["updated"],
							Id = (int)reader["bookingID"],
							EndTime = (DateTime)reader["endtime"],
							StartTime = (DateTime)reader["starttime"],
							Deleted = (bool)reader["deleted"],
							User = new User()
							{
								Id = reader["userID"].ToString(),
								FirstName = reader["firstname"].ToString(),
								LastName = reader["lastname"].ToString(),
								Email = reader["email"].ToString(),
								PhoneNumber = reader["phonenumber"].ToString(),
								Address = reader["address"].ToString(),
								UserName = reader["username"].ToString()
							},
							Item = new Material()
							{
								Id = (int)reader["materialID"],
								Name = reader["materialsname"].ToString(),
								Description = reader["description"].ToString(),
								Condition = reader["condition"].ToString(),
							}
						});
					}
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
			var pages = new Page<Booking>(totalRows, materials, page, pageSize);
			return pages;
		}

		public Page<Booking> ReadPage(int? page, int? pageSize)
		{
			page = page ?? 1;
			pageSize = pageSize ?? 10;
			var materials = new List<Booking>();
			var totalRows = 0;
			var rowStart = (((page - 1) * pageSize) + 1);

			const string sql = "SELECT booking.updated, booking.ID bookingID, booking.StartTime starttime, booking.Deleted deleted, booking.EndTime endtime, materials.ID materialID, materials.Name materialsname, materials.Description description, materials.Condition condition, users.ID userID, users.Email email, users.PhoneNumber phonenumber, users.UserName username, users.FirstName firstname, users.LastName lastname, users.Address address FROM Bookings booking JOIN Materials_Unique materials ON booking.MaterialID = materials.ID JOIN AspNetUsers users ON booking.UserID = users.Id WHERE booking.deleted = 0 ORDER BY booking.starttime OFFSET @page ROWS FETCH NEXT @pageSize ROWS ONLY";
			const string sql2 = "SELECT COUNT(*) AS total FROM Bookings WHERE Deleted = 0";
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
					while (reader.Read())
					{
						materials.Add(new Booking
						{
							Updated = (DateTime)reader["updated"],
							Id = (int)reader["bookingID"],
							EndTime = (DateTime)reader["endtime"],
							StartTime = (DateTime)reader["starttime"],
							Deleted = (bool)reader["deleted"],
							User = new User()
							{
								Id = reader["userID"].ToString(),
								FirstName = reader["firstname"].ToString(),
								LastName = reader["lastname"].ToString(),
								Email = reader["email"].ToString(),
								PhoneNumber = reader["phonenumber"].ToString(),
								Address = reader["address"].ToString(),
								UserName = reader["username"].ToString()
							},
							Item = new Material()
							{
								Id = (int)reader["materialID"],
								Name = reader["materialsname"].ToString(),
								Description = reader["description"].ToString(),
								Condition = reader["condition"].ToString(),
							}
						});
					}
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
			var pages = new Page<Booking>(totalRows, materials, page, pageSize);
			return pages;
		}
	}
}

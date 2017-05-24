using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using Model;

namespace DAL
{
	/*
	 * Note that no exception handling has been made for these functions 
	 * this is mainly due to the fact that this class remains mostly unused.
	 */
	public class UserDb : IDataAccess<User>
	{
		private readonly string connectionString;

		public UserDb()
		{
			connectionString = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
		}

		public User Create(User user)
		{
			int emailConfirmed = Convert.ToInt32(user.EmailConfirmed);
			int phoneNumberConfirmed = Convert.ToInt32(user.PhoneNumberConfirmed);
			int twoFactorEnabled = Convert.ToInt32(user.TwoFactorEnabled);
			int lockoutEnabled = Convert.ToInt32(user.LockoutEnabled);

			const string sql = "INSERT INTO AspNetUsers VALUES (@Id, @Email, @EmailConfirmed, @PasswordHash, @SecurityStamp, @PhoneNumber, " +
			                   "@PhoneNumberConfirmed, @TwoFactorEnabled, @LockoutEndDateUtc, @LockoutEnabled, @AccessFailedCount, @UserName, @FirstName, " +
			                   "@LastName, @Phone, @Address, @ApiKey) SELECT SCOPE_IDENTITY()";

			SqlParameter[] arrayOfParameters =
			{
				new SqlParameter {ParameterName = "@Id", SqlValue = user.Id, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@Email", SqlValue = user.Email, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@EmailConfirmed", SqlValue = emailConfirmed, SqlDbType = SqlDbType.Bit},
				new SqlParameter {ParameterName = "@PasswordHash", SqlValue = user.PasswordHash, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@SecurityStamp", SqlValue = user.SecurityStamp, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@PhoneNumber", SqlValue = user.PhoneNumber, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter
				{
					ParameterName = "@PhoneNumberConfirmed",
					SqlValue = phoneNumberConfirmed,
					SqlDbType = SqlDbType.Bit
				},
				new SqlParameter {ParameterName = "@TwoFactorEnabled", SqlValue = twoFactorEnabled, SqlDbType = SqlDbType.Bit},
				new SqlParameter
				{
					ParameterName = "@LockoutEndDateUtc",
					SqlValue = user.LockoutEndDateUtc,
					SqlDbType = SqlDbType.DateTime
				},
				new SqlParameter {ParameterName = "@LockoutEnabled", SqlValue = lockoutEnabled, SqlDbType = SqlDbType.Bit},
				new SqlParameter
				{
					ParameterName = "@AccessFailedCount",
					SqlValue = user.AccessFailedCount,
					SqlDbType = SqlDbType.Int
				},
				new SqlParameter {ParameterName = "@UserName", SqlValue = user.UserName, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@FirstName", SqlValue = user.FirstName, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@LastName", SqlValue = user.LastName, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@Address", SqlValue = user.Address, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@ApiKey", SqlValue = user.ApiKey, SqlDbType = SqlDbType.NVarChar}
			};

			try
			{
				using (var connection = new SqlConnection(connectionString))
				{
					using (var command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddRange(arrayOfParameters);
						command.Connection.Open();
						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception e)
			{
				throw new FaultException<Exception>(e, new FaultReason(e.Message), new FaultCode("Sender"));
			}

			return user;
		}

		public User Read(User user)
		{
			const string sql = "SELECT * FROM AspNetUsers WHERE ID = @Id";

			var idParameter = new SqlParameter {ParameterName = "@Id", SqlValue = user.Id, SqlDbType = SqlDbType.NVarChar};

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add(idParameter);
					command.Connection.Open();

					using (var reader = command.ExecuteReader())
					{
						if (!reader.HasRows) return user;
						int idCol = reader.GetOrdinal("ID");
						int emailCol = reader.GetOrdinal("Email");
						int emailConfirmedCol = reader.GetOrdinal("EmailConfirmed");
						int passwordHashCol = reader.GetOrdinal("PasswordHash");
						int securityStampCol = reader.GetOrdinal("SecurityStamp");
						int phoneNumberCol = reader.GetOrdinal("PhoneNumber");
						int phoneNumberConfirmedCol = reader.GetOrdinal("PhoneNumberConfirmed");
						int twoFactorEnabledCol = reader.GetOrdinal("TwoFactorEnabled");
						int lockoutEndDateUtcCol = reader.GetOrdinal("LockoutEndDateUtc");
						int lockoutEnabledCol = reader.GetOrdinal("LockoutEnabled");
						int accessFailedCountCol = reader.GetOrdinal("AccessFailedCount");
						int userNameCol = reader.GetOrdinal("UserName");
						int firstNameCol = reader.GetOrdinal("FirstName");
						int lastNameCol = reader.GetOrdinal("LastName");
						int addressCol = reader.GetOrdinal("Address");
						int apiKeyCol = reader.GetOrdinal("ApiKey");

						if (!reader.Read()) return user;
						user.Id = GetDataSafe(reader, idCol, reader.GetString);
						user.Email = GetDataSafe(reader, emailCol, reader.GetString);
						user.EmailConfirmed = GetDataSafe(reader, emailConfirmedCol, reader.GetBoolean);
						user.PasswordHash = GetDataSafe(reader, passwordHashCol, reader.GetString);
						user.SecurityStamp = GetDataSafe(reader, securityStampCol, reader.GetString);
						user.PhoneNumber = GetDataSafe(reader, phoneNumberCol, reader.GetString);
						user.PhoneNumberConfirmed = GetDataSafe(reader, phoneNumberConfirmedCol, reader.GetBoolean);
						user.TwoFactorEnabled = GetDataSafe(reader, twoFactorEnabledCol, reader.GetBoolean);
						user.LockoutEndDateUtc = GetDataSafe(reader, lockoutEndDateUtcCol, reader.GetDateTime);
						user.LockoutEnabled = GetDataSafe(reader, lockoutEnabledCol, reader.GetBoolean);
						user.AccessFailedCount = GetDataSafe(reader, accessFailedCountCol, reader.GetInt32);
						user.UserName = GetDataSafe(reader, userNameCol, reader.GetString);
						user.FirstName = GetDataSafe(reader, firstNameCol, reader.GetString);
						user.LastName = GetDataSafe(reader, lastNameCol, reader.GetString);
						user.Address = GetDataSafe(reader, addressCol, reader.GetString);
						user.ApiKey = GetDataSafe(reader, apiKeyCol, reader.GetString);
					}
				}
			}
			return user;
		}

		public User Update(User user)
		{
			const string sql = "UPDATE AspNetUsers SET FirstName = @FirstName, LastName = @LastName, Address = @Address, ApiKey = @ApiKey, Email = @Email WHERE ID = @Id";

			SqlParameter[] arrayOfParameters =
			{
				new SqlParameter {ParameterName = "@Id", SqlValue = user.Id, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@Email", SqlValue = user.Email, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@FirstName", SqlValue = user.FirstName, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@LastName", SqlValue = user.LastName, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@Address", SqlValue = user.Address, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@ApiKey", SqlValue = user.ApiKey, SqlDbType = SqlDbType.NVarChar}
			};

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddRange(arrayOfParameters);
					command.Connection.Open();

					int affectedRows = command.ExecuteNonQuery();
					if (affectedRows < 1)
						throw new Exception("No rows affected. Update failed - Does the object exist beforehand in the database?");
				}
			}
			return user;
		}

		public User Delete(User user)
		{
			const string sql = "UPDATE AspNetUsers SET Deleted = 1 WHERE ID = @Id";

			var idParameter = new SqlParameter {ParameterName = "@Id", SqlValue = user.Id, SqlDbType = SqlDbType.NVarChar};

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					try
					{
						command.Parameters.Add(idParameter);
						command.Connection.Open();
						int affectedRows = command.ExecuteNonQuery();
						if (affectedRows < 1)
							return user;
					}
					catch (Exception)
					{
						throw new Exception();
					}
				}
			}

			return user;
		}

		public List<User> ReadAll()
		{
			var users = new List<User>();

			const string sql = "SELECT * FROM AspNetUsers";

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Connection.Open();

					using (var reader = command.ExecuteReader())
					{
						if (!reader.HasRows) return users;
						int idCol = reader.GetOrdinal("ID");
						int emailCol = reader.GetOrdinal("Email");
						int passwordHashCol = reader.GetOrdinal("PasswordHash");
						int phoneNumberCol = reader.GetOrdinal("PhoneNumber");
						int firstNameCol = reader.GetOrdinal("FirstName");
						int lastNameCol = reader.GetOrdinal("LastName");
						int addressCol = reader.GetOrdinal("Address");

						while (reader.Read())
							users.Add(new User
							(
								GetDataSafe(reader, idCol, reader.GetString),
								GetDataSafe(reader, firstNameCol, reader.GetString),
								GetDataSafe(reader, lastNameCol, reader.GetString),
								GetDataSafe(reader, emailCol, reader.GetString),
								GetDataSafe(reader, passwordHashCol, reader.GetString),
								GetDataSafe(reader, phoneNumberCol, reader.GetString),
								GetDataSafe(reader, addressCol, reader.GetString)
							));
					}
				}
			}
			return users;
		}

		public IList<User> SearchByName(string name)
		{
			if (name == null) return new List<User>();
			IList<User> list = new List<User>();

			const string sql = "SELECT ID, FirstName, LastName, UserName FROM AspNetUsers WHERE FirstName LIKE '%' + @name + '%' OR LastName LIKE '%' + @name + '%' OR UserName LIKE '%' + @name + '%'";

			var searchParams = new SqlParameter {ParameterName = "@name", SqlValue = name, SqlDbType = SqlDbType.NVarChar};

			using (var conn = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand(sql, conn))
				{
					command.Parameters.Add(searchParams);
					command.Connection.Open();

					using (var reader = command.ExecuteReader())
					{
						if (!reader.HasRows) return list;
						int idCol = reader.GetOrdinal("ID");
						int firstNameCol = reader.GetOrdinal("FirstName");
						int lastNameCol = reader.GetOrdinal("LastName");
						int userNameCol = reader.GetOrdinal("UserName");

						while (reader.Read())
							list.Add(
								new User
								{
									Id = GetDataSafe(reader, idCol, reader.GetString),
									FirstName = GetDataSafe(reader, firstNameCol, reader.GetString),
									LastName = GetDataSafe(reader, lastNameCol, reader.GetString),
									UserName = GetDataSafe(reader, userNameCol, reader.GetString)
								}
							);
					}
				}
			}

			return list;
		}

		public T GetDataSafe<T>(SqlDataReader reader, int columnIndex, Func<int, T> getData)
		{
			return !reader.IsDBNull(columnIndex) ? getData(columnIndex) : default(T);
		}
	}
}
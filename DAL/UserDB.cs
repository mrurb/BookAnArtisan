using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;

namespace DAL
{
	public class UserDB : IDataAccess<User>
	{
		private string connectionString;
		public UserDB()
		{
			connectionString = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
		}

		public User Create(User user)
		{
			int EmailConfirmed = Convert.ToInt32(user.EmailConfirmed);
			int PhoneNumberConfirmed = Convert.ToInt32(user.PhoneNumberConfirmed);
			int TwoFactorEnabled = Convert.ToInt32(user.TwoFactorEnabled);
			int LockoutEnabled = Convert.ToInt32(user.LockoutEnabled);

			string sql = "INSERT INTO AspNetUsers VALUES (@Id, @Email, @EmailConfirmed, @PasswordHash, @SecurityStamp, @PhoneNumber, " +
				"@PhoneNumberConfirmed, @TwoFactorEnabled, @LockoutEndDateUtc, @LockoutEnabled, @AccessFailedCount, @UserName, @FirstName, " +
				"@LastName, @Phone, @Address, @ApiKey) SELECT SCOPE_IDENTITY()";

			SqlParameter[] arrayOfParameters =
			{
				new SqlParameter { ParameterName = "@Id", SqlValue = user.Id, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@Email", SqlValue = user.Email, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@EmailConfirmed", SqlValue = EmailConfirmed, SqlDbType = SqlDbType.Bit },
				new SqlParameter { ParameterName = "@PasswordHash", SqlValue = user.PasswordHash, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@SecurityStamp", SqlValue = user.SecurityStamp, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@PhoneNumber", SqlValue = user.PhoneNumber, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@PhoneNumberConfirmed", SqlValue = PhoneNumberConfirmed, SqlDbType = SqlDbType.Bit },
				new SqlParameter { ParameterName = "@TwoFactorEnabled", SqlValue = TwoFactorEnabled, SqlDbType = SqlDbType.Bit },
				new SqlParameter { ParameterName = "@LockoutEndDateUtc", SqlValue = user.LockoutEndDateUtc, SqlDbType = SqlDbType.DateTime },
				new SqlParameter { ParameterName = "@LockoutEnabled", SqlValue = LockoutEnabled, SqlDbType = SqlDbType.Bit },
				new SqlParameter { ParameterName = "@AccessFailedCount", SqlValue = user.AccessFailedCount, SqlDbType = SqlDbType.Int },
				new SqlParameter { ParameterName = "@UserName", SqlValue = user.UserName, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@FirstName", SqlValue = user.FirstName, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@LastName", SqlValue = user.LastName, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@Address", SqlValue = user.Address, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@ApiKey", SqlValue = user.ApiKey, SqlDbType = SqlDbType.NVarChar },
			};

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddRange(arrayOfParameters);
						command.Connection.Open();
						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception e)
			{
				throw new FaultException<Exception>(e,new FaultReason(e.Message), new FaultCode("Sender"));
			}

			return user;
		}

		public IList<User> SearchByName(string name)
		{
			if (name == null) {
				return new List<User>();
			}
			IList<User> list = new List<User>();

			string sql = "SELECT ID, FirstName, LastName, UserName FROM AspNetUsers WHERE FirstName LIKE '%' + @name + '%' OR LastName LIKE '%' + @name + '%' OR UserName LIKE '%' + @name + '%'";

			SqlParameter searchParams = new SqlParameter { ParameterName = "@name", SqlValue = name, SqlDbType = SqlDbType.NVarChar };

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				using (SqlCommand command = new SqlCommand(sql, conn))
				{
					command.Parameters.Add(searchParams);
					command.Connection.Open();

					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.HasRows)
						{
							var idCol = reader.GetOrdinal("ID");
							var firstNameCol = reader.GetOrdinal("FirstName");
							var lastNameCol = reader.GetOrdinal("LastName");
							var userNameCol = reader.GetOrdinal("UserName");

							while (reader.Read())
							{
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
				}
			}

			return list;
		}

		public User Read(User user)
		{
			string sql = "SELECT * FROM AspNetUsers WHERE ID = @Id";

			SqlParameter idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = user.Id, SqlDbType = SqlDbType.NVarChar };

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add(idParameter);
					command.Connection.Open();
					// Add exceptionhandling
					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.HasRows)
						{
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

							if (reader.Read())
							{
								user.Id = GetDataSafe<string>(reader, idCol, reader.GetString);
								user.Email = GetDataSafe<string>(reader, emailCol, reader.GetString);
								user.EmailConfirmed = GetDataSafe<bool>(reader, emailConfirmedCol, reader.GetBoolean);
								user.PasswordHash = GetDataSafe<string>(reader, passwordHashCol, reader.GetString);
								user.SecurityStamp = GetDataSafe<string>(reader, securityStampCol, reader.GetString);
								user.PhoneNumber = GetDataSafe<string>(reader, phoneNumberCol, reader.GetString);
								user.PhoneNumberConfirmed = GetDataSafe<bool>(reader, phoneNumberConfirmedCol, reader.GetBoolean);
								user.TwoFactorEnabled = GetDataSafe<bool>(reader, twoFactorEnabledCol, reader.GetBoolean);
								user.LockoutEndDateUtc = GetDataSafe<DateTime>(reader, lockoutEndDateUtcCol, reader.GetDateTime);
								user.LockoutEnabled = GetDataSafe<bool>(reader, lockoutEnabledCol, reader.GetBoolean);
								user.AccessFailedCount = GetDataSafe<int>(reader, accessFailedCountCol, reader.GetInt32);
								user.UserName = GetDataSafe<string>(reader, userNameCol, reader.GetString);
								user.FirstName = GetDataSafe<string>(reader, firstNameCol, reader.GetString);
								user.LastName = GetDataSafe<string>(reader, lastNameCol, reader.GetString);
								user.Address = GetDataSafe<string>(reader, addressCol, reader.GetString);
								user.ApiKey = GetDataSafe<string>(reader, apiKeyCol, reader.GetString);
							}
						}
					}
				}
			}
			return user;
		}

		public User Update(User user)
		{

			string sql = "UPDATE AspNetUsers SET FirstName = @FirstName, LastName = @LastName, Address = @Address, ApiKey = @ApiKey, Email = @Email WHERE ID = @Id";

			SqlParameter[] arrayOfParameters =
			{
				new SqlParameter { ParameterName = "@Id", SqlValue = user.Id, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@Email", SqlValue = user.Email, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@FirstName", SqlValue = user.FirstName, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@LastName", SqlValue = user.LastName, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@Address", SqlValue = user.Address, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@ApiKey", SqlValue = user.ApiKey, SqlDbType = SqlDbType.NVarChar },
			};

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddRange(arrayOfParameters);
					command.Connection.Open();
					// Add exceptionhandling 
					int affectedRows = command.ExecuteNonQuery();
					if (affectedRows < 1)
					{
						throw new System.Exception("No rows affected. Update failed - Does the object exist beforehand in the database?");
					}
				}
			}
			return user;
		}

		public User Delete(User user)
		{
			int bitRepresentationOfBool = Convert.ToInt32(true);

			string sql = "UPDATE AspNetUsers SET Deleted = 1 WHERE ID = @Id";

			SqlParameter idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = user.Id, SqlDbType = SqlDbType.NVarChar };

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					try
					{
						command.Parameters.Add(idParameter);
						command.Connection.Open();
						int affectedRows = command.ExecuteNonQuery();
						if (affectedRows < 1)
						{
							return user;
						}
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
			List<User> users = new List<User>();

			string sql = "SELECT * FROM AspNetUsers";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Connection.Open();
					// Add exceptionhandling
					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.HasRows)
						{
							int IdCol = reader.GetOrdinal("ID");
							int EmailCol = reader.GetOrdinal("Email");
							int EmailConfirmedCol = reader.GetOrdinal("EmailConfirmed");
							int PasswordHashCol = reader.GetOrdinal("PasswordHash");
							int SecurityStampCol = reader.GetOrdinal("SecurityStamp");
							int PhoneNumberCol = reader.GetOrdinal("PhoneNumber");
							int PhoneNumberConfirmedCol = reader.GetOrdinal("PhoneNumberConfirmed");
							int TwoFactorEnabledCol = reader.GetOrdinal("TwoFactorEnabled");
							int LockoutEndDateUtcCol = reader.GetOrdinal("LockoutEndDateUtc");
							int LockoutEnabledCol = reader.GetOrdinal("LockoutEnabled");
							int AccessFailedCountCol = reader.GetOrdinal("AccessFailedCount");
							int UserNameCol = reader.GetOrdinal("UserName");
							int FirstNameCol = reader.GetOrdinal("FirstName");
							int LastNameCol = reader.GetOrdinal("LastName");
							int PhoneCol = reader.GetOrdinal("Phone");
							int AddressCol = reader.GetOrdinal("Address");
							int ApiKeyCol = reader.GetOrdinal("ApiKey");

							while (reader.Read())
							{
								users.Add(new User
								(
									GetDataSafe<string>(reader, IdCol, reader.GetString),
									GetDataSafe<string>(reader, FirstNameCol, reader.GetString),
									GetDataSafe<string>(reader, LastNameCol, reader.GetString),
									GetDataSafe<string>(reader, EmailCol, reader.GetString),
									GetDataSafe<string>(reader, PasswordHashCol, reader.GetString),
									GetDataSafe<string>(reader, PhoneNumberCol, reader.GetString),
									GetDataSafe<string>(reader, AddressCol, reader.GetString)
								));
							}
						}
					}
				}
			}
			return users;
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

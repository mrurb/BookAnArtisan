using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
	public class UserRoleDb : IDataAccess<Role>
	{
		private readonly string connectionString;

		public UserRoleDb()
		{
			connectionString = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
		}

		public Role Create(Role role)
		{
			const string sql = "INSERT INTO AspNetRoles VALUES(@Id, @Name)";

			SqlParameter[] arrayOfParameters =
			{
				new SqlParameter {ParameterName = "@Id", SqlValue = role.Id, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@Name", SqlValue = role.Name, SqlDbType = SqlDbType.NVarChar}
			};

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddRange(arrayOfParameters);
					command.Connection.Open();
					role.Id = Convert.ToString(command.ExecuteScalar());
				}
			}

			return role;
		}

		public Role Read(Role role)
		{
			const string sql = "SELECT Name FROM AspNetRoles WHERE Id = @Id";

			var idParameter = new SqlParameter {ParameterName = "@Id", SqlValue = role.Id, SqlDbType = SqlDbType.NVarChar};

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add(idParameter);
					command.Connection.Open();

					using (var reader = command.ExecuteReader())
					{
						int nameCol = reader.GetOrdinal("Name");

						if (reader.Read())
							role.Name = reader.GetString(nameCol);
					}
				}
			}

			return role;
		}

		public Role Update(Role role)
		{
			const string sql = "UPDATE AspNetRoles SET Name = @Name WHERE Id = @Id";

			SqlParameter[] arrayOfParameters =
			{
				new SqlParameter {ParameterName = "@Id", SqlValue = role.Id, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@Name", SqlValue = role.Name, SqlDbType = SqlDbType.NVarChar}
			};
			var con = new SqlConnection(connectionString);
			var sqlcommand = new SqlCommand(sql, con);
			con.Open();
			var myTrans = con.BeginTransaction(IsolationLevel.RepeatableRead);
			sqlcommand.Parameters.AddRange(arrayOfParameters);
			sqlcommand.Connection.Open();
			sqlcommand.Transaction = myTrans;
			int affectedRows = sqlcommand.ExecuteNonQuery();
			if (affectedRows < 1)
				throw new Exception("No rows affected");
			myTrans.Commit();
			return role;
		}

		public Role Delete(Role role)
		{
			const string sql = "DELETE FROM AspNetRoles WHERE Id = @Id";

			var idParameter = new SqlParameter {ParameterName = "@Id", SqlValue = role.Id, SqlDbType = SqlDbType.NVarChar};

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add(idParameter);
					command.Connection.Open();
					int affectedRows = command.ExecuteNonQuery();
					if (affectedRows < 1)
						return role;
				}
			}

			return role;
		}

		public List<Role> ReadAll()
		{
			var roles = new List<Role>();

			const string sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; BEGIN TRANSACTION SELECT * FROM AspNetRoles; COMMIT";

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Connection.Open();

					using (var reader = command.ExecuteReader())
					{
						if (!reader.HasRows) return roles;
						int idCol = reader.GetOrdinal("Id");
						int nameCol = reader.GetOrdinal("Name");

						while (reader.Read())
							roles.Add(new Role
							{
								Id = reader.GetString(idCol),
								Name = reader.GetString(nameCol)
							});
					}
				}
			}
			return roles;
		}
	}
}
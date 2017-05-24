using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
	public class StatusDb : IDataAccess<Status>
	{
		private readonly string connectionString;

		public StatusDb()
		{
			connectionString = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
		}

		public Status Create(Status status)
		{
			//check if it exist ....????
			const string sql = "INSERT INTO Project_status VALUES(@Name) SELECT SCOPE_IDENTITY()";

			SqlParameter[] arrayOfParameters =
			{
				new SqlParameter {ParameterName = "@Name", SqlValue = status.Name, SqlDbType = SqlDbType.NVarChar}
			};

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddRange(arrayOfParameters);
					command.Connection.Open();
					status.Id = Convert.ToInt32(command.ExecuteScalar());
				}
			}

			return status;
		}

		public Status Read(Status status)
		{
			const string sql = "SELECT Name FROM Project_status WHERE ID = @Id";

			var idParameter = new SqlParameter {ParameterName = "@Id", SqlValue = status.Id, SqlDbType = SqlDbType.Int};

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
							status.Name = reader.GetString(nameCol);
					}
				}
			}

			return status;
		}

		public Status Update(Status status)
		{
			const string sql = "UPDATE Project_status SET Name = @Name WHERE ID = @Id";

			SqlParameter[] arrayOfParameters =
			{
				new SqlParameter {ParameterName = "@Id", SqlValue = status.Id, SqlDbType = SqlDbType.Int},
				new SqlParameter {ParameterName = "@Name", SqlValue = status.Name, SqlDbType = SqlDbType.NVarChar}
			};
			var con = new SqlConnection(connectionString);
			var sqlcommand = new SqlCommand(sql, con);
			con.Open();
			var myTrans = con.BeginTransaction(IsolationLevel.RepeatableRead);

			sqlcommand.Parameters.AddRange(arrayOfParameters);
			sqlcommand.Transaction = myTrans;
			int affectedRows = sqlcommand.ExecuteNonQuery();
			if (affectedRows < 1)
				throw new Exception("No rows affected");
			myTrans.Commit();
			return status;
		}

		public Status Delete(Status status)
		{
			const string sql = "DELETE FROM Project_status WHERE ID = @Id";
			var idParameter = new SqlParameter {ParameterName = "@Id", SqlValue = status.Id, SqlDbType = SqlDbType.NVarChar};

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add(idParameter);
					command.Connection.Open();
					int affectedRows = command.ExecuteNonQuery();
					if (affectedRows < 1)
						return status;
				}
			}

			return status;
		}

		public List<Status> ReadAll()
		{
			var status = new List<Status>();

			const string sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; BEGIN TRANSACTION SELECT * FROM Project_status; COMMIT";

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Connection.Open();

					using (var reader = command.ExecuteReader())
					{
						if (!reader.HasRows) return status;
						int idCol = reader.GetOrdinal("Id");
						int nameCol = reader.GetOrdinal("Name");

						while (reader.Read())
							status.Add(new Status
							{
								Id = reader.GetInt32(idCol),
								Name = reader.GetString(nameCol)
							});
					}
				}
			}
			return status;
		}
	}
}
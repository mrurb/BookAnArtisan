using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
	public class TagDb : IDataAccess<Tag>
	{
		private readonly string connectionString;

		public TagDb()
		{
			connectionString = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
		}

		public Tag Create(Tag tag)
		{
			const string sql = "INSERT INTO Tags VALUES ID = @Id, Name = @Name SELECT";

			SqlParameter[] arrayOfParameters =
			{
				new SqlParameter {ParameterName = "@Id", SqlValue = tag.Id, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@Name", SqlValue = tag.Name, SqlDbType = SqlDbType.NVarChar}
			};

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddRange(arrayOfParameters);
					command.Connection.Open();
					tag.Id = Convert.ToString(command.ExecuteScalar());
				}
			}

			return tag;
		}


		public Tag Read(Tag tag)
		{
			const string sql = "SELECT Name FROM Tags WHERE ID = @Id";

			var idParameter = new SqlParameter {ParameterName = "@Id", SqlValue = tag.Id, SqlDbType = SqlDbType.NVarChar};

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
							tag.Name = reader.GetString(nameCol);
					}
				}
			}

			return tag;
		}

		public Tag Update(Tag tag)
		{
			const string sql = "UPDATE Tags SET Name = @Name WHERE ID = @Id";

			SqlParameter[] arrayOfParameters =
			{
				new SqlParameter {ParameterName = "@Id", SqlValue = tag.Name, SqlDbType = SqlDbType.NVarChar},
				new SqlParameter {ParameterName = "@Name", SqlValue = tag.Name, SqlDbType = SqlDbType.NVarChar}
			};
			var con = new SqlConnection(connectionString);
			var sqlcommand = new SqlCommand(sql, con);
			con.Open();
			var myTrans = con.BeginTransaction(IsolationLevel.RepeatableRead);
			sqlcommand.Parameters.AddRange(arrayOfParameters);
			sqlcommand.Transaction = myTrans;
			int affectedRows = sqlcommand.ExecuteNonQuery();
			if (affectedRows < 1)
				throw new Exception("No rows affected. Update failed - Does the object exist beforehand in the database?");
			myTrans.Commit();
			return tag;
		}

		public Tag Delete(Tag tag)
		{
			const string sql = "DELETE FROM Tags WHERE ID = @Id";

			var idParameter = new SqlParameter {ParameterName = "@Id", SqlValue = tag.Id, SqlDbType = SqlDbType.NVarChar};

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add(idParameter);
					command.Connection.Open();
					int affectedRows = command.ExecuteNonQuery();
					if (affectedRows < 1)
						return tag;
				}
			}

			return tag;
		}

		public List<Tag> ReadAll()
		{
			var tags = new List<Tag>();

			const string sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; BEGIN TRANSACTION SELECT * FROM AspNetRoles; COMMIT";

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					using (var reader = command.ExecuteReader())
					{
						if (!reader.HasRows) return tags;
						int idCol = reader.GetOrdinal("ID");
						int nameCol = reader.GetOrdinal("Name");

						while (reader.Read())
							tags.Add(new Tag
							{
								Id = reader.GetString(idCol),
								Name = reader.GetString(nameCol)
							});
					}
				}
			}
			return tags;
		}
	}
}
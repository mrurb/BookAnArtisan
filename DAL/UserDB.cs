using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class UserDB : IDataAccess<User>
    {
        private string connectionString;
        public UserDB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["testConnection"].ConnectionString;
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

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(arrayOfParameters);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            return user;
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

                            if (reader.Read())
                            {
                                user.Id = GetDataSafe<string>(reader, IdCol, reader.GetString);
                                user.Email = GetDataSafe<string>(reader, EmailCol, reader.GetString);
                                user.EmailConfirmed = GetDataSafe<bool>(reader, EmailConfirmedCol, reader.GetBoolean);
                                user.PasswordHash = GetDataSafe<string>(reader, PasswordHashCol, reader.GetString);
                                user.SecurityStamp = GetDataSafe<string>(reader, SecurityStampCol, reader.GetString);
                                user.PhoneNumber = GetDataSafe<string>(reader, PhoneNumberCol, reader.GetString);
                                user.PhoneNumberConfirmed = GetDataSafe<bool>(reader, PhoneNumberConfirmedCol, reader.GetBoolean);
                                user.TwoFactorEnabled = GetDataSafe<bool>(reader, TwoFactorEnabledCol, reader.GetBoolean);
                                user.LockoutEndDateUtc = GetDataSafe<DateTime>(reader, LockoutEndDateUtcCol, reader.GetDateTime);
                                user.LockoutEnabled = GetDataSafe<bool>(reader, LockoutEnabledCol, reader.GetBoolean);
                                user.AccessFailedCount = GetDataSafe<int>(reader, AccessFailedCountCol, reader.GetInt32);
                                user.UserName = GetDataSafe<string>(reader, UserNameCol, reader.GetString);
                                user.FirstName = GetDataSafe<string>(reader, FirstNameCol, reader.GetString);
                                user.LastName = GetDataSafe<string>(reader, LastNameCol, reader.GetString);
                                user.Address = GetDataSafe<string>(reader, AddressCol, reader.GetString);
                                user.ApiKey = GetDataSafe<string>(reader, ApiKeyCol, reader.GetString);
                            }
                        }
                    }
                }
            }
            return user;
        }

        public User Update(User user)
        {
            int EmailConfirmed = Convert.ToInt32(user.EmailConfirmed);
            int PhoneNumberConfirmed = Convert.ToInt32(user.PhoneNumberConfirmed);
            int TwoFactorEnabled = Convert.ToInt32(user.TwoFactorEnabled);
            int LockoutEnabled = Convert.ToInt32(user.LockoutEnabled);

            string sql = "UPDATE AspNetUsers SET FirstName = @FirstName, LastName = @LastName, Phone = @Phone, Address = @Address, ApiKey = @ApiKey, Email = @Email WHERE ID = @Id";

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

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(arrayOfParameters);
                    command.Connection.Open();
                    // Add exceptionhandling 
                    int affectedRows = command.ExecuteNonQuery();
                    if (!(0 < affectedRows))
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

            string sql = "DELETE FROM AspNetUsers WHERE ID = @Id";

            SqlParameter idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = user.Id, SqlDbType = SqlDbType.NVarChar };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(idParameter);
                    command.Connection.Open();
                    int affectedRows = command.ExecuteNonQuery();
                    if (!(0 < affectedRows))
                    {
                        throw new System.Exception("No rows affected. Update failed - Does the object exist beforehand in the database?");
                    }
                }
            }

            return user;
        }

        public List<User> ReadAll()
        {
            List<User> users = new List<User>();

            string connectionString = ConfigurationManager.ConnectionStrings["testConnection"].ConnectionString;

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

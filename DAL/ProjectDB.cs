using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class ProjectDB : ICRUD<Project>
    {
        public Project Create(Project t)
        {
            // We should work out a more centralized way to obtain a connection to the database.
            string connectionString = "Data Source=aur.dk; Database=apacta; User Id=datamatikerne; Password=A6stvHkW;";
            //string connectionString = ConfigurationManager.ConnectionStrings["testConnection"].ConnectionString;

            string sql = "INSERT INTO Projects VALUES(@Name, @Created_by_ID, @Contact_ID, @Project_status_ID, @Project_description, @Street_Name, @Start_time, @Created, @Modified, @Deleted) SELECT SCOPE_IDENTITY()";

            SqlParameter[] arrayOfParameters =
            {
                new SqlParameter { ParameterName = "@Name", Value = t.Name, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Created_by_ID", Value = t.Created_by_ID, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Contact_ID", Value = t.Contact_ID, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Project_status_ID", Value = t.Project_status_ID, SqlDbType = SqlDbType.Int },
                new SqlParameter { ParameterName = "@Project_description", Value = t.Project_description, SqlDbType = SqlDbType.Text },
                new SqlParameter { ParameterName = "@Street_Name", Value = t.Street_Name, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Start_time", Value = t.Start_time, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@Created", Value = t.Created, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@Modified", Value = t.Modified, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@Deleted", Value = Convert.ToInt32(t.Deleted), SqlDbType = SqlDbType.Bit }
            };
            

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    
                    command.Parameters.AddRange(arrayOfParameters);
                    connection.Open();
                    // Add exceptionhandling
                    t.ID = Convert.ToInt32(command.ExecuteScalar());
                }   
            }

            return t;
        }

        public Project Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Project Read(int id)
        {
            throw new NotImplementedException();
        }

        public Project Update(Project t)
        {
            throw new NotImplementedException();
        }
    }
}

using CodingTasks.Dao;
using CodingTasks.Entities;
using CodingTasks.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTasks.Dao
{
    public class CourierAdminServiceImpl : ICourierAdminService
    {
        public int AddCourierStaff(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(DbConnUtil.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"INSERT INTO Employee
                                        (Names, Email, ContactNumber, Roles, Salary, LocationID)
                                        VALUES 
                                        (@Names, @Email, @ContactNumber, @Roles, @Salary, @LocationID);
                                        SELECT SCOPE_IDENTITY();";

                    cmd.Parameters.AddWithValue("@Names", employee.Names);
                    cmd.Parameters.AddWithValue("@Email", employee.Email);
                    cmd.Parameters.AddWithValue("@ContactNumber", employee.ContactNumber);
                    cmd.Parameters.AddWithValue("@Roles", employee.Roles);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@LocationID", employee.LocationID);

                    cmd.Connection = connection;
                    connection.Open();

                    int newId = Convert.ToInt32(cmd.ExecuteScalar());
                    return newId;
                }
            }
        }
    }
}
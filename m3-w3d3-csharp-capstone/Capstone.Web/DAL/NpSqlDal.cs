using System;
using System.Collections.Generic;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class NpSqlDal
    {
        private readonly string connectionString;
        private const string SQL_GetAllParks = "SELECT * FROM park";

        public NpSqlDal(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public List<Park> GetAllParks()
        {
            List<Park> parks = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllParks, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park p = new Park();

                        p.ParkName = Convert.ToString(reader["parkName"]);
                        p.State = Convert.ToString(reader["state"]);
                        p.ParkDescription = Convert.ToString(reader["parkDescription"]);

                        parks.Add(p);
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return parks;
        }


    }
}
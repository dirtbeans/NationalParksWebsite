using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public class WeatherSqlDal
    {
        private readonly string connectionString;
        private const string SQL_GetWeatherForPark = "SELECT * FROM weather WHERE parkCode = @parkCode";


        public WeatherSqlDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Weather> GetWeatherForPark(string parkCode)
        {
            List<Weather> weather = new List<Weather>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetWeatherForPark, conn);

                    cmd.Parameters.AddWithValue("@parkCode", parkCode);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Weather w = new Weather();

                        w.ParkCode = Convert.ToString(reader["parkCode"]);
                        w.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        w.Low = Convert.ToInt32(reader["low"]);
                        w.High = Convert.ToInt32(reader["high"]);
                        w.Forecast = Convert.ToString(reader["forecast"]);

                        weather.Add(w);
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return weather;
        }

     

    }
}
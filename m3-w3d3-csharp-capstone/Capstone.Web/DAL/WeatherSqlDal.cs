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

        public Weather GetWeatherForPark(string parkCode)
        {
            Weather weather = new Weather();

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
                        weather.ParkCode = Convert.ToString(reader["parkCode"]);
                        weather.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        weather.Low = Convert.ToInt32(reader["low"]);
                        weather.High = Convert.ToInt32(reader["high"]);
                        weather.Forecast = Convert.ToString(reader["forecast"]);
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
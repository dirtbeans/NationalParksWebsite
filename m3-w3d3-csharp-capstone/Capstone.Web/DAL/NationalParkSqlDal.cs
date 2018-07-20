using System;
using System.Collections.Generic;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class NationalParkSqlDal
    {
        private readonly string connectionString;
        private const string SQL_GetAllParks = "SELECT * FROM park";
        private const string SQL_GetOnePark = "SELECT * FROM park WHERE parkCode = @parkCode";


        public NationalParkSqlDal(string connectionString)
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

                        p.ParkCode = Convert.ToString(reader["parkCode"]);
                        p.ParkName = Convert.ToString(reader["parkName"]);
                        p.Acreage = Convert.ToInt32(reader["acreage"]);
                        p.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
                        p.MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]);
                        p.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
                        p.Climate = Convert.ToString(reader["climate"]);
                        p.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                        p.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                        p.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
                        p.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                        p.State = Convert.ToString(reader["state"]);
                        p.ParkDescription = Convert.ToString(reader["parkDescription"]);
                        p.EntryFee = Convert.ToInt32(reader["entryFee"]);
                        p.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);

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


        public Park GetOnePark(string parkCode)
        {
            Park park = new Park();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetOnePark, conn);

                    cmd.Parameters.AddWithValue("@parkCode", parkCode);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        park.ParkCode = Convert.ToString(reader["parkCode"]);
                        park.ParkName = Convert.ToString(reader["parkName"]);
                        park.Acreage = Convert.ToInt32(reader["acreage"]);
                        park.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
                        park.MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]);
                        park.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
                        park.Climate = Convert.ToString(reader["climate"]);
                        park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                        park.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                        park.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
                        park.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                        park.State = Convert.ToString(reader["state"]);
                        park.ParkDescription = Convert.ToString(reader["parkDescription"]);
                        park.EntryFee = Convert.ToInt32(reader["entryFee"]);
                        park.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return park;
        }




    }
}
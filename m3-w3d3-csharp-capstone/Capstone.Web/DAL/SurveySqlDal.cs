using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class SurveySqlDal
    {
        private readonly string connectionString;
        private const string SQL_PostSurvey = "INSERT INTO survey_result " +
            "VALUES (@parkCode, @emailAddress, @state, @activityLevel);";
        private const string SQL_GetSurveyCount = "SELECT * FROM survey_result WHERE parkCode = @parkCode";
        private const string SQL_GetAllSurveys = "SELECT sr.parkCode, p.parkName, COUNT(*) AS surveyCount " +
            "FROM survey_result sr JOIN park p ON p.parkCode = sr.parkCode " +
            "GROUP BY sr.parkCode, p.parkName ORDER BY surveyCount DESC";

        public SurveySqlDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool PostSurvey(Survey newSurvey)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_PostSurvey, conn);
                    cmd.Parameters.AddWithValue("@parkCode", newSurvey.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", newSurvey.EmailAddress);
                    cmd.Parameters.AddWithValue("@state", newSurvey.State);
                    cmd.Parameters.AddWithValue("@activityLevel", newSurvey.ActivityLevel);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                // Error Logging that a problem occurred, don't show the user
                throw;
            }
        }

        public int GetSurveyCount(string parkCode)
        {
            int surveyCount = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetSurveyCount, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        surveyCount++;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return surveyCount;
        }

        public List<ParkSurveyCount> GetSurveyCountList()
        {
            List<ParkSurveyCount> surveyResults = new List<ParkSurveyCount>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllSurveys, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ParkSurveyCount psc = new ParkSurveyCount();

                        psc.ParkCode = Convert.ToString(reader["parkCode"]);
                        psc.ParkName = Convert.ToString(reader["parkName"]);
                        psc.SurveyCount = Convert.ToInt32(reader["surveyCount"]);

                        surveyResults.Add(psc);
                        
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }


            return surveyResults;
        }


    }
}
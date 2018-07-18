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
        private const string SQL_PostSurvey = "INSERT INTO survey_result VALUES (@parkCode, @emailAddress, @state, @activityLevel);";
        private const string SQL_GetSurveyCount = "SELECT * FROM survey_result WHERE state = @state";

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

    }
}
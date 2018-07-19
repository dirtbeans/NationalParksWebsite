using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using System.Data.SqlClient;
using System.Configuration;
using Capstone.Web.DAL;
using System.Collections.Generic;
using Capstone.Web.Models;

namespace Capstone.Web.Tests.DAL
{
    [TestClass()]
    public class NationalParkSqlDalTests
    {
        private TransactionScope tran;
        private string connectionString = ConfigurationManager.ConnectionStrings["NPGeek"].ConnectionString;
        private string parkCode;
        private int parkCount = 0;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                cmd = new SqlCommand("INSERT INTO park VALUES ('XXX', 'Test Park', 'Ohio', 65536, 5280, 1337, 99, 'Forest', 2018, 50, 'To test or not to test', 'Andrew Frank', 'A test park located in the forest', 19, 100);", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("INSERT INTO weather VALUES ('XXX', 1, 40, 70, 'rain');", conn);
                cmd.ExecuteNonQuery();

                parkCode = "XXX";

                cmd = new SqlCommand("SELECT * FROM park;", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    parkCount++;
                }
                                
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }


        [TestMethod()]
        public void GetAllParksTest()
        {

            // Arrange 
            NationalParkSqlDal parkDal = new NationalParkSqlDal(connectionString);

            //Act
            List<Park> parks = parkDal.GetAllParks();

            //Assert
            Assert.AreEqual(parks.Count, parkCount);

        }

        [TestMethod()]
        public void GetOneParkTest()
        {

            // Arrange 
            NationalParkSqlDal parkDal = new NationalParkSqlDal(connectionString);

            //Act
            Park park = parkDal.GetOnePark("XXX");

            //Assert
            Assert.AreEqual(parkCode, park.ParkCode);

        }
    }
}

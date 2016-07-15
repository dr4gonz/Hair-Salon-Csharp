using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
    public class StylistTest : IDisposable
    {
        public StylistTest()
        {
            // DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurants_test;Integrated Security=SSPI;";
            DBConfiguration.ConnectionString = "Data Source=DESKTOP-7OLC9FT\\SQLEXPRESS;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
        }
        public void Dispose()
        {
            Stylist.DeleteAll();
        }

        [Fact]
        public void Stylist_DatabaseEmptyAtFirst()
        {
            //Arrange, Act
            int result = Stylist.GetAll().Count;
            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Stylists_ChecksIfStylistsAreEqual_returnsTrue()
        {
            //Arrange, Act
            Stylist firstStylist = new Stylist("Matt", "5035555555", "none@none.com");
            Stylist secondStylist = new Stylist("Matt", "5035555555", "none@none.com");
            //Assert
            Assert.Equal(firstStylist, secondStylist);
        }

        [Fact]
        public void Stylists_SavesToDatabase()
        {
            //Arrange
            Stylist newStylist = new Stylist("Matt", "5035555555", "none@none.com");
            //Act
            newStylist.Save();
            Stylist savedStylist = Stylist.GetAll()[0];
            //Assert
            Assert.Equal(newStylist, savedStylist);
        }
    }
}

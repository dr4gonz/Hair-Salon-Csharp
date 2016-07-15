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
            Stylist firstStylist = new Stylist("Matt", "503.555.5555", "none@none.com");
            Stylist secondStylist = new Stylist("Matt", "503.555.5555", "none@none.com");
            //Assert
            Assert.Equal(firstStylist, secondStylist);
        }

        [Fact]
        public void Stylists_SavesToDatabase()
        {
            //Arrange
            Stylist newStylist = new Stylist("Matt", "503.555.5555", "none@none.com");
            newStylist.Save();
            //Act
            List<Stylist> result = Stylist.GetAll();
            List<Stylist> testList = new List<Stylist>{newStylist};
            //Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Stylists_SavesSavesWithID()
        {
            //Arrange
            Stylist newStylist = new Stylist("Matt", "503.555.5555", "none@none.com");
            newStylist.Save();
            //Act
            Stylist savedStylist = Stylist.GetAll()[0];
            int result = newStylist.GetId();
            int testId = savedStylist.GetId();
            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Stylists_FindsStylistInDatabase()
        {
            //Arrange
            Stylist newStylist = new Stylist("Matt", "503.555.5555", "none@none.com");
            newStylist.Save();
            //Act
            Stylist foundStylist = Stylist.Find(newStylist.GetId());
            //Assert
            Assert.Equal(newStylist, foundStylist);
        }

        [Fact]
        public void Stylists_Update_UpdatesStylistInDatabase()
        {
            //Arrange
            Stylist newStylist = new Stylist("Matt", "503.555.5555", "none@none.com");
            newStylist.Save();
            string newName = "Brad";
            string newPhone = "503.666.6666";
            string newEmail = "noneatall@none.com";
            //Act
            newStylist.Update(newName, newPhone, newEmail);

            string updateName = newStylist.GetName();
            string updatePhone = newStylist.GetPhone();
            string updateEmail = newStylist.GetEmail();
            //Assert
            Assert.Equal(newName, updateName);
            Assert.Equal(newPhone, updatePhone);
            Assert.Equal(newEmail, updateEmail);
        }

        [Fact]
        public void Stylists_Delete_DeletesStylistFromDatabase()
        {
            //Arrange
            string name1 = "Matt";
            string phone1 = "503.555.5555";
            string email1 = "none@none.com";
            Stylist testStylist1 = new Stylist(name1, phone1, email1);
            testStylist1.Save();

            string name2 = "Brad";
            string phone2 = "503.666.6666";
            string email2 = "noneatall@none.com";
            Stylist testStylist2 = new Stylist(name2, phone2, email2);
            testStylist2.Save();
            //Act
            testStylist2.Delete();
            List<Stylist> resultStylist = Stylist.GetAll();
            List<Stylist> testStylist = new List<Stylist> {testStylist1};
            //Assert
            Assert.Equal(testStylist, resultStylist);
        }

        [Fact]
        public void Stylists_GetClients_GetsStylistsClientsFromDB()
        {
            //Arrange
            Stylist newStylist = new Stylist("Matt", "503.555.5555", "none@none.com");
            newStylist.Save();
            Client newClient1 = new Client("Noel", "503.555.5555", "none@none.com", newStylist.GetId());
            newClient1.Save();
            Client newClient2 = new Client("Colin", "503.555.5555", "none@none.com", newStylist.GetId());
            newClient2.Save();
            //Act
            List<Client> testList = new List<Client>{newClient1, newClient2};
            List<Client> resultList = newStylist.GetClients();
            //Assert
            Assert.Equal(testList, resultList);
        }

        public void Dispose()
        {
            Stylist.DeleteAll();
            Client.DeleteAll();
        }
    }
}

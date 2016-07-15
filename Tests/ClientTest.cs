using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
    public class ClientTest : IDisposable
    {
        public ClientTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurants_test;Integrated Security=SSPI;";
            // DBConfiguration.ConnectionString = "Data Source=DESKTOP-7OLC9FT\\SQLEXPRESS;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Client_DatabaseEmptyAtFirst()
        {
            //Arrange, Act
            int result = Client.GetAll().Count;
            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Clients_ChecksIfClientsAreEqual_returnsTrue()
        {
            //Arrange, Act
            Client firstClient = new Client("Noel", "503.555.5555", "none@none.com", 1);
            Client secondClient = new Client("Noel", "503.555.5555", "none@none.com", 1);
            //Assert
            Assert.Equal(firstClient, secondClient);
        }

        [Fact]
        public void Clients_SavesToDatabase()
        {
            //Arrange
            Client newClient = new Client("Noel", "503.555.5555", "none@none.com", 1);
            newClient.Save();
            //Act
            List<Client> result = Client.GetAll();
            List<Client> testList = new List<Client>{newClient};
            //Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Clients_SavesSavesWithID()
        {
            //Arrange
            Client newClient = new Client("Noel", "503.555.5555", "none@none.com", 1);
            newClient.Save();
            //Act
            Client savedClient = Client.GetAll()[0];
            int result = newClient.GetId();
            int testId = savedClient.GetId();
            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Clients_FindsClientInDatabase()
        {
            //Arrange
            Client newClient = new Client("Noel", "503.555.5555", "none@none.com", 1);
            newClient.Save();
            //Act
            Client foundClient = Client.Find(newClient.GetId());
            //Assert
            Assert.Equal(newClient, foundClient);
        }

        [Fact]
        public void Clients_Update_UpdatesClientInDatabase()
        {
            //Arrange
            Client newClient = new Client("Noel", "503.555.5555", "none@none.com", 1);
            newClient.Save();
            string newName = "Brad";
            string newPhone = "503.666.6666";
            string newEmail = "noneatall@none.com";
            int newStylistId = 2;
            //Act
            newClient.Update(newName, newPhone, newEmail, newStylistId);

            string updateName = newClient.GetName();
            string updatePhone = newClient.GetPhone();
            string updateEmail = newClient.GetEmail();
            int updateStylistId = newClient.GetStylistId();
            //Assert
            Assert.Equal(newName, updateName);
            Assert.Equal(newPhone, updatePhone);
            Assert.Equal(newEmail, updateEmail);
            Assert.Equal(newStylistId, updateStylistId);
        }

        [Fact]
        public void Clients_Delete_DeletesClientFromDatabase()
        {
            //Arrange
            string name1 = "Noel";
            string phone1 = "503.555.5555";
            string email1 = "none@none.com";
            Client testClient1 = new Client(name1, phone1, email1, 1);
            testClient1.Save();

            string name2 = "Brad";
            string phone2 = "503.666.6666";
            string email2 = "noneatall@none.com";
            Client testClient2 = new Client(name2, phone2, email2, 1);
            testClient2.Save();
            //Act
            testClient2.Delete();
            List<Client> resultClient = Client.GetAll();
            List<Client> testClient = new List<Client> {testClient1};
            //Assert
            Assert.Equal(testClient, resultClient);
        }

        public void Dispose()
        {
            Client.DeleteAll();
        }
    }
}

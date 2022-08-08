using LionSkyNot.Services.Gym;
using LionSkyNot.Tests.Mock;
using System;
using System.Linq;
using Xunit;

namespace LionSkyNot.Tests.Services
{
    public class ClientServiceTest
    {

        [Fact]
        public void AddNewClient_ShouldBeCorrect()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var clientService = new ClientService(data);
            string clientName = "Test tester";
            DateTime startDate = DateTime.Now;
            DateTime expiredDate = DateTime.Now;

            //Act
            clientService.Create(
                                 clientName,
                                 startDate,
                                 expiredDate);

            var currentClient = data.Clients.Where(c => c.FullName == clientName).FirstOrDefault();

            //Assert

            Assert.Equal(clientName, currentClient.FullName);
            Assert.Equal(startDate, currentClient.StartDate);
            Assert.Equal(expiredDate, currentClient.ExpireDate);
        }

        [Fact]
        public void AllCreatedClients_ShouldHave_DifferentNumber()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var clientService = new ClientService(data);
            string clientName = "Test tester";
            DateTime startDate = DateTime.Now;
            DateTime expiredDate = DateTime.Now;

            //Act
            for (int i = 0; i < 2; i++)
            {
                clientService.Create(
                                     clientName + i,
                                     startDate,
                                     expiredDate);
            }

            var clientsNumbers = data.Clients.Select(c => c.Number).ToList();

            //Assert
            Assert.NotEqual(clientsNumbers[0], clientsNumbers[1]);

        }

        [Fact]
        public void EditClient_ShouldReturnTrue_And_EditCorrect()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var clientService = new ClientService(data);
            string createdName = "Test tester";
            string editName = "Tester test";
            DateTime startDate = DateTime.Now;
            DateTime expiredDate = DateTime.Now;

            //Act
            clientService.Create(
                                 createdName,
                                 startDate,
                                 expiredDate);

            var currentClient = data.Clients.Where(c => c.FullName == createdName).FirstOrDefault();

            var isEditted = clientService.Edit(currentClient.Number, editName, startDate, expiredDate);

            //Assert
            Assert.True(isEditted);
            Assert.Equal(editName, currentClient.FullName);

        }

        [Fact]
        public void EditClient_ShouldReturnFalse()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var clientService = new ClientService(data);
            string createdName = "Test tester";
            DateTime startDate = DateTime.Now;
            DateTime expiredDate = DateTime.Now;

            //Act

            var isEditted = clientService.Edit(3123123, createdName, startDate, expiredDate);

            //Assert
            Assert.False(isEditted);

        }


        [Fact]
        public void CheckClientNumber_ShouldReturnTrue()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var clientService = new ClientService(data);
            string createdName = "Test tester";
            DateTime startDate = DateTime.Now;
            DateTime expiredDate = DateTime.Now;

            clientService.Create(
                                 createdName,
                                 startDate,
                                 expiredDate);

            var clientNumber = data.Clients
                                    .Where(c => c.FullName == createdName)
                                    .Select(c => c.Number)
                                    .FirstOrDefault();

            //Act
            var exists = clientService.CheckNumber(clientNumber);

            //Assert
            Assert.True(exists);

        }

        [Fact]
        public void CheckClientNumber_ShouldReturnFlase()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var clientService = new ClientService(data);

            //Act
            var exists = clientService.CheckNumber(13123);

            //Assert
            Assert.False(exists);

        }


    }
}

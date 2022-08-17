using LionSkyNot.Services.Gym;
using LionSkyNot.Tests.Mock;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LionSkyNot.Tests.Services
{
    public class ClientServiceTest
    {

        [Fact]
        public async Task AddNewClient_ShouldBeCorrect()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var clientService = new ClientService(data);
            string clientName = "Test tester";
            DateTime startDate = DateTime.Now;
            DateTime expiredDate = DateTime.Now;

            //Act
            await clientService.CreateAsync(
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
        public async Task AllCreatedClients_ShouldHave_DifferentNumber()
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
                await clientService.CreateAsync(
                                       clientName + i,
                                       startDate,
                                       expiredDate);
            }

            var clientsNumbers = data.Clients.Select(c => c.Number).ToList();

            //Assert
            Assert.NotEqual(clientsNumbers[0], clientsNumbers[1]);

        }

        [Fact]
        public async Task EditClient_ShouldReturnTrue_And_EditCorrect()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var clientService = new ClientService(data);
            string createdName = "Test tester";
            string editName = "Tester test";
            DateTime startDate = DateTime.Now;
            DateTime expiredDate = DateTime.Now;

            //Act
            await clientService.CreateAsync(
                                  createdName,
                                  startDate,
                                  expiredDate);

            var currentClient = data.Clients.Where(c => c.FullName == createdName).FirstOrDefault();

            var isEditted = await clientService.EditAsync(currentClient.Number, editName, startDate, expiredDate);

            //Assert
            Assert.True(isEditted);
            Assert.Equal(editName, currentClient.FullName);

        }

        [Fact]
        public async Task EditClient_ShouldReturnFalse()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var clientService = new ClientService(data);
            string createdName = "Test tester";
            DateTime startDate = DateTime.Now;
            DateTime expiredDate = DateTime.Now;

            //Act

            var isEditted = await clientService.EditAsync(3123123, createdName, startDate, expiredDate);

            //Assert
            Assert.False(isEditted);

        }


        [Fact]
        public async Task CheckClientNumber_ShouldReturnTrue()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var clientService = new ClientService(data);
            string createdName = "Test tester";
            DateTime startDate = DateTime.Now;
            DateTime expiredDate = DateTime.Now;

            await clientService.CreateAsync(
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


        [Fact]
        public async Task GetClientByNumber_ShouldReturn_CorrectClient()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var clientService = new ClientService(data);
            string createdName = "Test tester";
            DateTime startDate = DateTime.Now;
            DateTime expiredDate = DateTime.Now;

            await clientService.CreateAsync(
                                 createdName,
                                 startDate,
                                 expiredDate);

            var clientNumber = data.Clients
                                    .Where(c => c.FullName == createdName)
                                    .Select(c => c.Number)
                                    .FirstOrDefault();

            //Act
            var client = clientService.GetClientByNumber(clientNumber);

            //Assert
            Assert.Equal(createdName, client.FullName);
            Assert.Equal(startDate, client.StartDate);
            Assert.Equal(expiredDate, client.ExpireDate);

        }


    }
}

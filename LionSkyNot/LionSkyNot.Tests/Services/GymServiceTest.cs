using LionSkyNot.Services.Gym;
using LionSkyNot.Tests.Mock;
using System;
using System.Linq;
using Xunit;

namespace LionSkyNot.Tests.Services
{
    public class GymServiceTest
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


    }
}

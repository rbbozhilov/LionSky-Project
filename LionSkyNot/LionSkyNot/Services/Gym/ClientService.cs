using LionSkyNot.Data;
using LionSkyNot.Data.Models.Gym;

namespace LionSkyNot.Services.Gym
{
    public class ClientService : IClientService
    {

        private LionSkyDbContext data;

        public ClientService(LionSkyDbContext data)
        {
            this.data = data;
        }


        public void Create(
                           string fullName,
                           DateTime startDate,
                           DateTime expireDate)
        {

            var client = new Client()
            {
                FullName = fullName,
                Number = GenerateNumber(),
                StartDate = startDate,
                ExpireDate = expireDate
            };



            while (CheckNumber(client.Number))
            {
                client.Number = GenerateNumber();
            }

            this.data.Clients.Add(client);

            this.data.SaveChanges();

        }

        public bool CheckNumber(int number)
        => this.data.Clients.Any(c => c.Number == number);



        private int GenerateNumber()
        => new Random().Next(0, 10000);

    }
}

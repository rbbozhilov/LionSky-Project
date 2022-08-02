using LionSkyNot.Areas.Admin.Models.Gym;
using LionSkyNot.Data;
using LionSkyNot.Data.Models.Gym;
using LionSkyNot.Views.ViewModels.Gym;

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

        public bool Edit(
                        int number,
                        string fullName,
                        DateTime startDate,
                        DateTime expireDate)
        {
            var client = this.data.Clients.Where(c => c.Number == number).FirstOrDefault();

            if(client == null)
            {
                return false;
            }

            client.FullName = fullName;
            client.StartDate = startDate;
            client.ExpireDate = expireDate;

            this.data.SaveChanges();

            return true;
        }


        public bool CheckNumber(int number)
        => this.data.Clients.Any(c => c.Number == number);

        public ClientViewModel SearchByNumberAndName(string searchTerm)
        => this.data.Clients
                        .Where(c => c.Number.ToString() == searchTerm || c.FullName.ToLower().Contains(searchTerm.ToLower()))
                        .Select(c => new ClientViewModel()
                        {
                            Number = c.Number,
                            FullName = c.FullName,
                            StartDate = c.StartDate,
                            ExpireDate = c.ExpireDate
                        })
                        .FirstOrDefault();


        public ClientFormModel GetClientByNumber(int number)
        => this.data.Clients
                       .Where(c => c.Number.ToString() == number.ToString())
                       .Select(c => new ClientFormModel()
                       {
                           ExpireDate = c.ExpireDate,
                           StartDate = c.StartDate,
                           FullName = c.FullName
                       })
                        .FirstOrDefault();


        private int GenerateNumber()
        => new Random().Next(0, 10000);

    }
}

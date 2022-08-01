namespace LionSkyNot.Services.Gym
{
    public interface IClientService
    {

        void Create(
                   string fullName,
                   DateTime startDate,
                   DateTime expireDate);

        bool CheckNumber(int number);

       
    }
}

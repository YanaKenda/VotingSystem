namespace VotingSystem.Interfaces.Managers
{
    using System.Linq;
    using VotingSystem.Entities;

    public interface IUserManager
    {
        IQueryable<User> Users { get; }

        bool isUserValid(string email, string password);

        User GetUserByEmail(string email);

        void CreateNewUser(User user);

        string[] GetRolesForUser(string username);

        void AddRoleToUser(string name, string role);

        //void DestroyContext();
    }
}

namespace VotingSystem.BusinessLogic.Managers
{
    using System.Linq;
    using VotingSystem.Entities;
    using VotingSystem.Interfaces.Managers;
    using VotingSystem.Interfaces.Repositories;

    //implementation user manager
    public class UserManager: IUserManager
    {
        private IUserRepository userRepository;
        private IRoleRepository roleRepository;
        
        public UserManager(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        public IQueryable<User> Users
        {
            get { return userRepository.Users; }
        }

        public bool isUserValid(string email, string password)
        {
            var isValid = false;

            try
            {
                var user = this.GetUserByEmail(email);

                //check if user exist in database
                if (user != null && user.Password == password)
                {
                    isValid = true;
                }
            }
            catch
            {
                isValid = false;
            }
            
            return isValid;
        }

        public User GetUserByEmail(string email)
        {
            return userRepository.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        }

        public void CreateNewUser(User user)
        {
            userRepository.Add(user);
        }

        public string[] GetRolesForUser(string username)
        {
            return GetUserByEmail(username).Roles.Select(role => role.Name).ToArray<string>();
        }

        public void AddRoleToUser(string name, string role)
        {
            Role foundRole = roleRepository.Roles.FirstOrDefault(r => r.Name == role);

            if (foundRole != null)
            {
                userRepository.AddRole(name, foundRole);
            }
        }
    }
}

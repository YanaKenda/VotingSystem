namespace VotingSystem.BusinessLogic.Managers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using VotingSystem.BusinessLogic.Interfaces.Managers;
	using VotingSystem.DataAccess.Interfaces.Repositories;
	using VotingSystem.Entities;

	//implementation user manager
	public class UsersManager : IUsersManager
	{
		private readonly IRoleRepository _roleRepository;
		private readonly IUserRepository _userRepository;

		public UsersManager(IUserRepository userRepository, IRoleRepository roleRepository)
		{
			_userRepository = userRepository;
			_roleRepository = roleRepository;
		}

		public User GetUser(int userId)
		{
			return _userRepository.All.FirstOrDefault(u => u.Id == userId);
		}

		public IEnumerable<User> GetUsers()
		{
			return _userRepository.All;
		}

		public bool IsUserValid(string email, string password)
		{
			bool isValid = false;

			try
			{
				User user = GetUserByEmail(email);

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
			return _userRepository.All.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
		}

		public void CreateNewUser(User user)
		{
			_userRepository.InsertOrUpdate(user);
			_userRepository.Save();
		}

		public string[] GetRolesForUser(string email)
		{
			return GetUserByEmail(email).Roles.Select(role => role.Name).ToArray();
		}

		public void AddRoleToUser(string email, string roleName)
		{
			Role role = _roleRepository.All.FirstOrDefault(r => r.Name == roleName);

			if (role != null)
			{
				role.Users.Add(GetUserByEmail(email));
				_roleRepository.Save();
			}
			else
			{
				throw new NullReferenceException("Role is not found.");
			}
		}

		public void AddRoleToUser(int userId, string roleName)
		{
			var role = _roleRepository.All.FirstOrDefault(r => r.Name == roleName);

			if (role != null)
			{
				role.Users.Add(GetUserById(userId));
				_roleRepository.Save();
			}
			else
			{
				throw new NullReferenceException("Role is not found.");
			}
		}

		public User GetUserById(int userId)
		{
			return _userRepository.All.FirstOrDefault(u => u.Id == userId);
		}

		public void EditUser(User user)
		{
			_userRepository.InsertOrUpdate(user);
			_userRepository.Save();
		}

		public void RemoveRole(int userId, string roleName)
		{
			Role role = _roleRepository.All.FirstOrDefault(r => r.Name == roleName);

			if (role != null)
			{
				role.Users.Remove(GetUserById(userId));
				_roleRepository.Save();
			}
			else
			{
				throw new NullReferenceException("Role is not found.");
			}
		}

		public IEnumerable<Poll> GetUsersVoices(string email)
		{
			return _userRepository.All.First(u => u.Email == email).Answers.Select(answer => answer.Poll);
		}
	}
}
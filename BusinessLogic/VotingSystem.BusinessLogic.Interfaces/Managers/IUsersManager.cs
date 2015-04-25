namespace VotingSystem.BusinessLogic.Interfaces.Managers
{
	using System.Collections.Generic;
	using VotingSystem.Entities;

	public interface IUsersManager
	{
		bool IsUserValid(string email, string password);

		User GetUserByEmail(string email);

		User GetUserById(int userId);

		void CreateNewUser(User user);

		string[] GetRolesForUser(string email);

		void AddRoleToUser(string email, string role);

		void AddRoleToUser(int userId, string role);

		IEnumerable<User> GetUsers();

		User GetUser(int userId);

		void EditUser(User user);

		void RemoveRole(int userId, string role);

		IEnumerable<Poll> GetUsersVoices(string email);
	}
}

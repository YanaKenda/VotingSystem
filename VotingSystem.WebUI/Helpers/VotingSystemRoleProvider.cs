namespace VotingSystem.WebUI.Helpers
{
	using System;
	using System.Linq;
	using System.Web.Security;
	using Ninject;
	using VotingSystem.BusinessLogic.Interfaces.Managers;
	using VotingSystem.WebUI.App_Start;

	public class VotingSystemRoleProvider : RoleProvider
	{
		public IUsersManager UsersManager
		{
			get { return NinjectWebCommon.GetKernel().Get<IUsersManager>(); }
		}

		public override bool IsUserInRole(string username, string roleName)
		{
			return GetRolesForUser(username).Contains(roleName);
		}

		public override string[] GetRolesForUser(string username)
		{
			return UsersManager.GetRolesForUser(username);
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			foreach (var name in usernames)
			{
				foreach (var role in roleNames)
				{
					UsersManager.AddRoleToUser(name, role);
				}
			}
		}


		//other method will implemented as needed
		#region

		public override string ApplicationName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override void CreateRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new NotImplementedException();
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new NotImplementedException();
		}

		public override string[] GetAllRoles()
		{
			throw new NotImplementedException();
		}

		public override string[] GetUsersInRole(string roleName)
		{
			throw new NotImplementedException();
		}


		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override bool RoleExists(string roleName)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
namespace VotingSystem.WebUI.Models.Helpers
{
    using Ninject;
    using System.Web.Security;
    using VotingSystem.Interfaces.Managers;
    using System.Linq;
    using VotingSystem.WebUI.App_Start;


    public class VotingSystemRoleProvider: RoleProvider
    {
        public IUsersManager usersManager
        {
            get{ return NinjectWebCommon.GetKernel().Get<IUsersManager>();}
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return GetRolesForUser(username).Contains(roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            return usersManager.GetRolesForUser(username);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            foreach (var name in usernames)
            {
                foreach (var role in roleNames)
                {
                    usersManager.AddRoleToUser(name, role);
                }
            }
        }


        //other method will implemented as needed
        #region

        public override string ApplicationName
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new System.NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new System.NotImplementedException();
        }


        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
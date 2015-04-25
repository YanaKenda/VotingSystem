namespace VotingSystem.WebUI.Helpers
{
	using System;
	using System.Web.Helpers;
	using System.Web.Security;
	using Ninject;
	using VotingSystem.BusinessLogic.Interfaces.Managers;
	using VotingSystem.Entities;
	using VotingSystem.WebUI.App_Start;

	public class VotingSystemMembershipProvider : MembershipProvider
	{
		public IUsersManager UsersManager
		{
			get { return NinjectWebCommon.GetKernel().Get<IUsersManager>(); }
		}

		public override bool ValidateUser(string email, string password)
		{
			bool isValid;

			try
			{
				isValid = UsersManager.IsUserValid(email, password);
			}
			catch { isValid = false; }

			return isValid;
		}

		public override MembershipUser GetUser(string email, bool userIsOnline)
		{
			var user = UsersManager.GetUserByEmail(email);

			if (!ReferenceEquals(user, null))
			{
				return new MembershipUser("VotingSystemMembershipProvider", null, null, user.Email, null, null,
						false, false, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
			}
			return null;
		}

		public MembershipUser CreateUser(string email, string password, string name, string surname, DateTime dateOfBirth)
		{
			MembershipUser membershipUser = GetUser(email, false);

			if (membershipUser == null)
			{
				UsersManager.CreateNewUser(new User
				{
					Surname = surname,
					Name = name,
					Email = email,
					DateOfBirth = dateOfBirth,
                    Password = password
					//Password = Crypto.HashPassword(password),
				});

				Roles.AddUserToRole(email, "User");

				return GetUser(email, false);
			}

			return null;
		}


		/// <summary>
		/// other methodes will implmenent as needed
		/// </summary>
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

		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			throw new NotImplementedException();
		}

		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			throw new NotImplementedException();
		}

		public override bool EnablePasswordReset
		{
			get { throw new NotImplementedException(); }
		}

		public override bool EnablePasswordRetrieval
		{
			get { throw new NotImplementedException(); }
		}

		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override int GetNumberOfUsersOnline()
		{
			throw new NotImplementedException();
		}

		public override string GetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			throw new NotImplementedException();
		}

		public override string GetUserNameByEmail(string email)
		{
			throw new NotImplementedException();
		}

		public override int MaxInvalidPasswordAttempts
		{
			get { throw new NotImplementedException(); }
		}

		public override int MinRequiredNonAlphanumericCharacters
		{
			get { throw new NotImplementedException(); }
		}

		public override int MinRequiredPasswordLength
		{
			get { throw new NotImplementedException(); }
		}

		public override int PasswordAttemptWindow
		{
			get { throw new NotImplementedException(); }
		}

		public override MembershipPasswordFormat PasswordFormat
		{
			get { throw new NotImplementedException(); }
		}

		public override string PasswordStrengthRegularExpression
		{
			get { throw new NotImplementedException(); }
		}

		public override bool RequiresQuestionAndAnswer
		{
			get { throw new NotImplementedException(); }
		}

		public override bool RequiresUniqueEmail
		{
			get { throw new NotImplementedException(); }
		}

		public override string ResetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override bool UnlockUser(string userName)
		{
			throw new NotImplementedException();
		}

		public override void UpdateUser(MembershipUser user)
		{
			throw new NotImplementedException();
		}

		public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			throw new NotImplementedException();
		}

		#endregion



	}
}
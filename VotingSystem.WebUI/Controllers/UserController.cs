namespace VotingSystem.WebUI.Controllers
{
	using System;
	using System.Linq;
	using System.Web.Mvc;
	using VotingSystem.WebUI.Models.User;
	using VotingSystem.BusinessLogic.Interfaces.Managers;

	public class UserController : Controller
	{

		private readonly IUsersManager _usersManager;

		public UserController(IUsersManager userManager)
		{
			_usersManager = userManager;
		}
		//
		// GET: /User/
		[Authorize(Roles = "admin")]
		public ActionResult Index(int? id)
		{
			var result = new IndexModel
			{
				Users = _usersManager.GetUsers().Select(
					userEntity => new User
					{
						Id = userEntity.Id,
						Name = userEntity.Name,
						Surname = userEntity.Surname,
						Email = userEntity.Email
					}
				).ToList()
			};

			foreach (var user in result.Users)
			{
				user.Role = _usersManager.GetRolesForUser(user.Email);
			}
			if (id != null)
			{
				result.ItemsPerPage = (int)id;
			}
			else
			{
				result.ItemsPerPage = 20;
			}

			return View(result);
		}


		[HttpPost]//AJAX
		public ActionResult Index(int id)
		{
			return View();
		}
		public ActionResult MyProfile()
		{
			return RedirectToAction("Profile", new { id = _usersManager.GetUserByEmail(User.Identity.Name).Id });
		}

		[HttpGet]
		public ActionResult Profile(int? id)
		{
			var model = new ProfileModel();

			if (id != null)
			{
				Entities.User userProfileData = _usersManager.GetUser((int)id);

				model.Id = userProfileData.Id;
				model.Name = userProfileData.Name;
				model.Surname = userProfileData.Surname;
				model.Email = userProfileData.Email;
				model.Roles = _usersManager.GetRolesForUser(userProfileData.Email).ToList();
			}
			return View(model);
		}

		[HttpGet]
		[Authorize]
		public ActionResult Edit(int? id)
		{
			if (id != null)
			{
				Entities.User userFromDatabase = _usersManager.GetUser(id.Value);
				EditModel userToEdit =
					 EditModel.FromEntity(userFromDatabase);

				return View(userToEdit);
			}

			return RedirectToAction("Index", "User");
		}

		[HttpPost]
		[Authorize]
		public ActionResult Edit(EditModel editedUserModel)
		{
			if (editedUserModel != null)
			{
                var existedUser = _usersManager.GetUserByEmail(editedUserModel.Email);
				//editedUserModel.Email = User.Identity.Name;
				Entities.User editedUserEntity = editedUserModel.ToEntity();
                editedUserEntity.Password = existedUser.Password;
                editedUserEntity.DateOfBirth = existedUser.DateOfBirth;
				_usersManager.EditUser(editedUserEntity);
				return RedirectToAction("Profile", "User", new { id = editedUserModel.Id });
			}

			throw new NullReferenceException();
		}

		[HttpGet]
		[Authorize(Roles = "admin")]
		public string SetModerator(int? id)
		{

			if (id != null)
			{
				_usersManager.AddRoleToUser((int)id, "Moderator");
				return _usersManager.GetUserById(id.Value).Roles.ElementAt(0).Name;
			}

			throw new NullReferenceException();
		}

		[HttpGet]
		[Authorize(Roles = "admin")]
		public String RemoveModerator(int? id)
		{
			if (id != null)
			{
				_usersManager.RemoveRole(id.Value, "Moderator");
				return _usersManager.GetUserById(id.Value).Roles.ElementAt(0).Name;
			}

			throw new NullReferenceException();
		}

		[HttpGet]
		[Authorize(Roles = "admin")]
		public String SetRole(int id, string roleName)
		{
			_usersManager.AddRoleToUser(id, roleName);
			return _usersManager.GetUserById(id).Roles.ElementAt(0).Name;
		}
	}
}

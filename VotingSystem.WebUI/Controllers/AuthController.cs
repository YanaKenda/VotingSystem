namespace VotingSystem.WebUI.Controllers
{
	using System.Web.Mvc;
	using System.Web.Security;
	using VotingSystem.WebUI.Models.Auth;
	using VotingSystem.WebUI.Helpers;

	public class AuthController : Controller
	{
		[HttpGet]
		public ActionResult LogIn()
		{
			return View();
		}
		[HttpPost]
		public ActionResult LogIn(LoginViewModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				if (Membership.ValidateUser(model.Email, model.Password))
				{
					FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);

					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", "Wrong email or pass");
			}
			return View(model);
		}

		public ActionResult LogOff()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Login", "Auth");
		}

		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				MembershipUser membershipUser = ((VotingSystemMembershipProvider)Membership.Provider).
					CreateUser(model.Email, model.Password, model.Name, model.Surname, model.DateOfBirth);

				if (membershipUser != null)
				{
					FormsAuthentication.SetAuthCookie(model.Email, false);
					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", "Registration Error");
			}
			return View(model);
		}
	}
}

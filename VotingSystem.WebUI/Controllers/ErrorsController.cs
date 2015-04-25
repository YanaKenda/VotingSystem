using System.Web.Mvc;

namespace VotingSystem.WebUI.Controllers
{
	public class ErrorsController : Controller
	{
		public ActionResult Error404()
		{
			return View();
		}

		public ActionResult Error500()
		{
			return View();
		}
	}
}

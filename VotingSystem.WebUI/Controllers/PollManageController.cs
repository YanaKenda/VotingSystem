using System.Web.Mvc;
using System.Web.Security;

namespace VotingSystem.WebUI.Controllers
{
	using VotingSystem.BusinessLogic.Interfaces.Managers;

	public class PollManageController : Controller
	{
		private readonly IPollsManager _pollsManager;

		public PollManageController(IPollsManager pollsManager)
		{
			_pollsManager = pollsManager;
		}
		[Authorize(Roles = "Admin")]
		public ActionResult Index(int? pollid)
		{
			if (pollid != null)
			{
				var pollToBan = _pollsManager.GetPollById(pollid.Value);
				pollToBan.IsBlocked = !pollToBan.IsBlocked;
				_pollsManager.EditPoll(pollToBan);

				return View("BanState", pollToBan);
			}

			return RedirectToAction("Index", "Home");
		}

	}
}

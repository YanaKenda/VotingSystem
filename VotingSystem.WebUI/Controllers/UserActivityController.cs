using VotingSystem.Entities;

namespace VotingSystem.WebUI.Controllers
{
	using System.Web.Mvc;
	using BusinessLogic.Interfaces.Managers;
	using Models.Poll;

	public class UserActivityController : Controller
	{

		private readonly IPollsManager _pollsManager;
		private readonly IUsersManager _usersManager;

		public UserActivityController(IPollsManager pollsManager, IUsersManager usersManager)
		{
			_pollsManager = pollsManager;
			_usersManager = usersManager;
		}

		[HttpGet]
		public ActionResult AddVoting()
		{
			return View(new PollViewModel());
		}

		[HttpPost]
		public string AddVoting(PollViewModel poll)
		{
			Poll entitiyPoll = poll.ToEntities();
			entitiyPoll.UserId = _usersManager.GetUserByEmail(User.Identity.Name).Id;
			var _pollId = _pollsManager.CreatePoll(entitiyPoll);
			var action = Url.Action("Details","Poll",new {pollId = _pollId});
			if (action != null)
				return action;

			return "";
		}

		public ActionResult UserVotings()
		{
			return View();
		}

		public ActionResult GetUserVotings(int page = 1)
		{
			return View("VotingsList",
				HomeController.GetVotings(_pollsManager.GetUsersPolls(User.Identity.Name),
				"GetUserVotings", page));
		}

		public ActionResult UserVoices()
		{
			return View();
		}

		public ActionResult GetUserVoices(int page = 1)
		{
			return View("VotingsList",
			   HomeController.GetVotings(_usersManager.GetUsersVoices(User.Identity.Name),
			   "GetUserVoices", page));
		}
	}
}

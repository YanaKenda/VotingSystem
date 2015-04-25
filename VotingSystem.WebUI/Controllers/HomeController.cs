namespace VotingSystem.WebUI.Controllers
{
	using System.Collections.Generic;
	using System.Web.Mvc;
	using VotingSystem.BusinessLogic.Interfaces.Managers;
	using VotingSystem.Entities;
	using VotingSystem.WebUI.Helpers;
	using VotingSystem.WebUI.Models;

	public class HomeController : Controller
	{

		private readonly IPollsManager _pollsManager;

		public HomeController(IPollsManager pollsManager)
		{
			_pollsManager = pollsManager;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult AllVotingsList(int page = 1)
		{
			if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
			{
				return View("VotingsList", GetVotings(_pollsManager.Polls, "AllVotingsList", page));
			}
			return View("VotingsList", GetVotings(_pollsManager.GetAllowedPolls(), "AllVotingsList", page));

		}

		public static VotingsListViewModel GetVotings(IEnumerable<Poll> votings, string action, int page = 1)
		{
			int pageSize = 20;


			return new VotingsListViewModel
			{
				Votings = votings,
				PageInfo = new PageInfo
				{
					CurrentPage = page,
					//                    PagesCount = (votings.Count() / pageSize) + 1,
					PagesCount = pageSize,
					UpdateAction = action
				}
			};
		}

	}
}

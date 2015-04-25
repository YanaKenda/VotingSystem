using VotingSystem.WebUI.Helpers;

namespace VotingSystem.WebUI.Controllers
{
	using System.Web.Mvc;
	using System.Web.Security;
	using Entities;
	using Models.Poll;
	using CaptchaMvc.HtmlHelpers;
	using BusinessLogic.Interfaces.Managers;
	using System;
	using System.Web.WebPages;

	public class PollController : Controller
	{
		private readonly IPollsManager _pollsManager;
		private readonly IUsersManager _usersManager;

		public PollController(IPollsManager pollsManager, IUsersManager usersManager)
		{
			_pollsManager = pollsManager;
			_usersManager = usersManager;
		}

		public ActionResult Details(int? pollId)
		{
			if (pollId.HasValue)
			{
				var voting = _pollsManager.GetPollById(pollId.Value);
				if (voting != null)
				{
					return View("Details", voting);
				}
			}

			TempData["ErrorMessage"] = "Voting does not found";
			return RedirectToAction("Index", "Home");
		}

		public ActionResult PollViewState(int pollId, int? answerId)
		{
			var poll = _pollsManager.GetPollById(pollId);

			if (poll.EndDateTime < DateTime.Now)
			{
				ViewBag.IsAlreadyVote = true;
				return View("ViewState/ResultView", poll);
			}

			if (User.Identity.IsAuthenticated)
			{
				ViewBag.IsAlreadyVote = _pollsManager.IsAlreadyVote(pollId, _usersManager.GetUserByEmail(User.Identity.Name).Id);
				if (ViewBag.IsAlreadyVote)
				{
					TempData["Message"] = "You have already vote";
					return View("ViewState/ResultView", poll);
				}
			}

			if (answerId != null)
			{
				if (answerId == 0)
				{
					return View("ViewState/ResultView", poll);
				}

				if (poll.IsPrivate)
				{

					return View("ViewState/PollSecurity",
								new PollSecurityViewModel { CurrentPollId = pollId, CurrentAnswerId = (int)answerId, IsPrivate = true });
				}
				if (User.Identity.IsAuthenticated)
				{
					_pollsManager.VoteFor(pollId, answerId.Value, _usersManager.GetUserByEmail(User.Identity.Name));
				}
				else
				{
					return View("ViewState/PollSecurity",
								new PollSecurityViewModel { CurrentPollId = pollId, CurrentAnswerId = (int)answerId });
				}
				return RedirectToAction("PollViewState", new { pollId });
			}
			return View("ViewState/Voting", poll);
		}

		[HttpPost]
		public ActionResult PollSecurity(PollSecurityViewModel model)
		{

			if (!User.Identity.IsAuthenticated)
			{
				if (model.IsValidEmail)
				{
					if (Session["keyWord"].ToString() == model.EmailApproved)
					{
						_usersManager.CreateNewUser(new User
						{
							Email = model.Email
						});
						Roles.AddUserToRole(model.Email, "Ananymous");
						TempData["Message"] = "You voice is checked";
						_pollsManager.VoteFor(model.CurrentPollId, model.CurrentAnswerId,
							_usersManager.GetUserByEmail(model.Email));
						return View("ViewState/ResultView", _pollsManager.GetPollById(model.CurrentPollId));
					}
				}
				else
				{
					if (!this.IsCaptchaValid("Captcha is not valid"))
					{
						TempData["CaptchaError"] = "Error: captcha is not valid.";
						return View("ViewState/PollSecurity", model);
					}
					if (model.KeyWord != _pollsManager.GetPollById(model.CurrentPollId).KeyWord)
					{
						TempData["KeyWordMessage"] = "Uncorrect keyword!";
						return View("ViewState/PollSecurity", model);
					}

					var user = _usersManager.GetUserByEmail(model.Email);
					if (user == null)
					{
						var keyWord = Guid.NewGuid();
						Session["keyWord"] = keyWord;
						SendEmail.SendEmailTo(model.Email, keyWord.ToString());
						model.IsValidEmail = true;
						return View("ViewState/PollSecurity", model);
					}
					else
					{
						if (user.Name == null)
						{
							if (_pollsManager.IsAlreadyVote(model.CurrentPollId, user.Id))
							{
								TempData["Message"] = "You already vote";
								return View("ViewState/ResultView", _pollsManager.GetPollById(model.CurrentPollId));
							}

							var keyWord = Guid.NewGuid();
							Session["keyWord"] = keyWord;
							SendEmail.SendEmailTo(model.Email, keyWord.ToString());
							model.IsValidEmail = true;
							return View("ViewState/PollSecurity", model);
						}

						TempData["LoginMessage"] = "This email has been already registered. Log in with your email";
						return View("ViewState/PollSecurity", model);
					}
				}
			}

			if (model.KeyWord != _pollsManager.GetPollById(model.CurrentPollId).KeyWord)
			{
				TempData["KeyWordMessage"] = "Uncorrect keyword!";
				return View("ViewState/PollSecurity", model);
			}

			_pollsManager.VoteFor(model.CurrentPollId, model.CurrentAnswerId,
				_usersManager.GetUserByEmail(User.Identity.Name));
			ViewBag.IsAlreadyVote = true;

			return View("ViewState/ResultView", _pollsManager.GetPollById(model.CurrentPollId));
		}

		[Authorize]
		[HttpGet]
		public ActionResult AddComment(int pollId, string text)
		{
			var poll = _pollsManager.GetPollById(pollId);
			if (poll == null || text.IsEmpty())
			{
				return null;
			}

			var newComment = new Comment
			{
				Poll = poll,
				Text = text,
				CommentTime = DateTime.Now,
				User = _usersManager.GetUserByEmail(User.Identity.Name)
			};

			_pollsManager.AddComment(newComment);

			var model = new CommentViewModel(poll);
			return Json(model, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public ActionResult GetCommentViewModel(int pollId)
		{
			var json = Json(new CommentViewModel(_pollsManager.GetPollById(pollId)), JsonRequestBehavior.AllowGet);
			return json;
		}

		[Authorize(Roles = "admin, moderator")]
		public ActionResult DeleteComment(int pollId, int commentId)
		{
			_pollsManager.DeleteComment(pollId, commentId);
			return Json(new CommentViewModel(_pollsManager.GetPollById(pollId)), JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public ActionResult GetTimeForFinfish(int pollId)
		{
			var poll = _pollsManager.GetPollById(pollId);

			//miliseconds
			var result = (int)poll.EndDateTime.Subtract(poll.StartDateTime).TotalSeconds * 10;
			//return Json((int)poll.EndDateTime.Subtract(DateTime.Now).TotalSeconds, JsonRequestBehavior.AllowGet);
			return Json(result, JsonRequestBehavior.AllowGet);
		}
	}
}

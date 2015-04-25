namespace VotingSystem.BusinessLogic.Managers
{
	using System.Collections.ObjectModel;
	using Entities;
	using Interfaces.Managers;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using DataAccess.Interfaces.Repositories;

	public class PollsManager : IPollsManager
	{
		private readonly IPollRepository _pollRepository;
		private readonly IAnswerRepository _answerRepository;
		private readonly ICommentRepository _commentRepository;

		public PollsManager(IPollRepository pollRepository,
			IAnswerRepository answerRepository, ICommentRepository commentRepository)
		{
			_pollRepository = pollRepository;
			_answerRepository = answerRepository;
			_commentRepository = commentRepository;
		}

		public IQueryable<Poll> Polls
		{
			get { return _pollRepository.All; }
		}

		public void DeletePoll(int id)
		{
			_pollRepository.Delete(id);
		}

		public IQueryable<Poll> GetActivePolls()
		{
			return _pollRepository.All.Where(p => p.EndDateTime > DateTime.Now);
		}

		public IQueryable<Poll> GetAllowedPolls()
		{
			return _pollRepository.All.Where(p => p.IsBlocked == false);
		}

		public IEnumerable<Poll> GetUsersPolls(string email)
		{
			return _pollRepository.All.Where(p => p.User.Email == email);
		}

		public IQueryable<Poll> GetClosedPolls()
		{
			return _pollRepository.All.Where(p => p.EndDateTime <= DateTime.Now);
		}

		public int CreatePoll(Poll poll)
		{
			_pollRepository.InsertOrUpdate(poll);
			_pollRepository.Save();

			foreach (var a in poll.Answers)
			{
				a.Users = new Collection<User>();
				a.Count = 0;
				a.PollId = poll.Id;
				_answerRepository.InsertOrUpdate(a);
			}
			_answerRepository.Save();

			
			return poll.Id;
		}

		public Poll GetPollById(int id)
		{
			return _pollRepository.Find(id);
		}

		public bool VoteFor(int pollId, int answerId, User user)
		{
			var poll = _pollRepository.Find(pollId);
			
			if (poll != null)
			{
				var answer = _answerRepository.Find(answerId);
				
				if (answer != null)
				{
					answer.Count++;
					answer.Users.Add(user);
					poll.Count++;

					_pollRepository.Save();
					return true;
				}
			}
			return false;
		}

		public void EditPoll(Poll poll)
		{
			_pollRepository.InsertOrUpdate(poll);
			_pollRepository.Save();
		}

		public bool IsAlreadyVote(int pollId, int userId)
		{
			return _pollRepository.Find(pollId).Answers.SelectMany(p => p.Users).Any(user => user.Id == userId);
		}


		public void AddComment(Comment newComment)
		{
			_commentRepository.InsertOrUpdate(newComment);
			_commentRepository.Save();
		}

		public void DeleteComment(int pollId, int commentId)
		{
			var poll = GetPollById(pollId);
			var comment = poll.Comments.FirstOrDefault(c => c.Id == commentId);
			if (comment == null)
			{
				return;
			}
			_commentRepository.Delete(commentId);
			_commentRepository.Save();
		}


	
	}
}

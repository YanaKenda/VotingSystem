namespace VotingSystem.BusinessLogic.Interfaces.Managers
{
	using System.Collections.Generic;
	using System.Linq;
	using Entities;


	public interface IPollsManager
	{
		IQueryable<Poll> Polls { get; }

		void DeletePoll(int id);

		IQueryable<Poll> GetActivePolls();

		IQueryable<Poll> GetClosedPolls();

		IQueryable<Poll> GetAllowedPolls();

		Poll GetPollById(int id);

		bool VoteFor(int pollId, int answerId, User user);

		void EditPoll(Poll pollToBan);

		bool IsAlreadyVote(int pollId, int userId);

		void AddComment(Comment newComment);

		void DeleteComment(int pollId, int commentId);

		IEnumerable<Poll> GetUsersPolls(string p);

		int CreatePoll(Poll poll);
	}
}
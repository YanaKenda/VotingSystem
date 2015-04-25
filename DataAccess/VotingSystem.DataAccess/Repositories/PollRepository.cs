namespace VotingSystem.DataAccess.Repositories
{
	using DbContext;
	using Interfaces.Repositories;
	using Entities;

	public class PollRepository : Repository<Poll>, IPollRepository
	{
		public PollRepository(VotingSystemContext context) : base(context) { }
	};
}

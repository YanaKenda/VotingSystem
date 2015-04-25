namespace VotingSystem.DataAccess.Repositories
{
	using DbContext;
	using Interfaces.Repositories;
	using Entities;

	public class AnswerRepository : Repository<Answer>, IAnswerRepository
	{
		public AnswerRepository(VotingSystemContext context) : base(context) { }
	};
}

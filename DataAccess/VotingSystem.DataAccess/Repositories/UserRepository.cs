namespace VotingSystem.DataAccess.Repositories
{
	using DbContext;
	using Interfaces.Repositories;
	using Entities;

	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(VotingSystemContext context) : base(context) { }
	}
}

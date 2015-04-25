namespace VotingSystem.DataAccess.Repositories
{
	using DbContext;
	using Interfaces.Repositories;
	using Entities;

	public class RoleRepository : Repository<Role>, IRoleRepository
	{
		public RoleRepository(VotingSystemContext context) : base(context) { }
	};
}

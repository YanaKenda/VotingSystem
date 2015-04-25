namespace VotingSystem.DataAccess.Repositories
{
	using DbContext;
	using Interfaces.Repositories;
	using Entities;

	public class CommentRepository : Repository<Comment>, ICommentRepository
	{
		public CommentRepository(VotingSystemContext context) : base(context) { }
	};
}

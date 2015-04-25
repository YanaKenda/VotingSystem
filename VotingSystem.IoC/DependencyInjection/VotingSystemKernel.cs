namespace VotingSystem.IoC.DependencyInjection
{
	using Ninject;
	using Ninject.Web.Common;
	using BusinessLogic.Managers;
	using DataAccess.DbContext;
	using DataAccess.Interfaces.Repositories;
	using DataAccess.Repositories;
	using BusinessLogic.Interfaces.Managers;


	public class VotingSystemKernel : StandardKernel
	{

		public VotingSystemKernel()
		{
			AddBindings();
		}

		private void AddBindings()
		{
			//repositories bindings
			Bind<VotingSystemContext>().ToSelf().InRequestScope();
			//kernel.Bind<VotingSystemContext>().To<VotingSystemContext>();
			Bind<IUserRepository>().To<UserRepository>().InRequestScope();
			Bind<IRoleRepository>().To<RoleRepository>().InRequestScope();
			Bind<IPollRepository>().To<PollRepository>().InRequestScope();
			Bind<IAnswerRepository>().To<AnswerRepository>().InRequestScope();
			Bind<ICommentRepository>().To<CommentRepository>().InRequestScope();
			//business logic bindings
			Bind<IUsersManager>().To<UsersManager>().InRequestScope();
			Bind<IPollsManager>().To<PollsManager>().InRequestScope();
		}


	}
}

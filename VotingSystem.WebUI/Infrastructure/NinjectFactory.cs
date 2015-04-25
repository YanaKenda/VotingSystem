namespace VotingSystem.WebUI.Infrastructure
{
	using Ninject;
	using System;
	using System.Web.Mvc;
	using System.Web.Routing;
	using VotingSystem.IoC.DependencyInjection;

	public class NinjectFactory : DefaultControllerFactory
	{
		private readonly IKernel _kernel = new VotingSystemKernel();

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			return controllerType == null
				? null
				: (IController)_kernel.Get(controllerType);
		}
	}
}
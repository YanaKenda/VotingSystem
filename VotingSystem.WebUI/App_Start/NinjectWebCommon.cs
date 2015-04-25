using VotingSystem.WebUI.App_Start;
using WebActivator;

[assembly: PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace VotingSystem.WebUI.App_Start
{
	using System;
	using System.Web;
	using Microsoft.Web.Infrastructure.DynamicModuleHelper;
	using Ninject;
	using Ninject.Web.Common;
	using VotingSystem.IoC.DependencyInjection;

	public static class NinjectWebCommon
	{
		private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

		/// <summary>
		/// Starts the application
		/// </summary>
		public static void Start()
		{
			DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
			DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
			Bootstrapper.Initialize(CreateKernel);
		}

		/// <summary>
		/// Stops the application.
		/// </summary>
		public static void Stop()
		{
			Bootstrapper.ShutDown();
		}

		/// <summary>
		/// Creates the kernel that will manage your application.
		/// </summary>
		/// <returns>The created kernel.</returns>
		private static IKernel CreateKernel()
		{
			var kernel = new VotingSystemKernel();
			kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
			kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();


			return kernel;
		}

		/// <summary>
		/// Load your modules or register your services here!
		/// </summary>
		public static IKernel GetKernel()
		{
			return Bootstrapper.Kernel;
		}
	}
}

namespace VotingSystem.WebUI
{
	using System.Web.Http;
	using System.Web.Mvc;
	using System.Web.Routing;
	using Infrastructure;
	using System.Data.Entity;
	using System.Web;
	using VotingSystem.DataAccess.DbContext;
	using VotingSystem.WebUI.App_Start;

	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{

			//add custom database initializer
			//Database.SetInitializer(
				//new VotingSystemInitializer());


			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			ControllerBuilder.Current.SetControllerFactory(new NinjectFactory());

		}
	}
}
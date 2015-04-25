using VotingSystem.WebUI.App_Start;
using WebActivator;

[assembly: PreApplicationStartMethod(typeof(NinjectWeb), "Start")]

namespace VotingSystem.WebUI.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject.Web;

    public static class NinjectWeb 
    {
        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
        }
    }
}

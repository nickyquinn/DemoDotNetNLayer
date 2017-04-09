using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DemoApp.Data;
using DemoApp.Data.Repository;
using DemoApp.Services;
using DemoApp.UI.Controllers;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;

namespace DemoApp.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureDependencies();
        }

        /// <summary>
        /// Configure the Unity DI.
        /// 
        /// I'm configuring *all* the app DI registrations here, even though this means the UI app has to
        /// know about n-layers it otherwise is not involved in. This is the app's "Composition root", so
        /// this is where this should happen. See more on Composition Roots here: 
        /// http://blog.ploeh.dk/2011/07/28/CompositionRoot/
        /// </summary>
        private void ConfigureDependencies()
        {
            IUnityContainer container = new UnityContainer();
            //Register per lifetime and because I've got an overloaded AppContext constructor, force it to use the 
            //parameterless version (by default Ninject does this, but Unity it appears uses the most parameterised instead).
            container.RegisterType<DbContext, AppContext>(new PerRequestLifetimeManager(), new InjectionConstructor());
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IPersonnelService, PersonnelService>();
            container.RegisterType<HomeController>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}

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

        private void ConfigureDependencies()
        {
            IUnityContainer container = new UnityContainer();
            //Register per lifetime and because I've got an overloaded AppContext constructor, force it to use the 
            //parameterless version (by default Ninject does this, but Unity it appears uses the most parameterised)
            container.RegisterType<DbContext, AppContext>(new PerRequestLifetimeManager(), new InjectionConstructor());
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IPersonnelService, PersonnelService>();
            container.RegisterType<HomeController>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}

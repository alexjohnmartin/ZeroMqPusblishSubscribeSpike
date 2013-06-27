using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using EnforcementFrontEnd.Logic;

namespace EnforcementFrontEnd
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            LocationsEventStore.Initialize(1000, 20);
            MessageSubscriberSingleton.Configure(new MessageSubscriberConfig{Host = ConfigurationManager.AppSettings["MessageSubscriberSingleton.Host"]});
        }

        protected void Application_End()
        {
            MessageSubscriberSingleton.Dispose();
        }
    }
}
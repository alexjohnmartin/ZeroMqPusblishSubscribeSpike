using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using ParkingFrontEnd.Service;

namespace ParkingFrontEnd
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            MessagePublisherSingleton.Configure(new MessagePublisherConfig{TestMode = false, Host = ConfigurationManager.AppSettings["MessagePublisherSingleton.Host"]});
        }

        public void Application_End()
        {
            MessagePublisherSingleton.Dispose();
        }
    }
}
using System.Collections.Generic;
using System.Web.Mvc;
using EnforcementFrontEnd.Logic;
using EnforcementFrontEnd.Models;

namespace EnforcementFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = PopulateViewModel(); 
            return View(model);
        }

        private EnforcementModel PopulateViewModel()
        {
            return new EnforcementModel
                       {
                           Locations = GetLocationsFromEventStore()
                       };
        }

        private IEnumerable<Location> GetLocationsFromEventStore()
        {
            return LocationsEventStore.Locations; 
        }
    }
}

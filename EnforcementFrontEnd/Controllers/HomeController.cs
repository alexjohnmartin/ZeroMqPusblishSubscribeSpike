using System.Collections.Generic;
using System.Web.Mvc;
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
                           Locations = GenerateLocations()
                       };
        }

        private IEnumerable<Location> GenerateLocations()
        {
            var locations = new List<Location>(); 
            for (int l = 1000; l < 1020; l++)
            {
                locations.Add(new Location{LocationId = l});
            }
            return locations; 
        }
    }
}

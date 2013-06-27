using System;
using System.Web.Mvc;
using ParkingFrontEnd.Models;
using ParkingFrontEnd.Service;

namespace ParkingFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new HomeModel(); 

            return View(model);
        }

        [HttpPost]
        public ActionResult StartParking(HomeModel model)
        {
            _sendMessageService.SendStartParkingMessage(model.Location, model.Duration, DateTime.Now);

            model.Message = "start parking message sent"; 
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult ExtendParking(HomeModel model)
        {
            _sendMessageService.SendExtendParkingMessage(model.Location, model.Duration);

            model.Message = "extend parking message sent";
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult StopParking(HomeModel model)
        {
            _sendMessageService.SendStopParkingMessage(model.Location, DateTime.Now);

            model.Message = "stop parking message sent";
            return View("Index", model);
        }

        private readonly ISendMessageService _sendMessageService;

        public HomeController() : this(new SendMessageService())
        {}

        public HomeController(ISendMessageService sendMessageService)
        {
            _sendMessageService = sendMessageService;
        }
    }
}

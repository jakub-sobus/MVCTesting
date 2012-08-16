using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Users.Repository;
using Common.Repository;

namespace ConstructionJournal.Controllers
{
    public class HomeController : Controller
    {

        private UsersRepository userRepository = new UsersRepository();
        private CommonRepository commonRepository = new CommonRepository();

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        [HttpGet]
        public ActionResult UpdateUserInfo()
        {
            List<string> Cities = commonRepository.GetCities();
            ViewBag.autocompleteCities = Cities;
            User oldInfo = userRepository.GetUserInfo(User.Identity.Name);
            return View(oldInfo);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}

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
            ViewBag.Message = "Strona główna";

            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult UpdateUserInfo()
        {
            List<string> Cities = commonRepository.GetCities();
            ViewBag.autocompleteCities = Cities;
            User oldInfo = userRepository.GetUserInfo(User.Identity.Name);

            UserInfoViewMetadata viewData = 
                new UserInfoViewMetadata() { 
                username=oldInfo.username,
                Apartment_number=oldInfo.Apartment_number,
                City=oldInfo.City,
                Email=oldInfo.Email,
                House_number=oldInfo.House_number,
                Name=oldInfo.Name,
                Project=oldInfo.Project,
                Street=oldInfo.Street,
                Surname=oldInfo.Surname,
                Zip_code=oldInfo.Zip_code
            };
            return View(viewData);
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateUserInfo(UserInfoViewMetadata user)
        {
            userRepository.UpdateUserInfo(user);
            return RedirectToAction("Index", "Blog");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}

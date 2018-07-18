using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Collections.Specialized.NameObjectCollectionBase;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly NationalParkSqlDal dal;

        public HomeController()
        {
            dal = new NationalParkSqlDal(ConfigurationManager.ConnectionStrings["NPGeek"].ConnectionString);
        }

        // GET: Home
        public ActionResult Index()
        {
            List<Park> parks = dal.GetAllParks();

            return View("Index", parks);
        }

        public ActionResult Detail()
        {
            return View("Index");
        }

        public ActionResult Survey()
        {
            return View("Index");
        }

        public ActionResult Favorite()
        {
            return View("Index");
        }
    }
}
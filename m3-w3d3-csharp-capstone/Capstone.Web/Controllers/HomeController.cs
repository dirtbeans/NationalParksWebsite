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
        private readonly SurveySqlDal surveyDal;
        private readonly WeatherSqlDal weatherDal;
        

        public HomeController()
        {
            dal = new NationalParkSqlDal(ConfigurationManager.ConnectionStrings["NPGeek"].ConnectionString);
            surveyDal = new SurveySqlDal(ConfigurationManager.ConnectionStrings["NPGeek"].ConnectionString);
            weatherDal = new WeatherSqlDal(ConfigurationManager.ConnectionStrings["NPGeek"].ConnectionString);

            
           
           
        }

        // GET: Home
        public ActionResult Index()
        {
            List<Park> parks = dal.GetAllParks();

            return View("Index", parks);
        }

        public ActionResult Detail(string id)
        {
            ViewBag.IsFahrenheit = Session["TempType"];
            ParksAndWeather parksAndWeather = new ParksAndWeather();

            Park park = dal.GetOnePark(id);

            List<Weather> weather = weatherDal.GetWeatherForPark(id);

            parksAndWeather.ParkWithWeather = park;
            parksAndWeather.ListOfWeather = weather;

            return View("Detail", parksAndWeather);
        }

        public ActionResult SwitchTemperature(string parkId)
        {
            Object value = this.Session["IsFahrenheit"];
            if (value == null)
            {
                Session["IsFahrenheit"] = true;
            }

            Session["IsFahrenheit"] = !(bool?)Session["IsFahrenheit"];



            return RedirectToAction("Detail", "Home", new { id = parkId });
        }

        public ActionResult Survey()
        {

            Survey model = new Survey();

            IEnumerable<Park> parks = dal.GetAllParks();


            IList<SelectListItem> parkListItems = new List<SelectListItem>();

            foreach (Park p in parks)
            {
                SelectListItem item = new SelectListItem { Value = p.ParkCode, Text = p.ParkName };
                parkListItems.Add(item);
            }

            model.ParkSelectListItems = parkListItems;

            return View("Survey", model);
        }

        [HttpPost]
        public ActionResult Survey(Survey survey)
        {

            surveyDal.PostSurvey(survey);

            return RedirectToAction("Favorite");
        }

        public ActionResult Favorite()
        {
            List<ParkSurveyCount> parkSurveyList = new List<ParkSurveyCount>();

            parkSurveyList = surveyDal.GetSurveyCountList();


            return View("Favorite", parkSurveyList);
        }
    }
}
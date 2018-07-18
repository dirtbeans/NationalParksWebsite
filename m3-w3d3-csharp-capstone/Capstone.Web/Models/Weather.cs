using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Weather
    {
        public string ParkCode { get; set; }
        public int FiveDayForecastValue { get; set; }
        public int Low { get; set; } //Degrees in F
        public int High { get; set; } //Degrees in F
        public string Forecast { get; set; }

    }
}
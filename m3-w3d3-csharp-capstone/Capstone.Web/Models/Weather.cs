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
        public int LowInC
        {
            get
            {
                double celciusLow;

                celciusLow = (High - 32) * (5.0 / 9.0);
                return (int)celciusLow;
            }
        }
        public int HighInC { get; set; }
    }
}
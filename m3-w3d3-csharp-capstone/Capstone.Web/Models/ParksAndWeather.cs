using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class ParksAndWeather
    {
        public Park ParkWithWeather { get; set; }
        public List<Weather> ListOfWeather { get; set; }
        
    }
}
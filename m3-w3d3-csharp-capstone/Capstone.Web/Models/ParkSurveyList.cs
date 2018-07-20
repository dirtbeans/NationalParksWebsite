using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class ParkSurveyList
    {
        public string ParkCode { get; set; }
        public string ParkName { get; set; }
        public int SurveyCount { get; set; }
    }
}
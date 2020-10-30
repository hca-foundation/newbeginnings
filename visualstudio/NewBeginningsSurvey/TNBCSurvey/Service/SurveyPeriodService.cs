using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNBCSurvey.Service
{
    public class SurveyPeriodService
    {
        public static string GetCurrentSurveyPeriod()
        {
            DateTime dt = DateTime.Now;
            string surveyPeriod = dt.Year + "Q" + (dt.Month + 2) / 3;
            return surveyPeriod;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNBCSurvey.Models
{
    public class SurveyExportAnswer
    {
        public int Client_SID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string GroupNumber { get; set; }
        public string Question_Period { get; set; }
        public string Question_Text { get; set; }
        public string Answer_Text { get; set; }
    }
}
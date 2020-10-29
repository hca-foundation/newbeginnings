using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TNBCSurvey.Models
{
    public class SurveyAnswer
    {
        [Key]
        public int Answer_SID { get; set; }
        [Required]
        public int Client_SID { get; set; }
        [Required]
        [StringLength(6)]
        public string  Question_Period { get; set; }
        [Required]
        public int Question_SID { get; set; }
        [Required]
        [StringLength(1000)]
        public string Answer_Text { get; set; }
    }
}
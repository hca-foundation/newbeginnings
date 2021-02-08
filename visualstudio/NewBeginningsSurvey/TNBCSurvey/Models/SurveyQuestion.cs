using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TNBCSurvey.Models
{
    public class SurveyQuestion
    {
        [Key]
        public int Question_SID { get; set; }
        [Required]
        [StringLength(10)]
        public string Question_Type { get; set; }
        [Required]
        [StringLength(1000)]
        public string Question_Text { get; set; }
        [Required]
        public int DisplayOrder { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
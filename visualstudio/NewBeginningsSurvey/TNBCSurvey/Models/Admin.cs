using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TNBCSurvey.Models
{
    public class Admin
    {
        [Key]
        public int Admin_SID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TNBCSurvey.Models
{
    public class Client
    {
        [Key]
        public int Client_SID { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        public int GroupNumber { get; set; }
        [Required]
        public DateTime ProgramStartDate { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
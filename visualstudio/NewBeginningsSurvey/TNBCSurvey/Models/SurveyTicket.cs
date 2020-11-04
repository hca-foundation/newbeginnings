using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TNBCSurvey.Models
{
    public class SurveyTicket
    {
        [Key]
        public int Ticket_SID { get; set; }
        [Required]
        public int Client_SID { get; set; }
        [Required]
        [StringLength(50)]
        public string Token { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public bool TokenUsed { get; set; }
        [Required]
        [StringLength(6)]
        public string TimePeriod { get; set; }
    }
}
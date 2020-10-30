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
<<<<<<< HEAD
        public int User_SID { get; set; }
=======
        public int Client_SID { get; set; }
>>>>>>> 6f833f77287e8d499be64e1d4522e3adf7b7e0f9
        [Required]
        [StringLength(50)]
        public string Token { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public bool TokenUsed { get; set; }
    }
}
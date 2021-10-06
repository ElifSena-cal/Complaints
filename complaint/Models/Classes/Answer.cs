using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Complaint.Models.Classes
{
    public class Answer
    {
        [Key]
        public int AnswerID { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(500)]
        public string Text { get; set; }
        public int CompanyID { get; set; }
        public virtual Company Company { get; set; }
        public bool Case { get; set; }

    }
}
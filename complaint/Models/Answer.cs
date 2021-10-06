using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace complaint.Models
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

        public int ComplaintID { get; set; }
        public virtual Complaints Complaint { get; set; }

    }
}
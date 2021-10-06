using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace complaint.Models
{
    public class Complaints
    {
        [Key]
        public int ComplaintID { get; set; }

        
        [Column(TypeName = "Varchar")]
        [StringLength(650)]
        [Required]
        [DisplayName("Şikayet")]
        public string ComplainDetail { get; set; }

        public bool Result { get; set; }

        public DateTime Date { get; set; }
        public bool Case { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string Document { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        [Required]
        [DisplayName("Başlık")]
        public string ComplaintTitle { get; set; }
        public int CompanyID { get; set; }


        public virtual Company  Company { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }
        public ICollection<Answer> Answers { get; set; }


        public ICollection<Comment> Comments { get; set; }



    }
}
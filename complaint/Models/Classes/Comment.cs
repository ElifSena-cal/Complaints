using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Complaint.Models.Classes
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(500)]
        public string Text { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(200)]
        public string Image { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public bool Case { get; set; }
    }
}
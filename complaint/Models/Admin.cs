using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace complaint.Models
{
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(80)]
        public string AdminEmail { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(80)]
        public string Password { get; set; }
    }
}
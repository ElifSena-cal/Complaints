using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Complaint.Models.Classes
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(80)]
        [Required]
        [DisplayName("Ad-Soyad")]
        public string UserNameSurname { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string UserImage { get; set; }
    

     
        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        [Required]
        [DisplayName("E-Mail")]
        [EmailAddress(ErrorMessage ="E-posta adresinizi düzgün giriniz")]
        public string EMail { get; set; }
     
        [Required]
        [DisplayName("Şifre")]
        public string UserPassword{ get; set; }

        public ICollection<Comment> Comments { get; set; }

        public List<Complaints> Complaints { get; set; }

        public int RoleID { get; set; }
        [ForeignKey("RoleID")]
        public UserRole Role { get; set; }
        public User()
        {
            RoleID = 1;
            UserImage = "/img/" + "anonymous.png";
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace complaint.Models
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
        [EmailAddress(ErrorMessage = "E-posta adresinizi düzgün giriniz")]
        public string EMail { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        [Required]
        [DisplayName("Şifre")]
        public string UserPassword { get; set; }
        public ICollection<Comment> Comments { get; set; }


        public ICollection<Complaints> Complaints { get; set; }


        public string Role { get; set; }
        public User()
        {
            Role = "User";
            UserImage = "/img/" + "anonymous.png";
        }
       
      
    }
}
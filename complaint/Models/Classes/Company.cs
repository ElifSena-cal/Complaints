using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Complaint.Models.Classes
{
 
    public class Company
    {
        [Key]
        public int CompanyID { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(150)]

        [Required]
        [DisplayName("Şirket Adı")]
       
        public string CompanyName { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        [Required]
        [DisplayName("Ad-Soyad")]
   

        public string NameSurname { get; set; }
        public int Dissolved { get; set; }

        [Required]
        [DisplayName("Telefon Numarası")]
      

        public string PhoneNumber { get; set; }
        public int TotalComplaint { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(150)]
     
        public string WebUrl { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(150)]

        [Required]
        [DisplayName("E-Mail")]
        [EmailAddress(ErrorMessage = "E-posta adresinizi düzgün giriniz")]
        public string EPosta { get; set; }
        public bool Case { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Complaints> Complaints { get; set; }

    }
}
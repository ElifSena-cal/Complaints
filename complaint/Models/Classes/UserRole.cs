using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Complaint.Models.Classes
{
    public class UserRole
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public List<User> Users { get; set; }
    }
}
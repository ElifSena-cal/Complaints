using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using MySql.Data.EntityFramework;

namespace Complaint.Models.Classes
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class Context : DbContext
    {

       
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Complaints> Complaints { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> Roles { get; set; }

    }
}
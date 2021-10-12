using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace complaint.Models
{
    public class ModelView
    {

        public Company Companies { get; set; }
        public List<Complaints> Complaints { get; set; }
        public Complaints complaints { get; set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; }
        public Answer Answer { get; set; }


    }
}
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

        public User User { get; set; }
 

    }
}
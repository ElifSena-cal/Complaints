using complaint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace complaint.Controllers
{
    public class HomeController : Controller
    {
        Context c = new Context();
        [OutputCache(Duration = 60)]
        public ActionResult Index()
        {

            var value1 = c.Users.Count().ToString();
            ViewBag.v1 = value1;
            var value2 = c.Companies.Where(x => x.Case == true).Count().ToString();
            ViewBag.v2 = value2;
            var value3 = c.Companies.Count().ToString();
            if (value3 != "0")
            {
                value3 = c.Companies.Sum(x => x.Dissolved).ToString();

            }
            else
            {
                value3 = "0";
            }

            ViewBag.v3 = value3;
            return View();
        }

        [HttpGet]
        public ActionResult BrandRequest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BrandRequest(Company company)
        {
            if (!ModelState.IsValid)
            {

                return View("Index");

            }

            c.Companies.Add(company);
            c.SaveChanges();
            System.Threading.Thread.Sleep(1000);
            return RedirectToAction("Index");
        }

    }
}
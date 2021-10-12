using complaint.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace complaint.Controllers
{
    public class BrandController : Controller
    {
        Context c = new Context();
        // GET: Brand
        public ActionResult CompareBrand()
        {
            var list = c.Companies.ToList();


            var r = new Random();
            var tenRandomBrand = list.OrderBy(u => r.Next()).Take(100);

            return View(tenRandomBrand);
        }

        public JsonResult GetBrand(string brandName)
        {

            var customers = (from brand in c.Companies
                             where brand.CompanyName.StartsWith(brandName)
                             select new
                             {
                                 label = brand.CompanyName,
                                 val = brand.CompanyID
                             }).ToList();

            return Json(customers, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult Top100()
        {
            var list = c.Companies.OrderByDescending(a => a.Dissolved).Take(100).ToList();
            return View(list);
        }

        public ActionResult CompareDetail(string id, string brandName1, string brandName2)
        {
            if (brandName1 == "" || brandName2 == "")
            {

                ViewBag.Message = "Lütfen marka isimlerini boş geçmeyiniz";
                return RedirectToAction("CompareBrand");
            }
            if (id != null)
            {
                var brandNames = id.Split('-');
                brandName1 = brandNames[0];
                brandName2 = brandNames[1];
            }

            List<Company> bran2 = c.Companies.Where(b => b.CompanyName == brandName1).ToList();
            List<Company> brand1 = c.Companies.Where(a => a.CompanyName == brandName2).ToList();
            if (bran2.Count == 0 || brand1.Count == 0)
            {
                return RedirectToAction("CompareBrand");
            }
            var d = brand1.Concat(bran2).ToList();



            return View(d);
        }

        public ActionResult BrandDetail(int id)
        {
            ModelView model = new ModelView();
            model.Companies = c.Companies.Where(a => a.CompanyID == id).FirstOrDefault();
            model.Complaints = c.Complaints.Where(d => d.CompanyID == id).Where(e => e.Case).ToList();



            return View(model);
        }

     

    }
}
using complaint.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace complaint.Controllers
{
    public class BrandPanelController : Controller
    {
        // GET: BrandPanel
        Context c = new Context();
        public ActionResult Index(Company Companies)
        {
            var brandName = c.Companies.Where(a => a.EPosta == Companies.EPosta && a.CompanyPassword == Companies.CompanyPassword).FirstOrDefault();
             
            if (brandName == null)
            {
                return RedirectToAction("Index", "Home");
            }
         
            int ıd = brandName.CompanyID;
            HttpCookie kt = new HttpCookie("Company", ıd.ToString());
            Response.Cookies.Add(kt);
            ViewBag.CompanyID = kt;
            FormsAuthentication.SetAuthCookie(brandName.CompanyName, false);

            var totalComplaint = c.Complaints.Where(a => a.CompanyID == brandName.CompanyID).Count().ToString();
            ViewBag.totalComplaint = totalComplaint;

            var totalSolution = c.Complaints.Where(a => a.CompanyID==brandName.CompanyID).Count().ToString();
            ViewBag.totalSolution = totalSolution;
            return View();
        }
        public ActionResult GetComplaint(int id)
        {
            var list = c.Complaints.Where(a => a.CompanyID == id).Where(b=>b.Result==false).ToList();
            return View(list);
        }

        public ActionResult CompanySpecificComplaint(int id)
        {
            var detail = c.Complaints.Where(a => a.ComplaintID == id).FirstOrDefault();

            return View(detail);
        }
        public ActionResult ReplytoComplaint(string detailtxt, int ComplaintID, int CompanyID)
        {

            Answer answer = new Answer();
            answer.Text = detailtxt;
            answer.CompanyID = CompanyID;
            answer.ComplaintID = ComplaintID;
            c.Answers.Add(answer);


            c.SaveChanges();

            return Redirect($"/BrandPanel/CompanySpecificComplaint/{ComplaintID}");
        }
        [HttpGet]
        public ActionResult BrandProfile(int id)
        {
            var profile = c.Companies.Find(id);
            return View("BrandProfile", profile);
        }
        [HttpPost]
        public ActionResult BrandProfileUpdate(Company company)
        {
            var profile = c.Companies.Where(a => a.CompanyID == company.CompanyID).FirstOrDefault();


            string fileName = Path.GetFileName(Request.Files[0].FileName);
            if (fileName != "")
            {
                string _fileName = DateTime.Now.ToString("yymmssfff") + fileName;

                string path = "~/CompanyLogo/" + _fileName;
                Request.Files[0].SaveAs(Server.MapPath(path));
                profile.CompanyLogo = "/CompanyLogo/" + _fileName;
            }




            profile.CompanyName = company.CompanyName;
            profile.EPosta = company.EPosta;


            profile.CompanyPassword = company.CompanyPassword;
            c.SaveChanges();
            return Redirect($"/BrandPanel/BrandProfile/{profile.CompanyID}");
        }
        public ActionResult BrandLogout()
        {
            Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult AnsweredComplaints(int id)
        {
            var list = c.Complaints.Where(a => a.CompanyID == id).Where(x => x.Result == true).ToList();
            return View(list);
        }
        public ActionResult AnsweredComplaintDetail(int id)
        {
            var detail = c.Complaints.Where(a => a.ComplaintID == id).FirstOrDefault();
            var answer = c.Answers.Where(b => b.ComplaintID == detail.ComplaintID).Select(d => d.Text).FirstOrDefault();
            ViewBag.Answer = answer;
            return View(detail);
        }


    }
}
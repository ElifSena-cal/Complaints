using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using complaint.Models;
namespace complaint.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        // GET: Admin
        Context c = new Context();
        public ActionResult Index()
        {
            var totalComplaint = c.Complaints.Count().ToString();
            ViewBag.totalComplaint = totalComplaint;
            var totalComment = c.Comments.Count().ToString();
            ViewBag.totalComment = totalComment;
            var totalCompany = c.Complaints.Count().ToString();
            ViewBag.totalCompany = totalCompany;
            var totalSolution = c.Complaints.Where(a => a.Case == true).Count().ToString();
            ViewBag.totalSolution = totalSolution;
            return View();
        }
        public ActionResult GetComplaints()
        {
            var list = c.Complaints.Where(x => x.Case == true).ToList();
            return View(list);
        }
        public ActionResult GetComplaintDetailAdmin(int id)
        {
            List<SelectListItem> value1 = (from x in c.Companies.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CompanyName,
                                               Value = x.CompanyID.ToString()
                                           }).ToList();
            ViewBag.vlu1 = value1;
            var detail = c.Complaints.Where(a => a.ComplaintID == id).FirstOrDefault();
            return View("GetComplaintDetailAdmin", detail);
        }
        public ActionResult UpdateComplaint(Complaints complaints)
        {
            var com = c.Complaints.Find(complaints.ComplaintID);

            com.ComplaintTitle = complaints.ComplaintTitle;
            com.Date = complaints.Date;
            com.CompanyID = complaints.CompanyID;
            com.ComplainDetail = complaints.ComplainDetail;
            c.SaveChanges();
            return RedirectToAction("GetComplaints");
        }
        public ActionResult ComplaintRequest()
        {

            var list = c.Complaints.Where(a => a.Case == false).ToList();
            return View(list);


        }
        public ActionResult DeleteComplaint(int id)
        {
            Complaints complaints = c.Complaints.Where(a => a.ComplaintID == id).FirstOrDefault();

            c.Complaints.Remove(complaints);
            c.SaveChanges();
            return RedirectToAction("ComplaintRequest");
        }
        public ActionResult ApproveComplaint(int id)
        {
            var a = c.Complaints.Find(id);
            a.Case = true;
            c.SaveChanges();
            return RedirectToAction("ComplaintRequest");

        }
        public ActionResult GetComment()
        {
            var list = c.Comments.Where(x => x.Case == true).ToList();
            return View(list);
        }
        public ActionResult ApproveComment(int id)
        {
            var a = c.Comments.Find(id);
            a.Case = true;
            c.SaveChanges();
            return RedirectToAction("GetComment");

        }
        public ActionResult CommentRequest()
        {

            var list = c.Comments.Where(a => a.Case == false).ToList();
            return View(list);


        }
        public ActionResult DeleteComment(int id)
        {
            Comment comment = c.Comments.Where(a => a.CommentID == id).FirstOrDefault();

            c.Comments.Remove(comment);
            c.SaveChanges();
            return RedirectToAction("CommentRequest");
        }
        public ActionResult GetBrand()
        {
            var list = c.Companies.Where(x => x.Case == true).ToList();
            return View(list);
        }
        public ActionResult BrandRequest()
        {

            var list = c.Companies.Where(a => a.Case == false).ToList();
            return View(list);


        }
        public ActionResult ApproveBrand(int id)
        {
            var a = c.Companies.Where(b => b.CompanyID == id).FirstOrDefault();
            a.Case = true;
            c.SaveChanges();


            if (a.EMail != null)
            {
                Random rnd = new Random();
                int newPassword = rnd.Next();
                a.CompanyPassword = newPassword.ToString();
                c.SaveChanges();
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "elifsenacal99@gmail.com";
                WebMail.Password = "ElifSena00.";
                WebMail.SmtpPort = 587;
                WebMail.Send(a.EMail, "Giriş Şifreniz", "Şifreniz: " + newPassword);




            }
            return RedirectToAction("BrandRequest");


        }
        [HttpGet]
        public ActionResult AddCompany()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCompany(Company company)
        {
            if (Request.Files.Count > 0)
            {
                string fileName = Path.GetFileName(Request.Files[0].FileName);
                string _fileName = DateTime.Now.ToString("yymmssfff") + fileName;

                string path = "~/CompanyLogo/" + _fileName;
                Request.Files[0].SaveAs(Server.MapPath(path));
                company.CompanyLogo = "/CompanyLogo/" + _fileName;


            }
            c.Companies.Add(company);
            c.SaveChanges();
            return RedirectToAction("GetBrand");
        }
        public ActionResult AdminLogout()
        {
            Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            FormsAuthentication.SignOut();
            return RedirectToAction("Home", "Account");
        }

    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using complaint.Models;
using static complaint.Models.Company;
using static complaint.Models.Complaints;

namespace Complaint.Controllers
{
    public class ComplaintController : Controller
    {
        // GET: Complaint
        Context c = new Context();


        public ActionResult Index()
        {
            var value1 = c.Complaints.Where(a => a.Case == true).Count().ToString();
            ViewBag.v1 = value1;
            var list = c.Complaints.Where(a => a.Case == true).ToList();
            return View(list.ToList());
        }
        [Authorize(Roles = "User")]
        [HttpGet]
        public ActionResult WriteaComplaint()
        {
            IEnumerable<Company> companies = c.Companies.ToList();

            List<SelectListItem> value1 = companies.Select(company => new SelectListItem()
            {
                Value = company.CompanyID.ToString(),
                Text = company.CompanyName
            }).ToList();
            value1.Insert(0, new SelectListItem()
            {
                Value = ((int)CompanySpecialOption.Diğer).ToString(),
                Text = CompanySpecialOption.Diğer.ToString()

            });
            ViewBag.vlu1 = value1;
            return View();
        }
        [HttpPost]

        public ActionResult WriteaComplaint(Complaints complaint, int id, string other)
        {
            try
            {
                


                    if (Request.Files.Count > 0)
                    {
                        string fileName = Path.GetFileName(Request.Files[0].FileName);
                        string _fileName = DateTime.Now.ToString("yymmssfff") + fileName;
                        if (fileName != "")
                        {
                            string path = "~/Document/" + _fileName;
                            Request.Files[0].SaveAs(Server.MapPath(path));
                            complaint.Document = "/Document/" + _fileName;
                        }

                    }
                    if (other != "")
                    {
                        Company company = new Company();
                        company.CompanyName = other;


                        c.Companies.Add(company);
                        c.SaveChanges();

                        complaint.CompanyID = company.CompanyID;

                    }

                    complaint.Date = DateTime.Now;
                    complaint.UserID = id;

                    c.Complaints.Add(complaint);
                    c.SaveChanges();
                    //email the company
                    var companyEposta = c.Companies.Where(e => e.Case == true).Select(x => x.EMail).FirstOrDefault();
                    if (companyEposta != null)
                    {
                        WebMail.SmtpServer = "smtp.gmail.com";
                        WebMail.EnableSsl = true;
                        WebMail.UserName = "elifsenacal99@gmail.com";
                        WebMail.Password = "ElifSena00.";
                        WebMail.SmtpPort = 587;
                        WebMail.Send(companyEposta, "Yeni şikayet var", "Şikayet sayfanızı kontrol edeniz");
                    }
                
                return RedirectToAction("Home", "Account");
            }
            catch (Exception)
            {

                return RedirectToAction("Home", "Account");
            }
          
        }


    
        public ActionResult GetComplaintDetail(int id)
        {
            ModelView view = new ModelView();
            view.complaints= c.Complaints.Where(z => z.ComplaintID == id).Where(a => a.Case == true).FirstOrDefault();


            view.Comments = c.Comments.Where(e => e.ComplaintID == id).Where(f => f.Case == true).ToList();
            view.Answer = c.Answers.Where(a => a.ComplaintID==id).FirstOrDefault();
            var companyName = c.Answers.Where(e => e.ComplaintID==id).Select(b => b.Company.CompanyName).FirstOrDefault();
            var UserName = c.Comments.Where(e => e.ComplaintID == id).Where(f => f.Case == true).Select(a => a.User.UserNameSurname).ToList();
            ViewBag.UserCommentName = UserName;
            ViewBag.companyName = companyName;

            return View(view);
        }
        [Authorize(Roles = "User")]

        public ActionResult WriteaComments(Comment comment)
        {
            try
            {
                
                c.Comments.Add(comment);
                c.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
          
        }
        public ActionResult UserSpecificComplaint(int id)
        {
            
            var value1 = c.Complaints.Where(a => a.Case == true).Where(b => b.UserID == id).Count().ToString();
            ViewBag.v1 = value1;
            var list = c.Complaints.Where(a => a.Case == true).Where(b => b.UserID == id).ToList();
            return View(list);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using complaint.Models;

namespace Complaint.Controllers
{
    public class AccountController : Controller
    {

        Context c = new Context();

        public ActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User user)
        {
            if (!ModelState.IsValid)
            {


                return RedirectToAction("Index", "Home");

            }


            c.Users.Add(user);
            c.SaveChanges();
            ViewBag.Message = "Üyelik başarılı";
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult Logın()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Logın(User user)
        {
            var value = c.Users.FirstOrDefault(x => x.EMail == user.EMail && x.UserPassword == user.UserPassword);
            if (value != null)
            {
                int a = value.UserID;
                HttpCookie kt = new HttpCookie("user", a.ToString());
                Response.Cookies.Add(kt);
                ViewBag.UserID = kt;
                FormsAuthentication.SetAuthCookie(value.UserNameSurname, false);



                return RedirectToAction("Index", "Home");
            }
            else
            {
                Response.Write("Bir şeyler yanlış");
                System.Threading.Thread.Sleep(1000);
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult Logout()
        {
            Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult PasswordReset()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PasswordReset(User user)
        {
            var mail2 = c.Users.FirstOrDefault(x => x.EMail == user.EMail);

            if (mail2 != null)
            {
                Random rnd = new Random();
                int newPassword = rnd.Next();
                mail2.UserPassword = newPassword.ToString();
                c.SaveChanges();
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "elifsenacal99@gmail.com";
                WebMail.Password = "ElifSena00.";
                WebMail.SmtpPort = 587;
                WebMail.Send(user.EMail, "Giriş Şifreniz", "Şifreniz: " + newPassword);
                ViewBag.uyari = "Şifreniz başarıyla gönderildi";



            }
            else
            {
                ViewBag.ss = "Harta";
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult UserProfile(int id)
        {
            var profile = c.Users.Find(id);
            return View("UserProfile", profile);
        }
        [HttpPost]
        public ActionResult UpdateUserProfile(User user)
        {
            var profile = c.Users.Where(a => a.UserID == user.UserID).FirstOrDefault();


            string fileName = Path.GetFileName(Request.Files[0].FileName);
            if (fileName != "")
            {
                string _fileName = DateTime.Now.ToString("yymmssfff") + fileName;

                string path = "~/Userimg/" + _fileName;
                Request.Files[0].SaveAs(Server.MapPath(path));
                profile.UserImage = "/Userimg/" + _fileName;
            }





            profile.UserNameSurname = user.UserNameSurname;
            profile.EMail = user.EMail;


            profile.UserPassword = user.UserPassword;
            c.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
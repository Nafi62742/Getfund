using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Getfund.Models;


namespace Getfund.Controllers
{
    public class HomeController : Controller
    {
        GetFundEntities db = new GetFundEntities();
        public ActionResult Index()
        {
            return View();
              
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }
        public ActionResult LoginPage()
        {
            ViewBag.Message = "";

            return View();
        }
        public ActionResult Search(string email, string LoginPass)
        {
            
            List<GUser> Users = db.GUsers.Where(temp => temp.Email.Equals(email) && temp.Password.Equals(LoginPass)).ToList();
            if (Users.Count > 0)
            {
                ViewBag.Message = "Login Successful";
                return View(Users);
            }
            else
            {
                return RedirectToAction("LoginPage");
            }
            
        }

        public ActionResult Contact()
        {
            List<GUser> Users = db.GUsers.Include("Profiles").ToList();
            Users.ToList();
            return View(Users);
        }

        public ActionResult Profile()
        {
            //List<GUser> Users = db.GUsers.ToList();
            return View();
        }

        public ActionResult Details()
        {
            //List<GUser> Users = db.GUsers.ToList();
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(GUser user)
        {
           
            db.GUsers.Add(user);
            db.SaveChanges();
             BuildEmailTemplate(user.ID);
            return RedirectToAction("LoginPage");
        }
        public ActionResult Confirm(int regId)
        {
            ViewBag.regID = regId;
            return View();
        }
        public JsonResult RegistrationConfirm(int regId)
        {
            GUser Data = db.GUsers.FirstOrDefault(x => x.ID == regId);
            Data.IsValid = true;
            db.SaveChanges();
            var msg = "Your Email is varified.";
            return Json(msg,JsonRequestBehavior.AllowGet);
            
        }
        

        private void BuildEmailTemplate(int RegiD)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/")+"GetFundMail"+".cshtml");
            //throw new NotImplementedException();
            var regInfo =db.GUsers.Where(temp =>temp.ID==RegiD).FirstOrDefault();
            var url = "https://localhost:44336/" + "Home/Confirm?regId=" + RegiD;
            body =body.Replace("@ViewBag.ConfirmationLink",url);
            body =  body.ToString();

            BuildEmailTemplate("Your account is verified",body,regInfo.Email);
        }

        private void BuildEmailTemplate(string SubjectText, string TextBody, string EmailTo)
        {
            string from,to,cc,bcc, subject,body;
            from = "botracer007@gmail.com";
            to = EmailTo.Trim();
            bcc = "";
            cc = "";
            subject = SubjectText;
            StringBuilder sb = new StringBuilder();
            sb.Append(TextBody);
            body= sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if (!String.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!String.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            mail.Subject = SubjectText;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendMail(mail);
        }

        private void SendMail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("racerkenway318@gmail.com", "Ghum1234");
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
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
        public static int idUser;
        public static String userEmail;
        public static String userName;
<<<<<<< HEAD
        public static bool emailexists=false;
=======
>>>>>>> 8bf4967bdd5b9e1bef8395bc4305d8036c8f1feb
        public ActionResult Index()
        {
            List<Project> projects = db.Projects.ToList();
            return View(projects);
              
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }
        
        public ActionResult Search(string email, string LoginPass)
        {

            if (Session["IdUsSS"] != null)
            {
                var pro = (from g in db.GUsers
                           join p in db.Profiles on g.ID equals p.ID
                           where g.Email == userEmail
                           select new ProfileShow
                           {
                               ID = g.ID,
                               Name = p.Name,
                               Address = p.Address,
                               Email = g.Email,
                               NID = p.NID,
                           }).SingleOrDefault();
                ViewBag.Message = pro.Name;
                List<Project> projects = db.Projects.ToList();
                return View(projects);

            }
            else
            {List<GUser> Users = db.GUsers.Where(temp => temp.Email.Equals(email) && temp.Password.Equals(LoginPass)).ToList();

                if (Users.Count > 0)
                {

                    var pro = (from g in db.GUsers
                               join p in db.Profiles on g.ID equals p.ID
                               where g.Email == email
                               select new ProfileShow
                               {
                                   ID = g.ID,
                                   Name = p.Name,
                                   Address = p.Address,
                                   Email = g.Email,
                                   NID = p.NID,
                               }).SingleOrDefault();
                    idUser = pro.ID;
                    userEmail = pro.Email;
                    ViewBag.Message = pro.Name;
                    Session["IdUsSS"] = pro.ID.ToString();
                    Session["UsernameSS"] = pro.Name.ToString();
                    Session["UserEmail"] = pro.Email.ToString();

                    List<Project> projects = db.Projects.ToList();
                    return View(projects);
                }
                else
                {
                    TempData["LoginError"] = "Check password or username!";
                    return RedirectToAction("LoginPage");

                }
            }
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(GUser user)
        {

<<<<<<< HEAD
            List<GUser> Users = db.GUsers.Where(temp => temp.Email.Equals(user.Email)).ToList();

            if (Users.Count < 1)
            {
                db.GUsers.Add(user);
                db.SaveChanges();
                BuildEmailTemplate(user.ID);
                return RedirectToAction("LoginPage");
=======
            List<GUser> Users = db.GUsers.Where(temp => temp.Email.Equals(email) && temp.Password.Equals(LoginPass)).ToList();
            
            if (Users.Count > 0)
            {
                
                var pro = (from g in db.GUsers
                           join p in db.Profiles on g.ID equals p.ID
                           where g.Email == email
                           select new ProfileShow
                           {
                               ID = g.ID,
                               Name = p.Name,
                               Address = p.Address,
                               Email = g.Email,
                               NID = p.NID,
                           }).SingleOrDefault();
                idUser=pro.ID;
                ViewBag.Message = pro.Name;
                
                List<Project> projects = db.Projects.ToList();
                return View(projects);
>>>>>>> 8bf4967bdd5b9e1bef8395bc4305d8036c8f1feb
            }
            else
            {
                TempData["LoginError"] = "Check password or username!";
                return RedirectToAction("Register");

            }

        }
        public ActionResult Logout()
        {
            Session["IdUsSS"] = null;
            Session["UsernameSS"] = null;
            Session["UserEmail"] = null;
            return RedirectToAction("Index");
        
        }
        [HttpGet]
        public ActionResult LoginPage()
        {
            if (Session["IdUsSS"]!=null)
            {
                return RedirectToAction("Search");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult LoginPage(GUser user)
        {
            if(user == null)
            {
                return View();
            }
            else
            {
            db.GUsers.Add(user);
            db.SaveChanges();
            BuildEmailTemplate(user.ID);
            return RedirectToAction("LoginPage");
            }
            
           
        }
      
        public ActionResult Contact()
        {
<<<<<<< HEAD
=======
            var pro = (from g in db.GUsers
                       join p in db.Profiles on g.ID equals p.ID
                       where g.ID == idUser
                       select new ProfileShow
                       {
                           Name = p.Name,
                           Address = p.Address,
                           Email = g.Email,
                           NID = p.NID,
                       }).SingleOrDefault();
            if(pro == null)
            {
                return RedirectToAction("LoginPage");
            }
            else
            {
            return View(pro);
            }
            
        }
        public ActionResult Project()
        {
            //List<GUser> Users = db.GUsers.ToList();
>>>>>>> 8bf4967bdd5b9e1bef8395bc4305d8036c8f1feb
            return View();
        }

        public ActionResult Profile()
        {
            var pro = (from g in db.GUsers
                       join p in db.Profiles on g.ID equals p.ID
                       where g.ID == idUser
                       select new ProfileShow
                       {
                           ID=g.ID,
                           Name = p.Name,
                           Address = p.Address,
                           Email = g.Email,
                           NID = p.NID,
                       }).SingleOrDefault();
            if(pro == null)
            {
                return RedirectToAction("LoginPage");
            }
            else
            {
                 return View(pro);
            }
            
        }
        public ActionResult Project()
        {
            List<Project> projects = db.Projects.ToList();
            return View(projects);
        }

        public ActionResult Details(int BoxID)
        {
<<<<<<< HEAD
            var Detail = (from pj in db.Projects
                       join p in db.Profiles on pj.ID equals p.ID
                       where pj.PId == BoxID
                       select new ShowDetails
                       {
                           ID = p.ID,
                           Name = p.Name,
                           Address = p.Address,
                           NID = p.NID,
                           Title = pj.Title,
                          Info=pj.Info,
                          Type=pj.Type,
                          Target=pj.Target,
                          MoneyRaised=pj.MoneyRaised,
                       }).SingleOrDefault();
            return View(Detail);
=======

            List<GUser> Users = db.GUsers.Where(temp => temp.Email.Equals(user.Email)).ToList();

            if (Users.Count <1)
            {
                db.GUsers.Add(user);
                db.SaveChanges();
                BuildEmailTemplate(user.ID);
                return RedirectToAction("LoginPage");
            }
            else
            {
                return RedirectToAction("LoginPage");
            }

>>>>>>> 8bf4967bdd5b9e1bef8395bc4305d8036c8f1feb
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
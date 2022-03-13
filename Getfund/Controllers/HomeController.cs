using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
       
        public static bool emailexists=false;
        public ActionResult Index()
        {
            if (Session["IdUsSS"] != null)
            {
                return RedirectToAction("Search");
            }
            else
            {
                List<Project> projects = db.Projects.OrderByDescending(x => x.Likes).Take(3).ToList();
                return View(projects);
            }
        }
        public ActionResult PictureChanger()
        {
            if (Session["IdUsSS"] != null)
            {

                return View();
            }
            else 
            {
                return RedirectToAction("LoginPage");
            }
        }
        public ActionResult PictureUpdated()
        {
            if (Session["IdUsSS"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoginPage");
            }
        }
        [HttpPost]
        public ActionResult PictureUpdated(string ProfilePicture)
        {
            Profile user = new Profile();
            user = db.Profiles.SingleOrDefault(x => x.ID==idUser);
            if (user != null)
            {
                user.ProfilePicture = ProfilePicture;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profile");//redirecting to userprofile once again
            }
            else
            {
                return View("Search");
            }

        }

        public ActionResult ProjectList()
        {
            if (Session["IdUsSS"] != null)
            {
                List<Project> proj = db.Projects.Where(x=>x.ID==idUser).ToList();
                var pro = (from g in db.GUsers
                           join p in db.Profiles on g.ID equals p.ID
                           where g.Email == userEmail
                           select new ProfileShow
                           {
                               ProfilePicture = p.ProfilePicture,
                               ProfileId = p.ProfileId,
                               ID = g.ID,
                               Name = p.Name,
                               Address = p.Address,
                               Email = g.Email,
                               NID = p.NID,
                           }).SingleOrDefault();
                
                return View(proj);
            }
            else
            {
                return RedirectToAction("LoginPage");

            }

        }
        public ActionResult Post()
        {
            if (Session["IdUsSS"] != null)
            {
                var pro = (from g in db.GUsers
                           join p in db.Profiles on g.ID equals p.ID
                           where g.Email == userEmail
                           select new ProfileShow
                           {
                               ProfilePicture = p.ProfilePicture,
                               ProfileId = p.ProfileId,
                               ID = g.ID,
                               Name = p.Name,
                               Address = p.Address,
                               Email = g.Email,
                               NID = p.NID,
                           }).SingleOrDefault();
                ViewBag.Message = pro.Name;
                return View(pro);
            }
            else
            {
                return RedirectToAction("LoginPage");

            }
            
        }

        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase file, Profile profile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/UploadFiles"), Path.GetFileName(file.FileName));
                        ViewBag.FilePath = "Pic loaded click post to save.";
                        ViewBag.FileStatus = "/UploadFiles/" + Path.GetFileName(file.FileName);
                        file.SaveAs(path);
                    }
                }
                catch (Exception)
                {
                    ViewBag.FileStatus = "Error while file uploading.";
                }
            } 
            
            return View("Post");
            
        }




        [HttpPost]
        public ActionResult UploadDevPic(HttpPostedFileBase file, Profile profile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/UploadFiles"), Path.GetFileName(file.FileName));
                        ViewBag.FilePath = "Pic loaded click post to save.";
                        ViewBag.FileStatus = "/UploadFiles/" + Path.GetFileName(file.FileName);
                        file.SaveAs(path);
                    }
                }
                catch (Exception)
                {
                    ViewBag.FileStatus = "Error while file uploading.";
                }
            }

            return View("DevAdd");

        }
        [HttpPost]
        public ActionResult UploadProfilePic(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/UploadProfilePic"), Path.GetFileName(file.FileName));
                        ViewBag.ProfilePicDone = "Profile pic loaded click post to save.";
                        ViewBag.FileStatus = "/UploadProfilePic/" + Path.GetFileName(file.FileName);
                        file.SaveAs(path);
                    }
                }
                catch (Exception)
                {
                    ViewBag.FileStatus = "Error while file uploading.";
                }
            }
            return View("PictureChanger");
        }
        [HttpGet]
        public ActionResult PostProject()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public ActionResult PostProject(Project UpProj)
        {
            List<Project> Users = db.Projects.Where(temp => temp.Title.Equals(UpProj.Title)).ToList();
            List<GUser> Check = db.GUsers.Where(temp => temp.ID==idUser).ToList();
            if (Users.Count < 1)
            {
                db.Projects.Add(UpProj);
                db.SaveChanges();
                return RedirectToAction("Search");
            }
            else
            {
                TempData["SameProj"] = "Project available with the same name!";
                return RedirectToAction("Post");

            }
        
        }
      
        public ActionResult DonationPage(int ProjectId, int DonatedMoney,String LikeC)
        {
            var Detail = (from pj in db.Projects
                          join p in db.Profiles on pj.ID equals p.ID
                          where pj.PId == ProjectId
                          select new ShowDetails
                          {
                              ProfileId = p.ProfileId,
                              ID = p.ID,
                              PId =pj.PId,
                              Name = p.Name,
                              Address = p.Address,
                              NID = p.NID,
                              Title = pj.Title,
                              VideoLink = pj.VideoLink,
                              Info = pj.Info,
                              Type = pj.Type,
                              Target = pj.Target,
                              MoneyRaised = pj.MoneyRaised,
                          }).SingleOrDefault();
            DateTime now = DateTime.Now;
            TempData["Donationdate"] = now.ToString();
            TempData["Idcheck"] = Detail.ID;
            TempData["Idcheck"] = Detail.PId;
            TempData["LikeC"] = LikeC;
            TempData["DonatedMoney"] = Convert.ToInt32( DonatedMoney) ;
            ViewBag.Message = "Donating to project";
            return View(Detail);
        }
        public ActionResult About()
        {
            if (Session["UserEmail"] != null)
            {
                string emailUser = Session["UserEmail"].ToString();

                //List<Dev> devs1 = db.DevS.SqlQuery("").ToList();
                List<Dev> Users = db.DevS.Where(temp => temp.Email.Equals(emailUser)).ToList();

                if (Users.Count < 1)
                {
                }
                else
                {
                    TempData["AdminHere"] = "I am the admin now";
                }
            }
            
            List<Dev> devs = db.DevS.ToList();
            return View(devs);
        }
        [HttpGet]
        public ActionResult DevAdd()
        {
            return View();
        }
        public ActionResult DevAdding()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DevAdding(Dev dev)
        {

            List<Dev> Users = db.DevS.Where(temp => temp.Email.Equals(dev.Email)).ToList();

            if (Users.Count < 1)
            {
                db.DevS.Add(dev);
                db.SaveChanges();

                return RedirectToAction("About");
            }
            else
            {
                TempData["Same"] = " This person is already in the list!";
                return RedirectToAction("DevAdd");

            }


            
        }
        public ActionResult Donate()
        {

            return View();    
        }
        
        [HttpPost]
        public ActionResult Donate(Donation donation, String CommentArea, Comment comment, string getCname,int PId,String PTittle,String Liked)
        {
            Project user = new Project();
            TempData["data"] = PId;
            user = db.Projects.SingleOrDefault(x => x.PId.Equals(PId));
            if (user != null)
            {
                if(user.MoneyRaised == null)
                {
                    user.MoneyRaised = 0;
                    user.MoneyRaised += donation.Amount;
                    user.Likes = Convert.ToInt32(Liked);
                }
                else
                {
                    user.MoneyRaised += donation.Amount;
                    user.Likes = Convert.ToInt32(Liked);
                }
                
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges(); //redirecting to userprofile once again

                donation.ID = idUser;
                db.Donations.Add(donation);
                db.SaveChanges();
                if (CommentArea != null)
                {
                    comment.CName = getCname;
                    comment.Comment1 = CommentArea;
                    db.Comments.Add(comment);
                    db.SaveChanges();
                }
                
            
            }
            
            
            TempData["SameProj"] = "Project available with the same name!";
            return RedirectToAction("Search");




        }
        public ActionResult DonationHistory(int IdForDonation)
        {
            if (Session["IdUsSS"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                List<Donation> donations = db.Donations.Where(temp => temp.ID == IdForDonation).ToList();

                if (donations.Count > 0)
                {
                    return View(donations);
                }
                else
                {
                    return View();
                }
            }
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
                               ProfilePicture = p.ProfilePicture,
                               ProfileId = p.ProfileId,
                               ID = g.ID,
                               Name = p.Name,
                               Address = p.Address,
                               Email = g.Email,
                               NID = p.NID,
                           }).SingleOrDefault();
                ViewBag.Message = pro.Name;
                List<Project> projects = db.Projects.OrderByDescending(x => x.Likes).Take(3).ToList();
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
                                   ProfilePicture = p.ProfilePicture,
                                   ProfileId = p.ProfileId,
                                   ID = g.ID,
                                   Name = p.Name,
                                   Address = p.Address,
                                   Email = g.Email,
                                   NID = p.NID,

                               }).SingleOrDefault();

                    if (pro != null)
                    {
                        idUser = pro.ID;
                        userEmail = pro.Email;
                        ViewBag.Message = pro.Name;
                        Session["IdUsSS"] = pro.ID.ToString();
                        Session["UsernameSS"] = pro.Name.ToString();
                        Session["UserEmail"] = pro.Email.ToString();
                        Session["UserEmail"] = pro.Email.ToString();
                        List<Project> projects = db.Projects.OrderByDescending(x => x.Likes).Take(3).ToList();
                        return View(projects);
                    }
                    else
                    {
                        TempData["LoginError"] = "Null Value";
                        return RedirectToAction("LoginPage");
                    }
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
        public ActionResult Register(GUser user, String ConfirmPassword, Profile profile, String RegName)
        {
            if (user.Password.Equals(ConfirmPassword))
            {
                List<GUser> Users = db.GUsers.Where(temp => temp.Email.Equals(user.Email)).ToList();

                if (Users.Count < 1)
                {
                    db.GUsers.Add(user);
                    db.SaveChanges();
                    profile.ID = user.ID;
                    profile.Name = RegName;
                    db.Profiles.Add(profile);
                    db.SaveChanges();
                    BuildEmailTemplate(user.ID);
                    List<GUser> UsersCheck = db.GUsers.Where(temp => temp.Email.Equals(user.Email)).ToList();
                    if (UsersCheck.Count > 0)
                    {

                    }
                    return RedirectToAction("LoginPage");
                }
                else
                {
                    TempData["LoginError"] = "Check password or username!";
                    return RedirectToAction("Register");
                }
            }
            else
            {
                TempData["LoginError"] = "Password Doesn't match!";
                return RedirectToAction("Register");
            }


        }
        public ActionResult Logout()
        {
            idUser=0;
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
            return View();
        }
        public ActionResult Profile()
        {
            var pro = (from g in db.GUsers
                       join p in db.Profiles on g.ID equals p.ID
                       where g.ID == idUser
                       select new ProfileShow
                       {
                           ProfilePicture = p.ProfilePicture,
                           ProfileId = p.ProfileId,
                           ID = g.ID,
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
        [HttpPost]
        public ActionResult Profile(GUser user, String ConfirmPassword, String OldPassword, String CPassword, Profile profile)
        {
            if (ConfirmPassword != null)
            {
                if (user.Password.Equals(ConfirmPassword))
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    var pro = (from g in db.GUsers
                               join p in db.Profiles on g.ID equals p.ID
                               where g.ID == idUser
                               select new ProfileShow
                               {
                                   ProfilePicture = p.ProfilePicture,
                                   ProfileId = p.ProfileId,
                                   ID = g.ID,
                                   Name = p.Name,
                                   Address = p.Address,
                                   Email = g.Email,
                                   NID = p.NID,
                               }).SingleOrDefault();
                    if (pro == null)
                    {
                        return RedirectToAction("LoginPage");
                    }
                    else
                    {
                        return View(pro);
                    }
                }
                else
                {
                    var pro = (from g in db.GUsers
                               join p in db.Profiles on g.ID equals p.ID
                               where g.ID == idUser
                               select new ProfileShow
                               {   
                                   ProfilePicture=p.ProfilePicture,
                                   ProfileId = p.ProfileId,
                                   ID = g.ID,
                                   Name = p.Name,
                                   Address = p.Address,
                                   Email = g.Email,
                                   NID = p.NID,
                               }).SingleOrDefault();
                    if (pro == null)
                    {
                        return RedirectToAction("LoginPage");
                    }
                    else
                    {
                        return View(pro);
                    }
                }
            }
            else
            {
                    db.Entry(profile).State = EntityState.Modified;
                    db.SaveChanges();
               
                    var pro = (from g in db.GUsers
                               join p in db.Profiles on g.ID equals p.ID
                               where g.ID == idUser
                               select new ProfileShow
                               {
                                   ProfilePicture = p.ProfilePicture,
                                   ProfileId = p.ProfileId,
                                   ID = g.ID,
                                   Name = p.Name,
                                   Address = p.Address,
                                   Email = g.Email,
                                   NID = p.NID,
                               }).SingleOrDefault();
                    if (pro == null)
                    {
                        return RedirectToAction("LoginPage");
                    }
                    else
                    {
                        return View(pro);
                    }
            }
        }
        public ActionResult Project()
        {
            List<Project> projects = db.Projects.OrderByDescending(x=>x.Likes).ToList();
            return View(projects);
        }

        public ActionResult Details(int BoxID)
        {
            if (Session["IdUsSS"] != null)
            {
                var Detail = (from pj in db.Projects
                              join p in db.Profiles on pj.ID equals p.ID
                              where pj.PId == BoxID
                              select new ShowDetails
                              {
                                  PId = pj.PId,
                                  ID = p.ID,
                                  Name = p.Name,
                                  Address = p.Address,
                                  NID = p.NID,
                                  Title = pj.Title,
                                  VideoLink = pj.VideoLink,
                                  Info = pj.Info,
                                  Type = pj.Type,
                                  Likes = pj.Likes,
                                  Target = pj.Target,
                                  MoneyRaised = pj.MoneyRaised,
                                  LikesP = (pj.Likes + 1),
                              }).SingleOrDefault();
                TempData["LoggedInUser"]=idUser;

                TempData["PId"] = Detail.PId;
                TempData["ID"] = Detail.ID;
                TempData["Name"] = Detail.Name;
                TempData["Address"] = Detail.Address;
                TempData["NID"] = Detail.NID;
                TempData["VideoLink"] = Detail.VideoLink;
                TempData["Title"]=Detail.Title;
                TempData["Info"] = Detail.Info;
                TempData["Type"] = Detail.Type;
                TempData["Target"] = Detail.Target;
                TempData["MoneyRaised"] = Convert.ToInt32(Detail.MoneyRaised);
                TempData["MoneyPercentage"] = (Convert.ToDouble(Detail.MoneyRaised) / Convert.ToDouble(Detail.Target))*100;
                if (Detail.Likes == null)
                {
                    TempData["Likes"] = 0;
                    TempData["Likes"] = 0;
                }
                else
                {
                TempData["Likes"] = Detail.Likes;
                }
                
                TempData["LikesP"] = Detail.LikesP;

                List<Comment> comments = db.Comments.Where(x=>x.PId== Detail.PId).ToList();
                return View(comments);
            }
            else
            {
                return RedirectToAction("LoginPage");
            }
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
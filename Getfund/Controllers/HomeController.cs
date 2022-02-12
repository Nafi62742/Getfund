using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Getfund.Models;

namespace Getfund.Controllers
{
    public class HomeController : Controller
    {
        Test1Entities1 db = new Test1Entities1();
        public ActionResult Index()
        {
            return View();
              
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Search(string email)
        {
            ViewBag.Message = "Your application description page.";
            List<GUser> Users = db.GUsers.Where(temp => temp.Email.Equals(email)).ToList();
            if (Users.Count > 0)
            {
                return View(Users);
            }
            else
            {
                ViewBag.Message = "Wrong username or password.";
                return RedirectToAction("About");
            }
            
        }

        public ActionResult Contact()
        {
            List<GUser> Users = db.GUsers.ToList();
            return View(Users);
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
            return RedirectToAction("About");
        }
    }
}
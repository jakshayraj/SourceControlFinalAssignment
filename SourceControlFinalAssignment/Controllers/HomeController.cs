using SourceControlFinalAssignment.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using log4net;
using log4net.Config;

namespace SourceControlFinalAssignment.Controllers
{
    
    public class HomeController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(HomeController));

        private SourceControlEntities1 db = new SourceControlEntities1();
        public ActionResult Login()
        {   
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var db = new SourceControlEntities1())
                    {
                        var obj = db.Users.Where(a => a.EmailId.Equals(model.EmailId) && a.Password.Equals(model.Password)).FirstOrDefault();
                        if (obj != null)
                        {
                            FormsAuthentication.SetAuthCookie(obj.Name.ToString(), false);
                            Session["UserID"] = obj.Id;
                            Session["Image"] = obj.Image;
                            Log.Info("Login Successffuly");
                            return this.RedirectToAction("Details");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid Email Id or Password");
                            return View("Login");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,EmailId,Password,Age,PhoneNo,Image")] User user, HttpPostedFileBase Image)
        {
            try
            {
                string smallfilename = Path.GetFileName(Image.FileName);
                string _filename1 = DateTime.Now.ToString("yymmssfff") + smallfilename;
                string smallfileextension = Path.GetExtension(Image.FileName);
                user.Image = "~/Images/" + _filename1;
                string smallImagepath = Path.Combine(Server.MapPath("~/Images/"), _filename1);


                if (Image.ContentLength >= 1000000)
                {
                    ModelState.AddModelError("", "Invalid size of file");
                }
                else
                {
                    db.Users.Add(user);
                    if (db.SaveChanges() > 0)
                    {
                        FormsAuthentication.SetAuthCookie(user.Name.ToString(), false);
                        Image.SaveAs(smallImagepath);
                        Log.Info("Signup Successffuly");
                        ModelState.Clear();
                        Session["UserID"] = user.Id;
                        Session["Image"] = user.Image;
                        return RedirectToAction("Details", "Home");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return View(user);
        }
        public ActionResult Details()
        {
            User user = db.Users.Find(Session["UserID"]);
            return View(user);
        }
    }
    
}
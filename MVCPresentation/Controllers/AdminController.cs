using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCPresentation.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace MVCPresentation.Controllers
{ 
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationUserManager userManager;
        // private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Admin
        public ActionResult Index()
        {
            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return View(userManager.Users.OrderBy(n => n.FamilyName).ToList());
            // return View(db.ApplicationUsers.ToList());
        }


        // GET: Admin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser applicationUser = userManager.FindById(id);

            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            var allRoles = new string[] {"Checkout",
                "Doctor", "Manager", "Admin"};

            var roles = userManager.GetRoles(id);
            var noRoles = allRoles.Except(roles);

            ViewBag.Roles = roles;
            ViewBag.NoRoles = noRoles;

            return View(applicationUser);
        }


        public ActionResult RemoveRole(string id, string role)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);

            userManager.RemoveFromRole(id, role);


            var allRoles = new string[] {"Checkout", "Doctor", "Manager", "Admin"};
            var roles = userManager.GetRoles(id);
            var noRoles = allRoles.Except(roles);

            ViewBag.Roles = roles;
            ViewBag.NoRoles = noRoles;

            return View("Details", user);
        }

        public ActionResult AddRole(string id, string role)
        {  var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
              var user = userManager.Users.First(u => u.Id == id);

              userManager.AddToRole(id, role);
            var allRoles = new string[] {"Checkout",
                "Doctor", "Manager", "Admin"};
            var roles = userManager.GetRoles(id);
            var noRoles = allRoles.Except(roles);

            ViewBag.Roles = roles;
            ViewBag.NoRoles = noRoles;
             

            return View("Details",user);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);

            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var roles = userManager.GetRoles(id);
            ViewBag.Roles = roles;

            return View(user);
        }

        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);


            try
            {
                if(user != null) { 
                userManager.Delete(user);

                }

                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }
    }
}
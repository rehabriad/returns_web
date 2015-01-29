using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ST.WebUI.DataContext;
using ST.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ST.WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: Home
        public ActionResult Index()
        {

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            if (!roleManager.RoleExists("Administrators"))
            {
                roleManager.Create(new IdentityRole() { Name = "Administrators" });
            }

            if (!roleManager.RoleExists("Users"))
            {
                roleManager.Create(new IdentityRole() { Name = "Users" });
            }

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (userManager.FindByName("admin") == null)
            {
                var user = new ApplicationUser() { UserName = "admin", FullUsername = "Administrator", rin = "123456789" };
                userManager.Create(user, "P@ssw0rd");
                userManager.AddToRole(user.Id, "Administrators");

            }

            if (userManager.FindByName("user") == null)
            {
                var user = new ApplicationUser() { UserName = "user", FullUsername = "User", rin = "987654321" };
                userManager.Create(user, "P@ssw0rd");
                userManager.AddToRole(user.Id, "Users");
            }
            return RedirectToAction("Index", "Returns");
            //return "This is HOME PAGE";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && context != null)
            {
                context.Dispose();
                context = null;
            }

            base.Dispose(disposing);
        }
    }
}
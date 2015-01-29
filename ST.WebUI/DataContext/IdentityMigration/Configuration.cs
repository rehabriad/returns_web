namespace ST.WebUI.DataContext.IdentityMigration
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ST.WebUI.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Claims;

    internal sealed class Configuration : DbMigrationsConfiguration<ST.WebUI.DataContext.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataContext\IdentityMigration";
        }

        protected override void Seed(ST.WebUI.DataContext.ApplicationDbContext context)
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
                var user = new ApplicationUser() { UserName = "admin", FullUsername="Administrator" };
                userManager.Create(user, "P@ssw0rd");
                userManager.AddToRole(user.Id, "Administrators");

            }

            if (userManager.FindByName("user") == null)
            {
                var user = new ApplicationUser() { UserName = "user", FullUsername = "User" };
                userManager.Create(user, "P@ssw0rd");
                userManager.AddToRole(user.Id, "Users");
            }
        }
    }
}

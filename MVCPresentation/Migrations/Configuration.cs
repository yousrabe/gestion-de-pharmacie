namespace MVCPresentation.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MVCPresentation.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCPresentation.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MVCPresentation.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            const string admin = "admin@pharmarx.com";
            const string adminPassword = "P@ssw0rd";

            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole() { Name = "Checkout" });
            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole() { Name = "Doctor" });
            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole() { Name = "Manager" });
            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole() { Name = "Admin" });




            if (!context.Users.Any(u => u.UserName == admin))
            {
                var user = new ApplicationUser()
                {
                    UserName = admin,
                    Email = admin,
                    GivenName = "Admin",
                    FamilyName = "Company"
                };

                IdentityResult result = userManager.Create(user, adminPassword);
                context.SaveChanges(); // updates the database

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                    context.SaveChanges();
                }

            }
        }
    }
}

namespace s00164548Week6Lab2.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<s00164548Week6Lab2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(s00164548Week6Lab2.Models.ApplicationDbContext context)
        {
            var manager =
                new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(context));

            var roleManager =
                new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(context));

            if (context.Roles.FirstOrDefault(r => r.Name == "Admin") == null)
            {
                context.Roles.Add(new IdentityRole { Name = "Admin" });
            }

            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "ClubAdmin" });

            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "Member" });

            context.SaveChanges();
            // Seed Users
            PasswordHasher p = new PasswordHasher();

            context.Users.AddOrUpdate(u => u.StudentID,
                new ApplicationUser
                {
                    StudentID = "S00000001",
                    Email = "S00000001@mail.itsligo.ie",
                    PasswordHash = p.HashPassword("Sxxxxxx1$1"),
                    SignupDate = DateTime.Now,
                    UserName = "Bill Bloggs",
                    SecurityStamp = Guid.NewGuid().ToString(),
                });
            context.SaveChanges();

            ApplicationUser usr = new ApplicationUser
            {
                StudentID = "S00000002",
                Email = "S00000002@mail.itsligo.ie",
                SignupDate = DateTime.Now,
                UserName = "Jennfer Ecceles",
                PasswordHash = p.HashPassword("Sxxxxxx2$1"),
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            context.Users.AddOrUpdate(u => u.StudentID, usr);
            //manager.Create(usr, "Sxxxxxx2$1");

            var result = manager.Create(new ApplicationUser
            {
                StudentID = "ppowell",
                Email = "powell.paul@itsligo.ie",
                SignupDate = DateTime.Now,
                UserName = "PaulPowell",
                SecurityStamp = Guid.NewGuid().ToString(),
            }, "Ppowell$1");

            if (!result.Succeeded)
            {
                throw new Exception("Manager Failed to create");
            }

            context.SaveChanges();
            // Setup Paul Powell as system Admin Role
            IdentityRole adminRole = roleManager.FindByName("Admin");
            if (adminRole != null)
            {
                ApplicationUser adminUser = manager.FindByName("PaulPowell");
                if (adminUser != null && adminUser.Roles.Count() < 1)
                {
                    adminUser.Roles.Add(new IdentityUserRole { RoleId = adminRole.Id, UserId = adminUser.Id });
                }
            }
            // Setup Jennifer as system Club Admin Role
            IdentityRole clubAdminRole = roleManager.FindByName("ClubAdmin");
            if (clubAdminRole != null)
            {
                ApplicationUser adminUser = manager.FindByName("Jennfer Ecceles");
                if (adminUser != null && adminUser.Roles.Count() < 1)
                {
                    adminUser.Roles.Add(new IdentityUserRole { RoleId = clubAdminRole.Id, UserId = adminUser.Id });
                }
            }
            // Setup Bill as system member Role
            IdentityRole memberRole = roleManager.FindByName("Member");
            if (memberRole != null)
            {
                ApplicationUser adminUser = manager.FindByName("Bill Bloggs");
                if (adminUser != null && adminUser.Roles.Count() < 1)
                {
                    adminUser.Roles.Add(new IdentityUserRole { RoleId = memberRole.Id, UserId = adminUser.Id });
                }
            }

            context.SaveChanges();

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}

using Coolntern.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Configuration;
using Coolntern.Models;

[assembly: OwinStartup("CKSource", typeof(Coolntern.StartupConfig))]

namespace Coolntern
{
    public class StartupConfig
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/dang-nhap"),
                LogoutPath= new PathString("/")
            });

            this.CreateRoleAndUsers();
        }

        private void CreateRoleAndUsers()
        {
            var appDbContext = new AppDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new AppDbContext()));

            var appUserStore = new AppUserStore(appDbContext);
            var userManager = new AppUserManager(appUserStore);

            //Create role "Amin"
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();

                role.Name = "Admin";

                roleManager.Create(role);
            }

            //Create account for admin
            if (userManager.FindByName("admin") == null)
            {
                var user = new AppUser()
                {
                    UserName = "admin",
                    Email= "admin@gmail.com",
                };

                string userPassword = "admin123";

                var checkedUser = userManager.Create(user, userPassword);

                if (checkedUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

            //Create role "User"
            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";

                roleManager.Create(role);
            }
        }
    }
}

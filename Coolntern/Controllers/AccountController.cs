using Coolntern.Filters;
using Coolntern.Identity;
using Coolntern.Models;
using Coolntern.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using GoogleAuthentication.Services;
using System.Web.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Facebook;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Web.UI.WebControls;
using Microsoft.Win32;
using System.Net;
using Coolntern.Help;
using Coolntern.Help;
using System.IO;
using System.Data.Entity;

namespace Coolntern.Controllers
{
    public class AccountController : Controller
    {
        EntityModel _db = new EntityModel();

        // GET: Register
        [Authenticated]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Authenticated]
        public ActionResult Register(Register register)
        {
            if (ModelState.IsValid)
            {
                var dbContext = new AppDbContext();
                var userStore = new AppUserStore(dbContext);
                var userManager = new AppUserManager(userStore);

                var tempUser = _db.AspNetUsers.Where(_ => _.UserName == register.Username).FirstOrDefault();

                if (tempUser != null)
                {
                    ModelState.AddModelError("message", "Please use another username!!!");
                    return View();
                }

                var passwordHashed = Crypto.HashPassword(register.Password);

                var user = new AppUser()
                {
                    Name = register.Name,
                    Email = register.Email,
                    UserName = register.Username,
                    PasswordHash = passwordHashed,
                    Address = register.Address,
                    PhoneNumber = register.Phone,
                    Avatar = "avatar_default.png"
                };

                this.RegisterUser(userManager, user);

                return Redirect("/");
            }
            else
            {
                return View();
            }
        }

        [Authenticated]
        public ActionResult Login()
        {
            ViewData["url"] = HttpContext.Request.QueryString["ReturnUrl"];

            //Google Login
            var clientId = WebConfigurationManager.AppSettings["ClientId"];
            var url = "https://localhost:44384/Account/GoogleCallback";
            var resopnse = GoogleAuth.GetAuthUrl(clientId, url);
            ViewBag.GoogleSignIn = resopnse;

            return View();
        }

        public async Task<ActionResult> GoogleCallback(string code)
        {
            var dbContext = new AppDbContext();
            var userStore = new AppUserStore(dbContext);
            var userManager = new AppUserManager(userStore);

            try
            {
                var ClientId = WebConfigurationManager.AppSettings["ClientId"];
                var ClientSecret = WebConfigurationManager.AppSettings["ClientSecret"];
                var url = "https://localhost:44384/Account/GoogleCallback";

                var token = await GoogleAuth.GetAuthAccessToken(code, ClientId, ClientSecret, url);
                var UserProfile = await GoogleAuth.GetProfileResponseAsync(token.AccessToken.ToString());
                var googleUser = JsonConvert.DeserializeObject<GoogleProfile>(UserProfile);

                if (userManager.FindByEmail(googleUser.Email) == null)
                {
                    var mUser = new AppUser()
                    {
                        Name = googleUser.Name,
                        UserName = "google."+ Functions.GenerateRandomNumber() + "." + googleUser.Email,
                        Email = googleUser.Email,
                        PhoneNumber = googleUser.MobilePhone,
                        PasswordHash = "",
                        Avatar = "avatar_default.png",
                    };

                    this.RegisterUser(userManager, mUser);
                }
                else
                {
                    var user = userManager.FindByEmail(googleUser.Email);
                    this.LoginUser(userManager, user);
                }
            }
            catch (Exception ex)
            {

            }

            return Redirect("/");
        }


        [HttpPost]
        [Authenticated]
        public ActionResult Login(ViewModel.Login login)
        {
            if (ModelState.IsValid)
            {
                var dbContext = new AppDbContext();
                var userStore = new AppUserStore(dbContext);
                var userManager = new AppUserManager(userStore);

                var user = userManager.Find(login.Username, login.Password);

                if (user != null)
                {
                    this.LoginUser(userManager, user);

                    if (userManager.IsInRole(user.Id, "Admin"))
                    {
                        return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                    }
                    else
                    {
                        var urlRedirect = HttpContext.Request.QueryString["ReturnUrl"];

                        if (urlRedirect != null)
                        {
                            return Redirect(urlRedirect);
                        }

                        return Redirect("/");
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid username or password");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            var authenManager = HttpContext.GetOwinContext().Authentication;
            authenManager.SignOut();

            //Destroy session
            Session.RemoveAll();

            return Redirect("/");
        }

        [NonAction]
        private void LoginUser(AppUserManager userManager, AppUser user)
        {
            var authenManager = HttpContext.GetOwinContext().Authentication;
            var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            authenManager.SignIn(new AuthenticationProperties(), userIdentity);

            Session["Name"] = user.Name;
            Session["Avatar"] = user.Avatar;
        }

        [NonAction]
        private bool RegisterUser(AppUserManager userManager, AppUser user)
        {
            IdentityResult identityResult = userManager.Create(user);

            if (identityResult.Succeeded)
            {
                userManager.AddToRole(user.Id, "User");

                this.LoginUser(userManager, user);

                return true;
            }

            return false;
        }

        private bool FindUserByUsername(string UserName)
        {
            var currUser = _db.AspNetUsers
                .Where(_ => _.UserName == UserName)
                .FirstOrDefault();

            return currUser != null;
        }

        //Part for login with facebook
        private Uri RediredtUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);

                uriBuilder.Query = null;

                uriBuilder.Fragment = null;

                uriBuilder.Path = Url.Action("FacebookCallback");

                return uriBuilder.Uri;
            }
        }

        [AllowAnonymous]
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();

            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = WebConfigurationManager.AppSettings["FacebookClientId"],

                client_secret = WebConfigurationManager.AppSettings["FacebookClientSecret"],

                redirect_uri = RediredtUri.AbsoluteUri,

                response_type = "code",

                scope = "email"
            });

            return Redirect(loginUrl.AbsoluteUri);
        }
        public ActionResult FacebookCallback(string code)
        {
            var dbContext = new AppDbContext();
            var userStore = new AppUserStore(dbContext);
            var userManager = new AppUserManager(userStore);

            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = WebConfigurationManager.AppSettings["FacebookClientId"],

                client_secret = WebConfigurationManager.AppSettings["FacebookClientSecret"],

                redirect_uri = RediredtUri.AbsoluteUri,

                code = code,
            });

            var accessToken = result.access_token;

            fb.AccessToken = accessToken;

            dynamic me = fb.Get("me?fields=link,first_name,last_name,email,picture");

            var facebookProfile = new FacebookProfile()
            {
                Name = me.first_name + " " + me.last_name,
                Email = me.email,
            };

            if (userManager.FindByEmail(facebookProfile.Email) == null)
            {
                var mUser = new AppUser()
                {
                    Name = facebookProfile.Name,
                    UserName = "facebook." + Functions.GenerateRandomNumber() + "." + facebookProfile.Email,
                    Email = facebookProfile.Email,
                    PasswordHash = "",
                    Avatar = "avatar_default.png",
                };

                this.RegisterUser(userManager, mUser);
            }
            else
            {
                var user = userManager.FindByEmail(facebookProfile.Email);
                this.LoginUser(userManager, user);
            }

            return Redirect("/");
        }

        [MyAuthentication]
        public ActionResult EditProfile()
        {
            var userId = User.Identity.GetUserId();
            var user = _db.AspNetUsers
                .Where(s => s.Id == userId)
                .FirstOrDefault();

            var profile = new EditProfile() 
            { 
                Name = user.Name,
                Address= user.Address,
                GPA = user.GPA,
                TrainingPoint = user.TrainingPoint,
                Major = user.Major,
                Avatar = user.Avatar,
                PhoneNumber = user.PhoneNumber,
            };

            return View(profile);
        }

        [MyAuthentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(EditProfile editProfile, HttpPostedFileBase avatar)
        {
            var userId = User.Identity.GetUserId();
            var user = _db.AspNetUsers
                        .Where(s => s.Id == userId)
                        .FirstOrDefault();

            try
            {
                if (ModelState.IsValid)
                {
                    

                    user.Name = editProfile.Name;
                    user.Address = editProfile.Address;
                    user.PhoneNumber = editProfile.PhoneNumber;
                    user.GPA = editProfile.GPA;
                    user.TrainingPoint = editProfile.TrainingPoint;

                    if (avatar != null)
                    {
                        var filename = avatar.FileName;
                        var path = Path.Combine(Server.MapPath("~/Content/upload/img/avatar"), filename);
                        avatar.SaveAs(path);
                        user.Avatar = filename;
                    }

                    _db.Entry(user).State = EntityState.Modified;
                    _db.SaveChanges();

                    Session["Name"] = user.Name;
                    Session["Avatar"] = user.Avatar;

                    return Redirect("/");
                }
            }
            catch(Exception ex)
            {

            }

            var profile = new EditProfile()
            {
                Name = user.Name,
                Address = user.Address,
                GPA = user.GPA,
                TrainingPoint = user.TrainingPoint,
                Major = user.Major,
                Avatar = user.Avatar,
                PhoneNumber = user.PhoneNumber,
            };

            return View(profile);
        }

        [HttpGet]
        [MyAuthentication]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost, Authorize]
        [ValidateAntiForgeryToken]
        [MyAuthentication]
        public ActionResult ChangePassword(ViewModel.ChangePassword model)
        {
            var userId = User.Identity.GetUserId();
            
            if (ModelState.IsValid)
            {
                var currPasswordHash = Crypto.HashPassword(model.Password);

                var user = _db.AspNetUsers
                    .Where(u => u.Id == userId)
                    .FirstOrDefault();

                if (!Crypto.VerifyHashedPassword(user.PasswordHash, model.Password))
                {
                    ModelState.AddModelError("message", "Old password incorrect!!!");
                    return View();
                }

                var NewPasswordHash = Crypto.HashPassword(model.NewPassword);
                user.PasswordHash = NewPasswordHash;
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
                    
                return Redirect("/");
            }
           
            return View();
        }
    }

   
}
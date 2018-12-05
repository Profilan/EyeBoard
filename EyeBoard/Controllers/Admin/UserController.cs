using EyeBoard.Logic.Helpers;
using EyeBoard.Logic.Models;
using EyeBoard.Logic.Repositories;
using EyeBoard.Models;
using EyeBoard.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EyeBoard.Controllers.Admin
{
    [Authorize(Roles = "superuser")]
    public class UserController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public UserController()
        {

        }

        public UserController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;

        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ActionResult Index()
        {
            // var items = _userRepository.List();
            var items = UserManager.Users.ToList();

            var users = new List<UserViewModel>();
            foreach (var item in items)
            {
                users.Add(new UserViewModel()
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    DisplayName = item.DisplayName,
                        
                    // Role = item.Role,

                });
            }

            return View(users);
       }

        // GET: Url/Create
        public ActionResult Create()
        {
            var userViewModel = new UserViewModel()
            {
                Roles = RoleManager.Roles.ToList()
            };

             if (User.IsInRole("superuser"))
            {
                return View(userViewModel);
            }
            else
            {
                throw new Exception("You are not allowed to access this");
            }
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var user = new User();
                user.SetCredentials(collection["UserName"], collection["Password"]);
                user.DisplayName = collection["DisplayName"];

                // var role = (Role)Convert.ToInt32(collection["UserRole"]);
                // user.Role = role.ToString();

                UserManager.Create(user);
                UserManager.AddToRole(user.Id, collection["UserRole"]);

                // _userRepository.Insert(user);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
         }

        public ActionResult Edit(int id)
        {
            var user = UserManager.FindById(id);

            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                Roles = user.Roles
            };

            return View(userViewModel);
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var roles = collection["Roles[]"].Split(',');

                var user = UserManager.FindById(id);
                user.DisplayName = collection["DisplayName"];
                UserManager.Update(user);

                foreach (string roleName in roles)
                {
                    if (!String.IsNullOrEmpty(roleName))
                    {
                        UserManager.AddToRole(user.Id, roleName);
                    }
                }
                    

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult ChangePassword(int id)
        {
            var user = UserManager.FindById(id);

            var model = new PasswordViewModel()
            {
                Id = user.Id
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult ChangePassword(int id, FormCollection collection)
        {
            try
            {
                var user = UserManager.FindById(id);
                user.SetPassword(collection["Password1"]);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
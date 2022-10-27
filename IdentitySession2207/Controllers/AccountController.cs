using IdentitySession2207.Models;
using IdentitySession2207.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySession2207.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService _accountService)
        {
            accountService = _accountService;
        }
        public IActionResult Index()
        {
            return View("CreateAccount");
        }

        public IActionResult SignIn()
        {
            return View("SignIn");
        }

        [Authorize(Roles ="Admin")]
        public IActionResult NewRole()
        {
            return View("NewRole");
        }
        public async Task<IActionResult> CheckPassword(SignInModel singInModel)
        {
           var result= await accountService.SignIn(singInModel);
            if (result.Succeeded == true)
            {
                //return View("SignIn");
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewData["Message"] = "Invalid Username or Password";
                return View("SignIn");
            }
        }

        public async Task<IActionResult> CreateAccount(SignUpModel signUp)
        {
            // code 

            var result= await accountService.CreateAccount(signUp);

            ViewData["Message"] = result;
            return View("CreateAccount");
        }

        public async Task<IActionResult> AddRole(RoleModel roleModel)
        {
            var result= await accountService.AddRole(roleModel);
            return View("NewRole");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult GetUsers()
        {
            List<ApplicationUser> li= accountService.getUsers();
            return View("UserList", li);
        }

        public async Task<IActionResult> UserRoles(string UserId,string name)
        {
            ViewData["Name"] = name;
            List<UserRoles> liUserRoles = await accountService.getRoles(UserId);
            return View(liUserRoles);
        }

        public async Task<IActionResult> UpdateUserRole(List<UserRoles> liuserRole) 
        {
           await accountService.UpdateUserRole(liuserRole);
            List<UserRoles> liUserRoles = await accountService.getRoles(liuserRole[0].UserId);
            return View("UserRoles", liUserRoles);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Logout()
        {
            accountService.Logout();
            //return View("SignIn");
            return RedirectToAction("SignIn", "Account");
        }

    }
}

using IdentitySession2207.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySession2207.services
{
    public class AccountService:IAccountService
    {
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;
        RoleManager<IdentityRole> roleManager;

        public AccountService(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager, RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }

        public async Task<IdentityResult> CreateAccount(SignUpModel signUp)
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = signUp.Username;
            user.Email = signUp.Email;
            user.Name = signUp.Name;
            user.BDate = signUp.BDate;
            //user.PasswordHash = signUp.Password;

            var result = await userManager.CreateAsync(user, signUp.Password);
            return result;
            // code => insert AspNetUsers
        }

        public async Task<SignInResult> SignIn(SignInModel signInModel)
        {
            var result = await signInManager.PasswordSignInAsync(signInModel.Username, signInModel.Password, signInModel.RememberMe, false);
            return result;
        }

        public async Task<IdentityResult> AddRole(RoleModel roleModel)
        {
            IdentityRole role = new IdentityRole();
            role.Name = roleModel.Name;
            var result = await roleManager.CreateAsync(role);
            return result;

        }

        public List<ApplicationUser> getUsers()
        {
            List<ApplicationUser> li= userManager.Users.ToList();
            return li;
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<List<UserRoles>> getRoles(string UserId)
        {
            List<IdentityRole> liRole = roleManager.Roles.ToList();
            List<UserRoles> liUserRole = new List<UserRoles>();
            foreach (IdentityRole item in liRole)
            {
                UserRoles uRole = new UserRoles();
                uRole.RoleId = item.Id;
                uRole.RoleName = item.Name;
                uRole.UserId = UserId;
                uRole.IsSelected = false;
                liUserRole.Add(uRole);
            }

            foreach (UserRoles uR in liUserRole)
            {
                var user= await userManager.FindByIdAsync(uR.UserId);
                var Roles = await userManager.GetRolesAsync(user);
                foreach (string r in Roles)
                {
                    if (r == uR.RoleName)
                    {
                        uR.IsSelected = true;
                    }
                }

            }


            return liUserRole;


        }


        public async Task UpdateUserRole(List<UserRoles> liUserRole)
        {
            foreach (UserRoles item in liUserRole)
            {
                var user = await userManager.FindByIdAsync(item.UserId);
                if (item.IsSelected == true)
                {
                    if (await userManager.IsInRoleAsync(user, item.RoleName) == false)
                    {
                        await userManager.AddToRoleAsync(user, item.RoleName);
                    }
                }
                else
                {
                    if (await userManager.IsInRoleAsync(user, item.RoleName) == true)
                    {
                        await userManager.RemoveFromRoleAsync(user, item.RoleName);
                    }
                }

            }
        }

    }
}

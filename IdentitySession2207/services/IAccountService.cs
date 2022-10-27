using IdentitySession2207.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySession2207.services
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateAccount(SignUpModel signUp);

        Task<SignInResult> SignIn(SignInModel signInModel);

        Task<IdentityResult> AddRole(RoleModel roleModel);

        List<ApplicationUser> getUsers();

        Task<List<UserRoles>> getRoles(string UserId);

        Task UpdateUserRole(List<UserRoles> liUserRole);

        Task Logout();
    }
}

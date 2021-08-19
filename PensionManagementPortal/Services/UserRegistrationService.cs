using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PensionManagementPortal.Interfaces;
using PensionManagementPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PensionManagementPortal.Services
{
    public class UserRegistrationService: IUserRegistration
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IUserLogin userLogin;

        public UserRegistrationService()
        {

        }
        public UserRegistrationService(UserManager<IdentityUser> userManager,
                                        IUserLogin userLogin)
        {
            this.userManager = userManager;
            this.userLogin = userLogin;
        }
        public async Task<IdentityResult> ChangePassword(ClaimsPrincipal user, ChangePassword changePassword)
        {
            var loggedinUser = await userManager.GetUserAsync(user);
            IdentityResult result = null;
            if (loggedinUser == null)
            {
                return result;
            }
            result = await userManager.ChangePasswordAsync(loggedinUser,
            changePassword.CurrentPassword, changePassword.NewPassword);
            if(result.Succeeded)
            {
               await userLogin.RefreshSigninAfterPasswordChange(loggedinUser);
            }
            return result;
            
        }
        public async Task<IdentityResult> RegisterUser(RegistrationViewModel registerationDetails)
        {
            var user = new IdentityUser { UserName = registerationDetails.Email, Email = registerationDetails.Email };
            var result = await userManager.CreateAsync(user, registerationDetails.Password);
            //await userManager.AddToRoleAsync(user, "User");

            return result;
        }
    }
}

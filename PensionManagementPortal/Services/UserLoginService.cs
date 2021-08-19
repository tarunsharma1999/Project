using Microsoft.AspNetCore.Identity;
using PensionManagementPortal.Interfaces;
using PensionManagementPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PensionManagementPortal.Services
{
    public class UserLoginService:IUserLogin
    {
        private readonly SignInManager<IdentityUser> signInManager;

        public UserLoginService(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public void SignIn(RegistrationViewModel registerationDetails)
        {
            var user = new IdentityUser { UserName = registerationDetails.Email, Email = registerationDetails.Email };
            signInManager.SignInAsync(user, isPersistent: false);
        }

        public async Task<SignInResult> SignIn(LoginModel loginDetails)
        {
            var result = await signInManager.PasswordSignInAsync(loginDetails.Email, 
                                                                loginDetails.Password, 
                                                                loginDetails.RememberMe, false);

            return result;

        }
        
        public  async Task RefreshSigninAfterPasswordChange(IdentityUser user)
        {
            await signInManager.RefreshSignInAsync(user);
        }

        public async Task logOut()
        {
            await signInManager.SignOutAsync();
        }
    }
}

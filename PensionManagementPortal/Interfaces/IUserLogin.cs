using Microsoft.AspNetCore.Identity;
using PensionManagementPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagementPortal.Interfaces
{
    public interface IUserLogin
    {
        void SignIn(RegistrationViewModel registerationDetails);
        Task<SignInResult> SignIn(LoginModel loginDetails);
         Task RefreshSigninAfterPasswordChange(IdentityUser user);
        Task logOut();
    }
}

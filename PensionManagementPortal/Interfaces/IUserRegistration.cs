using Microsoft.AspNetCore.Identity;
using PensionManagementPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PensionManagementPortal.Interfaces
{
    public interface IUserRegistration
    {
        Task<IdentityResult> RegisterUser(RegistrationViewModel registerationDetails);
        Task<IdentityResult> ChangePassword(ClaimsPrincipal user, ChangePassword changePassword);
    }
}

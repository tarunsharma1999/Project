using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PensionManagementPortal.DatabaseRepo;
using PensionManagementPortal.Interfaces;
using PensionManagementPortal.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PensionManagementPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRegistration userRegistration;
        private readonly IUserLogin userLogin;
        private readonly IClientHelper client;
        private readonly IDb db;
        static PensionData pensionData = new PensionData();
        static PensionInput userData = new PensionInput();

        public HomeController(IUserRegistration userRegistration,
                                IUserLogin userLogin,
                                IClientHelper client,
                                IDb db)
        {
            this.userRegistration = userRegistration;
            this.userLogin = userLogin;
            this.client = client;
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Route("Account/Register")]
        public ActionResult Register()
        {
            return View();
        }
        [Route("Account/Register")]

        [HttpPost]
        public async Task<ActionResult> Register(RegistrationViewModel registerationDetails)
        {
            if(ModelState.IsValid)
            {
                var result = await userRegistration.RegisterUser(registerationDetails);
                
                if(result.Succeeded)
                {
                    userLogin.SignIn(registerationDetails);
                    return RedirectToAction("index");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(registerationDetails);
        }
        [Route("Account/Login")]

        public ActionResult Login()
        {
            return View();
        }
        [Route("Account/Login")]

        [HttpPost]
        public async  Task<ActionResult> Login(LoginModel loginDetails,string returnUrl)
        {
            if(ModelState.IsValid)
            {
                var result  = await userLogin.SignIn(loginDetails);

                if(result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(returnUrl))
                    {
                        //Using Redirect here to eliminate %2 characters from url
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("index");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt !");
            }
            return View(loginDetails);
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePassword changePassword)
        {
            if(ModelState.IsValid)
            {
               var result = await userRegistration.ChangePassword(User, changePassword);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }
                return RedirectToAction("index");
            }
            return View(changePassword);
        }

        public async Task<ActionResult> LogOut()
        {
            await userLogin.logOut();
            return RedirectToAction("index", "home");

        }

        [Authorize]
        [HttpGet]
        [Route("SubmitPensionDetails")]
        public ActionResult SubmitPensionDetails()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [Route("SubmitPensionDetails")]

        public ActionResult SubmitPensionDetails(PensionInput pensionDetails)
        {
            if(ModelState.IsValid)
            {
                var result = client.SubmitPensionInput(pensionDetails);
                pensionData = result.Result;
                userData = pensionDetails;
                return RedirectToAction("ShowPensionDetails");

            }
            return View(pensionDetails);
        }
        [Authorize]
        [Route("ShowPensionDetails")]

        public ActionResult ShowPensionDetails()
        {
            return View(pensionData);
        }
        [Authorize]
        [Route("ShowPensionDetails")]
        [HttpPost]
        public ActionResult ShowPensionDetails(PensionData Data)
        {
            var i = 4;
            if(ModelState.IsValid)
            {
                if(!db.CheckPensionWithdrawn(userData.AadharNumber))
                {
                    userData = db.AddUserDetails(userData);
                    //pensionData.Id = userData.Id;
                    pensionData.DateOFWithdraw = DateTime.Now;
                    db.AddPensionDetails(pensionData);
                    ViewBag.withdraw = true;
                    return View(pensionData);
                }
                else
                {
                    ViewBag.DateOfWithdrawn = db.DateWithdrawn(userData.AadharNumber);
                    return View(pensionData);
                }

                
            }
            return View(pensionData);
        }
    }
}

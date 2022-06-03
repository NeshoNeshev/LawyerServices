﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using LawyerServices.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LaweyrServices.Web.Server.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }


        [BindProperty]
        public InputModel Input { get; set; }


        public IList<AuthenticationScheme> ExternalLogins { get; set; }


        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }


        public class InputModel
        {

            [Required(ErrorMessage = "Имейлът е задължителен")]
            [EmailAddress(ErrorMessage ="Въведете валиден имейл адрес")]
            [Display(Name = "Имейл")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Паролата е вадължителна")]
            [DataType(DataType.Password)]
            [Display(Name = "Парола")]
            public string Password { get; set; }


            [Display(Name = "Запомни ме")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {


            ;            //returnUrl ??= Url.Content("~/Identity/Account/Manage");
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = await  this._userManager.FindByNameAsync(Input.Email);
                if (user != null)
                {
                    if (user.IsBan == true)
                    {
                        this.ModelState.AddModelError("", "Временно достъпът ви е ограничен от администратор");

                        return Page();
                    }
                }
                
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                   
                    if (await this._userManager.IsInRoleAsync(user, "Administrator"))
                    {
                        return Redirect("~/administration");
                    }
                    else if (await this._userManager.IsInRoleAsync(user, "Notary"))
                    {
                        return Redirect("~/notary-profile");
                    }
                    else if (await this._userManager.IsInRoleAsync(user, "Lawyer"))
                    {
                        return Redirect("~/lawyer-profile");
                    }
                    else if (await this._userManager.IsInRoleAsync(user, "User"))
                    {
                        return Redirect("~/profile");
                    }
                    _logger.LogInformation("User logged in.");

                    return Redirect(returnUrl);
                }
               
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Неуспешен опит за вход.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}

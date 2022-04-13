using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CTA.BlazorWasm.Shared;
using CTA.BlazorWasm.Shared.Models;
using CTA.BlazorWasm.Server.Services;
using CTA.BlazorWasm.Shared.Models.SMTPModels;
using CTA.BlazorWasm.Shared.Models.UserAccount;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;

namespace CTA.BlazorWasm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ISmtpEmailSender _smtpEmailSender;

        public AccountsController(UserManager<IdentityUser> userManager, ISmtpEmailSender smtpEmailSender)
        {
            _userManager = userManager;
            _smtpEmailSender = smtpEmailSender;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterModel model)
        {
            var newUser = new IdentityUser { UserName = model.Email, Email = model.Email, EmailConfirmed = false };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return Ok(new RegisterResult { Successful = false, Errors = errors });

            }
            await _userManager.AddToRoleAsync(newUser, "Guest");



            return Ok(new RegisterResult { Successful = true });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] PasswordResetRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user is null) //add the confirmation as well
            {
                return Ok(new PasswordResetConfirmation { Successful = false });
            }

            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            resetToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(resetToken));

            var callbackUrl = Url.Page(
                "/Account/ResetPassword",
                pageHandler: null,
                values: new { area = "Identity", code = resetToken},
                protocol: Request.Scheme);

            var message = new Message(
                new string[] { model.Email }, 
                "Password Reset", 
                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.", 
                    new FormFileCollection());
            
            await _smtpEmailSender.SendEmailAsync(message);
            
            return Ok(new PasswordResetConfirmation { Successful = true });
        }
    }
}

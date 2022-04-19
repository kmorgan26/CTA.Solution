using CTA.BlazorWasm.Shared.Models;
using CTA.BlazorWasm.Shared.Models.UserAccount;

namespace CTA.BlazorWasm.Client.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
        Task<RegisterResult> Register(RegisterModel registerModel);
        Task<PasswordResetConfirmation> ResetPassword(PasswordResetRequest resetRequest);
        Task<PasswordChangeConfirmation> ChangePassword(PasswordChangeRequest changeRequest);
        Task<EmailConfirmationResponse> ConfirmEmail(ConfirmEmailRequest confirmEmailRequest);
    }
}
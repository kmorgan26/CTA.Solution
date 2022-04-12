using CTA.BlazorWasm.Shared.Models.SMTPModels;

namespace CTA.BlazorWasm.Server.Services
{
    public interface ISmtpEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
}

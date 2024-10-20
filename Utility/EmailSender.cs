using Microsoft.AspNetCore.Identity.UI.Services;

namespace webProje.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //email gönderme işlemleri burda yapılabilir. f
            return Task.CompletedTask;
        }
    }
}

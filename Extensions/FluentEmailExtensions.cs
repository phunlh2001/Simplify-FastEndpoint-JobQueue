using System.Net;
using System.Net.Mail;

namespace TaskProcessingSystem.Extensions
{
    public static class FluentEmailExtensions
    {
        public static void AddFluentEmail(this IServiceCollection services, ConfigurationManager config)
        {
            var emailSettings = config.GetSection("EmailSettings");

            var defaultFromEmail = emailSettings["DefaultFromEmail"];
            var host = emailSettings["SMTPSetting:Host"];
            var port = emailSettings.GetValue<int>("SMTPSetting:Port");
            var userName = emailSettings["UserName"];
            var password = emailSettings["Password"];

            var client = new SmtpClient
            {
                EnableSsl = true,
                Host = host ?? "smtp.gmail.com",
                Port = port,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(userName, password)
            };
            services.AddFluentEmail(defaultFromEmail)
                .AddSmtpSender(client);
        }
    }
}

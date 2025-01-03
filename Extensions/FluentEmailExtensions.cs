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

            var client = new SmtpClient
            {
                Host = host ?? "localhost",
                Port = port
            };
            services.AddFluentEmail(defaultFromEmail)
                .AddSmtpSender(client);
        }
    }
}

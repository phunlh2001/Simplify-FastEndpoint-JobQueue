
using FluentEmail.Core;

namespace TaskProcessingSystem.Services.Email
{
    public class EmailService(IFluentEmail email) : IEmailService
    {
        private readonly IFluentEmail _fluentEmail = email;

        public async Task Send(EmailMetadata emailMetadata)
        {
            await _fluentEmail.To(emailMetadata.ToAddress)
                .Subject(emailMetadata.Subject)
                .Body(emailMetadata.Body)
                .SendAsync();
        }
    }
}

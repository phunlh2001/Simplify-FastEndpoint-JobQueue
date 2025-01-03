namespace TaskProcessingSystem.Services.Email
{
    public interface IEmailService
    {
        Task Send(EmailMetadata emailMetadata);
    }
}

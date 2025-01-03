namespace TaskProcessingSystem.Services.Email
{
    public class EmailMetadataBuilder
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public EmailMetadataBuilder AddToEmail(string toEmail)
        {
            ToEmail = toEmail;
            return this;
        }

        public EmailMetadataBuilder AddSubject(string subject)
        {
            Subject = subject;
            return this;
        }

        public EmailMetadataBuilder AddBody(string body)
        {
            Body = body;
            return this;
        }

        public EmailMetadata Build()
        {
            return new EmailMetadata(ToEmail, Subject, Body);
        }
    }
}

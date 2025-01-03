using TaskProcessingSystem.Queues;
using TaskProcessingSystem.Services.Email;

namespace TaskProcessingSystem.Workers
{
    public class TaskWorker(IJobQueue queue, IEmailService emailService) : BackgroundService
    {
        private readonly IJobQueue _queue = queue;
        private readonly IEmailService _emailService = emailService;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var taskId = await _queue.DequeueAsync(stoppingToken);
                await ProcessTaskAsync(taskId);
            }
        }

        private async Task ProcessTaskAsync(string taskId)
        {
            Console.WriteLine($"Processing task '{taskId}'");

            var emailMetadata = new EmailMetadataBuilder()
                .AddSubject("Notification for new task")
                .AddBody("This is email to inform you that we have a new task, please check that one!")
                .AddToEmail("phunlh2001@gmail.com")
                .Build();

            await _emailService.Send(emailMetadata);

            Console.WriteLine($"Task '{taskId}' completed");
        }
    }
}

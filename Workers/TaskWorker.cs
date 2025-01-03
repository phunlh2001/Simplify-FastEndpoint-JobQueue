using TaskProcessingSystem.Queues;
using TaskProcessingSystem.Services.Email;
using TaskProcessingSystem.Services.File;

namespace TaskProcessingSystem.Workers
{
    public class TaskWorker(IJobQueue queue, IEmailService emailService, IFileHandler fileHandler) : BackgroundService
    {
        private readonly IJobQueue _queue = queue;
        private readonly IEmailService _emailService = emailService;
        private readonly IFileHandler _fileHandler = fileHandler;
        private const string FILE_PATH = "task.txt";

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
            Console.WriteLine($"Background service start processing on task {taskId}");

            var tasks = _fileHandler.ReadList<Models.Task>(FILE_PATH);

            var task = tasks.Find(x => x.Id == taskId) ?? throw new Exception($"could not found task with id: {taskId}");

            var emailMetadata = new EmailMetadata
            {
                Subject = "Notification for new task",
                Body = $"<h4>You have assigned the new task. Here is task detail:</h4><br/>" +
                $"<em>Task ID: {task.Id}</em><br/>" +
                $"<em>Task name: {task.Name}</em><br/>" +
                $"<em>Task descriptioon: {task.Description}</em><br/>",
                ToAddress = "annie2005@gmail.com"
            };

            await _emailService.Send(emailMetadata);

            Console.WriteLine($"Task {taskId} completed!");
        }
    }
}

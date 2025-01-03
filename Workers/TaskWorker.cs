using TaskProcessingSystem.Queues;

namespace TaskProcessingSystem.Workers
{
    public class TaskWorker(IJobQueue queue) : BackgroundService
    {
        private readonly IJobQueue _queue = queue;

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
            await Task.Delay(5000);
            Console.WriteLine($"Task '{taskId}' completed");
        }
    }
}

namespace TaskProcessingSystem.Queues
{
    public interface IJobQueue
    {
        Task EnqueueAsync(string taskId);
        Task<string> DequeueAsync(CancellationToken cancellationToken);
    }
}

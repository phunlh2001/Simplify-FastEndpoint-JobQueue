using System.Threading.Channels;

namespace TaskProcessingSystem.Queues
{
    public class JobQueue : IJobQueue
    {
        private readonly Channel<string> _channel = Channel.CreateUnbounded<string>();

        public async Task<string> DequeueAsync(CancellationToken cancellationToken)
        {
            var task = await _channel.Reader.ReadAsync(cancellationToken);
            Console.WriteLine($"Job dequeued: {task}");
            return task;
        }

        public async Task EnqueueAsync(string task)
        {
            Console.WriteLine($"Job enqueued: {task}");
            await _channel.Writer.WriteAsync(task);
        }
    }
}

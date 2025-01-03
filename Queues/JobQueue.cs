using System.Threading.Channels;

namespace TaskProcessingSystem.Queues
{
    public class JobQueue : IJobQueue
    {
        private readonly Channel<string> _channel = Channel.CreateUnbounded<string>();

        public async Task<string> DequeueAsync(CancellationToken cancellationToken)
        {
            var id = await _channel.Reader.ReadAsync(cancellationToken);
            Console.WriteLine($"Job dequeued: '{id}'");
            return id;
        }

        public async Task EnqueueAsync(string taskId)
        {
            Console.WriteLine($"Job enqueued '{taskId}'");
            await _channel.Writer.WriteAsync(taskId);
        }
    }
}

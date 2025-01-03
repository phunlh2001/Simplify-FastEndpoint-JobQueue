using FastEndpoints;

namespace TaskProcessingSystem.Processors
{
    public class AddTaskPreprocessor<TRequest> : IPreProcessor<TRequest>
    {
        public Task PreProcessAsync(IPreProcessorContext<TRequest> context, CancellationToken ct)
        {
            var logger = context.HttpContext.Resolve<ILogger<TRequest>>();
            logger.LogInformation($"request: {context.Request?.GetType().FullName} - endpoint: {context.HttpContext.Request.Path}");
            return Task.CompletedTask;
        }
    }
}

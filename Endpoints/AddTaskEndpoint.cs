using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using TaskProcessingSystem.DTOs;
using TaskProcessingSystem.Processors;
using TaskProcessingSystem.Queues;

namespace TaskProcessingSystem.Endpoints
{
    public class AddTaskEndpoint(IJobQueue queue) : Endpoint<AddTaskRequest, Results<Ok<ResInfo<AddTaskRequest>>, ProblemDetails>>
    {
        private readonly IJobQueue _queue = queue;

        public override void Configure()
        {
            Post("/api/tasks");
            AllowAnonymous();
            Description(x => x.WithTags("Tasks"));
            PreProcessor<AddTaskPreprocessor<AddTaskRequest>>();
        }

        public override async Task<Results<Ok<ResInfo<AddTaskRequest>>, ProblemDetails>> ExecuteAsync(AddTaskRequest req, CancellationToken ct)
        {
            await Task.CompletedTask;
            if (req.Name is null || req.Name == string.Empty)
            {
                AddError(x => x.Name, "Task name cannot be empty.");
            }

            if (req.Description is null || req.Description == string.Empty)
            {
                AddError(x => x.Description, "Task description cannot be empty.");
            }

            if (ValidationFailures.Count > 0)
            {
                return new ProblemDetails(ValidationFailures);
            }

            var taskId = Guid.NewGuid();

            await _queue.EnqueueAsync(taskId.ToString());

            return TypedResults.Ok(new ResInfo<AddTaskRequest>
            {
                Message = "Add new task",
                Info = new AddTaskRequest
                {
                    Name = req.Name,
                    Description = req.Description,
                }
            });
        }
    }
}

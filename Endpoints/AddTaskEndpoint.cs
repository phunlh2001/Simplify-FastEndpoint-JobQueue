using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using TaskProcessingSystem.DTOs;
using TaskProcessingSystem.Processors;
using TaskProcessingSystem.Queues;
using TaskProcessingSystem.Services.File;

namespace TaskProcessingSystem.Endpoints
{
    public class AddTaskEndpoint(IJobQueue queue, IFileHandler fileHandler) : Endpoint<AddTaskRequest, Results<Ok<ResInfo<AddTaskRequest>>, ProblemDetails>>
    {
        private readonly IJobQueue _queue = queue;
        private readonly IFileHandler _fileHandler = fileHandler;
        private const string FILE_PATH = "task.txt";

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

            var taskId = new Random().Next(100, 1000);
            var task = new Models.Task
            {
                Id = taskId.ToString(),
                Name = req.Name,
                Description = req.Description,
                CreatedAt = DateTime.Now.ToString("dd/MM/yyyy"),
            };

            _fileHandler.Write(task, FILE_PATH);
            Console.WriteLine($"Write task {taskId} into file {FILE_PATH} successfully!");

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

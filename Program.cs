using FastEndpoints;
using FastEndpoints.Swagger;
using TaskProcessingSystem.Queues;
using TaskProcessingSystem.Workers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints().AddSwaggerDocument();

builder.Services.AddSingleton<IJobQueue, JobQueue>();
builder.Services.AddHostedService<TaskWorker>();

var app = builder.Build();
app.UseFastEndpoints().UseSwaggerGen();

app.Run();

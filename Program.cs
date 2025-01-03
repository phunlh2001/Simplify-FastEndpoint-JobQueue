using FastEndpoints;
using FastEndpoints.Swagger;
using TaskProcessingSystem.Extensions;
using TaskProcessingSystem.Queues;
using TaskProcessingSystem.Services.Email;
using TaskProcessingSystem.Services.File;
using TaskProcessingSystem.Workers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints().AddSwaggerDocument();

builder.Services.AddSingleton<IJobQueue, JobQueue>();
builder.Services.AddHostedService<TaskWorker>();

builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddFluentEmail(builder.Configuration);

builder.Services.AddSingleton<IFileHandler, FileHandler>();

var app = builder.Build();
app.UseFastEndpoints().UseSwaggerGen();

app.Run();

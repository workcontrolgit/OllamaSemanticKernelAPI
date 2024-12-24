using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using OllamaSemanticKernelAPI.Extensions;
using OllamaSemanticKernelAPI.Interfaces;
using OllamaSemanticKernelAPI.Models;
using OllamaSharp;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var ollamaSettings = builder.Configuration.GetSection("OllamaSettings").Get<OllamaSettings>();

// Register IChatCompletionService service with specified model name and URI
builder.Services.AddOllamaChatCompletion(ollamaSettings.ModelName, new Uri(ollamaSettings.Uri));

// Add services to the container.
builder.Services.AddScoped<IOllamaService, OllamaService>(provider => new OllamaService(new OllamaApiClient(new Uri(ollamaSettings.Uri))));
// Register ChatHistory as a transient service
builder.Services.AddTransient<ChatHistory>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
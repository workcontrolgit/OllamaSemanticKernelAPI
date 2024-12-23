using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using OllamaSemanticKernelAPI.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var ollamaSettings = builder.Configuration.GetSection("OllamaSettings").Get<OllamaSettings>();
builder.Services.AddOllamaChatCompletion(ollamaSettings.ModelName, new Uri(ollamaSettings.Uri));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddControllers();

// Register ChatHistory as a transient service
builder.Services.AddTransient<ChatHistory>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthorization();
app.MapControllers();

//var history = new ChatHistory();
//history.AddSystemMessage("You are a helpful assistant.");

//app.MapGet("/", () => "Hello World!");

//app.MapPost("/chat", async (ChatRequest chatRequest, IChatCompletionService chatCompletionService) =>
//{
//    history.AddUserMessage(chatRequest.Message);
//    var response = await chatCompletionService.GetChatMessageContentAsync(chatRequest.Message);
//    history.AddMessage(response.Role, response.Content ?? string.Empty);
//    return response.Content;
//});

app.Run();

public class ChatRequest
{
    public string Message { get; set; } = string.Empty;
}
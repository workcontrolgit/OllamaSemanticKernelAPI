using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Services;
using OllamaSemanticKernelAPI.Models;

namespace OllamaSemanticKernelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatCompletionService _chatCompletionService;
        private readonly ChatHistory _history;

        public ChatController(IChatCompletionService chatCompletionService, ChatHistory history)
        {
            _chatCompletionService = chatCompletionService;
            _history = history;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ChatPrompt chatPrompt)
        {
            if (string.IsNullOrEmpty(chatPrompt.Message))
                return BadRequest("Message is required");

            _history.AddUserMessage(chatPrompt.Message);

            var response = await _chatCompletionService.GetChatMessageContentAsync(chatPrompt.Message);
            _history.AddMessage(AuthorRole.User, response.Content ?? string.Empty);

            return Ok(new { Message = response.Content });
        }
    }
}
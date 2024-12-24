using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel.ChatCompletion;
using OllamaSemanticKernelAPI.Interfaces;
using OllamaSemanticKernelAPI.Models;

namespace OllamaSemanticKernelAPI.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private readonly IChatCompletionService _chatCompletionService;
        private readonly ChatHistory _history;
        private readonly IConfiguration _configuration;

        public ChatController(IChatCompletionService chatCompletionService, ChatHistory history, IConfiguration configuration)
        {
            _chatCompletionService = chatCompletionService;
            _history = history;
            _configuration = configuration;

            // Add system and assistant messages from appsettings
            var systemMessage = _configuration["ModelContextSettings:SystemMessage"];
            var assistantMessage = _configuration["ModelContextSettings:AssistantMessage"];

            if (!string.IsNullOrEmpty(systemMessage))
                _history.AddSystemMessage(systemMessage);

            if (!string.IsNullOrEmpty(assistantMessage))
                _history.AddAssistantMessage(assistantMessage);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] ChatPrompt chatPrompt)
        {
            if (string.IsNullOrEmpty(chatPrompt.Message))
                return BadRequest("Message is required");

            _history.AddUserMessage(chatPrompt.Message);
            var response = await _chatCompletionService.GetChatMessageContentAsync(chatPrompt.Message);

            if (response.Content == null)
            {
                return StatusCode(500, "Internal Server Error: Chat completion service returned a null response.");
            }
            _history.AddUserMessage(response.Content ?? string.Empty);
            return Ok(new { Message = response.Content });
        }
    }
}
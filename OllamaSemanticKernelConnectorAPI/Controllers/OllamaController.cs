using Microsoft.AspNetCore.Mvc;
using OllamaSemanticKernelAPI.Services;
using OllamaSemanticKernelAPI.Interfaces;
using OllamaSemanticKernelAPI.Models;
using System.Threading;

namespace OllamaSemanticKernelAPI.Controllers
{
    [Route("api/ollama")]
    [ApiController]
    public class OllamaController : ControllerBase
    {
        private readonly IOllamaService _ollamaService;

        public OllamaController(IOllamaService ollamaService)
        {
            _ollamaService = ollamaService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
        {
            var models = await _ollamaService.GetLocalModelsAsync(cancellationToken);
            return Ok(models);
        }
    }
}
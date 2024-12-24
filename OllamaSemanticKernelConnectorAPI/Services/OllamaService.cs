using OllamaSemanticKernelAPI.Interfaces;
using OllamaSharp;
using OllamaSharp.Models;

namespace OllamaSemanticKernelAPI.Services
{
    public class OllamaService : IOllamaService
    {
        private readonly OllamaApiClient _ollamaClient;

        public OllamaService(OllamaApiClient ollamaClient)
        {
            _ollamaClient = ollamaClient;
        }

        public async Task<IEnumerable<Model>> GetLocalModelsAsync(CancellationToken cancellationToken = default)
        {
            return await _ollamaClient.ListLocalModelsAsync();
        }
    }
}
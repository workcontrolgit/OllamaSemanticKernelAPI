using OllamaSemanticKernelAPI.Models;
using OllamaSharp.Models;
using System.Runtime.CompilerServices;

namespace OllamaSemanticKernelAPI.Interfaces
{
    public interface IOllamaService
    {
        Task<IEnumerable<Model>> GetLocalModelsAsync(CancellationToken cancellationToken = default);
    }
}
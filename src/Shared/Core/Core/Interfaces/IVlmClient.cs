using Core.DataStructures.Vlm;

namespace Core.Interfaces
{
    public interface IVlmClient
    {
        string ProviderName { get; }

        Task<VlmResponse> ChatAsync(VlmRequest request, CancellationToken ct);
    }
}

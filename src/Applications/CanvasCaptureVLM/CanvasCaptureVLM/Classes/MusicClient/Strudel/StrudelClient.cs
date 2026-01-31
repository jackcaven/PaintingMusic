using CanvasCaptureVLM.Classes.Logging;
using CanvasCaptureVLM.Classes.MusicClient.Strudel.Models;
using System.Net.Http.Json;

namespace CanvasCaptureVLM.Classes.MusicClient.Strudel
{
    internal class StrudelClient
    {
        private readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri("https://strudel.thinklet.co.uk/api/control/")
        };

        private string? authToken;

        public bool IsLoggedIn => !string.IsNullOrWhiteSpace(authToken);

        public async Task<bool> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("login", new
                {
                    email,
                    password
                }, cancellationToken);

                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken: cancellationToken)
                    ?? throw new InvalidOperationException("Failed to read login response.");

                authToken = responseData.Token;

                return responseData.Success;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to login to Strudel API.", ex);
            }
        }

        public async Task Prompt(string prompt, CancellationToken cancellationToken = default)
        {
            try
            {
                if (authToken == null)
                {
                    throw new InvalidOperationException("Client is not authenticated. Please login first.");
                }

                var request = new HttpRequestMessage(HttpMethod.Post, "prompt")
                {
                    Content = JsonContent.Create(new
                    {
                        prompt
                    })
                };

                request.Headers.Add("Authorization", $"Bearer {authToken}");
                var response = await httpClient.SendAsync(request, cancellationToken);
                
                response.EnsureSuccessStatusCode();

                Log.Debug($"Strudel Request Success: {response.IsSuccessStatusCode}", nameof(StrudelClient));
            }
            catch (Exception ex) 
            {
                throw new InvalidOperationException("Failed to send prompt to Strudel API.", ex);
            }
        }

        public async Task Reset(CancellationToken cancellationToken = default)
        {
            try
            {
                if (authToken == null)
                {
                    throw new InvalidOperationException("Client is not authenticated. Please login first.");
                }
                var request = new HttpRequestMessage(HttpMethod.Post, "reset");
                request.Headers.Add("Authorization", $"Bearer {authToken}");
                var response = await httpClient.SendAsync(request, cancellationToken);

                response.EnsureSuccessStatusCode();

                Log.Debug($"Reset: {response.StatusCode}", nameof(StrudelClient));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to reset Strudel API session.", ex);
            }
        }
    }
}

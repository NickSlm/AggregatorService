using AggregatorService.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;
using Polly;
using static System.Net.WebRequestMethods;
using Polly.Retry;
using Shared.Models;

namespace AggregatorService.Services
{
    public class BlizzardApiService: IBlizzardApiService
    {
        private readonly IBlizzardAuthService _authService;
        private readonly IAsyncPolicy<HttpResponseMessage> _asyncPolicy;
        private readonly HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://eu.api.blizzard.com")
        };

        public BlizzardApiService(IBlizzardAuthService authService, IAsyncPolicy<HttpResponseMessage> asyncPolicy)
        {
            _authService = authService;
            _asyncPolicy = asyncPolicy;
        }

        public async Task<Leaderboard> GetRawData()
        {
            var token = await _authService.GetAccessTokenAsync();

            var request = new HttpRequestMessage(HttpMethod.Get, $"/data/wow/pvp-season/33/pvp-leaderboard/3v3?namespace=dynamic-eu&locale=en_US");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _asyncPolicy.ExecuteAsync(() => _httpClient.SendAsync(request));

            var json = await response.Content.ReadAsStringAsync();
            var leaderboard = JsonSerializer.Deserialize<Leaderboard>(json);

            return leaderboard;
        }
    }
}

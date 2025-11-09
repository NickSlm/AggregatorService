using AggregatorService.Interfaces;
using AggregatorService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace AggregatorService.Services
{
    public class BlizzardApiService: IBlizzardApiService
    {
        private readonly IBlizzardAuthService _authService;
        private readonly HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://eu.api.blizzard.com")
        };

        public BlizzardApiService(IBlizzardAuthService authService)
        {
            _authService = authService;
        }


        // TODO: add polly method to retry to fetch data on error
        public async Task GetRawData()
        {
            var token = await _authService.GetAccessTokenAsync();
            var request = new HttpRequestMessage(HttpMethod.Get, $"/data/wow/pvp-season/33/pvp-leaderboard/3v3?namespace=dynamic-eu&locale=en_US");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);


            var json = await response.Content.ReadAsStringAsync();


            var leaderboard = JsonSerializer.Deserialize<Leaderboard>(json);

            foreach (Entry entry in leaderboard.Entries)
            {
                Console.WriteLine(entry.Player.Name);
            }

        }
    }
}

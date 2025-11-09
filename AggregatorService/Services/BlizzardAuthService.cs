using AggregatorService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AggregatorService.Services
{
    public class BlizzardAuthService: IBlizzardAuthService
    {
        private string _accessToken;
        private DateTime _tokenExpiration;


        public async Task<string> GetAccessTokenAsync()
        {
            if (string.IsNullOrEmpty(_accessToken) || DateTime.UtcNow >= _tokenExpiration)
            {
                await RefreshAccessTokenAsync();
            }
            return _accessToken;
        }

        public async Task RefreshAccessTokenAsync()
        {
            // TODO: Encrypt using Windows Credentials Manager
            string clientId = "6eef8ac48dbc417197ed8a34c731e398";
            string clientSecret = "mxzBzxKXxQZsYU3ogGjPhoodi9O6VVmQ";

            using var httpClient = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://oauth.battle.net/token")
            {
                Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                })
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"))
                );

            var response = await httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var token = doc.RootElement.GetProperty("access_token");
            var expiresIn = doc.RootElement.GetProperty("expires_in");

            _tokenExpiration = DateTime.UtcNow.AddSeconds(expiresIn.GetInt32() - 60);
            _accessToken = token.ToString();

        }

    }
}

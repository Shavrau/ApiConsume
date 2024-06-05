using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SpotifyAPI.Services
{
    public class AccessTokenService
    {
        private const string ClientId = "5d6396e8ea294d0982be0aa32d871bf7";
        private const string ClientSecret = "959fbe6295a9493cac1be0a1c4f6aa6c";
        private const string RedirectUri = "http://localhost:8080/callback";

        public async Task<string> RequestAccessToken(string authorizationCode)
        {
            using (var client = new HttpClient())
            {
                var body = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("code", authorizationCode),
                    new KeyValuePair<string, string>("redirect_uri", RedirectUri),
                    new KeyValuePair<string, string>("client_id", ClientId),
                    new KeyValuePair<string, string>("client_secret", ClientSecret)
                });

                var response = await client.PostAsync("https://accounts.spotify.com/api/token", body);
                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);

                return json["access_token"]?.ToString();
            }
        }
    }
}

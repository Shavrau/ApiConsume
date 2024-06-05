using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace SpotifyAPI.Services
{
    public class RecommendationsService
    {
        public async Task<List<JToken>> GetRecommendations(string accessToken)
        {
            var topArtistIds = await GetUserTopArtistIds(accessToken);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                var seedArtists = string.Join(",", topArtistIds);
                var response = await client.GetAsync($"https://api.spotify.com/v1/recommendations?seed_artists={seedArtists}&limit=5");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Falha ao obter recomendações. Código de status: {response.StatusCode}");
                }

                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);

                if (json["tracks"] == null)
                {
                    throw new Exception("Não foi possível obter recomendações de músicas.");
                }

                return json["tracks"].ToList();
            }
        }

        private async Task<List<string>> GetUserTopArtistIds(string accessToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                var response = await client.GetAsync("https://api.spotify.com/v1/me/top/artists?limit=5");
                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);

                var artistIds = new List<string>();
                foreach (var item in json["items"])
                {
                    artistIds.Add(item["id"].ToString());
                }

                return artistIds;
            }
        }

        public void PrintRecommendations(List<JToken> recommendations)
        {
            if (recommendations != null && recommendations.Count > 0)
            {
                Console.WriteLine("Recomendações de músicas:");
                foreach (var recommendation in recommendations)
                {
                    if (recommendation != null && recommendation["name"] != null && recommendation["artists"] != null && recommendation["artists"][0] != null && recommendation["artists"][0]["name"] != null)
                    {
                        Console.WriteLine($"{recommendation["name"]} - {recommendation["artists"][0]["name"]}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Não foi possível obter recomendações de músicas.");
            }
        }
    }
}

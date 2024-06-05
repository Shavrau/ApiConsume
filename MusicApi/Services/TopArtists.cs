using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SpotifyAPI.Services
{
    public class TopArtistsService
    {
        public async Task<List<JToken>> GetUserTopArtists(string accessToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                var response = await client.GetAsync("https://api.spotify.com/v1/me/top/artists?limit=10");
                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);

                return json["items"].ToList();
            }
        }


        public async Task<List<string>> GetUserTopArtistIds(string accessToken)
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

        public async Task<List<string>> GetRelatedArtistIds(string accessToken, string artistId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                var response = await client.GetAsync($"https://api.spotify.com/v1/artists/{artistId}/related-artists");
                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);

                var relatedArtistIds = new List<string>();
                foreach (var item in json["artists"])
                {
                    relatedArtistIds.Add(item["id"].ToString());
                }

                return relatedArtistIds;
            }
        }

        public void PrintTopArtists(List<JToken> topArtists)
        {
            if (topArtists != null && topArtists.Count > 0)
            {
                Console.WriteLine("Top Artists:");
                foreach (var artist in topArtists)
                {
                    if (artist != null && artist["name"] != null)
                    {
                        Console.WriteLine(artist["name"]);
                    }
                }
            }
            else
            {
                Console.WriteLine("No top artists found.");
            }
        }
    }
}

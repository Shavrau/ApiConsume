using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SpotifyAPI
{
    public static class TopTracks
    {
        public static async Task<List<JToken>> GetUserTopTracks(string accessToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                var response = await client.GetAsync("https://api.spotify.com/v1/me/top/tracks");
                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);

                return json["items"].ToList();
            }
        }

        public static void PrintTopTracks(List<JToken> topTracks)
        {
            if (topTracks != null && topTracks.Count > 0)
            {
                Console.WriteLine("Top 20 músicas mais ouvidas:");
                foreach (var track in topTracks)
                {
                    if (track != null && track["name"] != null && track["artists"] != null && track["artists"][0] != null && track["artists"][0]["name"] != null)
                    {
                        Console.WriteLine($"{track["name"]} - {track["artists"][0]["name"]}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Não foi possível obter as músicas mais ouvidas.");
            }
        }
    }
}

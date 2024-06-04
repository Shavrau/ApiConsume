using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SpotifyAPI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var authorizationCode = Auth.GetAuthorizationCode();
                var accessToken = await AccessToken.RequestAccessToken(authorizationCode);

                if (!string.IsNullOrEmpty(accessToken))
                {
                    var topTracks = await TopTracks.GetUserTopTracks(accessToken);
                    TopTracks.PrintTopTracks(topTracks);
                }
                else
                {
                    Console.WriteLine("Falha ao obter token.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

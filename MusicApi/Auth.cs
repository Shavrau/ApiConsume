using System;

namespace SpotifyAPI
{
    public static class Auth
    {
        private const string ClientId = "5d6396e8ea294d0982be0aa32d871bf7";
        private const string RedirectUri = "http://localhost:8080/callback";

        public static string GetAuthorizationCode()
        {
            Console.WriteLine($"autorize o aplicativo em: https://accounts.spotify.com/authorize?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=code&scope=user-top-read");
            Console.WriteLine("cole o código de autorização aqui(pos code):");
            return Console.ReadLine();
        }
    }
}

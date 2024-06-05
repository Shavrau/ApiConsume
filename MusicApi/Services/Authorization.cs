using System;

namespace SpotifyAPI.Services
{
    public class AuthorizationService
    {
        private const string ClientId = "5d6396e8ea294d0982be0aa32d871bf7";
        private const string RedirectUri = "http://localhost:8080/callback";

        public string GetAuthorizationCode()
        {
            Console.WriteLine($"Por favor, autorize o aplicativo em: https://accounts.spotify.com/authorize?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=code&scope=user-top-read");
            Console.WriteLine("Após autorizar, cole o código de autorização aqui:");
            return Console.ReadLine();
        }
    }
}

using System;
using System.Threading.Tasks;
using SpotifyAPI.Services;

namespace SpotifyAPI.Controllers
{
    public class SpotifyController
    {
        private readonly AuthorizationService _authorization;
        private readonly AccessTokenService _accessToken;
        private readonly TopTracksService _topTracks;
        private readonly TopArtistsService _topArtists;
        private readonly RecommendationsService _recommendations;

        public SpotifyController()
        {
            _authorization = new AuthorizationService();
            _accessToken = new AccessTokenService();
            _topTracks = new TopTracksService();
            _topArtists = new TopArtistsService();
            _recommendations = new RecommendationsService();
        }

        public async Task Run()
        {
            try
            {
                var authorizationCode = _authorization.GetAuthorizationCode();
                var accessToken = await _accessToken.RequestAccessToken(authorizationCode);

                if (!string.IsNullOrEmpty(accessToken))
                {
                    int choice;
                    do
                    {
                        Console.WriteLine("Escolha uma opção:");
                        Console.WriteLine("1. Mostrar músicas favoritas");
                        Console.WriteLine("2. Mostrar artistas favoritos");
                        Console.WriteLine("3. Obter recomendações de músicas");
                        Console.WriteLine("0. Sair");

                        if (!int.TryParse(Console.ReadLine(), out choice))
                        {
                            Console.WriteLine("Opção inválida. Tente novamente.");
                            continue;
                        }

                        switch (choice)
                        {
                            case 1:
                                var topTracks = await _topTracks.GetUserTopTracks(accessToken);
                                _topTracks.PrintTopTracks(topTracks);
                                break;
                            case 2:
                                var topArtists = await _topArtists.GetUserTopArtists(accessToken);
                                _topArtists.PrintTopArtists(topArtists);
                                break;
                            case 3:
                                var recommendations = await _recommendations.GetRecommendations(accessToken);
                                _recommendations.PrintRecommendations(recommendations);
                                break;
                            case 0:
                                Console.WriteLine("Saindo do programa...");
                                break;
                            default:
                                Console.WriteLine("Opção inválida. Tente novamente.");
                                break;
                        }
                    } while (choice != 0);
                }
                else
                {
                    Console.WriteLine("Falha ao obter token de acesso.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }
}

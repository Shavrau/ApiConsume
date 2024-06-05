using System;
using System.Threading.Tasks;
using SpotifyAPI.Controllers;

namespace SpotifyAPI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var controller = new SpotifyController();
            await controller.Run();
        }
    }
}

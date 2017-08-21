using SFML;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;
 

namespace CavernCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Run();
        }
    }
}

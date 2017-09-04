using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;

namespace CavernCrawler.Src
{
    class CharacterManager
    {
        Map currentMap;

        public CharacterManager(Map theMap)
        {
            currentMap = theMap;
        }
    }
}

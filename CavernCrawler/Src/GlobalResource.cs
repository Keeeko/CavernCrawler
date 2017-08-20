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

namespace CavernCrawler
{
    public class GlobalResource
    {
        //Root level loading
        const string contentFolder = @"\Content";
        const string texturesFolder = @"\Textures";

        //Dungeon Tileset
        const string tilesetFolder = @"\TX_Dungeon";
        const string wallTilesFolder = @"\wall";
        const string floorTilesFolder = @"\floor";
        const string waterTilesFolder = @"\water";

        string[] tilePaths { get; set; }

        public GlobalResource()
        {

        }
        
    }
}

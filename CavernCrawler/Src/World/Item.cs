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
    class Item
    {
        Texture itemGraphic;

        static public int itemIDCount = 0;
        public int itemID;
        public string name;

        public Item(string pName)
        {
            itemIDCount++;
            itemID = itemIDCount;

            name = pName;
        }
        
    }
}

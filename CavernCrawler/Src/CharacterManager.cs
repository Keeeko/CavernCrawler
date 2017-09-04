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
    class CharacterManager
    {
        Map currentMap;
        List<Character> characters;

        public CharacterManager(Map theMap)
        {
            currentMap = theMap;
            characters = new List<Character>();
        }

        public void AddCharacter(Character character)
        {
            characters.Add(character);
        }

        public void RemoveCharacter(Character character)
        {
            characters.Remove(character);
        }

        public void Update()
        {
            foreach(Character character in characters)
            {
                character.Update();
            }
        }
    }
}

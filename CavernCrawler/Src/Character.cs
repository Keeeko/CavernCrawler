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
    class Character
    {
        public Vector2f position;
        public bool isMale;
        public float maxHealth;
        public float currentHealth;

        public Character()
        {
            position = new Vector2f(3.0f, 3.0f);
            isMale = true;
            maxHealth = 100.0f;
            currentHealth = maxHealth;
        }

        public void Move(float xPos, float yPos)
        {
            position.X = xPos;
            position.Y = yPos;
        }
    }
}

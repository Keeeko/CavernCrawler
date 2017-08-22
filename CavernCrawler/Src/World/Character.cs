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
        public int xPos;
        public int yPos;
        public int graphicsID;
        public int maxMoves;
        public bool isMale;
        public float maxHealth;
        public float currentHealth;


        public Character()
        {
            xPos = 3;
            yPos = 3;
            maxMoves = 1;
            isMale = true;
            maxHealth = 100.0f;
            currentHealth = maxHealth;
        }

        public void Move(int xAmount, int yAmount)
        {
            xPos += xAmount;
            yPos += yAmount;
        }

        public void MoveTo(int xPosition, int yPosition)
        {
            xPos = xPosition;
            yPos = yPosition;
        }
    }
}

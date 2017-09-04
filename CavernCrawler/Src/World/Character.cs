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

        public Map currentMap;

        public Character(Map mapReference)
        {
            xPos = 3;
            yPos = 3;
            maxMoves = 1;
            isMale = true;
            maxHealth = 100.0f;
            currentHealth = maxHealth;
            currentMap = mapReference;

            currentMap.SetCharacterMap(xPos, yPos, this);
        }

        public Character(Map mapReference, int xPosRef, int yPosRef)
        {
            xPos = xPosRef;
            yPos = yPosRef;
            maxMoves = 1;
            isMale = true;
            maxHealth = 100.0f;
            currentHealth = maxHealth;
            currentMap = mapReference;

            currentMap.SetCharacterMap(xPos, yPos, this);
        }

        public void Move(int xAmount, int yAmount)
        {
            if (currentMap.GetCharacterFromMap(xPos + xAmount, yPos + yAmount) == null)
            {
                //Delete characters old position in dictionary
                currentMap.RemoveCharacterFromPosition(xPos, yPos);

                xPos += xAmount;
                yPos += yAmount;

                //Set character to new position in character dictionary
                currentMap.SetCharacterMap(xPos, yPos, this);
            }
            else
            {
                Console.WriteLine("There is an enemy in that tile");
            }
        }

        public void MoveTo(int xPosition, int yPosition)
        {
            //currentMap.RemoveCharacterFromPosition(xPos, yPos);

            xPos = xPosition;
            yPos = yPosition;

            //currentMap.SetCharacterMap(xPos, yPos, this);
        }
    }
}

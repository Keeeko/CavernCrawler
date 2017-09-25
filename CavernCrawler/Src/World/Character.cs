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
        public float damage;
        public float defense;
        public string name;

        public Map currentMap;
        public ItemContainer inventory;
        public GlobalResource globalResource;

        CharacterManager characterManager;

        public Character(Map mapReference, CharacterManager characterManagerReference, GlobalResource theGlobalResource)
        {
            xPos = 3;
            yPos = 3;
            maxMoves = 1;
            isMale = true;
            maxHealth = 100.0f;
            currentHealth = maxHealth;
            globalResource = theGlobalResource;
            currentMap = mapReference;
            inventory = new ItemContainer(15, globalResource);
            inventory.SetContainerName("inventory");
            inventory.PrintContentsToConsole(globalResource.GeteventConsole());

            characterManager = characterManagerReference;
            characterManager.AddCharacter(this);
            currentMap.SetCharacterMap(xPos, yPos, this);
        }

        public Character(Map mapReference, CharacterManager characterManagerReference, int xPosRef, int yPosRef, string nameRef, GlobalResource theGlobalResource)
        {
            xPos = xPosRef;
            yPos = yPosRef;
            maxMoves = 1;
            damage = 5;
            isMale = true;
            maxHealth = 100.0f;
            currentHealth = maxHealth;
            currentMap = mapReference;
            name = nameRef;
            globalResource = theGlobalResource;

            inventory = new ItemContainer(15, globalResource);
            inventory.SetContainerName("inventory");
            inventory.PrintContentsToConsole(globalResource.GeteventConsole());
            characterManager = characterManagerReference;
            characterManager.AddCharacter(this);
            currentMap.SetCharacterMap(xPos, yPos, this);
        }

        public void Update()
        {
            if(currentHealth <= 0)
            {
                Die();
            }
        }

        public void Move(int xAmount, int yAmount)
        {
            if (currentMap.GetCharacterFromMap(xPos + xAmount, yPos + yAmount) == null && currentMap.GetMapTile(xPos + xAmount, yPos + yAmount) == 0)
            {
                //Delete characters old position in dictionary
                currentMap.RemoveCharacterFromPosition(xPos, yPos);

                xPos += xAmount;
                yPos += yAmount;

                //Set character to new position in character dictionary
                currentMap.SetCharacterMap(xPos, yPos, this);
            }
            else if(currentMap.GetCharacterFromMap(xPos + xAmount, yPos + yAmount) != null)
            {
                Attack(currentMap.GetCharacterFromMap(xPos + xAmount, yPos + yAmount));
            }
        }

        public void Attack(Character target)
        {
            Console.WriteLine(name + " attacks " + target.name + " for " + damage + " damage!");
            target.currentHealth -= damage;
            Console.Write(target.name + " has " + target.currentHealth + "health left!\n");
        }

        public void MoveTo(int xPosition, int yPosition)
        {
            xPos = xPosition;
            yPos = yPosition;
        }

        public void Die()
        {
            currentMap.RemoveCharacterFromPosition(xPos, yPos);
            //characterManager.RemoveCharacter(this);
        }
    }
}

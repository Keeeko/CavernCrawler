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
    class ItemContainer
    {
        static int containerIDCount = 0;
        int containerID;
        int itemCapacity;
        string containerName;

        Dictionary<int, Item> items;
        Vector2i tilePos;
        Vector2f worldPos;
        
        public ItemContainer(int size)
        {
            containerIDCount++;
            containerID = containerIDCount;
            itemCapacity = size;
            containerName = "Container";
            items = new Dictionary<int, Item>();

            for (int i = 0; i < size; i++)
            {
                AddItemToContainer(new Item("Sword"));
            }
        }

        public void AddItemToContainer(Item item)
        {
            if(items.Count < itemCapacity)
            {
                items.Add(item.itemID ,item);
            }
        }

        public void SetContainerName(string pName)
        {
            containerName = pName;
        }

        public void MoveItemToContainer(ItemContainer itemContainer, int itemID)
        {
            itemContainer.AddItemToContainer(items[itemID]);
        }

        public void PrintContentsToConsole(EventConsole eventConsole)
        {
            List<Item> itemList = GetContents();
            foreach(Item item in itemList)
            {
                eventConsole.AddTextToConsole(containerName + " has a: " + item.name);
            }
        }

        public List<Item> GetContents()
        {
            return items.Values.ToList();
        }

        public Item GetItem(int itemID)
        {
            //This will return a null item if it is not found in the container
            Item theItem;
            items.TryGetValue(itemID, out theItem);

            return theItem;
        }
    }
}

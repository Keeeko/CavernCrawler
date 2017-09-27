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
        GlobalResource globalResource;
        int containerID;
        int itemCapacity;
        string containerName;

        Dictionary<int, Item> items;
        List<ContainerSlot> slots;
        Vector2i tilePos;
        Vector2f worldPos;

        //Drawing variables
        Vector2f drawPosition;
        Vector2f slotSpacing;

        //The maximum number of slots that can be drawn on a line
        int maxHorizontalItems;

        public ItemContainer(int size, GlobalResource theGlobalResource)
        {
            globalResource = theGlobalResource;
            containerIDCount++;
            containerID = containerIDCount;
            itemCapacity = size;
            containerName = "Container";
            items = new Dictionary<int, Item>();

            slots = new List<ContainerSlot>();
            drawPosition = new Vector2f(5.0f, 800.0f);
            slotSpacing = new Vector2f(76.0f + 10.0f,76 + 10.0f);
            maxHorizontalItems = 5;

            int xPos = 0;
            int yPos = 0;

            for (int i = 0; i < size; i++)
            {

                if (i % maxHorizontalItems == 0 && i != 0)
                {
                    yPos++;
                    xPos = 0;
                }

                Vector2f slotPos = new Vector2f(drawPosition.X + xPos * slotSpacing.X, drawPosition.Y + yPos * slotSpacing.Y);
                slots.Add(new ContainerSlot(slotPos));
                AddItemToContainer(new Item("Sword"));
                slots[i].SetContainedItem(GetContents()[i]);

                xPos++;

            }
        }

        public void Draw(View view)
        {
            globalResource.window.SetView(view);

            foreach(ContainerSlot slot in slots)
            {
                slot.Draw(globalResource);
            }

            globalResource.window.SetView(globalResource.GetMainView());
        }

        public void AddItemToContainer(Item item)
        {
            if(items.Count < itemCapacity)
            {
                items.Add(item.itemID ,item);
                slots[items.Count - 1].SetContainedItem(item);
            }
        }

        public void SetContainerName(string pName)
        {
            containerName = pName;
        }

        public void MoveItemToContainer(ItemContainer itemContainer, int itemID)
        {
            itemContainer.AddItemToContainer(items[itemID]);
            //Remove from slot, need to find the slot the item was on and then remove it
        }

        public void PrintContentsToConsole(EventConsole eventConsole)
        {
            List<Item> itemList = GetContents();
            foreach(Item item in itemList)
            {
                //eventConsole.AddTextToConsole(containerName + " has a: " + item.name);
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

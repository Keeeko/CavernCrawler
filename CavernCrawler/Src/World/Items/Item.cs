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
    public enum State { IN_CONTAINER, IN_WORLD, EQUIPPED };

    class Item
    {
        State itemState;
        Image itemImage;
        Texture itemTexture;
        ContainerSlot parentSlot;

        static public int itemIDCount = 0;
        public int itemID;
        public string name;
        public Vector2f position;
        bool isEquippable;

        public Item(string pName)
        {
            itemIDCount++;
            itemID = itemIDCount;
            itemImage = new Image(@"Content\Textures\Tx_Item\weapon\short_sword2.png");
            itemImage.CreateMaskFromColor(Color.White);
            itemTexture = new Texture(itemImage);
            name = pName;
            itemState = State.IN_CONTAINER;
            isEquippable = false;
        }

        public void Draw(GlobalResource globalResource)
        {
            Sprite drawSprite = new Sprite(itemTexture);
            drawSprite.Position = position;
            globalResource.GetWindow().Draw(drawSprite);
        }

        public void SetItemState(State state)
        {
            itemState = state;
        }

        public void MoveItemToWorld(Vector2f pos)
        {
            position = pos;
            parentSlot = null;
            SetItemState(State.IN_WORLD);
        }

        public void MoveItemToContainer(ContainerSlot containerSlot)
        {
            parentSlot = containerSlot;
            position = parentSlot.position;
            SetItemState(State.IN_CONTAINER);
        }

        public void SetEquippable(bool value)
        {
            isEquippable = value;
        }

        public ContainerSlot GetParentSlot()
        {
            return parentSlot;
        }

        public void SetParentSlot(ContainerSlot slot)
        {
            parentSlot = slot;
        }
    }
}

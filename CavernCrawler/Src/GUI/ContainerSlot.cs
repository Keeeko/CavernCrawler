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
    class ContainerSlot
    {
        public Vector2f position;
        Image slotImage;
        Texture slotTexture;
        Item containedItem;
        float graphicWidth;
        float graphicHeight;

        public ContainerSlot(Vector2f pos)
        {

            slotImage = new Image(@"Content\Textures\Tx_GUI\inventorySlot.png");
            slotTexture = new Texture(slotImage);

            graphicWidth = slotImage.Size.X;
            graphicHeight = slotImage.Size.Y;

            position = pos;
        }

        public Item GetContainedItem()
        {
            return containedItem;
        }

        public void SetContainedItem(Item item)
        {
            containedItem = item;
            item.SetParentSlot(this);
        }

        public void Draw(GlobalResource globalResource)
        {
            Sprite drawSprite = new Sprite(slotTexture);
            drawSprite.Position = position;
            globalResource.GetWindow().Draw(drawSprite);

            containedItem.position = position;
            containedItem.Draw(globalResource);
        }
    }
}

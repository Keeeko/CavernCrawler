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
    class ItemPreviewPanel
    {
        Image panelImage;
        Texture panelTexture;
        Font panelFont;
        View itemPreviewView;

        Vector2f panelPosition;
        Vector2f panelDimensions;

        public Item item;

        public ItemPreviewPanel(Item theItem, Vector2f pos, Vector2f size)
        {
            panelPosition = pos;
            panelDimensions = size;
            itemPreviewView = new View(panelPosition, panelDimensions);
            panelImage = new Image(@"Content\Textures\Tx_GUI\ItemPreview.png");
            panelFont = new Font(@"Content\Fonts\novem.ttf");
            panelTexture = new Texture(panelImage);

            item = theItem;

        }

        public void Draw(GlobalResource globalResource)
        {
            globalResource.window.SetView(itemPreviewView);

            panelPosition.X = (float)globalResource.GetInputManager().GetMouseCoordinates().X;
            panelPosition.Y = (float)globalResource.GetInputManager().GetMouseCoordinates().Y;

            itemPreviewView.Viewport = new FloatRect((panelPosition.X - panelDimensions.X) / 1440, (panelPosition.Y - panelDimensions.Y) / 1080, 0.35f, 0.65f);
   

            //Draw preview at mouse pos
            Sprite drawSprite = new Sprite(panelTexture);

           // panelPosition = new Vector2f();
            Vector2f spritePos = new Vector2f(0.0f, 0.0f);

            drawSprite.Position = spritePos;

            //Draw item spite
            Sprite itemSprite = new Sprite(item.GetTexture());

            //Draw Text
            //Title
            Text writeText = new Text();

            writeText.CharacterSize = 32;
            writeText.Color = Color.Black;
            writeText.Position = new Vector2f(15.0f, 25.0f);
            writeText.Font = panelFont;
            writeText.DisplayedString = item.name;

            //Description
            Text descriptionText = new Text();

            descriptionText.CharacterSize = 24;
            descriptionText.Color = Color.Black;
            descriptionText.Position = new Vector2f(15.0f, 100.0f);
            descriptionText.Font = panelFont;
            descriptionText.DisplayedString = "-Description-\n" + item.description;

            //Stat print
            Text statText = new Text();

            statText.CharacterSize = 24;
            statText.Color = Color.Black;
            statText.Position = new Vector2f(15.0f, 175.0f);
            statText.Font = panelFont;
            statText.DisplayedString = "-Stats-\n" + item.statDescription;

            itemSprite.Scale *= 2;
            itemSprite.Position = new Vector2f(150.0f, 500.0f);

            globalResource.window.Draw(drawSprite);
            globalResource.window.Draw(descriptionText);
            globalResource.window.Draw(statText);
            globalResource.window.Draw(writeText);
            globalResource.window.Draw(itemSprite);
            globalResource.window.SetView(globalResource.mainView);
        }

        public void SetItem(Item theItem)
        {
            item = theItem;
        }
    }
}

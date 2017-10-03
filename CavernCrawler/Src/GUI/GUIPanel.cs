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
    class GUIPanel
    {
        private View guiView;
        GlobalResource globalResource;
        RenderWindow window;

        Image guiPanelImage;
        Texture guiPanelTexture;

        Font guiFont;

        public GUIPanel(Vector2f guiViewCenter, Vector2f guiViewSize , GlobalResource theGlobalResource)
        {
            globalResource = theGlobalResource;
            theGlobalResource.SetGUIPanel(this);
            window = globalResource.window;

            guiPanelImage = new Image(@"Content\Textures\Tx_GUI\panel_backgroundBase.png");
            guiFont = new Font(@"Content\Fonts\novem.ttf");
            guiPanelTexture = new Texture(guiPanelImage);

            guiView = new View(guiViewCenter, guiViewSize);


        }

        public void DrawGUIPanel()
        {
            guiView.Viewport = new FloatRect(0.7f, 0.0f, 0.3f, 1f);
            window.SetView(guiView);


            Sprite tempSprite = new Sprite(guiPanelTexture);
            tempSprite.Position = new Vector2f(0.0f, 0.0f);

            window.Draw(tempSprite);

            DrawInventory();

        }

        public void DrawInventory()
        {
            globalResource.GetPlayer().inventory.Draw(guiView);

            List<Item> items = globalResource.GetPlayer().inventory.GetContents();

            foreach(Item item in items)
            {
               // Text writeText = new Text(item.name, guiFont, 18);
                //writeText.Position = guiView.Center;
                //window.Draw(writeText);
            }

        }

    }
}

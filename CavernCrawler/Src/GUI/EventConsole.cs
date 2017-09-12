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
    class EventConsole
    {
        private View consoleView;
        GlobalResource globalResource;
        RenderWindow window;

        Image borderOverlayImage;
        Texture borderOverlay;
        Font consoleFont;
        Color fontColor;
        List<Text> consoleText;
        

        Vector2f borderMargin;
        Vector2f cursorPosition;

        float lineSpacing;
        uint fontSize;
        int maxLineLength;
        int occupiedLines;

        public EventConsole(Color fontColorRef, uint fontSizeRef, Vector2f consoleViewCenter, Vector2f consoleViewSize,
            GlobalResource theGlobalResource)
        {
            globalResource = theGlobalResource;
            theGlobalResource.SetEventConsole(this);
            consoleFont = new Font(@"Content\Fonts\novem.ttf");
            borderOverlayImage = new Image(@"Content\Textures\Tx_Gui\panel_console.png");
            borderOverlayImage.CreateMaskFromColor(Color.White);
            borderOverlay = new Texture(borderOverlayImage);
            consoleText = new List<Text>();
            fontColor = fontColorRef;
            fontSize = fontSizeRef;
            borderMargin = new Vector2f(10.0f, 10.0f);
            lineSpacing = fontSize + 5.0f;
            maxLineLength = 89;
            occupiedLines = 0;
            window = globalResource.window;
            consoleView = new View(consoleViewCenter, consoleViewSize);
           
        }

        public void AddTextToConsole(string eventMessage)
        {
            Text writeText = new Text();
            
            writeText.CharacterSize = fontSize;
            writeText.Color = fontColor;
            cursorPosition = new Vector2f(borderMargin.X, borderMargin.Y + (occupiedLines * lineSpacing));
            occupiedLines++;
            writeText.Position = cursorPosition;
            writeText.Font = consoleFont;
            writeText.DisplayedString = FormatText(eventMessage);
            
            consoleText.Add(writeText);
            
            if(consoleText.Count > 10)
            {
                SetConsoleView(occupiedLines);
            }
        }
        
        public string FormatText(string text)
        {
            //////
            //TODO: Make multiple lines wrap, wrap words and not individual characters
            /////
            StringBuilder stringBuilder = new StringBuilder(text);

            //Wrap text if it exceeds maximum line length
            if (text.Length > maxLineLength)
            {
                //If the character that exceeds the length is a space, then we dont need to shift any letters
                if (stringBuilder[maxLineLength] != ' ')
                {
                    //Increase the size of the string by 1 to allow for the insertion of the escape character
                    stringBuilder.Append(' ');

                    for (int i = 0; i < text.Length - maxLineLength; i++)
                    {
                        //Start at the end of the string, move each character forward, since arrays start at 0 we have to deduct 1 from the string length to ensure we dont go overbounds
                        stringBuilder[(stringBuilder.Length - 1) - i] = stringBuilder[(stringBuilder.Length - 1) - (i + 1)];
                    }
                }

                stringBuilder[maxLineLength] = '\n';
                occupiedLines++;
            }

            return stringBuilder.ToString();
        }

        public void DrawConsole()
        {
            consoleView.Viewport = new FloatRect(0.0f, 0.7f, 0.7f, 0.3f);
            window.SetView(consoleView);

            foreach (Text text in consoleText)
            {
                window.Draw(text);
            }

            //Draw border always on top of the view
            Sprite borderSprite = new Sprite(borderOverlay);
            borderSprite.Position = new Vector2f(consoleView.Center.X - borderOverlayImage.Size.X/2 , consoleView.Center.Y - borderOverlayImage.Size.Y / 2);
            window.Draw(borderSprite);
        }

        public void ScrollConsoleView(float scrollAmount)
        {
            window.SetView(consoleView);
            consoleView.Center = new Vector2f(consoleView.Center.X, consoleView.Center.Y + scrollAmount);

            window.SetView(globalResource.mainView);
        }

        public void SetConsoleView(int lineNumber)
        {
            window.SetView(consoleView);
            consoleView.Center = new Vector2f(consoleView.Center.X, (borderMargin.Y + (lineNumber * lineSpacing) + borderMargin.Y) - (consoleView.Size.Y / 2));
            window.SetView(globalResource.mainView);
        }

        public void WriteEventToConsole(string eventMessage)
        {

        }
    }
}

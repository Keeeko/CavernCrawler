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
        Font consoleFont;
        Color fontColor;
        List<Text> consoleText;

        Vector2f topPos;
        float lineSpacing;

        uint fontSize;

        public EventConsole(Color fontColorRef, uint fontSizeRef)
        {
            consoleFont = new Font(@"Content\Fonts\PressStart2P-Regular.ttf");
            consoleText = new List<Text>();
            fontColor = fontColorRef;
            fontSize = fontSizeRef;
            topPos = new Vector2f(10.0f, 10.0f);
            lineSpacing = 20f;
        }

        public void AddTextToConsole(string eventMessage)
        {
            Text writeText = new Text();
            writeText.CharacterSize = fontSize;
            writeText.Color = fontColor;
            writeText.Position = new Vector2f(topPos.X, topPos.Y + (consoleText.Count * lineSpacing));
            writeText.Font = consoleFont;
            writeText.DisplayedString = eventMessage;

            consoleText.Add(writeText);
        }
        
        public void DrawText(RenderWindow theWindow, View consoleView)
        {
           foreach(Text text in consoleText)
            {
                theWindow.Draw(text);
            }
        }

        public void WriteEventToConsole(string eventMessage)
        {

        }
    }
}

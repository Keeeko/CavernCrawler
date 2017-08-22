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
    class InputManager
    {
        public void Update(Character thePlayer, Camera mainCamera)
        {
            //Player controls
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                thePlayer.Move(1, 0);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                thePlayer.Move(-1, 0);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                thePlayer.Move(0, 1);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                thePlayer.Move(0, -1);
            }

            //Camera controls
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                mainCamera.MoveCamera(0, -1);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                mainCamera.MoveCamera(0, 1);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                mainCamera.MoveCamera(1, 0);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                mainCamera.MoveCamera(-1, 0);
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Add))
            {
                //Zoom in
                mainCamera.ZoomCamera(1);
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Subtract))
            {
                //Zoom out
                mainCamera.ZoomCamera(-1);
            }

        }
    }
}

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
        bool upKeyPressed = false;
        bool rightKeyPressed = false;
        bool leftKeyPressed = false;
        bool downKeyPressed = false;

        bool upKeyPreviousFrameState;
        bool leftKeyPreviousFrameState;
        bool rightKeyPreviousFrameState;
        bool downKeyPreviousFrameState;


        public void Update(Character thePlayer, Camera mainCamera)
        {
            PollKeyboard();

            //Player controls
            if (rightKeyPressed)
            {
                thePlayer.Move(1, 0);
            }

            if (leftKeyPressed)
            {
                thePlayer.Move(-1, 0);
            }
            else if (downKeyPressed)
            {
                thePlayer.Move(0, 1);
            }
            else if (upKeyPressed)
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

            upKeyPreviousFrameState = Keyboard.IsKeyPressed(Keyboard.Key.W);
            rightKeyPreviousFrameState = Keyboard.IsKeyPressed(Keyboard.Key.D);
            leftKeyPreviousFrameState = Keyboard.IsKeyPressed(Keyboard.Key.A);
            downKeyPreviousFrameState = Keyboard.IsKeyPressed(Keyboard.Key.S);
        }

        void PollKeyboard()
        {
            
            if(upKeyPreviousFrameState == false && Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                upKeyPressed = true;
            }
            else
            {
                upKeyPressed = false;
            }

            if (leftKeyPreviousFrameState == false && Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                leftKeyPressed = true;
            }
            else
            {
                leftKeyPressed = false;
            }

            if (rightKeyPreviousFrameState == false && Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                rightKeyPressed = true;
            }
            else
            {
                rightKeyPressed = false;
            }

            if (downKeyPreviousFrameState == false && Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                downKeyPressed = true;
            }
            else
            {
                downKeyPressed = false;
            }
        }
    }
}

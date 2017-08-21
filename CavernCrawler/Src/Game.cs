using System;
using System.Collections.Generic;
using System.Text;
using SFML;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;


namespace CavernCrawler
{
    enum GameState
    {
        MAIN_MENU,
        MAP_IN_PROGRESS,
        MAP_LOSE,
        MAP_WIN
    }

    class Game
    {
        Map theMap;
        Camera mainCamera;

        public void Run()
        {
            Init();

            mainCamera.GetWindow().SetActive();

            while (mainCamera.GetWindow().IsOpen)
            {
                Update();
                Draw();
            }
        }

        public void Init()
        {
            theMap = new Map(70, 50);
            mainCamera = new Camera(1280, 960, 12.5f, 0.75f, 0.75f);
        }

        public void Update()
        {
            mainCamera.Update();
            Input();
        }

        public void Draw()
        {
            theMap.DrawMap(mainCamera.GetWindow());
            mainCamera.Display();
        }

        public void Input()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                mainCamera.MoveCamera(1, 0);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                mainCamera.MoveCamera(-1, 0);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                mainCamera.MoveCamera(0, 1);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                mainCamera.MoveCamera(0, -1);
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

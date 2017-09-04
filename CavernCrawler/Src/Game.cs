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
        InputManager inputManager;

        Time deltaTime;
        Clock deltaClock;

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
            deltaClock = new Clock();
            inputManager = new InputManager();

            mainCamera.SetCameraPosition(theMap.player.xPos, theMap.player.yPos);
            mainCamera.SetCameraTarget(theMap.player);
        }

        public void Update()
        {
            deltaTime = deltaClock.Restart();
            inputManager.Update(theMap.player, mainCamera);
            mainCamera.Update();
        }

        public void Draw()
        {
            theMap.DrawMap(mainCamera.GetWindow());
            mainCamera.Display();
        }
    }
}

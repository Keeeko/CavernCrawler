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
        CharacterManager characterManager;
        GlobalResource globalResource;

        Time deltaTime;
        Clock deltaClock;

        public void Run()
        {
            Init();

            globalResource.GetWindow().SetActive();

            while (globalResource.GetWindow().IsOpen)
            {
                Update();
                Draw();
            }
        }

        public void Init()
        {
            globalResource = new GlobalResource();
            characterManager = new CharacterManager(theMap);
            mainCamera = new Camera(1440, 1080, 12.5f, 0.75f, 0.45f, globalResource);
            theMap = new Map(70, 50, characterManager, globalResource);

            deltaClock = new Clock();
            inputManager = new InputManager();
            
            mainCamera.SetCameraPosition(theMap.player.xPos, theMap.player.yPos);
            mainCamera.SetCameraTarget(theMap.player);
        }

        public void Update()
        {
            deltaTime = deltaClock.Restart();
            inputManager.Update(theMap.player, mainCamera);
            characterManager.Update();
            mainCamera.Update();
        }

        public void Draw()
        {
            globalResource.GetWindow().Clear();
            theMap.DrawMap(globalResource.GetWindow());

            mainCamera.DrawGUI();

            //Draw everything to screen
            globalResource.GetWindow().Display();

        }
    }
}

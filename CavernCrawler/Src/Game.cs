﻿using System;
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
        AudioManager audioManager;
        GlobalResource globalResource;

        ItemPreviewPanel test;

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
            audioManager = new AudioManager(globalResource);
            mainCamera = new Camera(1440, 1080, 12.5f, 0.75f, 0.45f, globalResource);
            theMap = new Map(70, 50, characterManager, globalResource);
            globalResource.GetPlayer().inventory.AddItemToContainer(ItemGenerator.GenerateItem());
            globalResource.GetPlayer().Equip(globalResource.GetPlayer().inventory.GetContents()[0]);
            deltaClock = new Clock();
            inputManager = new InputManager(globalResource);
            
            mainCamera.SetCameraPosition(theMap.player.xPos, theMap.player.yPos);
            mainCamera.SetCameraTarget(theMap.player);


            test = new ItemPreviewPanel(globalResource.GetPlayer().inventory.GetContents()[0], new Vector2f(252.0f, 351.0f), new Vector2f(504.0f, 702.0f));

            //Temporary
            //audioManager.PlayTrack();
        }

        public void Update()
        {
            deltaTime = deltaClock.Restart();
            inputManager.Update(theMap.player, mainCamera);
            theMap.Update();
            characterManager.Update();
            audioManager.Update();
            mainCamera.Update();
        }

        public void Draw()
        {
            globalResource.GetWindow().Clear();

            theMap.DrawMap();

            mainCamera.DrawGUI();

            test.Draw(globalResource);

            //Draw everything to screen
            globalResource.GetWindow().Display();

        }
    }
}

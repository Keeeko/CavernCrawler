﻿using System;
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
    class GlobalResource
    {
        //Root level loading
        const string contentFolder = @"\Content";
        const string texturesFolder = @"\Textures";

        //Dungeon Tileset
        const string tilesetFolder = @"\TX_Dungeon";
        const string wallTilesFolder = @"\wall";
        const string floorTilesFolder = @"\floor";
        const string waterTilesFolder = @"\water";

        const string guiGraphicsFolder = @"\GUI";

        string[] tilePaths { get; set; }

        //Reference to the mainView
        public View mainView;

        //Reference to the renderWindow
        public RenderWindow window;

        //Reference to the AudioManager
        private AudioManager audioManager;
        private InputManager inputManager;

        //Reference to the console
        private EventConsole eventConsole;
        private GUIPanel guiPanel;

        private Map map;
        private Character player;

        public GlobalResource()
        {

        }

        public View GetMainView()
        {
            return mainView;
        }

        public void SetMainView(View mainViewRef)
        {
            mainView = mainViewRef;
        }

        public Character GetPlayer()
        {
            return player;
        }

        public void SetPlayer(Character value)
        {
            player = value;
        }

        public InputManager GetInputManager()
        {
            return inputManager;
        }

        public void SetInputManager(InputManager value)
        {
            inputManager = value;
        }

        public AudioManager GetAudioManager()
        {
            return audioManager;
        }

        public void SetAudioManager(AudioManager value)
        {
            audioManager = value;
        }

        public RenderWindow GetWindow()
        {
            return window;
        }

        public void SetWindow(RenderWindow windowReference)
        {
            window = windowReference;
        }

        public EventConsole GeteventConsole()
        {
            return eventConsole;
        }

        public void SetEventConsole(EventConsole value)
        {
            eventConsole = value;
        }

        public GUIPanel GetGUIPanel()
        {
            return guiPanel;
        }

        public void SetGUIPanel(GUIPanel value)
        {
            guiPanel = value;
        }
        public Map GetCurrentMap()
        {
            return map;
        }

        public void SetCurrentMap(Map currentMap)
        {
            map = currentMap;
        }
    }
}

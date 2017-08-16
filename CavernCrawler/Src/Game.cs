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
    enum GameState
    {
        MAIN_MENU,
        MAP_IN_PROGRESS,
        MAP_LOSE,
        MAP_WIN
    }

    class Game
    {

        RenderWindow window;
        View main_view;
        Map theMap;

        float windowMoveSpeed;
        float zoomSpeed;
        float currentZoomFactor;

        public void Run()
        {
            Init();

            window.SetActive();
            while (window.IsOpen)
            {
                window.Clear();
                window.DispatchEvents();
                Update();
                Draw();

            }
        }

        public void Init()
        {
            window = new RenderWindow(new VideoMode(1280, 960), "Cavern Crawler");
            main_view = new View(new Vector2f(200, 200), new Vector2f(1280, 960));

            windowMoveSpeed = 20.0f;
            currentZoomFactor = 0.75f;
            zoomSpeed = 1.0f;

            main_view.Zoom(currentZoomFactor);
            window.SetView(main_view);
            theMap = new Map(70, 50);


        }

        public void Update()
        {
            Input();
        }

        public void Draw()
        {
            theMap.DrawMap(window);
            window.SetView(main_view);
            window.Display();
        }

        public void Input()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                main_view.Move(new Vector2f(windowMoveSpeed, 0.0f));
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                main_view.Move(new Vector2f(-windowMoveSpeed, 0.0f));
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                main_view.Move(new Vector2f(0.0f, windowMoveSpeed));
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                main_view.Move(new Vector2f(0.0f, -windowMoveSpeed));
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Add))
            {
                float newX = main_view.Size.X - zoomSpeed;
                float newY = newX / 1.333333333333333f; //4:3 aspect ratio
                main_view.Size = new Vector2f(newX, newY);
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Subtract))
            {
                float newX = main_view.Size.X + zoomSpeed;
                float newY = newX / 1.333333333333333f; //4:3 aspect ratio
                main_view.Size = new Vector2f(newX, newY);
            }

        }
    }
}

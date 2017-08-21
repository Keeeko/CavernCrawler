using System;
using System.Collections.Generic;
using SFML;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;

namespace CavernCrawler
{
    class Camera
    {
        uint horizontalResolution;
        uint verticalResolution;

        float aspectRatio;
        float windowMoveSpeed;
        float zoomSpeed;
        float currentZoomFactor;

        private RenderWindow window;
        private View mainview;

        //public float WindowMoveSpeed { get; set; }
        public float ZoomSpeed { get; set; }
        //public RenderWindow Window { get; set; }
        //public View MainView { get; set; }

        public Camera(uint horResolution, uint verResolution, float windowSpeed, float windowZoomSpeed, float initialZoomFactor)
        {
            horizontalResolution = horResolution;
            verticalResolution = verResolution;
            aspectRatio = (float)horizontalResolution / verticalResolution;

            windowMoveSpeed = windowSpeed;
            currentZoomFactor = initialZoomFactor;
            zoomSpeed = windowMoveSpeed;

            window = new RenderWindow(new VideoMode(horizontalResolution, verticalResolution), "Cavern Crawler");
            mainview = new View(new Vector2f(200, 200), new Vector2f(horizontalResolution, verticalResolution));
            mainview.Zoom(currentZoomFactor);
            window.SetView(mainview);
        }

        public void Update()
        {
            window.Clear();
            window.DispatchEvents();
        }

        public void Display()
        {
            window.SetView(mainview);
            window.Display();
        }
        
        public RenderWindow GetWindow()
        {
            return window;
        }

        public View GetView()
        {
            return mainview;
        }

        public void ZoomCamera(int direction)
        {
            float newX = mainview.Size.X  - Math.Sign(direction) * zoomSpeed;
            float newY = newX / aspectRatio; 
            mainview.Size = new Vector2f(newX, newY);
        }

        public void MoveCamera(int xDirection, int yDirection)
        {
            mainview.Move(new Vector2f(windowMoveSpeed * Math.Sign(xDirection), windowMoveSpeed * Math.Sign(yDirection)));

        }
    }
}

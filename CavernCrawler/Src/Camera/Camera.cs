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
        private View fixedView;
        private View consoleView;

        public Character target;

        Texture guiBackPanel;
        Texture consoleBackPanel;
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

            mainview = new View(new Vector2f(0.0f, 0.0f), new Vector2f(horizontalResolution, verticalResolution));
            //Set center to be equal to half the GUI graphics dimensions, the full size being the actual dimensions of the image
            fixedView = new View(new Vector2f(216.0f, 540.0f), new Vector2f(432.0f, verticalResolution));

            consoleView  = new View(new Vector2f(504.0f, 162.0f), new Vector2f(horizontalResolution * 0.7f, 324.0f));
            mainview.Zoom(currentZoomFactor);

            mainview.Viewport = new FloatRect(0.0f, 0.0f, 0.7f, 0.7f);


            guiBackPanel = new Texture(@"Content\Textures\Tx_GUI\panel_background.png");
            consoleBackPanel = new Texture(@"Content\Textures\Tx_GUI\panel_console.png");

            window.SetView(mainview);
        }

        public void Update()
        {
            window.Clear();
            window.DispatchEvents();

            if(target != null)
            {
                SetCameraPosition(target.xPos, target.yPos);
            }

        }

        public void DrawGUI()
        {
            fixedView.Viewport = new FloatRect(0.7f, 0.0f, 0.3f, 1f);
            window.SetView(fixedView);

            Sprite tempSprite = new Sprite(guiBackPanel);
            tempSprite.Position = new Vector2f(0.0f, 0.0f);
            window.Draw(tempSprite);

            consoleView.Viewport = new FloatRect(0.0f, 0.7f, 0.7f, 0.3f);
            window.SetView(consoleView);

            Sprite tempSprite2 = new Sprite(consoleBackPanel);
            tempSprite2.Position = new Vector2f(0.0f, 0.0f);
            window.Draw(tempSprite2);


            window.SetView(mainview);


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

        public void SetCameraPosition(int xPos, int yPos)
        {
            mainview.Center = new Vector2f(xPos * 32.0f, yPos * 32.0f);
        }

        public void SetCameraTarget(Character targetChar)
        {
            target = targetChar;
        }
    }
}

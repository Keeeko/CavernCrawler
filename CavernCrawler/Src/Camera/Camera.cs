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
        private View guiView;

        public Character target;
        public EventConsole eventConsole;

        GlobalResource globalResource;

        Texture guiBackPanel;
        Texture consoleBackPanel;
        //public float WindowMoveSpeed { get; set; }
        public float ZoomSpeed { get; set; }
        //public RenderWindow Window { get; set; }
        //public View MainView { get; set; }

        public Camera(uint horResolution, uint verResolution, float windowSpeed, float windowZoomSpeed, float initialZoomFactor, GlobalResource globalResourceReference)
        {
            horizontalResolution = horResolution;
            verticalResolution = verResolution;
            aspectRatio = (float)horizontalResolution / verticalResolution;

            windowMoveSpeed = windowSpeed;
            currentZoomFactor = initialZoomFactor;
            zoomSpeed = windowMoveSpeed;


            window = new RenderWindow(new VideoMode(horizontalResolution, verticalResolution), "Cavern Crawler");

            mainview = new View(new Vector2f(0.0f, 0.0f), new Vector2f(horizontalResolution, verticalResolution));

            globalResource = globalResourceReference;
            globalResource.mainView = mainview;
            globalResource.window = window;

            //Set center to be equal to half the GUI graphics dimensions, the full size being the actual dimensions of the image
            guiView = new View(new Vector2f(216.0f, 540.0f), new Vector2f(432.0f, verticalResolution));

            mainview.Zoom(currentZoomFactor);

            //To DO: Replace the magic numbers with variables
            mainview.Viewport = new FloatRect(0.0f, 0.0f, 0.7f, 0.7f);
            eventConsole = new EventConsole(Color.White, 22, new Vector2f((horizontalResolution * 0.7f) / 2, (verticalResolution * 0.3f) / 2), new Vector2f(horizontalResolution * 0.7f, verticalResolution * 0.3f), globalResource);


            guiBackPanel = new Texture(@"Content\Textures\Tx_GUI\panel_background.png");
            consoleBackPanel = new Texture(@"Content\Textures\Tx_GUI\panel_console.png");


            eventConsole.AddTextToConsole("Dragon attacks your mother for all dat ass!");
            
            eventConsole.AddTextToConsole("Player has picked up the sword of a thousand truths!");
            eventConsole.AddTextToConsole("You hear strange noises coming from beyond the veil! You hear strange noises coming from beyond the veil!");
            eventConsole.AddTextToConsole("This is a test of the console system, fuck the system!");
            eventConsole.AddTextToConsole("YYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY");

            window.SetView(mainview);
        }

        public void Update()
        {
            window.DispatchEvents();

            if(target != null)
            {
                SetCameraPosition(target.xPos, target.yPos);
            }

        }

        public void DrawGUI()
        {
            guiView.Viewport = new FloatRect(0.7f, 0.0f, 0.3f, 1f);
            window.SetView(guiView);

            Sprite tempSprite = new Sprite(guiBackPanel);
            tempSprite.Position = new Vector2f(0.0f, 0.0f);
            window.Draw(tempSprite);

            eventConsole.DrawConsole();

            window.SetView(mainview);


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

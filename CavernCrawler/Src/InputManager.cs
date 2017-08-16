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

        float windowMoveSpeed;
        float zoomSpeed;
        float currentZoomFactor;

        public InputManager()
        {
            windowMoveSpeed = 20.0f;
            currentZoomFactor = 0.75f;
            zoomSpeed = 1.0f;

        }

        // TODO: I think a command pattern would be best used here, to do this, encapsulate actions that can be performed into seperate objects, these can then be assigned to keys on the fly.
        public void CheckInput(View main_view)
        {

        }
    }
}

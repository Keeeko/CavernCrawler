using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CavernCrawler
{
    class Room
    {
        public int centerX;
        public int centerY;
        public int originX;
        public int originY;

        public int width;
        public int height;

        int[,] tiles;

        public Room(int xPos, int yPos, int roomWidth, int roomHeight, int tileType)
        {
            originX = xPos;
            originY = yPos;
            width = roomWidth;
            height = roomHeight;

            centerX = roomWidth / 2;
            centerY = roomHeight / 2;

            FillRoom(tileType);
 
        }

        //Requires a function to retrieve the global tile coords from local room coords

        void FillRoom(int tileType)
        {
            tiles = new int[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tiles[x, y] = tileType;
                }
            }
        }

        public int GetRoomTile(int xLocalPos, int yLocalPos)
        {
            return tiles[xLocalPos, yLocalPos];
        }

        public void SetRoomTile(Map theMap, int xLocalPos, int yLocalPos, int tileType)
        {
           tiles[xLocalPos, yLocalPos] = tileType;
            theMap.SetMapTile(originX + xLocalPos, originY + yLocalPos, tileType);
           
        }

    }
}

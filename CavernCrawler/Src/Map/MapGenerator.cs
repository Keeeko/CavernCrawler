using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CavernCrawler
{
    class MapGenerator
    {
        int minRoomWidth;
        int minRoomHeight;

        int maxRoomWidth;
        int maxRoomHeight;

        int minTotalMonsters;
        int minTotalMonstersPerRoom;
        int maximumTotalMonsters;
        int maxMonstersPerRoom;

        int maxRoomPlacementTries;

        Map theMap;

        public MapGenerator(int minimumRoomSizeX, int minimumRoomSizeY, int maximumRoomSizeX, 
            int maximumRoomSizeY, int maximumRoomPlacementTries)
        {
            minRoomWidth = minimumRoomSizeX;
            minRoomHeight = minimumRoomSizeY;

            maxRoomWidth = maximumRoomSizeX;
            maxRoomHeight = maximumRoomSizeX;
            maxRoomPlacementTries = maximumRoomPlacementTries;
        }

        public MapGenerator()
        {
            minRoomHeight = 3;
            minRoomWidth = 3;

            minTotalMonsters = 6;
            minTotalMonstersPerRoom = 0;

            maxMonstersPerRoom = 4;
            maximumTotalMonsters = 20;

            maxRoomWidth = 9;
            maxRoomHeight = 9;
            maxRoomPlacementTries = 30;
        }

        public void GenerateDungeon(Map map)
        {
            theMap = map;
            //Seed the random number generator with the date and time
            Random randomNum = new Random(DateTime.Now.Millisecond);

            //Place rooms over and over until we reach the predetermined maximum number of attempts
            for (int i = 0; i < maxRoomPlacementTries; i++)
            {
                //Store random values for room dimenons, ensure they never go outwidth the bounds of the map
                int originX = randomNum.Next(1, (map.mapSizeX - maxRoomWidth) - 1);
                int originY = randomNum.Next(1, (map.mapSizeY - maxRoomHeight) - 1);
                int width = randomNum.Next(minRoomWidth, maxRoomWidth);
                int height = randomNum.Next(minRoomHeight, maxRoomHeight);
                
                //Check if the desired room intersects with any existing rooms, if it does; skip it and start process again
                if(CheckForIntersection(originX, originY, width + 1, height + 1, map) == false)
                {
                    map.CreateRoom(originX, originY, width, height, 0);
                }

            }

            for(int i = 0; i < theMap.rooms.Count - 1; i++)
            {
                Console.WriteLine("Attempting to connect two rooms");
                JoinRooms(theMap.rooms[i], theMap.rooms[i + 1]);
            }
            
            //Place monsters
            for(int i = 0; i < maximumTotalMonsters; i ++)
            {
                Room choosenRoom = theMap.rooms[randomNum.Next(0, theMap.rooms.Count - 1)];
                theMap.PlaceMonster(randomNum.Next(choosenRoom.originX, choosenRoom.originX + choosenRoom.width),
                    randomNum.Next(choosenRoom.originY, choosenRoom.originY + choosenRoom.height));
            }
            //Plant the gateway
            theMap.rooms.Last<Room>().SetRoomTile(theMap,2, 2, 2);
        }
    
        public void JoinRooms(Room room1, Room room2)
        {
            int horizontalLength =  (room2.originX + room2.centerX) - (room1.originX + room1.centerX);
            int verticalLength = (room2.originY + room2.centerY) - (room1.originY + room1.centerY);

            theMap.CarveCorridor((room1.originX + room1.centerX), room1.originY + room1.centerY, verticalLength, true);
            theMap.CarveCorridor(room1.originX + room1.centerX, (room1.originY + room1.centerY) + verticalLength, horizontalLength + 1, false);

        }

        public bool CheckForIntersection(int originX, int originY, int width, int height, Map map)
        {
            for(int x = originX; x <= originX + width; x ++)
            {
                for (int y = originY; y <= originY + height; y++)
                {
                    if(map.GetMapTile(x, y) == 0)
                    {
                        Console.WriteLine("Intersection detected at: " + x + " and: " + y);
                        return true;
                    }
                }
            }

            //No intersections found, return false
            return false;
        }
    }
}
    

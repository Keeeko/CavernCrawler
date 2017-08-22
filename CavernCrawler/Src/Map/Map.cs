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
    class Map
    {
        public struct MapCoordinates
        {
            public int x;
            public int y;

            public MapCoordinates(int xVal, int yVal)
            {
                x = xVal;
                y = yVal;
            }

        };

        const float TILE_SIZE = 32.0f;

        public int mapSizeX;
        public int mapSizeY;

        //Variable decleration is in order of screen draw order
        public int[,] backgroundtiles;
        public Dictionary<MapCoordinates, List<Item>> itemMap;
        public Dictionary<MapCoordinates, Character> characterMap; //Stores position of characters on map
        public bool[,] fogOfWarTiles; //Determines of a square is covered by fogOfWar

        Dictionary<int, Texture> mapTileGraphics;
        public List<Room> rooms;

        public Character player;

        public Map(int sizeX, int sizeY)
        {
            mapSizeX = sizeX;
            mapSizeY = sizeY;

            backgroundtiles = new int[mapSizeX, mapSizeY];

            for(int x = 0; x < mapSizeX; x++)
            {
                for(int y = 0; y < mapSizeY; y++)
                {
                        //Fill the dungeons with walls
                        backgroundtiles[x, y] = 1 ;
                }
            }

            mapTileGraphics = new Dictionary<int, Texture>();
            itemMap = new Dictionary<MapCoordinates, List<Item>>();
            characterMap = new Dictionary<MapCoordinates, Character>();

            rooms = new List<Room>();
            MapGenerator mapGen = new MapGenerator();
            mapGen.GenerateDungeon(this);


            LoadMapResources();
            PlaceCharacters();
        }

        //Hardcoded for the time being, will clean up later
        public void PlaceCharacters()
        {
            player = new Character();
            player.xPos = rooms[0].originX;
            player.yPos = rooms[0].originY;
            player.graphicsID = 99;
           characterMap.Add(new MapCoordinates(player.xPos, player.yPos), player);

            PlaceMonster(rooms[0].originX + 2, rooms[0].originY + 2);
        }

        public void PlaceMonster(int xPos, int yPos)
        {
            if(characterMap.ContainsKey(new MapCoordinates(xPos, yPos)))
            {
                Console.WriteLine("Can't place character at: " + xPos + ", " + yPos);
            }
            else
            {
                Character monster = new Character();
                monster.graphicsID = 91;
                monster.MoveTo(xPos, yPos);
                characterMap.Add(new MapCoordinates(xPos, yPos), monster);
                

            }
        }

        public void LoadMapResources()
        {
            /* Dungeon tiles: 1 - 20, Item graphics: 21 - 50, Enemies: 51 - 75, Player base graphic: 99 */

            //Floor texutres
            mapTileGraphics[0] = new Texture(@"Content\Textures\Tx_Dungeon\floor\cobble_blood1.png");

            //Wall textures
            mapTileGraphics[1] = new Texture(@"Content\Textures\Tx_Dungeon\wall\brick_dark0.png");

            //Stairway
            mapTileGraphics[2] = new Texture(@"Content\Textures\Tx_Dungeon\gateways\stone_stairs_down.png");

            //Chracter textures
            mapTileGraphics[91] = new Texture(@"Content\Textures\Tx_Monster\dragon.png");
            mapTileGraphics[99] = new Texture(@"Content\Textures\Tx_Player\base\human_m.png");



        }

        public void DrawMap(RenderWindow window)
        {
            
            for (int x = 0; x < mapSizeX; x++)
            {
                for (int y = 0; y < mapSizeY; y++)
                {
                    //Draw map tiles
                        //retrieve the correct texture for displaying by using the tile value of the current square as a key in a texture dictionary
                        Sprite tempSprite = new Sprite(mapTileGraphics[backgroundtiles[x,y]]);
                        tempSprite.Position = new Vector2f(x *  TILE_SIZE, y * TILE_SIZE);
                        window.Draw(tempSprite);

                    //Draw items

                    //Draw characters
                    if (characterMap.ContainsKey(new MapCoordinates(x, y)))
                    {
                        //If there is a character at this key on the dictionary (by using their map coordinates as a key) then retrieve their
                        //characters graphicsID and use this to display the texture from the texture dictionary
                        Sprite tempSpriteC = new Sprite(mapTileGraphics[characterMap[new MapCoordinates(x, y)].graphicsID]);
                        tempSpriteC.Position = new Vector2f(x * TILE_SIZE, y * TILE_SIZE);
                        window.Draw(tempSpriteC);
                    }
                }
            }
        }

        public int GetMapTile(int xPos, int yPos)
        {
            return backgroundtiles[xPos, yPos];
        }
        
        public void SetMapTile(int xPos, int yPos, int tileNum)
        {
            backgroundtiles[xPos, yPos] = tileNum;
        }

        /*This will be used to find what types of tiles are surronding a certain spot
        public [,] getSurrondingTiles(int xPos, int yPos)
        {

        }
        */

        public void CreateRoom(int originX, int originY, int width, int height, int tileType)
        {
            Room newRoom = new Room(originX, originY, width, height, tileType);
            rooms.Add(newRoom);
            Console.WriteLine("Creating Room at: " + newRoom.originX + ", " + newRoom.originY);

            for(int x = originX; x <= originX + width; x ++)
            {
                for (int y = originY; y <= originY + height; y++)
                {
                    SetMapTile(x, y, tileType);
                }
            }
        }

        public void CarveCorridor(int originX, int originY, int corridoorLength, bool vertical)
        {
            if(vertical)
            {
                //verticalCarve
                for(int y = 0; y < Math.Abs(corridoorLength); y ++)
                {
                    int newPosY = originY + (Math.Sign(corridoorLength) * y);
                    SetMapTile(originX, newPosY, 0);
                }
            }
            else
            {
                //Horizontal carve
                for (int x = 0; x < Math.Abs(corridoorLength); x++)
                {
                    int newPosX = originX + (Math.Sign(corridoorLength) * x);
                    SetMapTile(newPosX, originY, 0);
                }
            }
        }

    }
}

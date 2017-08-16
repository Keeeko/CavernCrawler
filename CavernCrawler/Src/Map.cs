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
        const float TILE_SIZE = 32.0f;

        public int mapSizeX;
        public int mapSizeY;

        //Graphics 
        Texture floorTexture;
        Texture wallTexture;
        Texture playerTexture;
        Texture stairsTexture;

        //Variable decleration is in order of screen draw order
        public int[,] backgroundtiles;
        public Dictionary<Vector2f, List<Item>> itemMap;
        public Dictionary<Vector2f, Character> characterMap; //Stores position of characters on map
        public bool[,] fogOfWarTiles; //Determines of a square is covered by fogOfWar

        public List<Room> rooms;

        Character player;

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

            rooms = new List<Room>();
            MapGenerator mapGen = new MapGenerator();

            mapGen.GenerateDungeon(this);

            player = new Character();
            player.position = new Vector2f(rooms[0].originX, rooms[0].originY);
            

            LoadMapResources();
        }

        public void LoadMapResources()
        {
            wallTexture = new Texture(@"Content\Textures\Tx_Dungeon\wall\brick_dark0.png");
            floorTexture = new Texture(@"Content\Textures\Tx_Dungeon\floor\cobble_blood1.png");
            playerTexture = new Texture(@"Content\Textures\Tx_Player\base\human_m.png");
            stairsTexture = new Texture(@"Content\Textures\Tx_Dungeon\gateways\stone_stairs_down.png");
        }

        public void DrawMap(RenderWindow window)
        {
            for (int x = 0; x < mapSizeX; x++)
            {
                for (int y = 0; y < mapSizeY; y++)
                {
                    if (backgroundtiles[x, y] == 0)
                    {
                        Sprite tempSprite1 = new Sprite(floorTexture);
                        tempSprite1.Position = new Vector2f(x *  TILE_SIZE, y * TILE_SIZE);
                        window.Draw(tempSprite1);

                    }

                    if (backgroundtiles[x, y] == 1)
                    {
                        Sprite tempSprite2 = new Sprite(wallTexture);
                        tempSprite2.Position = new Vector2f(x * TILE_SIZE, y * TILE_SIZE);
                        window.Draw(tempSprite2);

                    }

                    if (backgroundtiles[x, y] == 2)
                    {
                        Sprite tempSprite3 = new Sprite(stairsTexture);
                        tempSprite3.Position = new Vector2f(x * TILE_SIZE, y * TILE_SIZE);
                        window.Draw(tempSprite3);

                    }
                }
            }

            //Draw characters
            Sprite tempSprite = new Sprite(playerTexture);
            tempSprite.Position = new Vector2f( player.position.X * TILE_SIZE, player.position.Y * TILE_SIZE);
            window.Draw(tempSprite);
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

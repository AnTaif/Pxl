using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public class Map
    {
        private readonly Dictionary<int, List<Level>> levelsByFloors =
            new Dictionary<int, List<Level>>();

        public Level CurrentLevel { get; private set; }

        public Map()
        {
            levelsByFloors.Add(0, new List<Level>() { new Level(0, 0, (960, 540), new List<Tile>
            {
                new Tile(new Rectangle(0, 384, 48, 128), TileType.Ground), // Wall
                new Tile(new Rectangle(0, 496, 1000, 96), TileType.Ground), // Ground
                new Tile(new Rectangle(320, 368, 128, 16), TileType.Platform), // Platform
                new Tile(new Rectangle(544, 320, 128, 16), TileType.Platform), // Platform
                new Tile(new Rectangle(928, 336, 128, 16), TileType.Platform)
                //new Tile(new Rectangle(832, 288, 128, 16), TileType.Platform), // Platform
                //new Tile(new Rectangle(880, 288, 80, 224), TileType.Ground), // Wall
            }) });
            levelsByFloors[0].Add(new Level(0, 1, (928, 512), new List<Tile>
            {
                new Tile(new Rectangle(0, 496, 928, 96), TileType.Ground), // Ground
                //new Tile(new Rectangle(160, 352, 128, 16), TileType.Platform), // Platform
                //new Tile(new Rectangle(0, 288, 128, 16), TileType.Platform), // Platform
                new Tile(new Rectangle(880, 384, 48, 128), TileType.Ground), // Wall
            }));
            CurrentLevel = levelsByFloors[0][0];
        }

        public void SetNextLevel()
        {
            var floor = CurrentLevel.Floor;
            var id = CurrentLevel.Id;

            if (levelsByFloors[floor].Count > id + 1)
            {
                id++;
            }
            else if (levelsByFloors.ContainsKey(floor + 1))
            {
                floor++;
                id = 0;
            }
            else
            {
                floor = 0;
                id = 0;
            }

            SetLevel(floor, id);
        }

        public void SetLevel(int currentFloor, int currentId)
        {
            if (levelsByFloors.ContainsKey(currentFloor) && levelsByFloors[currentFloor].Count > 0)
                CurrentLevel = levelsByFloors[currentFloor][currentId];
            else
                throw new ArgumentException($"Не существует уровня на {currentFloor} этаже с {currentId} id");
        }
    }
}

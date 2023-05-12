using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public class LevelManager
    {
        private readonly Dictionary<int, List<Level>> levelsByFloors =
            new Dictionary<int, List<Level>>();

        public Level CurrentLevel { get; private set; }

        public LevelManager()
        {
            levelsByFloors.Add(0, new List<Level>() { new Level(0, 0, (960, 540), new List<Tile>
            {
                new Tile(new Rectangle(0, 496, 1000, 96), TileType.Ground), // Ground
                new Tile(new Rectangle(336, 432, 160, 16), TileType.Platform), // Platform
                new Tile(new Rectangle(576, 368, 160, 32), TileType.Platform), // Platform
                new Tile(new Rectangle(0, 448, 48, 64), TileType.Ground), // Wall
                new Tile(new Rectangle(896, 384, 64, 128), TileType.Ground), // Wall
            }, new Vector2(10, 350)) });
            levelsByFloors[0].Add(new Level(0, 1, (960, 540), new List<Tile>
            {
                new Tile(new Rectangle(0, 512, 960, 96), TileType.Ground), // Ground
                new Tile(new Rectangle(0, 448, 250, 96), TileType.Ground), // Ground
                new Tile(new Rectangle(368, 432, 176, 16), TileType.Platform), // Platform
                new Tile(new Rectangle(250, 496, 454, 16), TileType.Spikes), // Spikes
                new Tile(new Rectangle(704, 448, 900, 96), TileType.Ground), // Ground
                 new Tile(new Rectangle(0, 384, 48, 128), TileType.Ground), // Wall
            }, new Vector2(10, 350)));
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

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
            // TODO: Load map data from csv file(???)
            // levelsByFloors = ...

            // TEMPORARY
            levelsByFloors.Add(0, new List<Level>() { new Level(0, 0) });
            levelsByFloors[0].Add(new Level(0, 1));
            CurrentLevel = levelsByFloors[0][0];
        }

        public void SetNextLevel()
        {
            var floor = CurrentLevel.Floor;
            var id = CurrentLevel.Id;

            if (levelsByFloors[floor].Count > id+1)
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

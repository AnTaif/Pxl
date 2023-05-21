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
            levelsByFloors.Add(0, new List<Level>() { new Level(0, 0, (960, 540), new List<GameObject>
            {
                new GameObject(new Rectangle(0, 496, 1000, 96), ObjectType.Ground), // Ground
                new GameObject(new Rectangle(336, 432, 160, 16), ObjectType.Platform), // Platform
                new GameObject(new Rectangle(576, 368, 160, 32), ObjectType.Platform), // Platform
                new GameObject(new Rectangle(0, 448, 48, 64), ObjectType.Ground), // Wall
               // new GameObject(new Rectangle(896, 384, 64, 128), ObjectType.Ground), // Wall
                new GameObject(new Rectangle(896, 384, 64, 128), ObjectType.Ground), // Wall
            }, new Vector2(10, 349)) });

            levelsByFloors[0].Add(new Level(0, 1, (960, 540), new List<GameObject>
            {
                new GameObject(new Rectangle(0, 512, 960, 96), ObjectType.Ground), // Ground
                new GameObject(new Rectangle(0, 448, 250, 96), ObjectType.Ground), // Ground
                new GameObject(new Rectangle(368, 432, 176, 16), ObjectType.Platform), // Platform
                new GameObject(new Rectangle(250, 496, 454, 16), ObjectType.Spikes), // Spikes
                new GameObject(new Rectangle(704, 448, 900, 96), ObjectType.Ground), // Ground
                 new GameObject(new Rectangle(0, 384, 48, 128), ObjectType.Ground), // Wall
            }, new Vector2(10, 349)));

            levelsByFloors[0].Add(new Level(0, 2, (960, 540), new List<GameObject>
            {
                new GameObject(new Rectangle(0, 448, 960, 96), ObjectType.Ground), // Ground
                new GameObject(new Rectangle(368, 0, 16, 400), ObjectType.Platform), // Wall
                new GameObject(new Rectangle(576, 272, 16, 176), ObjectType.Platform), // Wall
                new GameObject(new Rectangle(528, 384, 48, 16), ObjectType.Platform), // Platform
                new GameObject(new Rectangle(384, 320, 48, 16), ObjectType.Platform), // Platform
                new GameObject(new Rectangle(576, 0, 16, 144), ObjectType.Platform), // Wall
                new GameObject(new Rectangle(528, 272, 192, 16), ObjectType.Platform), // Platform
            }, new Vector2(10, 349)));
            CurrentLevel = levelsByFloors[0][0];
        }

        public void SetNextLevel()
        {
            var stage = CurrentLevel.Stage;
            var id = CurrentLevel.Id;

            if (levelsByFloors[stage].Count > id + 1)
            {
                id++;
            }
            else if (levelsByFloors.ContainsKey(stage + 1))
            {
                stage++;
                id = 0;
            }
            else
            {
                stage = 0;
                id = 0;
            }

            SetLevel(stage, id);
        }

        public void SetLevel(int currentStage, int currentId)
        {
            if (levelsByFloors.ContainsKey(currentStage) && levelsByFloors[currentStage].Count > 0)
                CurrentLevel = levelsByFloors[currentStage][currentId];
            else
                throw new ArgumentException($"Не существует уровня на {currentStage} этаже с {currentId} id");
        }
    }
}

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace Pxl
{
    public static class LevelManager
    {
        private static Dictionary<int, List<Level>> levelsByStage;
        public static TileSet TileSet { get; private set; }

        public static Level CurrentLevel { get; private set; }

        public static void LoadMapFromFile(string mapName)
        {
            var rootDirectory = MainGame.RootDirectory;
            var path = Path.Combine(rootDirectory, $"Model\\Levels\\Maps\\{mapName}");

            LoadTileSet(path, Directory.GetFiles(path).Where(file => file.Contains("tileset.json")).First());

            LoadLevels(path);

            SetLevel(0, 0);
        }

        public static void LoadLevels(string path)
        {
            levelsByStage = new Dictionary<int, List<Level>>();

            var stages = Directory.GetFiles(path)
                .Where(file => file.Contains("stage"))
                .ToList();

            for (int stage = 0; stage < stages.Count; stage++)
            {
                var stagePath = stages[stage];
                var jsonText = File.ReadAllText(stagePath);

                var levels = JsonConvert.DeserializeObject<List<Level>>(jsonText, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

                if (!levelsByStage.ContainsKey(stage))
                    levelsByStage.Add(stage, levels);
                else
                    levelsByStage[stage] = levels;
            }
        }

        public static void LoadTileSet(string path, string tileSetName)
        {
            var tileSetPath = Path.Combine(path, tileSetName);
            var jsonText = File.ReadAllText(tileSetPath);

            TileSet = JsonConvert.DeserializeObject<TileSet>(jsonText);
        }

        public static void SetNextLevel()
        {
            var stage = CurrentLevel.Stage;
            var id = CurrentLevel.Id;

            if (levelsByStage[stage].Count > id + 1)
            {
                id++;
            }
            else if (levelsByStage.ContainsKey(stage + 1))
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

        public static void SetLevel(int currentStage, int currentId)
        {
            if (levelsByStage.ContainsKey(currentStage) && levelsByStage[currentStage].Count > 0)
                CurrentLevel = levelsByStage[currentStage][currentId];
            else
                throw new ArgumentException($"Не существует уровня на {currentStage} этаже с {currentId} id");
        }
    }
}

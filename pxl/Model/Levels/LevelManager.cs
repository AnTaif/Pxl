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
        private static int currentId = 0;
        private static int currentStage = 0;

        private static string currentDirectory;
        private static string tileSetPath;
        private static List<string> stagePaths;

        public static TileSet TileSet { get; private set; }

        public static Level CurrentLevel => levelsByStage[currentStage][currentId];

        private static void InitializePaths(
                string mapName, 
                string tileSetName = "tileset.json", 
                string mapsDirectory = "Model\\Levels\\Maps"
        )
        {
            var rootDirectory = MainGame.RootDirectory;
            currentDirectory = Path.Combine(rootDirectory, $"{mapsDirectory}\\{mapName}");
            tileSetPath = Directory.GetFiles(currentDirectory).Where(file => file.Contains(tileSetName)).First();
            stagePaths = Directory.GetFiles(currentDirectory)
                .Where(file => file.Contains("stage"))
                .ToList();
        }

        public static void LoadMap(string mapName)
        {
            levelsByStage = new();

            InitializePaths(mapName);

            LoadTileSet(tileSetPath);

            LoadLevels(stagePaths);

            SetLevel(currentId, currentStage);
        }

        private static void LoadLevels(List<string> stagePaths)
        {
            for (int stage = 0; stage < stagePaths.Count; stage++)
            {
                var stagePath = stagePaths[stage];
                LoadStage(stage, stagePath);
            }
        }

        private static Level LoadLevel(string jsonText)
        {
            var level = JsonConvert.DeserializeObject<Level>(jsonText, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

            return level;
        }

        private static void LoadStage(int stage, string stagePath)
        {
            var levels = DeserializeLevels(stagePath);

            if (!levelsByStage.ContainsKey(stage))
                levelsByStage.Add(stage, levels);
            else
                levelsByStage[stage] = levels;
        }

        private static List<Level> DeserializeLevels(string stagePath)
        {
            var jsonText = File.ReadAllText(stagePath);

            var levels = JsonConvert.DeserializeObject<List<Level>>(jsonText, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

            return levels;
        }

        public static void ResetLevel(Level level)
        {
            var stage = level.Stage;
            var id = level.Id;

            var levels = DeserializeLevels(stagePaths[stage]);
            var currentLevel = levels[id];

            levelsByStage[stage][id] = currentLevel;

            CollisionManager.SetLevel(currentLevel);
        }

        public static void ResetLevel(int stage, int id)
        {
            var levels = DeserializeLevels(stagePaths[stage]);

            levelsByStage[stage][id] = levels[id];
        }

        public static void ResetStage(int stage)
        {
            var levels = DeserializeLevels(stagePaths[stage]);

            levelsByStage[stage] = levels;
        }

        public static void LoadTileSet(string tileSetPath)
        {
            var jsonText = File.ReadAllText(tileSetPath);

            TileSet = JsonConvert.DeserializeObject<TileSet>(jsonText);
        }

        public static void SetNextLevel()
        {
            if (levelsByStage[currentStage].Count > currentId + 1)
                currentId++;
            else if (levelsByStage.ContainsKey(currentStage + 1))
            {
                currentStage++;
                currentId = 0;
            }
            else
            {
                currentStage = 0;
                currentId = 0;
                ResetStage(currentStage);
            }
        }

        public static void SetLevel(int stage, int id)
        {
            if (levelsByStage.ContainsKey(stage) && levelsByStage[stage].Count > 0)
            {
                currentStage = stage;
                currentId = id;
            }
            else
                throw new ArgumentException($"Не существует уровня на {currentStage} этаже с {currentId} id");
        }

        public static List<Level> GetLevels()
        {
            var levels = new List<Level>();

            foreach(var levelList in levelsByStage.Values)
            {
                foreach(var level in levelList)
                {
                    levels.Add(level);
                }
            }

            return levels;
        }
    }
}

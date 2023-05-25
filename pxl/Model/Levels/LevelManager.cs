﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Pxl
{
    public static class LevelManager
    {
        private static Dictionary<int, List<Level>> levelsByStage;
        public static TileSet TileSet { get; private set; }

        public static Level CurrentLevel { get; private set; }

        public static void LoadMapFromFile(string mapName)
        {
            var rootDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\.."));
            var path = Path.Combine(rootDirectory, $"Model\\Levels\\Maps\\{mapName}");

            LoadTileSet(path, Directory.GetFiles(path).Where(file => file.Contains("tileset.json")).First());

        }

        public static void LoadTileSet(string path, string tileSetName)
        {
            var tileSetPath = Path.Combine(path, tileSetName);
            var jsonText = File.ReadAllText(tileSetPath);

            TileSet = JsonConvert.DeserializeObject<TileSet>(jsonText);
        }

        public static void LoadLevels() //TODO: path to level
        {
            levelsByStage = new Dictionary<int, List<Level>>();

            // TODO: load levels from file

            levelsByStage.Add(0, new List<Level>() { new Level(0, 0, (960, 540), 
                new List<IGameObject>
                {
                    new Ground(new Rectangle(0, 496, 1000, 96), new Vector2(0, 496)),
                    new Platform(new Rectangle(336, 432, 160, 16), new Vector2(336, 432)),
                    new Platform(new Rectangle(576, 368, 160, 32), new Vector2(576, 368)),
                    new Ground(new Rectangle(0, 448, 48, 64), new Vector2(0, 448)), // Wall
                    new Ground(new Rectangle(896, 384, 64, 128), new Vector2(896, 384)), // Wall
                }, 
                new List<IEntity>(),
                new Vector2(10, 349)) });

            levelsByStage[0].Add(new Level(0, 1, (960, 540), 
                new List<IGameObject>
                {
                    new Ground(new Rectangle(0, 512, 960, 96), new Vector2(0, 512)),
                    new Ground(new Rectangle(0, 448, 250, 96), new Vector2(0, 448)),
                    new Platform(new Rectangle(368, 432, 176, 16), new Vector2(368, 432)),
                    new Spikes(new Rectangle(250, 496, 454, 16), new Vector2(250, 496)),
                    new Ground(new Rectangle(704, 448, 900, 96), new Vector2(704, 448)),
                    new Ground(new Rectangle(0, 384, 48, 128), new Vector2(0, 384)), // Wall
                },
                new List<IEntity>(),
                new Vector2(10, 349)));

            levelsByStage[0].Add(new Level(0, 2, (960, 540), 
                new List<IGameObject>
                {
                    new Ground(new Rectangle(0, 448, 960, 96), new Vector2(0, 448)),
                    new Platform(new Rectangle(368, 0, 16, 400), new Vector2(368, 0)), // Wall
                    new Platform(new Rectangle(576, 272, 16, 176), new Vector2(576, 272)), // Wall
                    new Platform(new Rectangle(528, 384, 48, 16), new Vector2(528, 384)),
                    new Platform(new Rectangle(384, 320, 48, 16), new Vector2(384, 320)),
                    new Platform(new Rectangle(576, 0, 16, 144), new Vector2(576, 0)), // Wall
                    new Platform(new Rectangle(528, 272, 192, 16), new Vector2(528, 272)),
                },
                new List<IEntity>(),
                new Vector2(10, 349)));
            CurrentLevel = levelsByStage[0][0];
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
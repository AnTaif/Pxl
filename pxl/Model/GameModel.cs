using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace pxl.Model
{
    public class GameModel
    {
        public Player Player { get; set; }
        public Level Level { get; set; }

        public GameModel(Player player, Level level)
        {
            Player = player;
            Level = level;
        }
    }
}

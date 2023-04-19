using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pxl.Model
{
    public class Level
    {
        public Rectangle Bounds { get; set; }

        public Level(Rectangle bounds)
        {
            Bounds = bounds;
        }
    }
}

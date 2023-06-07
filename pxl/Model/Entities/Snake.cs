using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public class Snake : WalkingEnemy
    {
        public Snake(RectangleF bounds, float direction = 1, Point? leftRange = null, Point? rightRange = null) 
            : base(bounds, direction, leftRange, rightRange)
        {
            Speed = 30;
        }
    }
}

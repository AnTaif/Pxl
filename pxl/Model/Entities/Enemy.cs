using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public abstract class Enemy : Entity
    {
        public Enemy(RectangleF bounds) : base(bounds)
        {
            Type = CollisionType.Enemy;
        }

        protected override void Death()
        {
        }
    }
}

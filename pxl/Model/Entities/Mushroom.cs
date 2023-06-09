using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public class Mushroom : WalkingEnemy
    {
        public Mushroom(RectangleF bounds, float direction = 1, Point? leftRange = null, Point? rightRange = null) 
            : base(bounds, direction, leftRange, rightRange)
        {
            Speed = 40;
        }

        public override void HandleCollisionWithEntity(CollisionInfo collision)
        {
            if (collision.Type == CollisionType.Player && collision.Direction == CollisionDirection.Top)
                ApplyDeath();
        }
    }
}

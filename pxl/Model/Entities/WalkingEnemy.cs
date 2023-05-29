using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    [Serializable]
    public class WalkingEnemy : Enemy
    {
        private const float Speed = 10f;
        private const float MaxSpeed = 200f;

        private int direction;
        private string name;

        [JsonConstructor]
        public WalkingEnemy(RectangleF bounds, string name) : base(bounds)
        {
            direction = 1;
            this.name = name;
        }

        public override void Update(GameTime gameTime)
        {
            ApplyHorizontalMove();

            base.Update(gameTime);
        }

        private void ApplyHorizontalMove()
        {
            if (Math.Abs(velocity.X) < MaxSpeed)
            {
                velocity.X += direction * Speed;
            }
            else
                ChangeDirection();
        }

        private void ChangeDirection()
        {
            direction = -direction;
            velocity.X = direction * Speed;
        }
    }
}

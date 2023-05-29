using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public enum FollowType { None, AxisX, AxisY, Target}

    public class Camera
    {
        public Matrix Transform { get; private set; }
        public float Scale { get; set; }

        public Camera()
        {
            Scale = 1f;
        }

        public void Follow(Player target, FollowType followType)
        {
            var position = Matrix.CreateTranslation(
                followType == FollowType.AxisX || followType == FollowType.Target ?
                    -target.Bounds.X - target.Bounds.Width / 2 : -1,
                followType == FollowType.AxisY || followType == FollowType.Target ?
                    -target.Bounds.Y - target.Bounds.Height / 2 : - 1,
                0);

            var offset = Matrix.CreateTranslation(
                followType == FollowType.AxisX || followType == FollowType.Target ?
                    MainGame.RenderSize.Width / 2 : 1,
                 followType == FollowType.AxisY || followType == FollowType.Target ?
                    MainGame.RenderSize.Height / 2 : 1,
                    0);

            Transform = position * Matrix.CreateScale(Scale) * offset;
        }
    }
}

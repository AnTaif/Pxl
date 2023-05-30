using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pxl
{
    public struct Size
    {
        public int Width;
        public int Height;

        [JsonConstructor]
        public Size(Point point)
        {
            Width = point.X;
            Height = point.Y;
        }

        [JsonConstructor]
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}

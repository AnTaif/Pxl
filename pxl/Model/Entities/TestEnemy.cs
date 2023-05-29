using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pxl
{
    public class TestEnemy : Enemy
    {
        private int i;

        [JsonConstructor]
        public TestEnemy(RectangleF bounds, int i) : base(bounds)
        {
            this.i = i;
        }
    }
}

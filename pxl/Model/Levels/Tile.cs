using System;
using Newtonsoft.Json;

namespace Pxl
{
    public class Tile
    {
        public string Name { get; private set; }
        public CollisionType CollisionType { get; private set; }
        public CollisionDirection TargetDirection { get; private set; }

        [JsonConstructor]
        public Tile(string name, string collisionTypeName, string targetDirection)
        {
            Name = name;
            CollisionType = (CollisionType)Enum.Parse(typeof(CollisionType), collisionTypeName, true);
            TargetDirection = (CollisionDirection)Enum.Parse(typeof(CollisionDirection), targetDirection, true);
        }
    }
}

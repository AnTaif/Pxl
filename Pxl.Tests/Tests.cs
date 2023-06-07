using Microsoft.Xna.Framework;
using NUnit.Framework;
using Pxl;

namespace Pxl.Tests
{
    public class LevelTests
    {
        [Test]
        public void ConvertToCollisionMap_ShouldConvertStationaryMapToCollisionMap()
        {

        }
    }

    public class PlayerTests
    {
        [Test]
        public void Jump_WhenOnGround_ShouldSetOnGroundToFalseAndSetVerticalVelocity()
        {
            // Arrange
            Player player = new Player(new RectangleF(0, 0, 10, 10));

            // Act
            player.Jump();

            // Assert
            Assert.IsFalse(player.OnGround);
            Assert.AreEqual(-500, player.Velocity.Y);
        }

        [Test]
        public void ApplyHorizontalMove_WhenMovingInSameDirection_ShouldIncreaseHorizontalVelocity()
        {
            // Arrange
            Player player = new Player(new RectangleF(0, 0, 10, 10), 5f);

            // Act
            player.ApplyHorizontalMove(new Vector2(1, 0));

            // Assert
            Assert.AreEqual(5f, player.Velocity.X);
        }

        [Test]
        public void ApplyHorizontalMove_WhenMovingInOppositeDirection_ShouldChangeDirectionAndSetHorizontalVelocity()
        {
            // Arrange
            Player player = new Player(new RectangleF(0, 0, 10, 10), 5f);
            player.SetVelocity(new Vector2(3f, 0));

            // Act
            player.ApplyHorizontalMove(new Vector2(-1, 0));

            // Assert
            Assert.AreEqual(-5f, player.Velocity.X);
        }

        [Test]
        public void HandleCollisionsWithLevel_WhenCollidingWithSpikes_ShouldCallDeathMethod()
        {
            // Arrange
            Player player = new Player(new RectangleF(5, 5, 10, 10));

            // Act
            player.HandleCollisionsWithLevel();

            // Assert
            Assert.IsFalse(player.IsAlive);
        }

        [Test]
        public void SetSpawn_ShouldSetSpawnPosition()
        {
            // Arrange
            Player player = new Player(new RectangleF(0, 0, 10, 10));
            Point spawnPosition = new Point(5, 5);

            // Act
            player.SetSpawn(spawnPosition);

            // Assert
            Assert.AreEqual(spawnPosition, player.SpawnPosition);
        }

        // ... Add more test cases for other methods and functionalities ...
    }
}

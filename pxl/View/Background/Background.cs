using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public class Background
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly Random random = new Random();
        private readonly Size Size = MainGame.WorkingSize; 

        private Texture2D sky;
        private Texture2D glacialMountains;

        private List<Cloud> movingClouds;
        private List<Cloud> bgClouds;

        public Background(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;

            movingClouds = new List<Cloud>()
            {
                new Cloud("cloud_lonely", new Vector2(Size.Width, random.Next(10, 160)), new Point(215, 73), (float)random.NextDouble() + 0.5f),
                new Cloud("cloud_lonely",  new Vector2(Size.Width + random.Next(50, 400), random.Next(10, 160)), new Point(215, 73), (float)random.NextDouble() + 0.5f),
                new Cloud("clouds_mg_3", new Vector2(0, 0), new Point(Size.Width, Size.Height), 0.2f),
                new Cloud("clouds_mg_3", new Vector2(Size.Width, 0), new Point(Size.Width, Size.Height), 0.2f),
                new Cloud("clouds_mg_2", new Vector2(0, 0), new Point(Size.Width, Size.Height), 0.4f),
                new Cloud("clouds_mg_2", new Vector2(Size.Width, 0), new Point(Size.Width, Size.Height), 0.4f),
                new Cloud("clouds_mg_1", new Vector2(0, 0), new Point(Size.Width, Size.Height), 0.6f),
                new Cloud("clouds_mg_1", new Vector2(Size.Width, 0), new Point(Size.Width, Size.Height), 0.6f)
            };

            bgClouds = new List<Cloud>()
            {
                new Cloud("clouds_bg", new Vector2(0, 0), new Point(Size.Width, Size.Height), 0.01f),
                new Cloud("clouds_bg", new Vector2(Size.Width, 0), new Point(Size.Width, Size.Height), 0.01f)
            };
        }

        public void LoadContent(ContentManager content)
        {
            sky = content.Load<Texture2D>("background/sky");
            glacialMountains = content.Load<Texture2D>("background/glacial_mountains");
            LoadClouds(content, movingClouds);
            LoadClouds(content, bgClouds);
        }

        private void LoadClouds(ContentManager content, List<Cloud> clouds)
        {
            foreach(var cloud in clouds)
            {
                cloud.LoadContent(content);
            }
        }

        public void Update(GameTime gameTime)
        {
            UpdateClouds(gameTime, bgClouds);
            UpdateClouds(gameTime, movingClouds);
        }

        public void UpdateClouds(GameTime gameTime, List<Cloud> clouds)
        {
            foreach (var cloud in clouds)
            {
                cloud.Update(gameTime);
                if (cloud.Position.X + cloud.Size.X <= 0)
                    if (cloud.RootName.Equals("cloud_lonely"))
                        cloud.ReCreate(new Vector2(Size.Width, random.Next(10, 160)), new Point(215, 73), (float)random.NextDouble() + 0.5f);
                    else
                        cloud.ReCreate(new Vector2(cloud.Size.X + (cloud.Position.X + cloud.Size.X), 0));
            }
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Draw(sky, new Rectangle(0, 0, Size.Width, Size.Height), Color.White);

            DrawClouds(gameTime, bgClouds);

            _spriteBatch.Draw(glacialMountains, new Rectangle(0, 0, Size.Width, Size.Height), Color.White);

            DrawClouds(gameTime, movingClouds);
        }

        public void DrawClouds(GameTime gameTime, List<Cloud> clouds)
        {
            foreach (var cloud in clouds)
            {
                cloud.Draw(_spriteBatch);
            }
        }
    }
}

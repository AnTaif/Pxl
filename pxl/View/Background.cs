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
        private readonly (int Width, int Height) Size = MainGame.WorkingSize; 

        private Texture2D sky;
        private Texture2D glacialMountains;
        private Texture2D cloudsMg3;
        private Texture2D cloudsMg2;
        private Texture2D cloudsMg1;
        private Texture2D cloudsBg;
        private Texture2D cloudLonely;

        private List<Cloud> movingClouds;
        private List<Cloud> bgClouds;

        public Background(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public void LoadContent(ContentManager content)
        {
            sky = content.Load<Texture2D>("background/sky");
            glacialMountains = content.Load<Texture2D>("background/glacial_mountains");
            cloudsMg3 = content.Load<Texture2D>("background/clouds_mg_3");
            cloudsMg2 = content.Load<Texture2D>("background/clouds_mg_2");
            cloudsMg1 = content.Load<Texture2D>("background/clouds_mg_1");
            cloudsBg = content.Load<Texture2D>("background/clouds_bg");
            cloudLonely = content.Load<Texture2D>("background/cloud_lonely");

            movingClouds = new List<Cloud>()
            {
                new Cloud(cloudLonely, new Vector2(Size.Width, random.Next(10, 160)), new Point(cloudLonely.Width, cloudLonely.Height), (float)random.NextDouble() + 0.5f),
                new Cloud(cloudLonely,  new Vector2(Size.Width + random.Next(50, 400), random.Next(10, 160)), new Point(cloudLonely.Width, cloudLonely.Height), (float)random.NextDouble() + 0.5f),
                new Cloud(cloudsMg3, new Vector2(0, 0), new Point(Size.Width, Size.Height), 0.2f),
                new Cloud(cloudsMg3, new Vector2(Size.Width, 0), new Point(Size.Width, Size.Height), 0.2f),
                new Cloud(cloudsMg2, new Vector2(0, 0), new Point(Size.Width, Size.Height), 0.4f),
                new Cloud(cloudsMg2, new Vector2(Size.Width, 0), new Point(Size.Width, Size.Height), 0.4f),
                new Cloud(cloudsMg1, new Vector2(0, 0), new Point(Size.Width, Size.Height), 0.6f),
                new Cloud(cloudsMg1, new Vector2(Size.Width, 0), new Point(Size.Width, Size.Height), 0.6f)
            };

            bgClouds = new List<Cloud>()
            {
                new Cloud(cloudsBg, new Vector2(0, 0), new Point(Size.Width, Size.Height), 0.01f),
                new Cloud(cloudsBg, new Vector2(Size.Width, 0), new Point(Size.Width, Size.Height), 0.01f)
            };
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
                cloud.Update(gameTime);
                if (cloud.Position.X + cloud.Size.X < 0)
                    if (cloud.Texture == cloudLonely)
                        cloud.ReCreate(new Vector2(Size.Width, random.Next(10, 160)), new Point(cloudLonely.Width, cloudLonely.Height), (float)random.NextDouble() + 0.5f);
                    else
                        cloud.ReCreate(new Vector2(Size.Width, 0));
            }
        }
    }
}

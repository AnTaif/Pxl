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

        private Texture2D sky;
        private Texture2D glacialMountains;
        private Texture2D cloudsMg3;
        private Texture2D cloudsMg2;
        private Texture2D cloudsMg1;
        private Texture2D cloudsBg;
        private Texture2D cloudLonely;

        private List<Cloud> movingClouds;

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
                new Cloud(cloudLonely, new Rectangle(1856, random.Next(48, 192), cloudLonely.Width, cloudLonely.Height), 1f),
                new Cloud(cloudLonely, new Rectangle(1856, random.Next(48, 192), cloudLonely.Width/2, cloudLonely.Height/2), 2f),
                new Cloud(cloudsMg3, new Rectangle(0, 0, 1856, 1024), 1f),
                new Cloud(cloudsMg3, new Rectangle(1856, 0, 1856, 1024), 1f),
                new Cloud(cloudsMg2, new Rectangle(0, 0, 1856, 1024), 1f),
                new Cloud(cloudsMg2, new Rectangle(1856, 0, 1856, 1024), 1f),
                new Cloud(cloudsMg1, new Rectangle(0, 0, 1856, 1024), 1f),
                new Cloud(cloudsMg1, new Rectangle(1856, 0, 1856, 1024), 1f)
            };
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Draw(sky, new Rectangle(0, 0, 1856, 1024), Color.White);
            _spriteBatch.Draw(cloudsBg, new Rectangle(0, 0, 1856, 1024), Color.White);
            _spriteBatch.Draw(glacialMountains, new Rectangle(0, 0, 1856, 1024), Color.White);
            //_spriteBatch.Draw(cloudsMg3, new Rectangle(0, 0, 1856, 1024), Color.White);
            //_spriteBatch.Draw(cloudsMg2, new Rectangle(0, 0, 1856, 1024), Color.White);
            //_spriteBatch.Draw(cloudsMg1, new Rectangle(0, 0, 1856, 1024), Color.White);

            DrawClouds(gameTime);
        }

        public void DrawClouds(GameTime gameTime)
        {
            foreach(var cloud in movingClouds)
            {
                cloud.Draw(_spriteBatch);
                cloud.Update(gameTime);
                if (cloud.Bounds.X + cloud.Bounds.Width < 0)
                    cloud.ReCreate(1856, random.Next(48, 192));
            }
        }
    }
}

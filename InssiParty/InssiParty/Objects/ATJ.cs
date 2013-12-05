using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace InssiParty.Games
{
    public class ATJ
    {
        protected Texture2D tekstuuri;
        protected Vector2 nopeus;
        protected Vector2 kohta;

        public bool Visible = true;

        Random random = new Random();
        int randX, randY;

        public ATJ(Texture2D newTex, Vector2 newPos)
        {
            tekstuuri = newTex;
            kohta = newPos;

            randX = random.Next(1, 4);
            randY = random.Next(-4, 4);

            nopeus = new Vector2(randX, randY);

        }

        public void Update(GraphicsDevice graphics)
        {
            kohta += nopeus;

            if(kohta.Y <= graphics.Viewport.Height / 2)
            {
                randY = random.Next(1, 4);
            }
            
            if(kohta.Y >= graphics.Viewport.Height / 2)
            {
                randY = random.Next(-4, 1);
            }

            if(kohta.X <= graphics.Viewport.Width - tekstuuri.Width)
            {
                randX = random.Next(1, 4);
            }

            if (kohta.X >= graphics.Viewport.Width + tekstuuri.Width)
            {
                randX = random.Next(-4, -1);
            }

            if (kohta.X < 0 - tekstuuri.Width)
                Visible = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tekstuuri, kohta, Color.White);
        }
    }
}

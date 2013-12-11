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
        public Rectangle atj_alue { get; private set;}

        public bool Visible = true;
        public bool Hit = true;

        Random random = new Random();
        int randX, randY;

        public ATJ(Texture2D newTex, Vector2 newPos)
        {
            tekstuuri = newTex;
            kohta = newPos;

            randX = random.Next(-4,-1);
            randY = random.Next(-4, 4);

            nopeus = new Vector2(randX, randY);
        }

        public void Update()
        {
            kohta += nopeus;

            if(kohta.Y <= 0 || kohta.Y >= 600 - tekstuuri.Height )
            {
                nopeus.Y = -nopeus.Y;
            }

            atj_alue = new Rectangle((int)kohta.X, (int)kohta.Y, tekstuuri.Width, tekstuuri.Height);
            
            if (kohta.X < 0 - tekstuuri.Width)
                Visible = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tekstuuri, kohta, Color.White);
        }
    }
}

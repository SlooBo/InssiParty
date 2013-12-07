using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InssiParty.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace InssiParty.Games
{
    class vali : GameBase
    {
        //variaabelit
        private int value, moveX;

        //Tekstuurit
        Texture2D backgroundTexture, valiTexture, sydanTexture, tekstiTexture;
        
        //rektanglet
        Rectangle background = new Rectangle(0, 0, 800, 600);
        Rectangle keskipalkki = new Rectangle(0, 180, 800, 220);
        Rectangle sydanRect = new Rectangle(136, 236, 128, 128);
        Rectangle sydanRect2 = new Rectangle(336, 236, 128, 128);
        Rectangle sydanRect3 = new Rectangle(536, 236, 128, 128);
        Rectangle tekstiRect = new Rectangle(-500, 290, 147, 21);

        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //tekstuurit
            backgroundTexture = Content.Load<Texture2D>("tausta");
            valiTexture = Content.Load<Texture2D>("trans_palkit");
            sydanTexture = Content.Load<Texture2D>("sydan");
            tekstiTexture = Content.Load<Texture2D>("storymode");
        }

        public override void Start()
        {
            Console.WriteLine("Starting hello world");
            
            value = 0;
        }

        /**
         * Ajetaan kun peli sulkeutuu. Piilota äänet ja puhdista roskasi seuraavaa peliä varten.
         */
        public override void Stop()
        {
            Console.WriteLine("Closing hello world");
        }

        public override void Update(GameTime gameTime)
        {
            value++;

            sydanRect.X += moveX; sydanRect2.X += moveX; sydanRect3.X += moveX; tekstiRect.X += moveX;

            if (value > 150) 
            {
                moveX = 20;

                if(tekstiRect.X > 300 && value < 400) //muuta
                {
                    moveX = 0;
                }
            }
            if (value > 450)
            {
                CloseGame(true);
            }
        }

        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(backgroundTexture, background, Color.White);
            spriteBatch.Draw(valiTexture, keskipalkki, Color.White);
            spriteBatch.Draw(sydanTexture, sydanRect, Color.White);
            spriteBatch.Draw(sydanTexture, sydanRect2, Color.White);
            spriteBatch.Draw(sydanTexture, sydanRect3, Color.White);
            spriteBatch.Draw(tekstiTexture, tekstiRect, Color.White);
        }

    }
}

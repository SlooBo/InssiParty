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
        private int value, moveX, moveY, scaleY, scaleX;

        //Tekstuurit
        Texture2D backgroundTexture, valiTexture, sydanTexture, tekstiTexture;
        
        //rektanglet
        Rectangle background = new Rectangle(0, 0, 800, 600);
        Rectangle keskipalkki = new Rectangle(0, 180, 800, 220);
        Rectangle sydanRect = new Rectangle(136, 236, 128, 128);
        Rectangle sydanRect2 = new Rectangle(336, 236, 128, 128);
        Rectangle sydanRect3 = new Rectangle(536, 236, 128, 128);
        Rectangle tekstiRect = new Rectangle(-500, 290, 135, 21);

        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //tekstuurit
            backgroundTexture = Content.Load<Texture2D>("tausta");
            valiTexture = Content.Load<Texture2D>("trans_palkit");
            sydanTexture = Content.Load<Texture2D>("sydan");
            tekstiTexture = Content.Load<Texture2D>("next_game");
        }

        public override void Start()
        {
            Console.WriteLine("Starting hello world");
            
            value = 0;
            moveX = 0;
            moveY = 0;
            scaleY = 0;
            scaleX = 0;
            sydanRect.X = 136; sydanRect.Y = 236;
            sydanRect2.X = 336; sydanRect2.Y = 236;
            sydanRect3.X = 536; sydanRect3.Y = 236;
            tekstiRect.X = -500; tekstiRect.Y = 290;
        }

        public override void Stop()
        {
            Console.WriteLine("Transitio");
        }

        public override void Update(GameTime gameTime)
        {
            value++;

            sydanRect.X += moveX; sydanRect2.X += moveX; sydanRect3.X += moveX; tekstiRect.X += moveX;
            sydanRect.Y += moveY; sydanRect2.Y += moveY; sydanRect3.Y += moveY;
            sydanRect.Height += scaleY; sydanRect.Width += scaleX;
            sydanRect2.Height += scaleY; sydanRect2.Width += scaleX;
            sydanRect3.Height += scaleY; sydanRect3.Width += scaleX;

            if (value > 0 && value < 100)
            {
                if (value == 30) 
                {
                    moveX = -3;
                    moveY = -6;
                    scaleX = 6;
                    scaleY = 6;
                }

                if (value == 40) 
                {
                    moveX = 3;
                    moveY = 6;
                    scaleX = -6;
                    scaleY = -6;
                }

                if (value == 50)
                {
                    moveX = -3;
                    moveY = -6;
                    scaleX = 6;
                    scaleY = 6;
                }

                if (value == 60)
                {
                    moveX = 3;
                    moveY = 6;
                    scaleX = -6;
                    scaleY = -6;
                }
                if (value == 70) 
                {
                    moveX = 0;
                    moveY = 0;
                    scaleY = 0;
                    scaleX = 0;
                }

            }

            if (value > 150) 
            {
                moveX = 20;

                if(tekstiRect.X > 200 && value < 400)
                {
                    moveX = 4;
                }

                if (tekstiRect.X > 400 && value < 400)
                {
                    moveX = 20;
                }
            }
            if (value > 300)
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

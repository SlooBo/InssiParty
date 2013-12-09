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
        private int TRANSITION_value, TRANSITION_moveX, TRANSITION_moveY, TRANSITION_scaleY, TRANSITION_scaleX;

        //Tekstuurit
        Texture2D TRANSITION_backgroundTexture, TRANSITION_valiTexture, TRANSITION_sydanTexture, TRANSITION_tekstiTexture;
        
        //rektanglet
        Rectangle TRANSITION_background = new Rectangle(0, 0, 800, 600);
        Rectangle TRANSITION_keskipalkki = new Rectangle(0, 180, 800, 220);
        Rectangle TRANSITION_sydanRect = new Rectangle(136, 236, 128, 128);
        Rectangle TRANSITION_sydanRect2 = new Rectangle(336, 236, 128, 128);
        Rectangle TRANSITION_sydanRect3 = new Rectangle(536, 236, 128, 128);
        Rectangle TRANSITION_tekstiRect = new Rectangle(-500, 290, 135, 21);

        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //tekstuurit
            TRANSITION_backgroundTexture = Content.Load<Texture2D>("tausta");
            TRANSITION_valiTexture = Content.Load<Texture2D>("trans_palkit");
            TRANSITION_sydanTexture = Content.Load<Texture2D>("sydan");
            TRANSITION_tekstiTexture = Content.Load<Texture2D>("next_game");
        }

        public override void Start()
        {
            Console.WriteLine("Starting hello world");
            
            TRANSITION_value = 0;
            TRANSITION_moveX = 0;
            TRANSITION_moveY = 0;
            TRANSITION_scaleY = 0;
            TRANSITION_scaleX = 0;
            TRANSITION_sydanRect.X = 136; TRANSITION_sydanRect.Y = 236;
            TRANSITION_sydanRect2.X = 336; TRANSITION_sydanRect2.Y = 236;
            TRANSITION_sydanRect3.X = 536; TRANSITION_sydanRect3.Y = 236;
            TRANSITION_tekstiRect.X = -500; TRANSITION_tekstiRect.Y = 290;
        }

        public override void Stop()
        {
            Console.WriteLine("Transitio");
        }

        public override void Update(GameTime gameTime)
        {
            TRANSITION_value++;

            TRANSITION_sydanRect.X += TRANSITION_moveX; TRANSITION_sydanRect2.X += TRANSITION_moveX; TRANSITION_sydanRect3.X += TRANSITION_moveX; TRANSITION_tekstiRect.X += TRANSITION_moveX;
            TRANSITION_sydanRect.Y += TRANSITION_moveY; TRANSITION_sydanRect2.Y += TRANSITION_moveY; TRANSITION_sydanRect3.Y += TRANSITION_moveY;
            TRANSITION_sydanRect.Height += TRANSITION_scaleY; TRANSITION_sydanRect.Width += TRANSITION_scaleX;
            TRANSITION_sydanRect2.Height += TRANSITION_scaleY; TRANSITION_sydanRect2.Width += TRANSITION_scaleX;
            TRANSITION_sydanRect3.Height += TRANSITION_scaleY; TRANSITION_sydanRect3.Width += TRANSITION_scaleX;

            if (TRANSITION_value > 0 && TRANSITION_value < 100)
            {
                if (TRANSITION_value == 30) 
                {
                    TRANSITION_moveX = -3;
                    TRANSITION_moveY = -6;
                    TRANSITION_scaleX = 6;
                    TRANSITION_scaleY = 6;
                }

                if (TRANSITION_value == 40) 
                {
                    TRANSITION_moveX = 3;
                    TRANSITION_moveY = 6;
                    TRANSITION_scaleX = -6;
                    TRANSITION_scaleY = -6;
                }

                if (TRANSITION_value == 50)
                {
                    TRANSITION_moveX = -3;
                    TRANSITION_moveY = -6;
                    TRANSITION_scaleX = 6;
                    TRANSITION_scaleY = 6;
                }

                if (TRANSITION_value == 60)
                {
                    TRANSITION_moveX = 3;
                    TRANSITION_moveY = 6;
                    TRANSITION_scaleX = -6;
                    TRANSITION_scaleY = -6;
                }
                if (TRANSITION_value == 70) 
                {
                    TRANSITION_moveX = 0;
                    TRANSITION_moveY = 0;
                    TRANSITION_scaleY = 0;
                    TRANSITION_scaleX = 0;
                }

            }

            if (TRANSITION_value > 150) 
            {
                TRANSITION_moveX = 20;

                if(TRANSITION_tekstiRect.X > 200 && TRANSITION_value < 400)
                {
                    TRANSITION_moveX = 4;
                }

                if (TRANSITION_tekstiRect.X > 400 && TRANSITION_value < 400)
                {
                    TRANSITION_moveX = 20;
                }
            }
            if (TRANSITION_value > 300)
            {
                CloseGame(true);
            }
        }

        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(TRANSITION_backgroundTexture, TRANSITION_background, Color.White);
            spriteBatch.Draw(TRANSITION_valiTexture, TRANSITION_keskipalkki, Color.White);
            spriteBatch.Draw(TRANSITION_sydanTexture, TRANSITION_sydanRect, Color.White);
            spriteBatch.Draw(TRANSITION_sydanTexture, TRANSITION_sydanRect2, Color.White);
            spriteBatch.Draw(TRANSITION_sydanTexture, TRANSITION_sydanRect3, Color.White);
            spriteBatch.Draw(TRANSITION_tekstiTexture, TRANSITION_tekstiRect, Color.White);
        }

    }
}

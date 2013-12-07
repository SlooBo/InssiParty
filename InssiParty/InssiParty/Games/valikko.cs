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
    class valikko : GameBase
    {
        //variaabelit
        private int value, value2, scaleX, scaleY, moveX, moveY;

        //Tekstuurit
        Texture2D backgroundTexture, logoTexture, koodiTexture, arcadeTexture, exitTexture, storyTexture;

        //rektanglet
        Rectangle background = new Rectangle(0, 0, 800, 600);
        Rectangle logoRect = new Rectangle(130, 50, 561, 299);
        Rectangle arcadeRect = new Rectangle(353, 400, 85, 21);
        Rectangle exitRect = new Rectangle(368, 450, 50, 21);
        Rectangle storyRect = new Rectangle(325, 350, 147, 21);
        Vector2 koodiVector = new Vector2();
        Vector2 logoVector = new Vector2();

        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //tekstuurit
            backgroundTexture = Content.Load<Texture2D>("tausta");
            logoTexture = Content.Load<Texture2D>("logo_rotate");
            koodiTexture = Content.Load<Texture2D>("koodit_rotate");
            arcadeTexture = Content.Load<Texture2D>("arcade");
            exitTexture = Content.Load<Texture2D>("exit");
            storyTexture = Content.Load<Texture2D>("storymode");
        }

        public override void Start()
        {
            //alustukset
            koodiVector.Y = 0;
            koodiVector.X = -1502 + 800;
            logoVector.X = 0;
            logoVector.Y = 0;
            scaleX = 8;
            scaleY = 8;
            moveX = -4;
            moveY = -4;
            value = 0;
            value2 = 0;
        }

        public override void Stop()
        {
            Console.WriteLine("Closing hello world");
        }

        public override void Update(GameTime gameTime)
        {
            //value/gametime
            value++;

            //koodivektori hallinta
            koodiVector.Y -= 5;
            koodiVector.X += 1.81f;

            if (koodiVector.X > 107)
            {
                koodiVector.X = -1502 + 800;
                koodiVector.Y = 0;
            }

            //logo hallinta

            if (value > 100)
            {
                logoRect.Width += scaleX;
                logoRect.Height += scaleY;
                logoRect.X += moveX;
                logoRect.Y += moveY;

                if (logoRect.Height > 400)
                {
                    scaleX = -8;
                    scaleY = -8;
                    moveX = 4;
                    moveY = 4;
                }
                if (logoRect.Height < 300)
                {
                    scaleX = 8;
                    scaleY = 8;
                    moveX = -4;
                    moveY = -4;
                    value2++;
                }

            }
            if (value2 > 2 || value2 == 2)
            {
                value = 0;
                value2 = 0;
            }

            if (value < 0)
            {
                CloseGame(true);
            }

        }

        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(backgroundTexture, background, Color.White);
            spriteBatch.Draw(koodiTexture, koodiVector, Color.White);
            spriteBatch.Draw(logoTexture, logoRect, Color.White);
            spriteBatch.Draw(arcadeTexture, arcadeRect, Color.White);
            spriteBatch.Draw(exitTexture, exitRect, Color.White);
            spriteBatch.Draw(storyTexture, storyRect, Color.White);
        }

    }
}

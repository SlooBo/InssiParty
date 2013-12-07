using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InssiParty.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace InssiParty.Games
{
    class valikko_demo : GameBase
    {
        //variaabelit
        private int value, value2, scaleX, scaleY, moveX, moveY;

        //Tekstuurit
        Texture2D backgroundTexture, logoTexture, koodiTexture;
        AnimatedSprite sprite;
        
        //rektanglet
        Rectangle background = new Rectangle(0, 0, 800, 600);
        Rectangle logoRect = new Rectangle(130, 50, 561, 299);
        Vector2 koodiVector = new Vector2();
        Vector2 logoVector = new Vector2();

        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //tekstuurit
            backgroundTexture = Content.Load<Texture2D>("tausta");
            logoTexture = Content.Load<Texture2D>("logo_rotate");
            koodiTexture = Content.Load<Texture2D>("koodit_rotate");

            //animaatio
            sprite = new AnimatedSprite(Content.Load<Texture2D>("anykey60f"), 0, 296, 42);
            sprite.Position = new Vector2(400,450);
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

            //anykey hallinta
            sprite.anykeyMovement(gameTime);
            sprite.Animate(gameTime);

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
            spriteBatch.Draw(koodiTexture,koodiVector, Color.White);
            spriteBatch.Draw(logoTexture,logoRect, Color.White);
            spriteBatch.Draw(sprite.Texture, sprite.Position, sprite.SourceRect, Color.White, 0f, sprite.Origin, 1.0f, SpriteEffects.None, 0);
        }

    }
}

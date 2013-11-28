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
        private int value;

        //Tekstuurit
        Texture2D backgroundTexture, logoTexture, koodiTexture;
        AnimatedSprite sprite;
        
        //rektanglet
        Rectangle background = new Rectangle(0, 0, 800, 600);
        Rectangle logoRect = new Rectangle(130, 50, 561, 299);
        Rectangle koodiRect = new Rectangle(-1502+800, 0, 1502, 2985);

        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //tekstuurit
            backgroundTexture = Content.Load<Texture2D>("tausta");
            logoTexture = Content.Load<Texture2D>("logo_rotate");
            koodiTexture = Content.Load<Texture2D>("koodit_rotate");

            sprite = new AnimatedSprite(Content.Load<Texture2D>("anykey60f"), 0, 296, 42);

            sprite.Position = new Vector2(400,450);
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
            sprite.anykeyMovement(gameTime);
            sprite.Animate(gameTime);
            
            if (value < 0)
            {
                CloseGame(true);
            }
            koodiRect.X += 1;
            koodiRect.Y -= 5;
        }

        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(backgroundTexture, background, Color.White);
            spriteBatch.Draw(koodiTexture, koodiRect, Color.White);
            spriteBatch.Draw(logoTexture, logoRect, Color.White);
            spriteBatch.Draw(sprite.Texture, sprite.Position, sprite.SourceRect, Color.White, 0f, sprite.Origin, 1.0f, SpriteEffects.None, 0);
        }

    }
}

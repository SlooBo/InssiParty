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
        private int value;

        //Tekstuurit
        Texture2D backgroundTexture;
        AnimatedSprite sprite;
        
        //rektanglet
        Rectangle background = new Rectangle(0, 0, 800, 600);

        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //tekstuurit
            backgroundTexture = Content.Load<Texture2D>("alko");

            sprite = new AnimatedSprite(Content.Load<Texture2D>("pullo_spritesheet"), 1, 150, 388);

            sprite.Position = new Vector2(400,300);
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
            sprite.HandleSpriteMovement(gameTime);
            sprite.Animate(gameTime);

            if (value < 0)
            {
                CloseGame(true);
            }
        }

        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(backgroundTexture, background, Color.White);
            spriteBatch.Draw(sprite.Texture, sprite.Position, sprite.SourceRect, Color.White, 0f, sprite.Origin, 1.0f, SpriteEffects.None, 0);
        }

    }
}

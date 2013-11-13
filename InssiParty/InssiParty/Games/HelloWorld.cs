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
    /**
     * HelloWorld
     * 
     * Nopea selitys pelistä.
     * 
     * By: Lauri Mäkinen
     */
    class HelloWorld : GameBase
    {
        private int value;

        private Texture2D spriteBox;

        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            spriteBox = Content.Load<Texture2D>("propelli");
        }

        public override void Start()
        {
            Console.WriteLine("Starting hello world");
            
            value = 0;
        }

        public override void Stop()
        {
            Console.WriteLine("Closing hello world");
        }

        public override void Update(GameTime gameTime)
        {
            value++;
        }

        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Console.WriteLine("Draw " + value);

            spriteBatch.Draw(spriteBox, new Vector2(250,250), Color.White);
        }

    }
}

//gittest

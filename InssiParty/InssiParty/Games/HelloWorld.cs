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
    class HelloWorld : IGameBase
    {
        public bool IsRunning { get; set; }

        private int value;

        private Texture2D spriteBox;

        public void Load(ContentManager Content)
        {
            spriteBox = Content.Load<Texture2D>("propelli");
        }

        public void Start()
        {
            Console.WriteLine("Starting hello world");
            
            value = 0;
        }

        public void Stop()
        {
            Console.WriteLine("Closing hello world");
        }

        public void Update(GameTime gameTime)
        {
            value++;
        }

        public void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Console.WriteLine("Draw " + value);

            spriteBatch.Draw(spriteBox, new Vector2(250,250), Color.White);
        }

    }
}

//gittest

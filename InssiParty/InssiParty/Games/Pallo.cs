using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InssiParty.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace InssiParty.Games.FeedGameSrc
{
    class Pallo : GameBase
    {
        Rectangle background = new Rectangle(0, 0, 800, 600);

        private Texture2D backgroundTexture;
        private Texture2D pallo;
        private Texture2D Kori;
        private Texture2D Palkki;

        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            backgroundTexture = Content.Load<Texture2D>("Kenttä");
            pallo = Content.Load<Texture2D>("Pallo");
            Kori = Content.Load<Texture2D>("Kori");
            Palkki = Content.Load<Texture2D>("Palkki");
        }

        public override void Start()
        {
            Console.WriteLine("Start Game");

            //alustus
        }

        public override void Stop()
        {
            Console.WriteLine("Game Over");

            //alustus
        }

        public override void Update(GameTime gameTime)
        {
           
        }

        public override void Render(SpriteBatch spriteBatch, GameTime gameTime) 
        {
        
        }

    }
}

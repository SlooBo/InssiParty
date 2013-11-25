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
    class Pallo : GameBase
    {
        Rectangle background = new Rectangle(0, 0, 800, 600);

        private Texture2D backgroundTexture;
        private Texture2D pallo;
        private Texture2D Kori;
        private Texture2D Palkki;
        private int liike = 0;
        private int value;


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
            value = 500;
            //alustus
        }

        public override void Stop()
        {
            Console.WriteLine("Game Over");

            //alustus
            CloseGame(true);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.A))
              liike -= 2 ;
            if (keyboard.IsKeyDown(Keys.D))
              liike += 2;
            
        }

        public override void Render(SpriteBatch spriteBatch, GameTime gameTime) 
        {
            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.Beige);
            spriteBatch.Draw(Kori, new Vector2(330,520), Color.Beige); 
            spriteBatch.Draw(pallo, new Vector2(350, 0), Color.Beige);
        }

    }
}
            
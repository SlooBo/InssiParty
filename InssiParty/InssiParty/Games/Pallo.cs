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

        private int value = 0;
        private int joku;

        Rectangle kori = new Rectangle(330, 520, 100, 75);
        Rectangle ballo = new Rectangle(350, -90, 45, 45);

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
            Random random = new Random();
            joku = random.Next(-10, 10);
            

        }

        public override void Stop()
        {
            Console.WriteLine("Game Over");
        }

        public override void Update(GameTime gameTime)
        {
            
            ballo.Y += 1;
            if(joku > 0)
            {
                if (value == 0)
                {
                    ballo.X += joku;
                }

                else
                {
                    ballo.X -= joku;
                }


                if (ballo.X < 0)
                {
                    value = 0;
                }
                if (ballo.X > 755)
                {
                    value = 1;
                }
            }

            if (joku < 0)
            {
                if (value == 0)
                {
                    ballo.X += joku;
                }

                else
                {
                    ballo.X -= joku;
                }

                if (ballo.X > 755)
                {
                    value = 0;
                }
                
                if (ballo.X < 0)
                {
                    value = 1;
                }

                
            }
            
            /*if()
            {
                ballo*/
            
            
            if (ballo.Y > 600)
            {
                CloseGame(false);
            }

            if (kori.X < 0)
            {
                kori.X = 0;
            }

            if (kori.X > 690)
            {
                kori.X = 690;
            }
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.A))
                kori.X -= 10;
            if (keyboard.IsKeyDown(Keys.D))
                kori.X += 10;
        }

        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.Beige);
            spriteBatch.Draw(Kori, kori, Color.Beige);
            spriteBatch.Draw(pallo, ballo, Color.Beige);
        }

    }
}

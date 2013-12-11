using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InssiParty.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace InssiParty.Games
{
    class Pallo : GameBase
    {
        Rectangle background = new Rectangle(0, 0, 800, 600);

        private Texture2D backgroundTexture;
        private Texture2D pallo;
        private Texture2D Kori;
        private Texture2D Palkki;


        private int op;
        private bool osui;
        private int nopeus;
        private int value = 0;
        Rectangle kori = new Rectangle(330, 520, 100, 75);
        Rectangle ballo = new Rectangle(350, -90, 45, 45);
        Rectangle palkki = new Rectangle(0, 150, 800, 300);

        SoundEffect seina;
        SoundEffect nappaus;

        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            backgroundTexture = Content.Load<Texture2D>("Kenttä");
            pallo = Content.Load<Texture2D>("Pallo");
            Kori = Content.Load<Texture2D>("Kori");
            Palkki = Content.Load<Texture2D>("Palkki");

            seina = Content.Load<SoundEffect>("Pallon pomppu");
            nappaus = Content.Load<SoundEffect>("Pallo Korii");

        }

        public override void Start()
        {
            Console.WriteLine("Start Game");
            //alustus
            Random random = new Random();
            nopeus = random.Next(-9, 9);
            osui = false;
            op = 0;

            kori = new Rectangle(330, 520, 100, 75);
            ballo = new Rectangle(350, -90, 45, 45);


        }

        public override void Stop()
        {
            Console.WriteLine("Game Over");
        }

        public override void Update(GameTime gameTime)
        {
            ballo.Y = ballo.Y + 2;
            if (nopeus > 0)
            {
                if (value == 0)
                {
                    ballo.X += nopeus;
                }

                else
                {
                    ballo.X -= nopeus;
                }


                if (ballo.X < 0)
                {
                    value = 0;
                    seina.Play();
                }
                if (ballo.X > 755)
                {
                    value = 1;
                    seina.Play();
                }
            }

            if (nopeus < 0)
            {
                if (value == 0)
                {
                    ballo.X += nopeus;
                }

                else
                {
                    ballo.X -= nopeus;
                }

                if (ballo.X > 755)
                {
                    value = 0;
                    seina.Play();
                }

                if (ballo.X < 0)
                {
                    value = 1;
                    seina.Play();
                }


            }


            if (ballo.Y > 500)
            {


                //CloseGame(false);
                if (ballo.X + 23 > kori.X)
                {
                    if (ballo.X + 23 < kori.X + kori.Width)
                    {
                        osui = true;
                        nappaus.Play();
                    }

                }
                if (osui == true)
                {
                    ballo.Y = -90;
                    Rectangle palkki = new Rectangle(0, 250, 800, 300);
                    osui = false;
                    op = op + 1;
                }
                else
                {
                    CloseGame(false);
                }
            }
            if (op > 1)
            {
                CloseGame(true);
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
            spriteBatch.Draw(pallo, ballo, Color.Beige);
            spriteBatch.Draw(Kori, kori, Color.Beige);
            if (op == 1)
            {
                spriteBatch.Draw(Palkki, palkki, Color.Beige);
            }
        }

    }
}

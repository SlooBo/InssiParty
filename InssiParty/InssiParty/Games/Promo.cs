using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InssiParty.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace InssiParty.Games
{


    /**
     * Väistä ATJ-Promoja
     * 
     * Väistä ATJ promotointeja tarpeeksi kauan!
     * 
     * By: Toni Sarvela
     */
    class Promo : GameBase
    {
        SpriteBatch spritebatch;

        //variablesit
        private int value;
        private int health;
        private int randY;
        private Vector2 inssi_kohta, inssi_nopeus, inssi_vauhti;
        private List<ATJ> ATJs;
        private Random random = new Random();
        private float timer;
        private float end_timer;
        private bool gameRunning;

        //musa ja äänet

        private Song promo_musa;
        private SoundEffect hitSound;

        //Tekstuurit

        private Texture2D inssi_tex;
        private Texture2D atj_tex;
        private Texture2D win_tex;
        private Texture2D lose_tex;

        //rectangle
        private Rectangle inssi_alue;
        private Rectangle background;
    
        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            spritebatch = new SpriteBatch(GraphicsDevice);
            inssi_tex = Content.Load<Texture2D>("inssi2");
            atj_tex = Content.Load<Texture2D>("atj");
            win_tex = Content.Load<Texture2D>("promo_voitto");
            lose_tex = Content.Load<Texture2D>("promo_häviö");
            promo_musa = Content.Load<Song>("promo_music");
            hitSound = Content.Load<SoundEffect>("promo_hit");
        }

        public override void Start()
        {
            Console.WriteLine("Starting Väistä ATJ-Promoja");
            MediaPlayer.Play(promo_musa);
            gameRunning = true;

            value = 1500;

            //inssin liikkeiden vektoreita
            inssi_kohta = new Vector2(400 - inssi_tex.Width /2 , 300 - inssi_tex.Height / 2);
            inssi_vauhti = Vector2.Zero;
            inssi_nopeus = new Vector2(5, 5);

            //rectangleja
            inssi_alue = new Rectangle((int)(inssi_kohta.X - inssi_tex.Width / 2),
                (int)(inssi_kohta.Y - inssi_tex.Height / 2), inssi_tex.Width, inssi_tex.Height);
            background = new Rectangle(0, 0, 800, 600);

            //muiden muutujien alustus
            randY = random.Next(100, 500);     
            ATJs = new List<ATJ>();
            health = 3;
            timer = 0;
            end_timer = 0;
        }

        public override void Stop()
        {
            Console.WriteLine("Closing Väistä ATJ-Promoja");
            MediaPlayer.Stop();
            ATJs = null;
        }

        public override void Update(GameTime gameTime)
        {

            value--;

            //liikkumistoiminnot
            inssi_vauhti = Vector2.Zero;

            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.D))
            {
                inssi_vauhti.X = inssi_nopeus.X;
            }

            if (keyboard.IsKeyDown(Keys.A))
            {
                inssi_vauhti.X = -inssi_nopeus.X;
            }

            if (keyboard.IsKeyDown(Keys.S))
            {
                inssi_vauhti.Y = inssi_nopeus.Y;
            }

            if (keyboard.IsKeyDown(Keys.W))
            {
                inssi_vauhti.Y = -inssi_nopeus.Y;
            }

            //rajaa pelaajan liikkumisen ruudun sisään
            inssi_kohta += inssi_vauhti;

            if (inssi_kohta.X < 0)
            {
                inssi_kohta.X = 0;
            }
            if (inssi_kohta.Y < 0)
            {
                inssi_kohta.Y = 0;
            }
            if (inssi_kohta.X > 800 - inssi_tex.Width / 2)
            {
                inssi_kohta.X = 800 - inssi_tex.Width / 2;
            }
            if (inssi_kohta.Y > 600 - inssi_tex.Height / 2)
            {
                inssi_kohta.Y = 600 - inssi_tex.Height / 2;
            }


            inssi_alue.X = (int)inssi_kohta.X;
            inssi_alue.Y = (int)inssi_kohta.Y;

            timer++;

            //vihollisten spawnaus
            if (gameRunning == true)
            {
                if (timer >= 20)
                {
                    ATJs.Add(new ATJ(atj_tex, new Vector2(900, randY)));
                    timer = 0;

                    for (int i = 0; i < ATJs.Count; i++)
                    {
                        if (!ATJs[i].Visible)
                        {
                            ATJs.RemoveAt(i);
                            i--;
                        }
                    }
                }
            }

            //ATJ objektien ja inssin collsioncheck
            if (gameRunning == true)
            {
                foreach (ATJ atj in ATJs)
                {
                    atj.Update();

                    if (atj.atj_alue.Intersects(inssi_alue))
                    {
                        hitSound.Play();
                        health--;
                        atj.Hit = false;
                    }

                }

                //poistaa objektin jos se osuu pelaajaan
                for (int i = 0; i < ATJs.Count; i++)
                {
                    if (!ATJs[i].Hit)
                    {
                        ATJs.RemoveAt(i);
                        i--;
                    }
                }
            }
            //lopettaa pelin jos inssin hp menee nollaan
            if (health <= 0)
            {         
                gameRunning = false;
                value++;
                end_timer++;
                ATJs = null;

                if (end_timer >= 150)
                {
                    CloseGame(false);
                }
            }

            //lopettaa pelin, kun aika loppuu
            if (value <= 0)
            {
                gameRunning = false;
                value++;
                end_timer++;
                ATJs = null;

                if (end_timer >= 150)
                {
                    CloseGame(true);
                }
            }
        }
        
        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (gameRunning == true)
            {
                foreach (ATJ ATJ in ATJs)
                {
                    ATJ.Draw(spriteBatch);
                }
                spriteBatch.Draw(inssi_tex, inssi_kohta, Color.White);
            }

            if (health <= 0)
            {
                spriteBatch.Draw(lose_tex, background, Color.White);
            }
            if (value <= 0)
            {
                spriteBatch.Draw(win_tex, background, Color.White);
            }
        }
    }
}

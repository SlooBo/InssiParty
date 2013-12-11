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

        //variablesit
        private int value;
        private int health;
        private int randY;
        private Vector2 inssi_kohta, inssi_nopeus, inssi_vauhti;
        private List<ATJ> ATJs;
        private Random random = new Random();
        private float timer;
        private float end_timer;
        private double bar_timer;
        private bool gameRunning;
        private bool win;

        //musa ja äänet

        private Song promo_musa;
        private SoundEffect hitSound;
        private SoundEffect laugh;
        private SoundEffect applause;

        //Tekstuurit

        private Texture2D inssi_tex;
        private Texture2D atj_tex;
        private Texture2D win_tex;
        private Texture2D lose_tex;
        private Texture2D bg_tex;
        private Texture2D timer_bar;

        //rectangle
        private Rectangle inssi_alue;
        private Rectangle background;
        private Rectangle timer_rec;
    
        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            inssi_tex = Content.Load<Texture2D>("inssi2");
            atj_tex = Content.Load<Texture2D>("atj");
            win_tex = Content.Load<Texture2D>("promo_voitto");
            lose_tex = Content.Load<Texture2D>("promo_häviö");
            bg_tex = Content.Load<Texture2D>("promo_bg");
            timer_bar = Content.Load<Texture2D>("timer_bar");
            promo_musa = Content.Load<Song>("promo_music");
            hitSound = Content.Load<SoundEffect>("promo_hit");
            applause = Content.Load<SoundEffect>("promo_clap");
            laugh = Content.Load<SoundEffect>("promo_laugh");
        }


        public override void Start()
        {
            Console.WriteLine("Starting Väistä ATJ-Promoja");
            MediaPlayer.Play(promo_musa);
            gameRunning = true;
            win = false;

            value = 1600;

            //inssin liikkeiden vektoreita
            inssi_kohta = new Vector2(400 - inssi_tex.Width /2 , 300 - inssi_tex.Height / 2);
            inssi_vauhti = Vector2.Zero;
            inssi_nopeus = new Vector2(5, 5);

            //rectangleja
            inssi_alue = new Rectangle((int)(inssi_kohta.X - inssi_tex.Width / 2),
                (int)(inssi_kohta.Y - inssi_tex.Height / 2), inssi_tex.Width, inssi_tex.Height);
            background = new Rectangle(0, 0, 800, 600);
            timer_rec = new Rectangle(0, 590, 800, 10);

            //muiden muutujien alustus
            randY = random.Next(100, 500);     
            ATJs = new List<ATJ>();
            health = 3;
            timer = 0;
            end_timer = 0;
            bar_timer = 0;
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
            bar_timer += 1;

            if (bar_timer >= 2)
            {
                bar_timer = 0;
                timer_rec.Width--;
            }
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
            if (inssi_kohta.X > 800 - inssi_tex.Width)
            {
                inssi_kohta.X = 800 - inssi_tex.Width;
            }
            if (inssi_kohta.Y > 600 - inssi_tex.Height)
            {
                inssi_kohta.Y = 600 - inssi_tex.Height;
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
                if (end_timer == 1)
                    laugh.Play();

                if (end_timer >= 150)
                {
                    CloseGame(false);
                }
            }

            //lopettaa pelin, kun aika loppuu
            if (value <= 0)
            {
                gameRunning = false;
                end_timer++;
                ATJs = null;
                win = true;
                if (end_timer == 1)
                    applause.Play();

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
                spriteBatch.Draw(bg_tex, background, Color.White);
                spriteBatch.Draw(timer_bar, timer_rec, Color.White);
                spriteBatch.Draw(inssi_tex, inssi_kohta, Color.White);
                foreach (ATJ ATJ in ATJs)
                {
                    ATJ.Draw(spriteBatch);
                }  
            }

            if (health <= 0)
            {
                spriteBatch.Draw(lose_tex, background, Color.White);
            }
            if (win == true)
            {
                spriteBatch.Draw(win_tex, background, Color.White);
            }
        }
    }
}

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
    /**
     * Speedtester
     * 
     * Painele A, D, K ja L -näppäimiä syttyvien "valojen" mukaisesti. Jos painat huti, häviät pelin. 
     * Jos taas selviät tietyn ajan, olet voittaja.
     * 
     * By: Annika Veteli
     */
    class speedtester : GameBase
    {
        private int value;

        //ladattavat kuvat
        Texture2D taustakuva; //esitellään taustakuva
        Texture2D haaleanappikuva; //esitellään nappikuva
        Texture2D gameOveranimaatio; // esitellään gameoverkuva
        Texture2D winwinAnimaatio;
       
        //fontti
        SpriteFont fontti; // esitellään fontti

        //muuttujia
        int nappi;
        int pisteet;
        int aika;

        Random random;

        //näppäimistö
        KeyboardState keys;
        KeyboardState previous;

        //äänet
        private bool soundLoaded;
        SoundEffect Gameover;

        //ruutu
        public static Vector2 ruudunKoko =  new Vector2(800, 600);

        //animaatio
        float animation_timer = 0.0f; //animaation ajastus
        int animation_frame_count = 5; // animaation kehysten määrä
        int gameanimation_frame = 3; //oma kehysten määrä gameoverille

        //bool muuttujat
        bool gameover;
        bool win;
        bool gameRunning;

        //animaatio
        int animation_frame_countInssi; // animaation kehysten määrä
        float animation_timerInssi = 0.0f;
        Texture2D insinööritaputtaa;
        Texture2D puhekuplanen;

        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            taustakuva = Content.Load<Texture2D>("taustaanimaatio"); // ladataan taustakuva
            haaleanappikuva = Content.Load<Texture2D>("nappi"); // ladataan nappi
            fontti = Content.Load<SpriteFont>("DefaultFont"); // ladataan fontti
            gameOveranimaatio = Content.Load<Texture2D>("gameover"); //ladataan gameover animaatio
            winwinAnimaatio = Content.Load<Texture2D>("winwin");
            insinööritaputtaa = Content.Load<Texture2D>("insinööritaputus");
            puhekuplanen = Content.Load<Texture2D>("puhekupla");

            try
            {
                Gameover = Content.Load<SoundEffect>("naurumies"); //ladataan gameover ääni
                soundLoaded = true;
            }
            catch (Exception eek)
            {
                Console.WriteLine(eek.Message);
                soundLoaded = false;
            }
            
        }

        public override void Start()
        {
            //Console.WriteLine("Starting hello world");

            random = new Random();
            nappi = random.Next(1, 4); //määritellään random nappi
            gameover = false;
            win = false;
            gameRunning = true;
            pisteet = 0;

            value = 500;
        }

        public override void Stop()
        {
            //Console.WriteLine("Closing hello world");
        }

        public override void Update(GameTime gameTime)
        {
            value--;

            if (gameRunning == true)
            {
                aika = aika + 1;
                if (aika >= 60)
                {
                    aika = 0;
                    nappi = random.Next(1, 5);
                    pisteet = pisteet - 1; //vähennetään pisteitä
                    gameRunning = false;
                    gameover = true;

                    if (soundLoaded)
                    {
                        Gameover.Play();
                    }
                }
            }

            //määritykset yhdelle painallukselle
            previous = keys; 
            keys = Keyboard.GetState();

            if ( gameRunning == true)
            {

                //Määritetään A-näppäin ja pistelaskuri
                if (keys.IsKeyDown(Keys.A) && previous.IsKeyUp(Keys.A)) //jos A-näppäin on painettuna alas
                {
                    if (nappi == 1) //ja jos nappi on yksi
                    {
                        nappi = random.Next(1, 5);
                        pisteet = pisteet + 1; //lisätään pisteitä
                        aika = +1; 
                    }
                    else
                    {
                        gameover = true; //jos ei paineta gameover true
                        gameRunning = false;
                        if (soundLoaded)
                        {
                            Gameover.Play();
                        }
                    }
                }

                if (keys.IsKeyDown(Keys.S) && previous.IsKeyUp(Keys.S))//määritetään jos s-näppäin on pohjassa
                {
                    if (nappi == 2) // jos nappi on 2
                    {
                        nappi = random.Next(1, 5);
                        pisteet = pisteet + 1; // lisätään piste
                        aika = 0;
                    }
                    else
                    {
                        gameover = true;
                        gameRunning = false;
                        if (soundLoaded)
                        {
                            Gameover.Play();
                        }
                    }
                }
                if (keys.IsKeyDown(Keys.K) && previous.IsKeyUp(Keys.K))
                {
                    if (nappi == 3)
                    {
                        nappi = random.Next(1, 5);
                        pisteet = pisteet + 1;
                        aika = 0;
                    }
                    else
                    {
                        gameover = true;
                        gameRunning = false;
                        if (soundLoaded)
                        {
                            Gameover.Play();
                        }
                    }
                }
                if (keys.IsKeyDown(Keys.L) && previous.IsKeyUp(Keys.L))
                {
                    if (nappi == 4)
                    {
                        nappi = random.Next(1, 5);
                        pisteet = pisteet + 1;
                        aika = 0;
                    }
                    else
                    {
                        gameover = true;
                        gameRunning = false;
                        if (soundLoaded)
                        {
                            Gameover.Play();
                        }
                    }
                }

                if (value == 100)
                {
                    win = true;
                    gameRunning = false;
                }
            }

            if (value == 0 && win == true && gameover == false)
            {
                CloseGame(true);
            }

            if (gameover == true && win == false && value == 0)
            {
                CloseGame(false);
            }

            //if (value < 0)
            //{
            //    //sammuta peli, true jos voitto tapahtui, false jos pelaaaja hävisi.
            //    CloseGame(true);
            //}
        }

        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //Console.WriteLine("Draw " + value);

            animation_timer += 0.08f;
            int currentFrame = (int)(animation_timer % animation_frame_count);

            spriteBatch.Draw(taustakuva, new Rectangle(0, 0, (int)ruudunKoko.X, (int)ruudunKoko.Y), new Rectangle(taustakuva.Width / animation_frame_count * currentFrame, 0, taustakuva.Width / animation_frame_count, taustakuva.Height),
            Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.0F);

            if (gameRunning == true)
            {

                for (int i = 1; i < 5; i++)
                {
                    Color button3 = Color.White;
                    float savy = 0.25f;
                    if (nappi == i)
                    {
                        savy = 1.0f;
                    }
                    if (i == 1)
                    {
                        button3 = Color.Yellow;
                    }
                    if (i == 2)
                    {
                        button3 = Color.Orange;
                    }
                    if (i == 3)
                    {
                        button3 = Color.Green;
                    }
                    if (i == 4)
                    {
                        button3 = Color.Red;
                    }
   
                    spriteBatch.Draw(haaleanappikuva, new Rectangle(i * 140, 200, 140, 160), button3 * savy);
                }
            }
            spriteBatch.DrawString(fontti, "Pisteet: " + pisteet.ToString(), new Vector2(40, 20), Color.White);

            if (win == true && gameRunning == false)
            {
                animation_timer += 0.08f;
                gameanimation_frame = 3;
                animation_frame_countInssi = 2;

                animation_timerInssi += 0.15f;
                
                int currentFrame1 = (int)(animation_timerInssi % animation_frame_countInssi);

                spriteBatch.Draw(winwinAnimaatio, new Vector2(100, 200), new Rectangle(winwinAnimaatio.Width / gameanimation_frame * currentFrame, 0, winwinAnimaatio.Width / gameanimation_frame, winwinAnimaatio.Height),
                Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0.0F);

                spriteBatch.Draw(insinööritaputtaa, new Vector2(500, 400), new Rectangle(insinööritaputtaa.Width / animation_frame_countInssi * currentFrame1, 0, insinööritaputtaa.Width / animation_frame_countInssi, insinööritaputtaa.Height),
                 Color.White, 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.0F);

                spriteBatch.Draw(puhekuplanen, new Vector2(245, 370), Color.White);
                spriteBatch.DrawString(fontti, "Kappas et olekaan", new Vector2(254, 390), Color.RoyalBlue);
                spriteBatch.DrawString(fontti, "kädetön inssi", new Vector2(255, 410), Color.RoyalBlue);
            } 

            if (gameover == true && gameRunning == false)
            {
                animation_timer += 0.08f;
                gameanimation_frame = 3;

                spriteBatch.Draw(gameOveranimaatio, new Vector2(100, 200), new Rectangle(gameOveranimaatio.Width / gameanimation_frame * currentFrame, 0, gameOveranimaatio.Width / gameanimation_frame, gameOveranimaatio.Height),
                Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0.0F);
                spriteBatch.DrawString(fontti, "Gameover", new Vector2(400, 20), Color.White);
            }

        }

    }
}

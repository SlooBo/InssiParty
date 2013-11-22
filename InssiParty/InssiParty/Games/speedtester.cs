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
     * Selitys pelistä
     * 
     * By: Annika Veteli
     */
    class speedtester : GameBase
    {
        //Mahdolliset variablet mitä tarvitset pelin aikana on hyvä listata tässä kohdassa.
        private int value;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D taustakuva; //ladataan taustakuva
        Texture2D haaleanappikuva; //
        Texture2D gameOveranimaatio;
        SpriteFont fontti;
        int nappi;
        Random random;
        bool gameover = false; //määritellään bool tyyppinen gameover muuttuja, oletusarvoksi false
        int pisteet;
        int aika;
        KeyboardState keys;
        KeyboardState previous;
        SoundEffect Gameover;

        public static Vector2 ruudunKoko =  new Vector2(800, 600);

        float animation_timer = 0.0f; //animaation ajastus
        int animation_frame_count = 5; // animaation kehysten määrä
        int gameanimation_frame = 3;

        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            taustakuva = Content.Load<Texture2D>("taustaanimaatio"); // ladataan taustakuva
            haaleanappikuva = Content.Load<Texture2D>("nappi"); // ladataan nappi
            fontti = Content.Load<SpriteFont>("DefaultFont"); // ladataan fontti
           
            
            //Gameover = Content.Load<SoundEffect>("naurumies"); //ladataan gameover ääni
            
            
            gameOveranimaatio = Content.Load<Texture2D>("gameover"); //ladataan gameover anim
        }

        /**
         * Kaikki mitä pitää tehdä kun peli käynnistyy.
         * 
         * Esimerkiksi aseta muuttujat tarvittaviin arvoihin, tai käynnistä musiikki.
         */
        public override void Start()
        {
            Console.WriteLine("Starting hello world");

            random = new Random();
            nappi = random.Next(1, 4); //määritellään random nappi

            value = 500;
        }

        /**
         * Ajetaan kun peli sulkeutuu. Piilota äänet ja puhdista roskasi seuraavaa peliä varten.
         */
        public override void Stop()
        {
            Console.WriteLine("Closing hello world");
        }

        /**
         * Ajetaan kun peliä pitää päivittää. Tänne menee itse pelin logiikka koodi,
         * törmäys chekkaukset, pisteen laskut, yms.
         * 
         * gameTime avulla voidaan synkata nopeus tasaikseksi vaikka framerate ei olisi tasainen.
         */
        public override void Update(GameTime gameTime)
        {
            value--;


            if (gameover == false)
            {
                aika = aika + 1;
                if (aika >= 60)
                {
                    aika = 0;
                    nappi = random.Next(1, 5);
                    //miinuspisteet
                    pisteet = pisteet - 1;
                    gameover = true;

                    //Gameover.Play();

                }
            }

            //määritykset yhdelle painallukselle
            previous = keys; //
            keys = Keyboard.GetState();

            //Määritetään A-näppäin ja pistelaskuri
            if (keys.IsKeyDown(Keys.A) && previous.IsKeyUp(Keys.A)) //jos A-näppäin on painettuna alas
            {
                if (nappi == 1) //ja jos nappi on yksi
                {
                    nappi = random.Next(1, 5);
                    pisteet = pisteet + 1; //lisätään pisteitä
                    aika = +1; // aika on nolla?
                }
                else
                {
                    gameover = true; //jos ei paineta gameover true
                    //Gameover.Play(); // soitetaan ääni
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
                    //Gameover.Play();
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
                    //Gameover.Play();
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
                    //Gameover.Play();
                }
            }


            if (value < 0)
            {
                //sammuta peli, true jos voitto tapahtui, false jos pelaaaja hävisi.
                CloseGame(true);
            }
        }

        /**
         * Pelkkä piirtäminen
         * 
         * ELÄ sijoita pelilogiikkaa tänne.
         *
         * gameTime avulla voidaan synkata nopeus tasaikseksi vaikka framerate ei olisi tasainen.
         */
        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Console.WriteLine("Draw " + value);

            animation_timer += 0.08f;
            int currentFrame = (int)(animation_timer % animation_frame_count);

            //Rectangle screenRetangle = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(taustakuva, new Rectangle(0, 0, (int)ruudunKoko.X, (int)ruudunKoko.Y), new Rectangle(taustakuva.Width / animation_frame_count * currentFrame, 0, taustakuva.Width / animation_frame_count, taustakuva.Height),
            Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.0F);

            if (gameover == false)
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

            if (gameover == true)
            {
                animation_timer += 0.08f;
                gameanimation_frame = 3;

                spriteBatch.Draw(gameOveranimaatio, new Vector2(100, 200), new Rectangle(gameOveranimaatio.Width / gameanimation_frame * currentFrame, 0, gameOveranimaatio.Width / gameanimation_frame, gameOveranimaatio.Height),
                Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0.0F);
                spriteBatch.DrawString(fontti, "Gameover", new Vector2(400, 20), Color.White);
            }

            ////spriteBatch.Draw funktiolla voit piirtää ruudulle.
            ////Palikka piirretään y akselilla, valuen kohtaan
            //spriteBatch.Draw(spriteBox, new Vector2(50, value), Color.White);
        }

    }
}

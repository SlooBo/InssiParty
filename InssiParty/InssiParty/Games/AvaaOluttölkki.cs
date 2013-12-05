using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InssiParty.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using InssiParty.Games.FeedGameSrc;


namespace InssiParty.Games
{

    /**
     * Uuden pelin luominen
     * 
     * -> kopio tämä filu, ja nimeä se ja classin nimi uusiksi
     * -> lisää listaan InssiGame.cs tiedostossa
     * 
     */

    /**
     * Classin nimi pitää vaihtaa, mieluiten sama kuin tiedoston nimi.
     * IGameBase pitää jättää semmoiseksi
     */

    /**
     * Avaa oluttölkki
     * 
     * Auta inssiä avaamaan oluttölkki ja välttämään janoon kuoleminen.
     * 
     * By: Markus Tolvanen
     */
    class Olut : GameBase
    {
        //Esitellään muuttujat
        int nappi;
        private int value;
        private string [] numValues;
        private int fail_counter;
        private int success_counter;
        Random random;
        private string syöte;
        int painettava;
        int aika;

        KeyboardState keys;
        KeyboardState previous;

        //Esitellään tekstuurit
        private Texture2D Can;
        private Texture2D OpeningCan;
        private Texture2D EmptyCan;
        SpriteFont font;
        private Texture2D backround_texture;
        private Texture2D gameover;

        //bool gameover = false;  //samaa muuttujaa alustat kaksi kertaa
        bool win = false;
        bool gameRunning = true;
        
        /**
         * 
         * 
         * Ladataan tekstuurit
         */
        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //Tiedoston pitäisi olla InssiPartyContent projektin alla solution explorerissa.
            Can = Content.Load<Texture2D>("Can");
            OpeningCan = Content.Load<Texture2D>("Can-Hissing");
            EmptyCan = Content.Load<Texture2D>("Can-Empty");
            font = Content.Load<SpriteFont>("DefaultFont");
            backround_texture = Content.Load<Texture2D>("FeedGame_background");
           // gameover = Content.Load<Texture2D>("gameover");
            //Boolean johonka laitetaan tekstuuria jota myöhemmin käytetään äänenä ?!?
        }

        /**
         *
         * 
         * Annetaan kirjaimille numeroarvot ja alustetaan laskimet nollille
         */
        public override void Start()
        {
            Console.WriteLine("Starting Avaa oluttölkki");

            value = 500;

            Console.WriteLine("Starting hello world");

            random = new Random();
            
            fail_counter=0;

            success_counter=0;

            painettava = random.Next(1, 4); //määritellään random nappi

            backround_texture = Can;
        }

        /**
         * Ajetaan kun peli sulkeutuu. Piilota äänet ja puhdista roskasi seuraavaa peliä varten.
         */
        public override void Stop()
        {
            Console.WriteLine("Closing Avaa oluttölkki");
        }

        /**
         * Ajetaan kun peliä pitää päivittää. Tänne menee itse pelin logiikka koodi,
         * törmäys chekkaukset, pisteen laskut, yms.
         * 
         * gameTime avulla voidaan synkata nopeus tasaikseksi vaikka framerate ei olisi tasainen.
         */
        public override void Update(GameTime gameTime)
        {
            /*
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

            if (gameRunning == true)
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
            

            if (success_counter == 5)
            {
                OpeningCan = EmptyCan;

                //sammuta peli, true jos voitto tapahtui, false jos pelaaaja hävisi.
                CloseGame(true);
            }

            else if (fail_counter == 3)
            {

                OpeningCan = gameover;
                CloseGame(false);

            }
              */
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

            //spriteBatch.Draw funktiolla voit piirtää ruudulle.
            //Palikka piirretään y akselilla, valuen kohtaan

            spriteBatch.DrawString(font, "Paina: " + numValues[painettava],new Vector2(10, 10), Color.Turquoise);

            spriteBatch.Draw(backround_texture, new Vector2(100, 100), Color.White);

            spriteBatch.Draw(Can, new Vector2(100, 100), Color.White);

            spriteBatch.DrawString(font, numValues[painettava], new Vector2(100, 100), Color.White);

            spriteBatch.Draw(OpeningCan, new Vector2(100, 100), Color.White);

            spriteBatch.Draw(EmptyCan, new Vector2(100,100), Color.White);

            

         




        }

    }
}
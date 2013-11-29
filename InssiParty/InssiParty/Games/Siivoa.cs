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
     * Uuden pelin luominen
     * 
     * -> kopio tämä filu, ja nimeä se ja classin nimi uusiksi
     * -> lisää listaan InssiGame.cs tiedostoss

    /**
     * Siivoa insinöörin kämppä
     * 
     * Selitys pelistä
     * 
     * By: Annika Veteli
     */
    class Siivoa : GameBase
    {
        //Mahdolliset variablet mitä tarvitset pelin aikana on hyvä listata tässä kohdassa.
        private int value;

        Texture2D taustakuva;
        Texture2D kuvaJuissi;
        Texture2D käsi;
        Texture2D ohjain;
        Texture2D alkomahooli;
        Texture2D kaukosäädin;
        Texture2D äänisäädin;
        Texture2D sytkäri;
        Texture2D insinöörikommentoi;
        Texture2D puhekuplaEmpty;

        SpriteFont fontti;

        private Rectangle juissi_alue;
        private Vector2 juissi_vektori;

        private Rectangle ohjain_alue;
        private Vector2 ohjain_vektori;

        private Rectangle alkomahooli_alue;
        private Vector2 alkomahooli_vektori;

        private Rectangle kaukosäädin_alue;
        private Vector2 kaukosäädin_vektori;

        private Rectangle äänisäädin_alue;
        private Vector2 äänisäädin_vektori;

        private Rectangle sytkäri_alue;
        private Vector2 sytkäri_vektori;

        bool juissiOlemassa = true;
        bool ohjainOlemassa = true;
        bool alkomahooliOlemassa = true;
        bool kaukosäädinOlemassa = true;
        bool äänisäädinOlemassa = true;
        bool sytkäriOlemassa = true;

        bool voitto = false;
        bool häviö = false;

        MouseState currentMouseState;
        MouseState lastMouseState;
      
        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //spriteBatch = new SpriteBatch(GraphicsDevice);
            taustakuva = Content.Load<Texture2D>("pöytä");
            kuvaJuissi = Content.Load<Texture2D>("juissi");
            käsi = Content.Load<Texture2D>("aloituskasi");
            ohjain = Content.Load<Texture2D>("ohjain");
            alkomahooli = Content.Load<Texture2D>("sol");
            kaukosäädin = Content.Load<Texture2D>("tvkaukosäädin");
            äänisäädin = Content.Load<Texture2D>("äänisäädin");
            sytkäri = Content.Load<Texture2D>("sytkäri");
            insinöörikommentoi = Content.Load<Texture2D>("insinööricopy");
            puhekuplaEmpty = Content.Load<Texture2D>("puhekupla");
            fontti = Content.Load<SpriteFont>("DefaultFont");
            
        }

        /**
         * Kaikki mitä pitää tehdä kun peli käynnistyy.
         * 
         * Esimerkiksi aseta muuttujat tarvittaviin arvoihin, tai käynnistä musiikki.
         */
        public override void Start()
        {
            Console.WriteLine("Starting hello world");
            juissi_vektori = new Vector2(500, 125);
            juissi_alue = new Rectangle((int)juissi_vektori.X, (int)juissi_vektori.Y, kuvaJuissi.Width, kuvaJuissi.Height);

            ohjain_vektori = new Vector2(260, 90);
            ohjain_alue = new Rectangle((int)ohjain_vektori.X, (int)ohjain_vektori.Y, ohjain.Width, ohjain.Height);

            alkomahooli_vektori = new Vector2(100, 100);
            alkomahooli_alue = new Rectangle((int)alkomahooli_vektori.X, (int)alkomahooli_vektori.Y, alkomahooli.Width, alkomahooli.Height);

            kaukosäädin_vektori = new Vector2(370, 250);
            kaukosäädin_alue = new Rectangle((int)kaukosäädin_vektori.X, (int)kaukosäädin_vektori.Y, kaukosäädin.Width, kaukosäädin.Height);

            äänisäädin_vektori = new Vector2(200, 250);
            äänisäädin_alue = new Rectangle((int)äänisäädin_vektori.X, (int)äänisäädin_vektori.Y, äänisäädin.Width, äänisäädin.Height);

            sytkäri_vektori = new Vector2(115, 350);
            sytkäri_alue = new Rectangle((int)sytkäri_vektori.X, (int)sytkäri_vektori.Y, sytkäri.Width + 90, sytkäri.Height + 20);
           
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

            var MouseState = Mouse.GetState();
            var MousePosition = new Point(MouseState.X, MouseState.Y);

            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

                if (juissi_alue.Contains(MousePosition))
                {
                    if (lastMouseState.LeftButton == ButtonState.Released &&
                            currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        juissiOlemassa = false;
                    }
                }

                if (ohjain_alue.Contains(MousePosition))
                {
                    if (lastMouseState.LeftButton == ButtonState.Released &&
                           currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        ohjainOlemassa = false;
                    }
                }

                if (alkomahooli_alue.Contains(MousePosition))
                {
                    if (lastMouseState.LeftButton == ButtonState.Released &&
                           currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        alkomahooliOlemassa = false;
                    }

                }

                if (kaukosäädin_alue.Contains(MousePosition))
                {
                    if (lastMouseState.LeftButton == ButtonState.Released &&
                          currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        kaukosäädinOlemassa = false;
                    }

                }

                if (äänisäädin_alue.Contains(MousePosition))
                {
                    if (lastMouseState.LeftButton == ButtonState.Released &&
                            currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        äänisäädinOlemassa = false;
                    }
                }

                if (sytkäri_alue.Contains(MousePosition))
                {
                    if (lastMouseState.LeftButton == ButtonState.Released &&
                        currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        sytkäriOlemassa = false;
                    }
                }
            

            if (juissiOlemassa == false && ohjainOlemassa == false && alkomahooliOlemassa == false &&
                    sytkäriOlemassa == false && kaukosäädinOlemassa == false && äänisäädinOlemassa == false)
            {
                voitto = true;
            }

            if (voitto == true && value == 0)
            {
                CloseGame(true);
            }
            else if (value == 90 && voitto == false)
            {
                häviö = true;
            }

            if (value == 0 && voitto == false)
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
            Console.WriteLine("Draw " + value);

            spriteBatch.Draw(taustakuva, new Rectangle(0, 0, 800, 600), Color.White);

            spriteBatch.Draw(insinöörikommentoi, new Vector2(710, 125), Color.White);
            spriteBatch.Draw(puhekuplaEmpty, new Vector2(525, 35), Color.White);

            if (voitto == false && häviö == false)
            {
                if (juissiOlemassa == true)
                {
                    spriteBatch.Draw(kuvaJuissi, juissi_vektori, Color.White);
                }

                if (ohjainOlemassa == true)
                {
                    spriteBatch.Draw(ohjain, ohjain_vektori, Color.White);
                }
                if (alkomahooliOlemassa == true)
                {
                    spriteBatch.Draw(alkomahooli, alkomahooli_vektori, Color.White);
                }
                if (kaukosäädinOlemassa == true)
                {
                    spriteBatch.Draw(kaukosäädin, kaukosäädin_vektori, Color.White);
                }
                if (äänisäädinOlemassa == true)
                {
                    spriteBatch.Draw(äänisäädin, äänisäädin_vektori, Color.White);
                }
                if (sytkäriOlemassa == true)
                {
                    spriteBatch.Draw(sytkäri, sytkäri_vektori, Color.White);
                }
                if (value < 500 && value > 400)
                {
                    spriteBatch.DrawString(fontti, "HopiHopi !!", new Vector2(545, 50), Color.Red);
                }
                else if (value < 400 && value > 300)
                {
                    spriteBatch.DrawString(fontti, "Ei tässä ole", new Vector2(545, 50), Color.Red);
                    spriteBatch.DrawString(fontti, "koko päivää aikaa", new Vector2(535, 75), Color.Red);
                }
                else if (value < 300 && value > 200)
                {
                    spriteBatch.DrawString(fontti, "Yrittäisit nyt", new Vector2(545, 50), Color.Red);
                    spriteBatch.DrawString(fontti, "...", new Vector2(560, 75), Color.Red);
                }
                else if (value < 200 && value > 100)
                {
                    spriteBatch.DrawString(fontti, "Ei tsiisus", new Vector2(545, 50), Color.Red);
                    spriteBatch.DrawString(fontti, "...", new Vector2(560, 75), Color.Red);
                }
                spriteBatch.Draw(käsi, new Vector2(Mouse.GetState().X - 100, Mouse.GetState().Y - 100), Color.White);
             
            }
           

            if (voitto == true && häviö == false)
            {
                spriteBatch.DrawString(fontti, "Vihdoinkin!", new Vector2(545, 50), Color.Red);
            }

            if (voitto == false && häviö == true)
            {
                spriteBatch.DrawString(fontti, "Sinä saatanan", new Vector2(545, 50), Color.Red);
                spriteBatch.DrawString(fontti, "kelvoton insinööri", new Vector2(535, 75), Color.Red);
            }
        }

    }
}
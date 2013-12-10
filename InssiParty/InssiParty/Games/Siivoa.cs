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
     * Siivoa insinöörin kämppä
     * 
     * Klikkaile hiiren vasemmalla painikkeella kaikki tavarat pois pöydältä ennen kuin aika loppuu
     * 
     * By: Annika Veteli
     */
    class Siivoa : GameBase
    {
        private int value;
        
        //tekstuureja
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

        //fontti
        SpriteFont fontti;

        //rectanglet ja vektorit
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

        //boolit
        bool juissiOlemassa;
        bool ohjainOlemassa;
        bool alkomahooliOlemassa;
        bool kaukosäädinOlemassa;
        bool äänisäädinOlemassa;
        bool sytkäriOlemassa ;

        bool voitto;
        bool häviö;

        private bool SoundLoaded;

        //hiiri
        MouseState currentMouseState;
        MouseState lastMouseState;

        //ääni
        SoundEffect piu;
      
        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
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

            try
            {
                piu = Content.Load<SoundEffect>("PIUcut");
                SoundLoaded = true;
            }
            catch (Exception eek)
            {
                Console.WriteLine(eek.Message);

                SoundLoaded = false;
            }
            
            
        }

        public override void Start()
        {
            //Console.WriteLine("Starting hello world");

            //alustukset tavaroiden sijainneille ja rectangleille
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

           //alustukset tavatoiden olemassa ololle 
           juissiOlemassa = true;
           ohjainOlemassa = true;
           alkomahooliOlemassa = true;
           kaukosäädinOlemassa = true;
           äänisäädinOlemassa = true;
           sytkäriOlemassa = true;

           //alustukset voitolle ja häviölle
           voitto = false;
           häviö = false;
        }

        public override void Stop()
        {
            //Console.WriteLine("Closing hello world");
        }

        public override void Update(GameTime gameTime)
        {
            value--;

            var MouseState = Mouse.GetState();
            var MousePosition = new Point(MouseState.X, MouseState.Y);

            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            //tarkistuksia onko hiiri tavaran alueella ja onko hiiren vasen klikattuna, jos on tavaran olemassaolo on false -> tavara häviää
                if (juissi_alue.Contains(MousePosition))
                {
                    if (lastMouseState.LeftButton == ButtonState.Released &&
                            currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        piu.Play();
                        juissiOlemassa = false;
                    }
                }

                if (ohjain_alue.Contains(MousePosition))
                {
                    if (lastMouseState.LeftButton == ButtonState.Released &&
                           currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        piu.Play();
                        ohjainOlemassa = false;
                    }
                }

                if (alkomahooli_alue.Contains(MousePosition))
                {
                    if (lastMouseState.LeftButton == ButtonState.Released &&
                           currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        piu.Play();
                        alkomahooliOlemassa = false;
                    }

                }

                if (kaukosäädin_alue.Contains(MousePosition))
                {
                    if (lastMouseState.LeftButton == ButtonState.Released &&
                          currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        piu.Play();
                        kaukosäädinOlemassa = false;
                    }

                }

                if (äänisäädin_alue.Contains(MousePosition))
                {
                    if (lastMouseState.LeftButton == ButtonState.Released &&
                            currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        piu.Play();
                        äänisäädinOlemassa = false;
                    }
                }

                if (sytkäri_alue.Contains(MousePosition))
                {
                    if (lastMouseState.LeftButton == ButtonState.Released &&
                        currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        piu.Play();
                        sytkäriOlemassa = false;
                    }
                }
            
            //tarkistus voitolle, kaikkien tavaroiden tulee olla olemassaololtaan falseja
            if (juissiOlemassa == false && ohjainOlemassa == false && alkomahooliOlemassa == false &&
                    sytkäriOlemassa == false && kaukosäädinOlemassa == false && äänisäädinOlemassa == false)
            {
                voitto = true;
            }

            //sammutetaan peli truena jos voitto on tosi ja value on 0
            if (voitto == true && value == 0 && häviö == false)
            {
                CloseGame(true);
            }

            //jos value on 90 ja voitto false -> häviö muuttuu todeksi
            if (value == 90 && voitto == false)
            {
                häviö = true;
            }

            //sammutetaan peli falsena, jos value on 0 ja voitto false ja häviö true
            if (value == 0 && voitto == false && häviö == true)
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

            spriteBatch.Draw(taustakuva, new Rectangle(0, 0, 800, 600), Color.White);

            spriteBatch.Draw(insinöörikommentoi, new Vector2(710, 125), Color.White);
            spriteBatch.Draw(puhekuplaEmpty, new Vector2(525, 35), Color.White);

            if (voitto == false && häviö == false) //tarkistetaan ettei häviötä/voittoa ole tapahtunut
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
           

            if (voitto == true && häviö == false) // voittokommentti
            {
                spriteBatch.DrawString(fontti, "Vihdoinkin!", new Vector2(545, 50), Color.Red);
            }

            if (voitto == false && häviö == true) //häviökommentti
            {
                spriteBatch.DrawString(fontti, "Sinä saatanan", new Vector2(545, 45), Color.Red);
                spriteBatch.DrawString(fontti, "kelvoton", new Vector2(539, 60), Color.Red);
                spriteBatch.DrawString(fontti, "insinööri", new Vector2(545, 79), Color.Red);
            }
        }

    }
}
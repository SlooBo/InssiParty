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

    /**
     * Lampun vaihto
     * 
     * Auta insinööriä vaihtamaan kattolamppu.
     * Muista kytkeä virta pois ennen uuden lampun asettamista.
     * By: Jari Tuomainen
     */
    class Lampunvaihto : GameBase
    {
        //Variablet
        private int value;
        private int add = 0;
        private int power = 0;
        private Vector2 cursorPos;
        Rectangle cursorRect = new Rectangle(0, 0, 1, 1);
        Rectangle Lamppu = new Rectangle(300, 180, 180, 180);
        Rectangle Katkaisin = new Rectangle(530, 285, 30, 30);
        Rectangle Uusilamppu = new Rectangle(710, 500, 60, 50);
        Rectangle Hollilla = new Rectangle(300, 240, 180, 180);


        //Tekstuurit
        private Texture2D Lamppupaalla;
        private Texture2D Lamppupois;
        private Texture2D Lamppuhollilla;
        private Texture2D Lamppuvoitto;
        private Texture2D Lamppuboom;
        private Texture2D Lamppuaika;
        private Texture2D Kursori;

        //Load content
        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //Tiedoston pitäisi olla InssiPartyContent projektin alla solution explorerissa.
            Lamppupaalla = Content.Load<Texture2D>("lamppupaalla");
            Lamppupois = Content.Load<Texture2D>("lamppupois");
            Lamppuhollilla = Content.Load<Texture2D>("lamppuhollilla");
            Lamppuvoitto = Content.Load<Texture2D>("lamppuvoitto");
            Lamppuboom = Content.Load<Texture2D>("lamppuboom");
            Lamppuaika = Content.Load<Texture2D>("lamppuaika");
            Kursori = Content.Load<Texture2D>("Lamppukursori");
        }

        /**
         * Kaikki mitä pitää tehdä kun peli käynnistyy.
         * 
         * Esimerkiksi aseta muuttujat tarvittaviin arvoihin, tai käynnistä musiikki.
         */
        public override void Start()
        {
            Console.WriteLine("Starting Lampun vaihto");
        }

        /**
         * Ajetaan kun peli sulkeutuu. Piilota äänet ja puhdista roskasi seuraavaa peliä varten.
         */
        public override void Stop()
        {
            Console.WriteLine("Closing Lampun vaihto");
        }

        /**
         * Ajetaan kun peliä pitää päivittää. Tänne menee itse pelin logiikka koodi,
         * törmäys chekkaukset, pisteen laskut, yms.
         * 
         * gameTime avulla voidaan synkata nopeus tasaikseksi vaikka framerate ei olisi tasainen.
         */
        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            cursorRect.X = mouseState.X;
            cursorRect.Y = mouseState.Y;
            cursorPos = new Vector2(mouseState.X, mouseState.Y);

            //Otetaan lamppu irti
            if (add < 20)
            {
                Console.WriteLine("Lamppu on himmea");

                if (Lamppu.Intersects(cursorRect))
                {
                    Console.WriteLine("Pitäisikö se vaihtaa?");

                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        Console.WriteLine("Hailuta niinku hullu!");
                        if (Lamppu.Intersects(cursorRect))
                        {
                            add++;
                            Console.WriteLine(add);
                        }
                    }

                }
                if (Katkaisin.Intersects(cursorRect))
                {
                    Console.WriteLine("Katkaisin");
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        power = 1;
                        Console.WriteLine("Virta on pois");
                    }
                }
            }

            //Laitetaan toinen lamppu hollille
            else if (add > 20)
            {
                Lamppupaalla = Lamppupois;
                Console.WriteLine("Tarvitaan uusi lamppu");

                if (Uusilamppu.Intersects(cursorRect))
                {
                    Console.WriteLine("Klikkaa uusi lamppu kayttoon");
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        Lamppupois = Lamppuhollilla;

                        //Lamppu on hollilla, tässä pitää viimeistään muistaa ottaa virrat pois
                        if (Katkaisin.Intersects(cursorRect))
                        {
                            Console.WriteLine("Katkaisin");

                            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                            {
                                power = 1;
                                Console.WriteLine("Virta on pois");
                            }
                        }

                        if (Hollilla.Intersects(cursorRect))
                        {

                        }
                    }
                }
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


            //spriteBatch.Draw funktiolla voit piirtää ruudulle.
            //Palikka piirretään y akselilla, valuen kohtaan
            spriteBatch.Draw(Lamppupaalla, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(Kursori, cursorPos, Color.White);
        }

    }
}

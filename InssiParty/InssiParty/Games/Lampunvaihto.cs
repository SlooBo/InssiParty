using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InssiParty.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

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
        Rectangle Lamppu = new Rectangle(300,180,180,180);
        Rectangle Katkaisin = new Rectangle(530, 285, 560, 315);
        Rectangle Uusilamppu = new Rectangle(710,500,770,550);

        //Tekstuurit
        private Texture2D Lamppupaalla;
        private Texture2D Lamppupois;
        private Texture2D Lamppuhollilla;
        private Texture2D Lamppuvoitto;
        private Texture2D Lamppuboom;
        private Texture2D Lamppuaika;

        //Load content
        public override void Load(ContentManager Content)
        {
            //Tiedoston pitäisi olla InssiPartyContent projektin alla solution explorerissa.
            Lamppupaalla = Content.Load<Texture2D>("lamppupaalla");
            Lamppupois = Content.Load<Texture2D>("lamppupois");
            Lamppuhollilla = Content.Load<Texture2D>("lamppuhollilla");
            Lamppuvoitto = Content.Load<Texture2D>("lamppuvoitto");
            Lamppuboom = Content.Load<Texture2D>("lamppuboom");
            Lamppuaika = Content.Load<Texture2D>("lamppuaika");
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
            spriteBatch.Draw(Lamppupaalla, new Vector2(0,0), Color.White);
        }

    }
}

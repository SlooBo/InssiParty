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
     * Väistä ATJ-Promoja
     * 
     * Selitys pelistä
     * 
     * By: Toni Sarvela
     */
    class Promo : GameBase
    {
        SpriteBatch spritebatch;
        
        //variablesit
        private int value;
        private int inssi_nopeus = 3;
        private bool osuma = false;
        private Vector2 inssi_kohta;

        //Tekstuurit
        
        private Texture2D inssi;

        private Rectangle inssi_alue = new Rectangle(0, 0, 64, 64);
        private Rectangle kentta = new Rectangle(0, 0, 800, 600);
       
        
    
        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //Tiedoston pitäisi olla InssiPartyContent projektin alla solution explorerissa.
            spritebatch = new SpriteBatch(GraphicsDevice);
            inssi = Content.Load<Texture2D>("Nyancat");

            inssi_kohta = Vector2.One * 100;
        }

        /**
         * Kaikki mitä pitää tehdä kun peli käynnistyy.
         * 
         * Esimerkiksi aseta muuttujat tarvittaviin arvoihin, tai käynnistä musiikki.
         */
        public override void Start()
        {
            Console.WriteLine("Starting Vaista ATJ-Promoja");
            
            value = 0;
        }

        /**
         * Ajetaan kun peli sulkeutuu. Piilota äänet ja puhdista roskasi seuraavaa peliä varten.
         */
        public override void Stop()
        {
            Console.WriteLine("Closing Väistä ATJ-Promoja");
        }

        /**
         * Ajetaan kun peliä pitää päivittää. Tänne menee itse pelin logiikka koodi,
         * törmäys chekkaukset, pisteen laskut, yms.
         * 
         * gameTime avulla voidaan synkata nopeus tasaikseksi vaikka framerate ei olisi tasainen.
         */
        public override void Update(GameTime gameTime)
        {
            //value--;

            //if (value < 0)
            //{
            //    sammuta peli, true jos voitto tapahtui, false jos pelaaaja hävisi.
            //    CloseGame(true);
            //}
           
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.D))
            {
                inssi_kohta += Vector2.UnitX * inssi_nopeus;
            }

            if (keyboard.IsKeyDown(Keys.A))
            {
                inssi_kohta -= Vector2.UnitX * inssi_nopeus;
            }

            if (keyboard.IsKeyDown(Keys.S))
            {
                inssi_kohta += Vector2.UnitY * inssi_nopeus;
            }

            if (keyboard.IsKeyDown(Keys.W))
            {
                inssi_kohta -= Vector2.UnitY * inssi_nopeus;
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

            //spriteBatch.Draw funktiolla voit piirtää ruudulle.
            //Palikka piirretään y akselilla, valuen kohtaan
            spriteBatch.Draw(inssi, new Vector2(400,300), Color.White);
        }

    }
}

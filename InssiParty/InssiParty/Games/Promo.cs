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
        private bool osuma = false;

        private Vector2 inssi_kohta, inssi_nopeus, inssi_vauhti;

        //Tekstuurit
        
        private Texture2D inssi;

        private Rectangle inssi_alue = new Rectangle(0, 0, 64, 64);
        private Rectangle kentta = new Rectangle(0, 0, 800, 600);
       
        
    
        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //Tiedoston pitäisi olla InssiPartyContent projektin alla solution explorerissa.
            spritebatch = new SpriteBatch(GraphicsDevice);
            inssi = Content.Load<Texture2D>("Nyancat");

 
        }

        /**
         * Kaikki mitä pitää tehdä kun peli käynnistyy.
         * 
         * Esimerkiksi aseta muuttujat tarvittaviin arvoihin, tai käynnistä musiikki.
         */
        public override void Start()
        {
            Console.WriteLine("Starting Väistä ATJ-Promoja");
            
            value = 500;

            //inssin liikkeiden vektoreita
            inssi_kohta = new Vector2(400, 300);
            inssi_vauhti = Vector2.Zero;
            inssi_nopeus = new Vector2(5, 5);
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


            //    sammuta peli, true jos voitto tapahtui, false jos pelaaaja hävisi.

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

            inssi_kohta += inssi_vauhti;

            if (value < 0)
            {
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

            //spriteBatch.Draw funktiolla voit piirtää ruudulle.
            //Palikka piirretään y akselilla, valuen kohtaan
            spriteBatch.Draw(inssi, inssi_kohta, Color.White);
        }

    }
}

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
     * Väistä ATJ promotointeja tarpeeksi kauan!
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
        private Vector2 atj_kohta;

        //Tekstuurit
        
        private Texture2D inssi;
        private Texture2D atj;

        private Rectangle inssi_alue;
        private Rectangle atj_alue;
                                 
        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //Tiedoston pitäisi olla InssiPartyContent projektin alla solution explorerissa.
            spritebatch = new SpriteBatch(GraphicsDevice);
            inssi = Content.Load<Texture2D>("Nyancat");
            atj = Content.Load<Texture2D>("propelli");
 
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
            inssi_alue = new Rectangle((int)(inssi_kohta.X - inssi.Width / 2),
                (int)(inssi_kohta.Y - inssi.Height / 2), inssi.Width, inssi.Height);

            atj_kohta = new Vector2(600, 400);
            atj_alue = new Rectangle((int)(atj_kohta.X - atj.Width / 2),
                (int)(atj_kohta.Y - atj.Height / 2), atj.Width, atj.Height);
            
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
            if (inssi_kohta.X > 800 - 32)
            {
                inssi_kohta.X = 800 - 32;
            }
            if (inssi_kohta.Y > 600 - 32)
            {
                inssi_kohta.Y = 600 - 32;
            }

         

            if (inssi_alue.Intersects(atj_alue))
            {
                Console.WriteLine("Osuu!");
                inssi_kohta.X = 400;
                inssi_kohta.Y = 300;
            }

            inssi_alue.X = (int)inssi_kohta.X;
            inssi_alue.Y = (int)inssi_kohta.Y;

            atj_alue.X = (int)atj_kohta.X;
            atj_alue.Y = (int)atj_kohta.Y;

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
            spriteBatch.Draw(atj, atj_kohta, Color.White);
        }

    }
}

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
     * Nimi: Kymysyspeli
     * 
     * Selitys pelistä: Peli kysyy pelaajalta viisi kysymystä, joista vähintään kolmeen on vastattava oikein.
     * 
     * By: Timo Partanen
     */
    class Kysymys : GameBase
    {
        //Mahdolliset variablet mitä tarvitset pelin aikana on hyvä listata tässä kohdassa.
        private int value;
        private SpriteFont font;
        
       

        //Tekstuurit pitää myös listata tässä kohdassa.
        private Texture2D taustak;

        /**
         * Lataa tekstuureihin itse data.
         * 
         * Ajetaan kun koko ohjelma käynnistyy.
         */
        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //Tiedoston pitäisi olla InssiPartyContent projektin alla solution explorerissa.
            taustak = Content.Load<Texture2D>("taustaktesti");
            font = Content.Load<SpriteFont>("DefaultFont");
        }

        /**
         * Kaikki mitä pitää tehdä kun peli käynnistyy.
         * 
         * Esimerkiksi aseta muuttujat tarvittaviin arvoihin, tai käynnistä musiikki.
         */
        public override void Start()
        {
            
            

            value = 500;
        }

        /**
         * Ajetaan kun peli sulkeutuu. Piilota äänet ja puhdista roskasi seuraavaa peliä varten.
         */
        public override void Stop()
        {
        
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
            spriteBatch.DrawString(font, "Perkele", new Vector2(400, 150), Color.Red);

            //spriteBatch.Draw funktiolla voit piirtää ruudulle.
            //Palikka piirretään y akselilla, valuen kohtaan
            spriteBatch.Draw(taustak, new Vector2(0, value), Color.White);
        }

    }
}

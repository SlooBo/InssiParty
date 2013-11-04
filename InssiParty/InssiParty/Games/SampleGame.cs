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
    class SampleGame : IGameBase
    {
        //Mahdolliset variablet mitä tarvitset pelin aikana on hyvä listata tässä kohdassa.
        private int value;

        //Tekstuurit pitää myös listata tässä kohdassa.
        private Texture2D spriteBox;

        /**
         * Lataa tekstuureihin itse data.
         * 
         * Ajetaan kun koko ohjelma käynnistyy.
         */
        public void Load(ContentManager Content)
        {
            //Tiedoston pitäisi olla InssiPartyContent projektin alla solution explorerissa.
            spriteBox = Content.Load<Texture2D>("propelli");
        }

        /**
         * Kaikki mitä pitää tehdä kun peli käynnistyy.
         * 
         * Esimerkiksi aseta muuttujat tarvittaviin arvoihin, tai käynnistä musiikki.
         */
        public void Start()
        {
            Console.WriteLine("Starting hello world");
            
            value = 0;
        }

        /**
         * Ajetaan kun peli sulkeutuu. Piilota äänet ja puhdista roskasi seuraavaa peliä varten.
         */
        public void Stop()
        {
            Console.WriteLine("Closing hello world");
        }

        /**
         * Ajetaan kun peliä pitää päivittää. Tänne menee itse pelin logiikka koodi,
         * törmäys chekkaukset, pisteen laskut, yms.
         */
        public void Update()
        {
            value++;
        }

        /**
         * Pelkkä piirtäminen
         * 
         * ELÄ sijoita pelilogiikkaa tänne.
         */
        public void Render(SpriteBatch spriteBatch)
        {
            Console.WriteLine("Draw " + value);

            //spriteBatch.Draw funktiolla voit piirtää ruudulle.
            spriteBatch.Draw(spriteBox, new Vector2(50,50), Color.White);
        }

    }
}

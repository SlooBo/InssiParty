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
     * Avaa oluttölkki
     * 
     * Auta inssiä avaamaan oluttölkki ja välttämään janoon kuoleminen.
     * 
     * By: Markus Tolvanen
     */
    class Olut : GameBase
    {
        //Mahdolliset variablet mitä tarvitset pelin aikana on hyvä listata tässä kohdassa.
        private int value;
        private string [] numValues;
        private int fail_counter;
        private int success_counter;
        Random rnd = new Random();
        private string syöte;
        int painettava;
        Rectangle Pelikuva = new Rectangle(0, 0, 1, 1);
        Rectangle Prompti = new Rectangle(300, 180, 180, 180);

        //Tekstuurit pitää myös listata tässä kohdassa.
        private Texture2D Can;
        private Texture2D OpeningCan;
        private Texture2D EmptyCan;
        SpriteFont font;
        
        /**
         * Lataa tekstuureihin itse data.
         * 
         * Ajetaan kun koko ohjelma käynnistyy.
         */
        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //Tiedoston pitäisi olla InssiPartyContent projektin alla solution explorerissa.
            Can = Content.Load<Texture2D>("propelli");
            OpeningCan = Content.Load<Texture2D>("propelli");
            EmptyCan = Content.Load<Texture2D>("propelli");
            font = Content.Load<SpriteFont>("DefaultFont");
        }

        /**
         * Kaikki mitä pitää tehdä kun peli käynnistyy.
         * 
         * Esimerkiksi aseta muuttujat tarvittaviin arvoihin, tai käynnistä musiikki.
         */
        public override void Start()
        {
            Console.WriteLine("Starting Avaa oluttölkki");

            value = 500;

            numValues = new string[26] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            
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
            for (int i = 0; i < 5; i++)
            {
                painettava = rnd.Next(27);
              
                syöte = Console.ReadLine();         
               
                if (syöte == numValues[painettava])
                {
                    success_counter++;
                }

                else if (syöte != numValues[painettava])
                {
                    fail_counter++;
                    
                }

                if (success_counter == 5)
                {
                    //sammuta peli, true jos voitto tapahtui, false jos pelaaaja hävisi.
                    CloseGame(true);
                }

                else if (fail_counter == 3)
                {
                    CloseGame(false);
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
            Console.WriteLine("Draw " + value);

            //spriteBatch.Draw funktiolla voit piirtää ruudulle.
            //Palikka piirretään y akselilla, valuen kohtaan
          spriteBatch.Draw(Can, new Vector2(50, value), Color.White); 
          spriteBatch.Draw(OpeningCan, new Vector2(50, value), Color.White);
          spriteBatch.Draw(EmptyCan, new Vector2(50, value), Color.White);
          spriteBatch.DrawString(font,numValues[painettava], new Vector2(0,0), Color.White);

            
        }

    }
}
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
     * PelinNimi
     * 
     * Selitys pelistä
     * 
     * By: Miika Saastamoinen
     */
    class tentti : GameBase
    {
        //Mahdolliset variablet mitä tarvitset pelin aikana on hyvä listata tässä kohdassa.
        private int value;
        private int picture;
        KeyboardState k_state_old;
        Rectangle background = new Rectangle(0, 0, 800, 600);


        //Tekstuurit pitää myös listata tässä kohdassa.
        private Texture2D picture1;
        private Texture2D picture2;
        private Texture2D picture3;
        // private Texture2D backgroundTexture;
        /**
         * Lataa tekstuureihin itse data.
         * 
         * Ajetaan kun koko ohjelma käynnistyy.
         */
        public override void Load(ContentManager Content)
        {
            picture1 = Content.Load<Texture2D>("inssi_start position");
            picture2 = Content.Load<Texture2D>("inssi_mid position");
            picture3 = Content.Load<Texture2D>("inssi_end position");
            k_state_old = Keyboard.GetState();
        }

        /**
         * Kaikki mitä pitää tehdä kun peli käynnistyy.
         * 
         * Esimerkiksi aseta muuttujat tarvittaviin arvoihin, tai käynnistä musiikki.
         */
        public override void Start()
        {
            Console.WriteLine("Start game");



            //laskuri
            value = 0;
            picture = 0;
        }

        /**
         * Ajetaan kun peli sulkeutuu. Piilota äänet ja puhdista roskasi seuraavaa peliä varten.
         */
        public override void Stop()
        {
            Console.WriteLine("End of game");
        }


        //update looppi
        public override void Update(GameTime gameTime)
        {
            KeyboardState k_state = Keyboard.GetState();

            for (int i = 0; i < 10; i++)
            {
                if (k_state.IsKeyDown(Keys.Space))
                {
                    if (!k_state_old.IsKeyDown(Keys.Space))
                    {
                        value++;

                    }
                }
                else if (k_state_old.IsKeyDown(Keys.Space))
                {
                }

                k_state_old = k_state;
            }

            if (value == 10)
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
            spriteBatch.Draw(picture1, background, new Color(255, 255, 255));
            Console.WriteLine("Valiluonnin iskut" + value);

            //spriteBatch.Draw funktiolla voit piirtää ruudulle.
            //Palikka piirretään y akselilla, valuen kohtaan
        }

    }
}

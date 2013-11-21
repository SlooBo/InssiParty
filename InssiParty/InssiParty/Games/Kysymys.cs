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

 
     /* Nimi: Kymysyspeli
     * 
     * Selitys pelistä: Peli kysyy pelaajalta viisi kysymystä, joista vähintään kolmeen on vastattava oikein.
     * 
     * By: Timo Partanen
     */

    class Kysymys : GameBase
    {
        //Variablet
        private int value;
        private SpriteFont font;

        //Muuttujat
        char valinta;


        
       

        //Tekstuurit
        private Texture2D taustakuva;
        private Texture2D kymysys1;
        private Texture2D kymysys2;
        private Texture2D kymysys3;
        private Texture2D kymysys4;
        private Texture2D kymysys5;
        private Texture2D voitto1;
        private Texture2D häviö1;
        /**
         * Lataa tekstuureihin itse data.
         * Ajetaan kun koko ohjelma käynnistyy.
         */
        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            taustakuva = Content.Load<Texture2D>("k1");
            kymysys1 = Content.Load<Texture2D>("k1");
            kymysys2 = Content.Load<Texture2D>("k2");
            kymysys3 = Content.Load<Texture2D>("k3");
            kymysys4 = Content.Load<Texture2D>("k4");
            kymysys5 = Content.Load<Texture2D>("k5");
            voitto1 = Content.Load<Texture2D>("v1");
            häviö1 = Content.Load<Texture2D>("h1");
            
            font = Content.Load<SpriteFont>("DefaultFont");
            
        }

        /**
         * Kaikki mitä pitää tehdä kun peli käynnistyy.
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
          

            taustakuva = kymysys1;
            KeyboardState state = new KeyboardState();

            if (state.IsKeyDown(Keys.K))
            {
                //K on painettu
            }


            if (value < 0)
            {
                //sammuta peli, true jos voitto tapahtui, false jos pelaaaja hävisi.
                CloseGame(true);
            }
        }

        /**
         * Pelkkä piirtäminen
         * ELÄ sijoita pelilogiikkaa tänne.
         * gameTime avulla voidaan synkata nopeus tasaikseksi vaikka framerate ei olisi tasainen.
         */
        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Console.WriteLine("Draw " + value);
            spriteBatch.DrawString(font, "Perkele", new Vector2(400, 150), Color.Red);

            //spriteBatch.Draw funktiolla voit piirtää ruudulle.
            //Palikka piirretään y akselilla, valuen kohtaan
            spriteBatch.Draw(taustakuva, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(kymysys1,new Vector2(0, 0), Color.White); 
            spriteBatch.Draw(kymysys2,new Vector2(0, 0), Color.White);
            spriteBatch.Draw(kymysys3,new Vector2(0, 0), Color.White);
            spriteBatch.Draw(kymysys4,new Vector2(0, 0), Color.White);
            spriteBatch.Draw(kymysys5,new Vector2(0, 0), Color.White);
            spriteBatch.Draw(voitto1,new Vector2(0, 0), Color.White);
            spriteBatch.Draw(häviö1,new Vector2(0, 0), Color.White);
            
        }

    }
}

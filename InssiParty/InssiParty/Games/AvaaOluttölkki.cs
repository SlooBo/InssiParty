using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InssiParty.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using InssiParty.Games.FeedGameSrc;


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
        //Esitellään muuttujat
        private int value;
        private string [] numValues;
        private int fail_counter;
        private int success_counter;
        Random rnd = new Random();
        private string syöte;
        int painettava;
        Rectangle Tölkki = new Rectangle(0, 0, 1, 1);
        Rectangle Prompti = new Rectangle(300, 180, 180, 180);

        //Esitellään tekstuurit
        private Texture2D Can;
        private Texture2D OpeningCan;
        private Texture2D EmptyCan;
        SpriteFont font;
        private Texture2D backround_texture;
        
        /**
         * 
         * 
         * Ladataan tekstuurit
         */
        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //Tiedoston pitäisi olla InssiPartyContent projektin alla solution explorerissa.
            Can = Content.Load<Texture2D>("Can");
            OpeningCan = Content.Load<Texture2D>("Can-Hissing");
            EmptyCan = Content.Load<Texture2D>("Can-Empty");
            font = Content.Load<SpriteFont>("DefaultFont");
            backround_texture = Content.Load<Texture2D>("FeedGame_background");
        }

        /**
         *
         * 
         * Annetaan kirjaimille numeroarvot ja alustetaan laskimet nollille
         */
        public override void Start()
        {
            Console.WriteLine("Starting Avaa oluttölkki");

            value = 500;

            numValues = new string[26] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            
            fail_counter=0;

            success_counter=0;
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
              
                syöte = Convert.ToString(Keyboard.GetState());
                Keyboard.GetState();
               
                if (syöte == numValues[painettava])
                {
                    success_counter++;
                }

                else if (syöte != numValues[painettava])
                {
                    fail_counter++;
                    
                }

                if (success_counter < 3 && fail_counter == 0)
                {
                    
                }

                else if (success_counter == 5)
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
         while(success_counter!=5&&fail_counter!=3)
         {
             spriteBatch.DrawString(font, numValues[painettava], new Vector2(0, 0), Color.White);
             
             spriteBatch.Draw(backround_texture, new Vector2(0, 0), Color.White);
             spriteBatch.Draw(Can, new Vector2(0, 0), Color.White);
             
             if(success_counter>=3&&fail_counter==0)
             {
                spriteBatch.Draw(OpeningCan, new Vector2(0, 0), Color.White);
             }

             else if(success_counter==5&&fail_counter==0)
             {
                spriteBatch.Draw(EmptyCan, new Vector2(0, 0), Color.White);
             }
          
         }
         if (success_counter == 5)
         {
             spriteBatch.DrawString(font, "Onnea. Voitit. Osaat avata töklin. Tapu tapu.", new Vector2(0, 0), Color.White);
         }

         else if(fail_counter==3)
         {
             spriteBatch.DrawString(font, "Saatanan tunari! Hävistit! Ootko ylpee ittestäs!?", new Vector2(0, 0), Color.White);
         }
  


        }

    }
}
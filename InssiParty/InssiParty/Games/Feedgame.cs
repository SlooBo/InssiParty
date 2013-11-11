using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InssiParty.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using InssiParty.Games.FeedGameSrc;
using Microsoft.Xna.Framework.Input;

namespace InssiParty.Games
{


    /**
     * PelinNimi
     * 
     * Selitys pelistä
     * 
     * By: Tekijän Nimi
     */
    class FeedGame : GameBase
    {
        //Mahdolliset variablet mitä tarvitset pelin aikana on hyvä listata tässä kohdassa.
        private int value;
        private List<Kaappi> kaapit;
        private Vector2 HandPos;

        //Tekstuurit pitää myös listata tässä kohdassa.
        private Texture2D backround_texture;
        private Texture2D box;
        private Texture2D handu;

        Rectangle HandRect = new Rectangle(0, 0, 100, 100);
        Rectangle TestiRect = new Rectangle(0, 0, 100, 800);
        /**
         * Lataa tekstuureihin itse data.
         * 
         * Ajetaan kun koko ohjelma käynnistyy.
         */
        public override void Load(ContentManager Content)
        {
            //Tiedoston pitäisi olla InssiPartyContent projektin alla solution explorerissa.
            backround_texture = Content.Load<Texture2D>("FeedGame_background");
            box = Content.Load<Texture2D>("tausta_temp");
            handu = Content.Load<Texture2D>("hand");
     
        }

        /**
         * Kaikki mitä pitää tehdä kun peli käynnistyy.
         * 
         * Esimerkiksi aseta muuttujat tarvittaviin arvoihin, tai käynnistä musiikki.
         */
        public override void Start()
        {
            kaapit = new List<Kaappi>();
            Kaappi temp = new Kaappi();

            temp.sijainti = new Vector2(700, 500);
            temp.koko = new Vector2(1,1);

            kaapit.Add(temp);

            
            temp = new Kaappi();
            temp.sijainti = new Vector2(600, 50);
            temp.koko = new Vector2(1,1);
            kaapit.Add(temp);

            temp = new Kaappi();
            temp.sijainti = new Vector2(100, 100);
            temp.koko = new Vector2(1,1);
            kaapit.Add(temp);
             
             
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
            var mouseState = Mouse.GetState();
            HandRect.X = mouseState.X;
            HandRect.Y = mouseState.Y;
            HandPos = new Vector2(mouseState.X, mouseState.Y);


            if(TestiRect.Intersects(HandRect))
            {
            Console.WriteLine("Hand hits the cupboard");
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
            spriteBatch.Draw(backround_texture, new Vector2(0, 0), Color.White);

            //for loop the list
            for(int i=0; i< kaapit.Count;i++)
            {
                //draw rectangle on kaappi.sijainti + koko
                spriteBatch.Draw(box, kaapit[i].sijainti+kaapit[i].koko, Color.Red);
            
            }

            spriteBatch.Draw(handu, HandPos, Color.White);
        }

    }
}

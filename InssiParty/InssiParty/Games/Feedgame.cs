﻿using System;
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
     * Ruokkimi-speli
     * Selitys pelistä
     * Löydä syötävää tai kuolet
     * By: Hannu
     */
    class FeedGame : GameBase
    {
        //Mahdolliset variablet mitä tarvitset pelin aikana on hyvä listata tässä kohdassa.
        private List<Kaappi> kaapit;
        private Vector2 HandPos;

        //private bool over = false;
        //private bool osuma = false;

        //Tekstuurit pitää myös listata tässä kohdassa.
        private Texture2D backround_texture;
        private Texture2D box;
        private Texture2D handu;
        private Texture2D win;
        private Texture2D lose;
        private Texture2D poison;
        private Texture2D ruoka;
        private Texture2D testi;

        Rectangle HandRect = new Rectangle(0, 0, 100, 100);
        //Rectangle TestiRect = new Rectangle(0, 0, 200, 300);
        //Rectangle TestiRect2 = new Rectangle(650, 0, 100, 300);
        /**
         * Lataa tekstuureihin itse data.
         * 
         * Ajetaan kun koko ohjelma käynnistyy.
         */
        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //Tiedoston pitäisi olla InssiPartyContent projektin alla solution explorerissa.
            backround_texture = Content.Load<Texture2D>("FeedGame_background");
            box = Content.Load<Texture2D>("kaappi");
            handu = Content.Load<Texture2D>("hand");
            win = Content.Load<Texture2D>("You_won");
            lose = Content.Load<Texture2D>("hävisit");
            poison = Content.Load<Texture2D>("Pullo");
            ruoka = Content.Load<Texture2D>("ruoka");
            testi = Content.Load<Texture2D>("testi_item");
        }

        /**
         * Kaikki mitä pitää tehdä kun peli käynnistyy.
         * 
         * Esimerkiksi aseta muuttujat tarvittaviin arvoihin, tai käynnistä musiikki.
         */
        public override void Start()
        {

            Random rand = new Random();
            kaapit = new List<Kaappi>();



            Kaappi temp = new Kaappi();
            temp.auki = false;
            temp.sijainti = new Vector2(0, 0);
            temp.koko = new Vector2(300,300);
            temp.tavara_id = rand.Next(4);
            kaapit.Add(temp);


            temp = new Kaappi();
            temp.auki = false;
            temp.sijainti = new Vector2(200, 300);
            temp.koko = new Vector2(300,300);
            temp.tavara_id = rand.Next(4); 
            kaapit.Add(temp);

            temp = new Kaappi();
            temp.auki = false;
            temp.sijainti = new Vector2(500, 0);
            temp.koko = new Vector2(300, 300);
            temp.tavara_id = rand.Next(4);
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

            for (int i = 0; i < kaapit.Count; i++)
            {
                if (HandRect.Intersects(new Rectangle((int)kaapit[i].sijainti.X, (int)kaapit[i].sijainti.Y, (int)kaapit[i].koko.X, (int)kaapit[i].koko.Y)) && mouseState.LeftButton == ButtonState.Pressed)
                {
                    Console.WriteLine("Hand hits the cupboard");
                    kaapit[i].auki = true;

                }

                if (HandRect.Intersects(new Rectangle((int)kaapit[i].sijainti.X + 255, (int)kaapit[i].sijainti.Y + 255, (int)kaapit[i].koko.X - 275, (int)kaapit[i].koko.Y - 275)) && kaapit[i].auki == true && mouseState.LeftButton == ButtonState.Pressed)
                {
                    Console.WriteLine("Hand hits item");
                    if (kaapit[i].tavara_id==1)
                    {
                        Console.WriteLine("You Die!!!");
                    }

                    else if (kaapit[i].tavara_id == 2)
                    {
                        Console.WriteLine("Wololooo");
                    }

                    else if (kaapit[i].tavara_id == 3)
                    {
                        Console.WriteLine("Selvisit hengissä!");
                    }
                    
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
            Console.WriteLine("Perkele");

            //spriteBatch.Draw funktiolla voit piirtää ruudulle.
            //Palikka piirretään y akselilla, valuen kohtaan
            spriteBatch.Draw(backround_texture, new Vector2(0, 0), Color.White);

            //for loop the list
            for(int i=0; i< kaapit.Count;i++)
            {
                //draw rectangle on kaappi.sijainti + koko
                spriteBatch.Draw(box, kaapit[i].sijainti, Color.White);
                if (kaapit[i].auki == true)
                {
                    if (kaapit[i].tavara_id == 1)
                    {
                        spriteBatch.Draw(poison, kaapit[i].sijainti, Color.White);
                    }
                    else if (kaapit[i].tavara_id == 2)
                    {
                        spriteBatch.Draw(testi, kaapit[i].sijainti, Color.White);
                    }

                    else if (kaapit[i].tavara_id == 3)
                    {
                        spriteBatch.Draw(ruoka, kaapit[i].sijainti, Color.White);
                    }
                }
            
            }


            spriteBatch.Draw(handu, HandPos, Color.White);
            //    spriteBatch.Draw(win, new Vector2(0, 0), Color.White);
            //spriteBatch.Draw(lose, new Vector2(0, 0), Color.White);
        }

    }
}
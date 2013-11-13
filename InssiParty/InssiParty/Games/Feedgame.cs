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
     * 
     * Selitys pelistä
     * 
     * By: Tekijän Nimi
     */
    class FeedGame : GameBase
    {
        //Mahdolliset variablet mitä tarvitset pelin aikana on hyvä listata tässä kohdassa.
        private List<Kaappi> kaapit;
        private Vector2 HandPos;

        private bool over = false;
        private bool over2 = false;
        private bool osuma = false;
        //Tekstuurit pitää myös listata tässä kohdassa.
        private Texture2D backround_texture;
        private Texture2D box;
        private Texture2D handu;
        private Texture2D win;
        private Texture2D lose;

        Rectangle HandRect = new Rectangle(0, 0, 100, 100);
        Rectangle TestiRect = new Rectangle(150, 250, 70, 70);
        Rectangle TestiRect2 = new Rectangle(800, 600, 100, 100);
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

            temp.sijainti = new Vector2(800, 600);
            temp.koko = new Vector2(-250,-600);

            kaapit.Add(temp);


            temp = new Kaappi();
            temp.sijainti = new Vector2(150, 250);
            temp.koko = new Vector2(70,70);
            kaapit.Add(temp);

            temp = new Kaappi();
            temp.sijainti = new Vector2(500, 400);
            temp.koko = new Vector2(-600,-400);
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
            HandPos = new Vector2(mouseState.X-80, mouseState.Y-100);
            

            if(TestiRect.Intersects(HandRect))
            {
            Console.WriteLine("Hand hits the cupboard");
            over = true;
            }
            else
            {
                over = false;
            }


            if (TestiRect2.Intersects(HandRect))
            {
                Console.WriteLine("Hand hits the cupboard2");
                over2 = true;
            }
            else
            {
                over2 = false;
            }


            if (HandRect.Intersects(TestiRect) && mouseState.LeftButton == ButtonState.Pressed || HandRect.Intersects(TestiRect2) && mouseState.LeftButton == ButtonState.Pressed)
            {
                osuma = true;
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
                spriteBatch.Draw(box, kaapit[i].sijainti+kaapit[i].koko, Color.White);
            
            }

            spriteBatch.Draw(handu, HandPos, Color.White);

            if (osuma == true && over2 == true)
            {
                spriteBatch.Draw(lose, new Vector2(0, 0), Color.White);
            }

            if (osuma == true && over == true)
            {
                spriteBatch.Draw(win,new Vector2(0,0), Color.White);
            }


        }

    }
}

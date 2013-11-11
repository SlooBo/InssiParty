﻿using System;
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
     * Shoot the Nyan Cat!
     * 
     * Ammu nyancattia turretilla.
     * 
     * By: Taneli
     */
    class Shooty : GameBase
    {
        private int nyancat_pos, health;

        Rectangle background = new Rectangle(0, 0, 800, 600);
        Rectangle targetRect = new Rectangle(0, 0, 62, 62);
        Rectangle NyancatRect = new Rectangle(0, 0, 62, 37);
        //Rectangle turretBarrelRect = new Rectangle(0, 0, 62, 62); 
        private Vector2 targetPos;
        //private Vector2 barrelhp;

        private Texture2D Nyancat;
        private Texture2D nyantail;
        private Texture2D targetTexture;
        private Texture2D cannonballTexture;
        private Texture2D backgroundTexture;
        private Texture2D turretTexture;
        //private Texture2D turretBarrelTexture;

        public override void Load(ContentManager Content)
        {
            //Tiedoston pitäisi olla InssiPartyContent projektin alla solution explorerissa.
            backgroundTexture = Content.Load<Texture2D>("nyanbackground");
            nyantail = Content.Load<Texture2D>("nyantail");
            Nyancat = Content.Load<Texture2D>("Nyancat");
            targetTexture = Content.Load<Texture2D>("Target");
            cannonballTexture = Content.Load<Texture2D>("cannonball");
            turretTexture = Content.Load<Texture2D>("playerturret");
            //turretBarrelTexture = Content.Load<Texture2D>("turretbarrel");
        }

        /**
         * Kaikki mitä pitää tehdä kun peli käynnistyy.
         * 
         * Esimerkiksi aseta muuttujat tarvittaviin arvoihin, tai käynnistä musiikki.
         */
        public override void Start()
        {
            nyancat_pos = 0;
            health = 3;
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
            nyancat_pos+=2;

            var mouseState = Mouse.GetState();
            targetRect.X = mouseState.X;
            targetRect.Y = mouseState.Y;
            targetPos = new Vector2(mouseState.X-32, mouseState.Y-32);


            if (nyancat_pos > background.Width)
            {
                //sammuta peli, true jos voitto tapahtui, false jos pelaaaja hävisi.
                CloseGame(false);
            }
            if (health == 0)
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

            //spriteBatch.Draw funktiolla voit piirtää ruudulle.
            //Palikka piirretään y akselilla, valuen kohtaan
            spriteBatch.Draw(backgroundTexture, background, Color.White);
            spriteBatch.Draw(turretTexture, new Vector2(50, 400), Color.White);
            spriteBatch.Draw(nyantail, new Vector2(nyancat_pos - 187, 10), Color.White);
            spriteBatch.Draw(Nyancat, new Vector2(nyancat_pos, 10), Color.White);
            spriteBatch.Draw(targetTexture, targetPos, Color.White);
        }

    }
}

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
     * Läpsytys
     * 
     * TODO: lisää selitys
     * 
     * By: Henri Tiihonen
     */
    class lapsytys : IGameBase
    {
        public bool IsRunning { get; set; }

        //Mahdolliset variablet mitä tarvitset pelin aikana on hyvä listata tässä kohdassa.
        private int value;
        Rectangle background = new Rectangle(0, 0, 800, 600);
        private Vector2 cursorPos;
        private Vector2 objPos;
        int i;

        //Tekstuurit pitää myös listata tässä kohdassa.
        private Texture2D backgroundTexture;
        private Texture2D cursorTexture;
        private Texture2D objectTexture;
        Rectangle objectRect = new Rectangle(0, 0, 64, 64);
        Rectangle cursorRect = new Rectangle(0, 0, 64, 64);

        /**
         * Lataa tekstuureihin itse data.
         * 
         * Ajetaan kun koko ohjelma käynnistyy.
         */
        public void Load(ContentManager Content)
        {
            //Tiedoston pitäisi olla InssiPartyContent projektin alla solution explorerissa.
            backgroundTexture = Content.Load<Texture2D>("tausta_temp");
            cursorTexture = Content.Load<Texture2D>("cursor");
            objectTexture = Content.Load<Texture2D>("obj");
        }

        /**
         * Kaikki mitä pitää tehdä kun peli käynnistyy.
         * 
         * Esimerkiksi aseta muuttujat tarvittaviin arvoihin, tai käynnistä musiikki.
         */
        public void Start()
        {
            Console.WriteLine("start game");

            objPos = new Vector2(100, 100);

            i = 1;

            Mouse.SetPosition(600, 300);
            
            value = 0;
        }

        /**
         * Ajetaan kun peli sulkeutuu. Piilota äänet ja puhdista roskasi seuraavaa peliä varten.
         */
        public void Stop()
        {
            Console.WriteLine("close game");
        }

        /**
         * Ajetaan kun peliä pitää päivittää. Tänne menee itse pelin logiikka koodi,
         * törmäys chekkaukset, pisteen laskut, yms.
         * 
         * gameTime avulla voidaan synkata nopeus tasaikseksi vaikka framerate ei olisi tasainen.
         */
        public void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            cursorPos = new Vector2(mouseState.X, mouseState.Y);
            cursorRect.X = mouseState.X;
            cursorRect.Y = mouseState.Y;


            objectRect.X = (int)objPos.X;
            objectRect.Y = (int)objPos.Y;

            if (objectRect.Intersects(cursorRect))
            {
                objPos.X = 700;
                objPos.Y = 300;
                objectRect.X = (int)objPos.X;
                objectRect.Y = (int)objPos.Y;
                value++;
                i = 2;
            }

            if (objectRect.Intersects(cursorRect))
            {
                objPos.X = 100;
                objPos.Y = 100;
                objectRect.X = (int)objPos.X;
                objectRect.Y = (int)objPos.Y;
                value++;
                i = 1;
            }


            if (value==10)
            {
                //Sammuta peli kun value o pienempi kuin 0
                //Moottori lukee IsRunning muuttujan ja sammuttaa pelin.
                IsRunning = false;
            }
        }

        /**
         * Pelkkä piirtäminen
         * 
         * ELÄ sijoita pelilogiikkaa tänne.
         *
         * gameTime avulla voidaan synkata nopeus tasaikseksi vaikka framerate ei olisi tasainen.
         */
        public void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(backgroundTexture, background, Color.White);
            spriteBatch.Draw(cursorTexture, cursorPos, Color.White);
            spriteBatch.Draw(objectTexture, objPos, Color.White);
        }

    }
}
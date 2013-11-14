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
     * Rölläile Röllibyrintissä
     * 
     * Selitys pelistä
     * 
     * By: röllipukki / Jouni Friman
     */
    class rollibyrintti : GameBase
    {
        SpriteBatch spriteBatch;

        private int liikeNopeus = 3;
        private bool powerNosto = false;


        private Texture2D pelaaja;
        private Texture2D powerUp;

        private Vector2 pelaajaLiike = Vector2.Zero;
        private Vector2 powerPaikka;

        private Rectangle pelaajaRajat;
        private Rectangle powerRajat;

        /**
         * Lataa tekstuureihin itse data.
         * 
         * Ajetaan kun koko ohjelma käynnistyy.
         */
        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //Tiedoston pitäisi olla InssiPartyContent projektin alla solution explorerissa.

            spriteBatch = new SpriteBatch(GraphicsDevice);
            pelaaja = Content.Load<Texture2D>("Nyancat");
            powerUp = Content.Load<Texture2D>("propelli");

            powerPaikka = new Vector2(300, 300);

            pelaajaRajat = new Rectangle((int)(pelaajaLiike.X - pelaaja.Width / 2),
                (int)(pelaajaLiike.Y - pelaaja.Height / 2), pelaaja.Width, pelaaja.Height);
            powerRajat = new Rectangle((int)(powerPaikka.X - powerUp.Width / 2),
                (int)(powerPaikka.Y - powerUp.Height / 2), powerUp.Width, powerUp.Height);
        }

        /**
         * Kaikki mitä pitää tehdä kun peli käynnistyy.
         * 
         * Esimerkiksi aseta muuttujat tarvittaviin arvoihin, tai käynnistä musiikki.
         */
        public override void Start()
        {



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
            //value--;
            //if (value < 0)
            //{
            //    //sammuta peli, true jos voitto tapahtui, false jos pelaaaja hävisi.
            //    CloseGame(true);
            //}

            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.W))
            { pelaajaLiike.Y -= liikeNopeus; }
            if (keyboard.IsKeyDown(Keys.S))
            { pelaajaLiike.Y += liikeNopeus; }
            if (keyboard.IsKeyDown(Keys.A))
            { pelaajaLiike.X -= liikeNopeus; }
            if (keyboard.IsKeyDown(Keys.D))
            { pelaajaLiike.X += liikeNopeus; }

            if (pelaajaRajat.Intersects(powerRajat))
            {

                liikeNopeus += 1;
                powerNosto = true;

            }


            pelaajaRajat.X = (int)pelaajaLiike.X;
            pelaajaRajat.Y = (int)pelaajaLiike.Y;

            powerRajat.X = (int)powerPaikka.X;
            powerRajat.Y = (int)powerPaikka.Y;

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

            spriteBatch.Draw(pelaaja, pelaajaLiike, Color.White);

 //           if (powerNosto == true)
   //         {
                spriteBatch.Draw(powerUp, powerPaikka, Color.White);
     //       } //MITÄ VITTUA!? If-lause renderissä aiheuttaa Avastin sekoamisen?



            //spriteBatch.Draw funktiolla voit piirtää ruudulle.
            //Palikka piirretään y akselilla, valuen kohtaan
            //spriteBatch.Draw(spriteBox, new Vector2(50, value), Color.White);
        }

    }
}

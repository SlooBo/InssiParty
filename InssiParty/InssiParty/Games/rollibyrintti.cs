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
     * Rallittele Röllirallissa
     * 
     * Selvitä labyrintti törmäämättä seiniin.
     * 
     * By: röllipukki / Jouni Friman PS. PASKINTA KOODIA IKINÄ!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
     */
    class rollibyrintti : GameBase
    {
        SpriteBatch spriteBatch;

        private float liikeNopeus;
        private int tutorialAika;
        private int tutorialKokonaisaika;
        private bool powerUpNosto;
        private bool powerUpNosto2;
        private bool powerDown1Nosto;
        private bool powerMix2Nosto;
        private bool powerMixNosto;

        //TEKSTUURIT

        private Texture2D backgroundTexture;
        private Texture2D pelaaja;
        private Texture2D powerUp;
        private Texture2D powerUp2;
        private Texture2D powerDown1;
        private Texture2D powerMix2;
        private Texture2D powerMix;

        private Texture2D pystySeina1;
        private Texture2D pystySeina2;
        private Texture2D pystySeina3;
        private Texture2D pystySeina4;
        private Texture2D pystySeina5;
        private Texture2D pystySeina6;
        private Texture2D pystySeina7;
        private Texture2D pystySeina8;
        private Texture2D pystySeina9;
        private Texture2D pystySeina10;
        private Texture2D pystySeina11;
        private Texture2D pystySeina12;
        private Texture2D pystySeina13;
        private Texture2D pystySeina14;

        private Texture2D vaakaSeina1;
        private Texture2D vaakaSeina2;
        private Texture2D vaakaSeina3;
        private Texture2D vaakaSeina4;

        private Texture2D maali;

        //VEKTORIT

        private Vector2 pelaajaLiike;
        private Vector2 powerUpPaikka;
        private Vector2 powerUpPaikka2;
        private Vector2 powerDown1Paikka;
        private Vector2 powerMix2Paikka;
        private Vector2 powerMixPaikka;

        private Vector2 pystySeina1Paikka;
        private Vector2 pystySeina2Paikka;
        private Vector2 pystySeina3Paikka;
        private Vector2 pystySeina4Paikka;
        private Vector2 pystySeina5Paikka;
        private Vector2 pystySeina6Paikka;
        private Vector2 pystySeina7Paikka;
        private Vector2 pystySeina8Paikka;
        private Vector2 pystySeina9Paikka;
        private Vector2 pystySeina10Paikka;
        private Vector2 pystySeina11Paikka;
        private Vector2 pystySeina12Paikka;
        private Vector2 pystySeina13Paikka;
        private Vector2 pystySeina14Paikka;

        private Vector2 vaakaSeina1Paikka;
        private Vector2 vaakaSeina2Paikka;
        private Vector2 vaakaSeina3Paikka;
        private Vector2 vaakaSeina4Paikka;

        private Vector2 maaliPaikka;

        //RECTANGLET

        private Rectangle backgroundRect;
        private Rectangle pelaajaRajat;
        private Rectangle powerUpRajat;
        private Rectangle powerUpRajat2;
        private Rectangle powerDown1Rajat;
        private Rectangle powerMix2Rajat;
        private Rectangle powerMixRajat;

        private Rectangle pystySeina1Rajat;
        private Rectangle pystySeina2Rajat;
        private Rectangle pystySeina3Rajat;
        private Rectangle pystySeina4Rajat;
        private Rectangle pystySeina5Rajat;
        private Rectangle pystySeina6Rajat;
        private Rectangle pystySeina7Rajat;
        private Rectangle pystySeina8Rajat;
        private Rectangle pystySeina9Rajat;
        private Rectangle pystySeina10Rajat;
        private Rectangle pystySeina11Rajat;
        private Rectangle pystySeina12Rajat;
        private Rectangle pystySeina13Rajat;
        private Rectangle pystySeina14Rajat;

        private Rectangle vaakaSeina1Rajat;
        private Rectangle vaakaSeina2Rajat;
        private Rectangle vaakaSeina3Rajat;
        private Rectangle vaakaSeina4Rajat;

        private Rectangle maaliRajat;

        /**
         * Lataa tekstuureihin itse data.
         * 
         * Ajetaan kun koko ohjelma käynnistyy.
         */
        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //Tiedoston pitäisi olla InssiPartyContent projektin alla solution explorerissa.

            //TEKSTUURIEN LATAUS

            spriteBatch = new SpriteBatch(GraphicsDevice);

            backgroundTexture = Content.Load<Texture2D>("rolliBackground"); //rolliBackground
            pelaaja = Content.Load<Texture2D>("rolliPelaaja"); //rolliPelaaja
            powerUp = Content.Load<Texture2D>("rolliPowerup"); //rolliPoweup
            powerUp2 = Content.Load<Texture2D>("rolliPowerup");
            powerDown1 = Content.Load<Texture2D>("rolliPowerdown"); //rolliPowerdown
            powerMix2 = Content.Load<Texture2D>("rolliPowerMix"); //rolliPOwerdown
            powerMix = Content.Load<Texture2D>("rolliPowermix"); //rolliPwermix

            pystySeina1 = Content.Load<Texture2D>("rolliPystyseina"); //rolliPystyseina
            pystySeina2 = Content.Load<Texture2D>("rolliPystyseina");
            pystySeina3 = Content.Load<Texture2D>("rolliPystyseina");
            pystySeina4 = Content.Load<Texture2D>("rolliPystyseina");
            pystySeina5 = Content.Load<Texture2D>("rolliPystyseina");
            pystySeina6 = Content.Load<Texture2D>("rolliPystyseina");
            pystySeina7 = Content.Load<Texture2D>("rolliPystyseina");
            pystySeina8 = Content.Load<Texture2D>("rolliPystyseina");
            pystySeina9 = Content.Load<Texture2D>("rolliPystyseina");
            pystySeina10 = Content.Load<Texture2D>("rolliPystyseina");
            pystySeina11 = Content.Load<Texture2D>("rolliPystyseina");
            pystySeina12 = Content.Load<Texture2D>("rolliPystyseina");
            pystySeina13 = Content.Load<Texture2D>("rolliPystyseina");
            pystySeina14 = Content.Load<Texture2D>("rolliPystyseina");

            vaakaSeina1 = Content.Load<Texture2D>("rolliVaakaseina"); // rolliVaakaseina
            vaakaSeina2 = Content.Load<Texture2D>("rolliVaakaseina");
            vaakaSeina3 = Content.Load<Texture2D>("rolliVaakaseina");
            vaakaSeina4 = Content.Load<Texture2D>("rolliVaakaseina");

            maali = Content.Load<Texture2D>("rolliMaali"); //rolliMaali

            //KOORDINAATIT

            pelaajaLiike = new Vector2(10, 10);
            powerUpPaikka = new Vector2(10, 560);
            powerUpPaikka2 = new Vector2(640, 25);
            powerDown1Paikka = new Vector2(330, 35);
            powerMix2Paikka = new Vector2(450, 550);
            powerMixPaikka = new Vector2(570, 250);

            pystySeina1Paikka = new Vector2(90, 0);
            pystySeina2Paikka = new Vector2(90, 300);
            pystySeina3Paikka = new Vector2(90, 420);

            pystySeina4Paikka = new Vector2(210, 450);
            pystySeina5Paikka = new Vector2(210, 250);
            pystySeina6Paikka = new Vector2(450, 300);

            pystySeina7Paikka = new Vector2(330, 100);
            pystySeina8Paikka = new Vector2(330, 270);
            pystySeina9Paikka = new Vector2(210, 80);

            pystySeina10Paikka = new Vector2(450, 50);
            pystySeina11Paikka = new Vector2(450, 200);

            pystySeina12Paikka = new Vector2(570, 0);
            pystySeina13Paikka = new Vector2(570, 300);
            pystySeina14Paikka = new Vector2(570, 500);

            vaakaSeina1Paikka = new Vector2(605, 110);
           // vaakaSeina2Paikka = new Vector2(740, 235);
            vaakaSeina3Paikka = new Vector2(605, 340);
            vaakaSeina4Paikka = new Vector2(700, 450);

            maaliPaikka = new Vector2(700, 530);


            // RAJOJEN MÄÄRITYS

            backgroundRect = new Rectangle(0, 0, 800, 600);

            
            pelaajaRajat = new Rectangle(10, 10 ,33, 35);

            powerUpRajat = new Rectangle(10, 560, 33, 35);

            powerUpRajat2 = new Rectangle(640, 25, 33, 35);

            powerDown1Rajat = new Rectangle(330, 35, 33, 35);

            powerMix2Rajat = new Rectangle(450, 550, 33, 35);

            powerMixRajat = new Rectangle(570, 250, 33, 35);


            pystySeina1Rajat = new Rectangle(90, 0, 44, 215);

            pystySeina2Rajat = new Rectangle(90, 300, 44, 215);

            pystySeina3Rajat = new Rectangle(90, 420, 44, 215);

            pystySeina4Rajat = new Rectangle(210, 450, 44, 215);

            pystySeina5Rajat = new Rectangle(210, 250, 44, 215);

            pystySeina6Rajat = new Rectangle(450, 300, 44, 215);

            pystySeina7Rajat = new Rectangle(330, 100, 44, 215);

            pystySeina8Rajat = new Rectangle(330, 270, 44, 215);

            pystySeina9Rajat = new Rectangle(210, 80, 44, 215);

            pystySeina10Rajat = new Rectangle(450, 50, 44, 215);

            pystySeina11Rajat = new Rectangle(450, 200, 44, 215);

            pystySeina12Rajat = new Rectangle(570, 0, 44, 215);

            pystySeina13Rajat = new Rectangle(570, 300, 44, 215);

            pystySeina14Rajat = new Rectangle(570, 500, 44, 215);


            vaakaSeina1Rajat = new Rectangle(605, 110, 137,46);

            //vaakaSeina2Rajat = new Rectangle(220, 220, 137, 46);

            vaakaSeina3Rajat = new Rectangle(605, 340, 137, 46);

            vaakaSeina4Rajat = new Rectangle(700, 450, 137, 46);

            maaliRajat = new Rectangle(700, 530, 100, 115);


        }

        /**
         * Kaikki mitä pitää tehdä kun peli käynnistyy.
         * 
         * Esimerkiksi aseta muuttujat tarvittaviin arvoihin, tai käynnistä musiikki.
         */
        public override void Start()
        {

            //MUUTTUJIEN ARVOT

            liikeNopeus = 3;
            tutorialAika = 300;
            tutorialKokonaisaika = 1;
            powerUpNosto = false;
            powerDown1Nosto = false;
            powerMix2Nosto = false;
            powerMixNosto = false;
            powerUpNosto2 = false;

            pelaajaRajat.X = 10;
            pelaajaRajat.Y = 10;

        }

        /**
         * Ajetaan kun peli sulkeutuu. Piilota äänet ja puhdista roskasi seuraavaa peliä varten.
         */
        public override void Stop()
        {

            pelaajaLiike = new Vector2(10, 10);

        }

        /**
         * Ajetaan kun peliä pitää päivittää. Tänne menee itse pelin logiikka koodi,
         * törmäys chekkaukset, pisteen laskut, yms.
         * 
         * gameTime avulla voidaan synkata nopeus tasaikseksi vaikka framerate ei olisi tasainen.
         */
        public override void Update(GameTime gameTime)
        {

            //INPUT MANAGEMENT

            if (powerMixNosto == true)
            {
                KeyboardState keyboard = Keyboard.GetState();
                if (keyboard.IsKeyDown(Keys.S))
                { pelaajaLiike.Y -= liikeNopeus; }
                if (keyboard.IsKeyDown(Keys.W))
                { pelaajaLiike.Y += liikeNopeus; }
                if (keyboard.IsKeyDown(Keys.D))
                { pelaajaLiike.X -= liikeNopeus; }
                if (keyboard.IsKeyDown(Keys.A))
                { pelaajaLiike.X += liikeNopeus; }

            }
            else
            {
                KeyboardState keyboard = Keyboard.GetState();
                if (keyboard.IsKeyDown(Keys.W))
                { pelaajaLiike.Y -= liikeNopeus; }
                if (keyboard.IsKeyDown(Keys.S))
                { pelaajaLiike.Y += liikeNopeus; }
                if (keyboard.IsKeyDown(Keys.A))
                { pelaajaLiike.X -= liikeNopeus; }
                if (keyboard.IsKeyDown(Keys.D))
                { pelaajaLiike.X += liikeNopeus; }
            }

            //POWERUPPIEN NOSTO

            if (pelaajaRajat.Intersects(powerMixRajat))
            {
                powerMixNosto = true;
            }

            if (pelaajaRajat.Intersects(powerUpRajat2))
            {
                liikeNopeus += 0.1f;
                powerUpNosto = true;
            }

            if (pelaajaRajat.Intersects(powerUpRajat))
            {
                liikeNopeus += 0.1f;
                powerUpNosto = true;
            }

            if (pelaajaRajat.Intersects(powerDown1Rajat))
            {
                liikeNopeus -= 0.1f;
                powerDown1Nosto = true;
            }

            if (pelaajaRajat.Intersects(powerMix2Rajat))
            {
                powerMixNosto = true;
            }

            if (pelaajaRajat.Intersects(backgroundRect))
            {
                //ebin pojat, parasta koodia ever
            }
            else
            {
                CloseGame(true);
            }


            // SEINIIN TÖRMÄÄMINEN

            if (pelaajaRajat.Intersects(maaliRajat))
            {
                Console.WriteLine("Maali!");
                CloseGame(true);
            }

            if (pelaajaRajat.Intersects(pystySeina1Rajat))
            {
                Console.WriteLine("Osui 1.");
                CloseGame(false);
            }
            if (pelaajaRajat.Intersects(pystySeina2Rajat))
            {
                Console.WriteLine("Osui 2.");
                CloseGame(false);
            }
            if (pelaajaRajat.Intersects(pystySeina3Rajat))
            {
                Console.WriteLine("Osui 3.");
                CloseGame(false);
            }
            if (pelaajaRajat.Intersects(pystySeina4Rajat))
            {
                Console.WriteLine("Osui 4.");
                CloseGame(false);
            }
            if (pelaajaRajat.Intersects(pystySeina5Rajat))
            {
                Console.WriteLine("Osui 5.");
                CloseGame(false);
            }
            if (pelaajaRajat.Intersects(pystySeina6Rajat))
            {
                Console.WriteLine("Osui 6.");
                CloseGame(false);
            }
            if (pelaajaRajat.Intersects(pystySeina7Rajat))
            {
                Console.WriteLine("Osui 7.");
                CloseGame(false);
            }
            if (pelaajaRajat.Intersects(pystySeina8Rajat))
            {
                Console.WriteLine("Osui 8.");
                CloseGame(false);
            }
            if (pelaajaRajat.Intersects(pystySeina9Rajat))
            {
                Console.WriteLine("Osui 9.");
                CloseGame(false);
            }
            if (pelaajaRajat.Intersects(pystySeina10Rajat))
            {
                Console.WriteLine("Osui 10.");
                CloseGame(false);
            }
            if (pelaajaRajat.Intersects(pystySeina11Rajat))
            {
                Console.WriteLine("Osui 11.");
                CloseGame(false);
            }
            if (pelaajaRajat.Intersects(pystySeina12Rajat))
            {
                Console.WriteLine("Osui 12.");
                CloseGame(false);
            }
            if (pelaajaRajat.Intersects(pystySeina13Rajat))
            {
                Console.WriteLine("Osui 13.");
                CloseGame(false);
            }
            if (pelaajaRajat.Intersects(pystySeina14Rajat))
            {
                Console.WriteLine("Osui 14.");
                CloseGame(false);
            }

            if (pelaajaRajat.Intersects(vaakaSeina1Rajat))
            {
                Console.WriteLine("Osui 15.");
                CloseGame(false);
            }
            if (pelaajaRajat.Intersects(vaakaSeina3Rajat))
            {
                Console.WriteLine("Osui 16.");
                CloseGame(false);
            }
            if (pelaajaRajat.Intersects(vaakaSeina4Rajat))
            {
                Console.WriteLine("Osui 17.");
                CloseGame(false);
            }


            //RECTANGLEN PÄIVITYS PELAAJAN LIIKKUMISESSA

            pelaajaRajat.X = (int)pelaajaLiike.X;
            pelaajaRajat.Y = (int)pelaajaLiike.Y;

            //TUTORIALIN NÄYTTÖAIKA

            tutorialKokonaisaika = tutorialKokonaisaika - tutorialAika;



            // powerUpRajat.X = (int)powerUpPaikka.X;
            // powerUpRajat.Y = (int)powerUpPaikka.Y;

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

            spriteBatch.Draw(backgroundTexture, backgroundRect, Color.White);
            spriteBatch.Draw(pelaaja, pelaajaLiike, Color.White);

            spriteBatch.Draw(pystySeina1, pystySeina1Paikka, Color.White);
            spriteBatch.Draw(pystySeina2, pystySeina2Paikka, Color.White);
            spriteBatch.Draw(pystySeina3, pystySeina3Paikka, Color.White);
            spriteBatch.Draw(pystySeina4, pystySeina4Paikka, Color.White);
            spriteBatch.Draw(pystySeina5, pystySeina5Paikka, Color.White);
            spriteBatch.Draw(pystySeina6, pystySeina6Paikka, Color.White);
            spriteBatch.Draw(pystySeina7, pystySeina7Paikka, Color.White);
            spriteBatch.Draw(pystySeina8, pystySeina8Paikka, Color.White);
            spriteBatch.Draw(pystySeina9, pystySeina9Paikka, Color.White);
            spriteBatch.Draw(pystySeina10, pystySeina10Paikka, Color.White);
            spriteBatch.Draw(pystySeina11, pystySeina11Paikka, Color.White);
            spriteBatch.Draw(pystySeina12, pystySeina12Paikka, Color.White);
            spriteBatch.Draw(pystySeina13, pystySeina13Paikka, Color.White);
            spriteBatch.Draw(pystySeina14, pystySeina14Paikka, Color.White);

            spriteBatch.Draw(vaakaSeina1, vaakaSeina1Paikka, Color.White);
           // spriteBatch.Draw(vaakaSeina2, vaakaSeina2Paikka, Color.White);
            spriteBatch.Draw(vaakaSeina3, vaakaSeina3Paikka, Color.White);
            spriteBatch.Draw(vaakaSeina4, vaakaSeina4Paikka, Color.White);

            spriteBatch.Draw(maali, maaliPaikka, Color.White);


            //POWERUPPIEN PIIRTÄMINEN NOSTAMISEN JÄLKEEN

            if (powerUpNosto == false)
            {
                spriteBatch.Draw(powerUp, powerUpPaikka, Color.White);
            }

            if (powerDown1Nosto == false)
            {
                spriteBatch.Draw(powerDown1, powerDown1Paikka, Color.White);
            }

            if (powerMix2Nosto == false)
            {
                spriteBatch.Draw(powerMix2, powerMix2Paikka, Color.White);
            }

            if (powerMixNosto == false)
            {
                spriteBatch.Draw(powerMix, powerMixPaikka, Color.White);
            }

            if(powerUpNosto2 == false)
            {
                spriteBatch.Draw(powerUp2, powerUpPaikka2, Color.White);
            }

            //TUTORIALIN PIIRTO

            //if (tutorialKokonaisaika > 0)
            //{
            //    spriteBatch.Draw();
            //}



            //spriteBatch.Draw funktiolla voit piirtää ruudulle.
            //Palikka piirretään y akselilla, valuen kohtaan
            //spriteBatch.Draw(spriteBox, new Vector2(50, value), Color.White);
        }

    }
}

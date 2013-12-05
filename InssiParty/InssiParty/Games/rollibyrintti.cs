//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using InssiParty.Engine;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Input;

//namespace InssiParty.Games
//{
//    /**
//     * Rallittele Röllirallissa
//     * 
//     * Selvitä labyrintti törmäämättä seiniin.
//     * 
//     * By: röllipukki / Jouni Friman PS. PASKINTA KOODIA IKINÄ!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//     */
//    class rollibyrintti : GameBase
//    {
//        SpriteBatch spriteBatch;

//        private float liikeNopeus = 3;
//        private bool powerUpNosto = false;
//        private bool powerDown1Nosto = false;
//        private bool powerDown2Nosto = false;
//        private bool powerMixNosto = false;

//        //tekstuurit
//        private Texture2D backgroundTexture;
//        private Texture2D pelaaja;
//        private Texture2D powerUp;
//        private Texture2D powerDown1;
//        private Texture2D powerDown2;
//        private Texture2D powerMix;

//        private Texture2D pystySeina1;
//        private Texture2D pystySeina2;
//        private Texture2D pystySeina3;
//        private Texture2D pystySeina4;
//        private Texture2D pystySeina5;
//        private Texture2D pystySeina6;
//        private Texture2D pystySeina7;
//        private Texture2D pystySeina8;
//        private Texture2D pystySeina9;
//        private Texture2D pystySeina10;
//        private Texture2D pystySeina11;
//        private Texture2D pystySeina12;
//        private Texture2D pystySeina13;
//        private Texture2D pystySeina14;

//        private Texture2D vaakaSeina1;
//        private Texture2D vaakaSeina2;
//        private Texture2D vaakaSeina3;
//        private Texture2D vaakaSeina4;

//        private Texture2D maali;

//        //vektorit
//        private Vector2 pelaajaLiike;
//        private Vector2 powerUpPaikka;
//        private Vector2 powerDown1Paikka;
//        private Vector2 powerDown2Paikka;
//        private Vector2 powerMixPaikka;

//        private Vector2 pystySeina1Paikka;
//        private Vector2 pystySeina2Paikka;
//        private Vector2 pystySeina3Paikka;
//        private Vector2 pystySeina4Paikka;
//        private Vector2 pystySeina5Paikka;
//        private Vector2 pystySeina6Paikka;
//        private Vector2 pystySeina7Paikka;
//        private Vector2 pystySeina8Paikka;
//        private Vector2 pystySeina9Paikka;
//        private Vector2 pystySeina10Paikka;
//        private Vector2 pystySeina11Paikka;
//        private Vector2 pystySeina12Paikka;
//        private Vector2 pystySeina13Paikka;
//        private Vector2 pystySeina14Paikka;

//        private Vector2 vaakaSeina1Paikka;
//        private Vector2 vaakaSeina2Paikka;
//        private Vector2 vaakaSeina3Paikka;
//        private Vector2 vaakaSeina4Paikka;

//        private Vector2 maaliPaikka;

//        //rectanglet
//        private Rectangle backgroundRect;
//        private Rectangle pelaajaRajat;
//        private Rectangle powerUpRajat;
//        private Rectangle powerDown1Rajat;
//        private Rectangle powerDown2Rajat;
//        private Rectangle powerMixRajat;

//        private Rectangle pystySeina1Rajat;
//        private Rectangle pystySeina2Rajat;
//        private Rectangle pystySeina3Rajat;
//        private Rectangle pystySeina4Rajat;
//        private Rectangle pystySeina5Rajat;
//        private Rectangle pystySeina6Rajat;
//        private Rectangle pystySeina7Rajat;
//        private Rectangle pystySeina8Rajat;
//        private Rectangle pystySeina9Rajat;
//        private Rectangle pystySeina10Rajat;
//        private Rectangle pystySeina11Rajat;
//        private Rectangle pystySeina12Rajat;
//        private Rectangle pystySeina13Rajat;
//        private Rectangle pystySeina14Rajat;

//        private Rectangle vaakaSeina1Rajat;
//        private Rectangle vaakaSeina2Rajat;
//        private Rectangle vaakaSeina3Rajat;
//        private Rectangle vaakaSeina4Rajat;

//        private Rectangle maaliRajat;

//        /**
//         * Lataa tekstuureihin itse data.
//         * 
//         * Ajetaan kun koko ohjelma käynnistyy.
//         */
//        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
//        {
//            //Tiedoston pitäisi olla InssiPartyContent projektin alla solution explorerissa.

//            //Tekstuurien lataaminen
//            spriteBatch = new SpriteBatch(GraphicsDevice);

//            backgroundTexture = Content.Load<Texture2D>("rolliBackground");
//            pelaaja = Content.Load<Texture2D>("rolliPelaaja");
//            powerUp = Content.Load<Texture2D>("rolliPowerup");
//            powerDown1 = Content.Load<Texture2D>("rolliPowerdown");
//            powerDown2 = Content.Load<Texture2D>("rolliPowerdown");
//            powerMix = Content.Load<Texture2D>("rolliPowermix");

//            pystySeina1 = Content.Load<Texture2D>("rolliPystyseina");
//            pystySeina2 = Content.Load<Texture2D>("rolliPystyseina");
//            pystySeina3 = Content.Load<Texture2D>("rolliPystyseina");
//            pystySeina4 = Content.Load<Texture2D>("rolliPystyseina");
//            pystySeina5 = Content.Load<Texture2D>("rolliPystyseina");
//            pystySeina6 = Content.Load<Texture2D>("rolliPystyseina");
//            pystySeina7 = Content.Load<Texture2D>("rolliPystyseina");
//            pystySeina8 = Content.Load<Texture2D>("rolliPystyseina");
//            pystySeina9 = Content.Load<Texture2D>("rolliPystyseina");
//            pystySeina10 = Content.Load<Texture2D>("rolliPystyseina");
//            pystySeina11 = Content.Load<Texture2D>("rolliPystyseina");
//            pystySeina12 = Content.Load<Texture2D>("rolliPystyseina");
//            pystySeina13 = Content.Load<Texture2D>("rolliPystyseina");
//            pystySeina14 = Content.Load<Texture2D>("rolliPystyseina");

//            vaakaSeina1 = Content.Load<Texture2D>("rolliVaakaseina");
//            vaakaSeina2 = Content.Load<Texture2D>("rolliVaakaseina");
//            vaakaSeina3 = Content.Load<Texture2D>("rolliVaakaseina");
//            vaakaSeina4 = Content.Load<Texture2D>("rolliVaakaseina");

//            maali = Content.Load<Texture2D>("rolliMaali");

//            //Powereiden koordinaatit

//            pelaajaLiike = new Vector2(10, 10);
//            powerUpPaikka = new Vector2(90, 560);
//            powerDown1Paikka = new Vector2(450, 35);
//            powerDown2Paikka = new Vector2(630, 40);
//            powerMixPaikka = new Vector2(625, 230);

//            pystySeina1Paikka = new Vector2(90, 100);
//            pystySeina2Paikka = new Vector2(90, 300);
//            pystySeina3Paikka = new Vector2(90, 420);
//            pystySeina4Paikka = new Vector2(210, 490);
//            pystySeina5Paikka = new Vector2(210, 270);
//            pystySeina6Paikka = new Vector2(210, 170);
//            pystySeina7Paikka = new Vector2(330, 100);
//            pystySeina8Paikka = new Vector2(330, 270);
//            pystySeina9Paikka = new Vector2(330, 575);
//            pystySeina10Paikka = new Vector2(450, 200);
//            pystySeina11Paikka = new Vector2(450, 420);
//            pystySeina12Paikka = new Vector2(570, 20);
//            pystySeina13Paikka = new Vector2(570, 310);
//            pystySeina14Paikka = new Vector2(570, 500);

//            vaakaSeina1Paikka = new Vector2(660, 110);
//            vaakaSeina2Paikka = new Vector2(740, 235);
//            vaakaSeina3Paikka = new Vector2(660, 340);
//            vaakaSeina4Paikka = new Vector2(740, 450);

//            maaliPaikka = new Vector2(740, 530);


//            // Rajojen määrittäminen
//            backgroundRect = new Rectangle(0, 0, 800, 600);

//            pelaajaRajat = new Rectangle((int)(pelaajaLiike.X - pelaaja.Width / 2),
//                (int)(pelaajaLiike.Y - pelaaja.Height / 2), pelaaja.Width, pelaaja.Height);

//            powerUpRajat = new Rectangle((int)(powerUpPaikka.X - powerUp.Width / 2),
//                (int)(powerUpPaikka.Y - powerUp.Height / 2), powerUp.Width, powerUp.Height);

//            powerDown1Rajat = new Rectangle((int)(powerDown1Paikka.X - powerDown1.Width / 2),
//                (int)(powerDown1Paikka.Y - powerDown1.Height / 2), powerDown1.Width, powerDown1.Height);

//            powerDown2Rajat = new Rectangle((int)(powerDown2Paikka.X - powerDown2.Width / 2),
//                (int)(powerDown2Paikka.Y - powerDown2.Height / 2), powerDown2.Width, powerDown2.Height);

//            powerMixRajat = new Rectangle((int)(powerMixPaikka.X - powerMix.Width / 2),
//                (int)(powerMixPaikka.Y - powerMix.Height / 2), powerMix.Width, powerMix.Height);

//            pystySeina1Rajat = new Rectangle((int)(pystySeina1Paikka.X - pystySeina1.Width / 2),
//                (int)(pystySeina1Paikka.Y - pystySeina1.Height / 2), pystySeina1.Width, pystySeina1.Height);

//            pystySeina2Rajat = new Rectangle((int)(pystySeina2Paikka.X - pystySeina2.Width / 2),
//                (int)(pystySeina2Paikka.Y - pystySeina2.Height / 2), pystySeina2.Width, pystySeina2.Height);

//            pystySeina3Rajat = new Rectangle((int)(pystySeina3Paikka.X - pystySeina3.Width / 2),
//                (int)(pystySeina3Paikka.Y - pystySeina3.Height / 2), pystySeina3.Width, pystySeina3.Height);

//            pystySeina4Rajat = new Rectangle((int)(pystySeina4Paikka.X - pystySeina4.Width / 2),
//                (int)(pystySeina4Paikka.Y - pystySeina4.Height / 2), pystySeina4.Width, pystySeina4.Height);

//            pystySeina5Rajat = new Rectangle((int)(pystySeina5Paikka.X - pystySeina5.Width / 2),
//                (int)(pystySeina5Paikka.Y - pystySeina5.Height / 2), pystySeina5.Width, pystySeina5.Height);

//            pystySeina6Rajat = new Rectangle((int)(pystySeina6Paikka.X - pystySeina6.Width / 2),
//                (int)(pystySeina6Paikka.Y - pystySeina6.Height / 2), pystySeina6.Width, pystySeina6.Height);

//            pystySeina7Rajat = new Rectangle((int)(pystySeina7Paikka.X - pystySeina7.Width / 2),
//                (int)(pystySeina7Paikka.Y - pystySeina7.Height / 2), pystySeina7.Width, pystySeina7.Height);

//            pystySeina8Rajat = new Rectangle((int)(pystySeina8Paikka.X - pystySeina8.Width / 2),
//                (int)(pystySeina8Paikka.Y - pystySeina8.Height / 2), pystySeina8.Width, pystySeina8.Height);

//            pystySeina9Rajat = new Rectangle((int)(pystySeina9Paikka.X - pystySeina9.Width / 2),
//                (int)(pystySeina9Paikka.Y - pystySeina9.Height / 2), pystySeina9.Width, pystySeina9.Height);

//            pystySeina10Rajat = new Rectangle((int)(pystySeina10Paikka.X - pystySeina10.Width / 2),
//                (int)(pystySeina10Paikka.Y - pystySeina10.Height / 2), pystySeina10.Width, pystySeina10.Height);

//            pystySeina11Rajat = new Rectangle((int)(pystySeina11Paikka.X - pystySeina11.Width / 2),
//                (int)(pystySeina11Paikka.Y - pystySeina11.Height / 2), pystySeina11.Width, pystySeina11.Height);

//            pystySeina12Rajat = new Rectangle((int)(pystySeina12Paikka.X - pystySeina12.Width / 2),
//                (int)(pystySeina12Paikka.Y - pystySeina12.Height / 2), pystySeina12.Width, pystySeina12.Height);

//            pystySeina13Rajat = new Rectangle((int)(pystySeina13Paikka.X - pystySeina13.Width / 2),
//                (int)(pystySeina13Paikka.Y - pystySeina13.Height / 2), pystySeina13.Width, pystySeina13.Height);

//            pystySeina14Rajat = new Rectangle((int)(pystySeina14Paikka.X - pystySeina14.Width / 2),
//                (int)(pystySeina14Paikka.Y - pystySeina14.Height / 2), pystySeina14.Width, pystySeina14.Height);


//            vaakaSeina1Rajat = new Rectangle((int)(vaakaSeina1Paikka.X - vaakaSeina1.Width / 2),
//                (int)(vaakaSeina1Paikka.Y - vaakaSeina1.Height / 2), vaakaSeina1.Width, vaakaSeina1.Height);

//            vaakaSeina2Rajat = new Rectangle((int)(vaakaSeina2Paikka.X - vaakaSeina2.Width / 2),
//                (int)(vaakaSeina2Paikka.Y - vaakaSeina2.Height / 2), vaakaSeina2.Width, vaakaSeina2.Height);

//            vaakaSeina3Rajat = new Rectangle((int)(vaakaSeina3Paikka.X - vaakaSeina3.Width / 2),
//                (int)(vaakaSeina3Paikka.Y - vaakaSeina3.Height / 2), vaakaSeina3.Width, vaakaSeina3.Height);

//            vaakaSeina4Rajat = new Rectangle((int)(vaakaSeina4Paikka.X - vaakaSeina4.Width / 2),
//                (int)(vaakaSeina4Paikka.Y - vaakaSeina4.Height / 2), vaakaSeina4.Width, vaakaSeina4.Height);

//            maaliRajat = new Rectangle((int)(maaliPaikka.X - maali.Width / 2),
//                (int)(maaliPaikka.Y - maali.Height / 2), maali.Width, maali.Height);


//        }

//        /**
//         * Kaikki mitä pitää tehdä kun peli käynnistyy.
//         * 
//         * Esimerkiksi aseta muuttujat tarvittaviin arvoihin, tai käynnistä musiikki.
//         */
//        public override void Start()
//        {



//        }

//        /**
//         * Ajetaan kun peli sulkeutuu. Piilota äänet ja puhdista roskasi seuraavaa peliä varten.
//         */
//        public override void Stop()
//        {

//            pelaajaLiike = new Vector2(10, 10);

//        }

//        /**
//         * Ajetaan kun peliä pitää päivittää. Tänne menee itse pelin logiikka koodi,
//         * törmäys chekkaukset, pisteen laskut, yms.
//         * 
//         * gameTime avulla voidaan synkata nopeus tasaikseksi vaikka framerate ei olisi tasainen.
//         */
//        public override void Update(GameTime gameTime)
//        {
//            //value--;
//            //if (value < 0)
//            //{
//            //    //sammuta peli, true jos voitto tapahtui, false jos pelaaaja hävisi.
//            //    CloseGame(true);
//            //}

//            if (powerMixNosto == true)
//            {
//                KeyboardState keyboard = Keyboard.GetState();
//                if (keyboard.IsKeyDown(Keys.S))
//                { pelaajaLiike.Y -= liikeNopeus; }
//                if (keyboard.IsKeyDown(Keys.W))
//                { pelaajaLiike.Y += liikeNopeus; }
//                if (keyboard.IsKeyDown(Keys.D))
//                { pelaajaLiike.X -= liikeNopeus; }
//                if (keyboard.IsKeyDown(Keys.A))
//                { pelaajaLiike.X += liikeNopeus; }

//            }
//            else
//            {
//                KeyboardState keyboard = Keyboard.GetState();
//                if (keyboard.IsKeyDown(Keys.W))
//                { pelaajaLiike.Y -= liikeNopeus; }
//                if (keyboard.IsKeyDown(Keys.S))
//                { pelaajaLiike.Y += liikeNopeus; }
//                if (keyboard.IsKeyDown(Keys.A))
//                { pelaajaLiike.X -= liikeNopeus; }
//                if (keyboard.IsKeyDown(Keys.D))
//                { pelaajaLiike.X += liikeNopeus; }
//            }

//            if (pelaajaRajat.Intersects(powerMixRajat))
//            {
//                powerMixNosto = true;
//            }

//            if (pelaajaRajat.Intersects(powerUpRajat))
//            {
//                liikeNopeus += 0.1f;
//                powerUpNosto = true;
//            }

//            if (pelaajaRajat.Intersects(powerDown1Rajat))
//            {
//                liikeNopeus -= 0.1f;
//                powerDown1Nosto = true;
//            }

//            if (pelaajaRajat.Intersects(powerDown2Rajat))
//            {
//                liikeNopeus -= 0.1f;
//                powerDown2Nosto = true;
//            }




//            if (pelaajaRajat.Intersects(maaliRajat))
//            {
//                Console.WriteLine("Maali!");
//                CloseGame(true);
//            }

//            if (pelaajaRajat.Intersects(pystySeina1Rajat))
//            {
//                Console.WriteLine("Osui 1.");
//                CloseGame(false);
//            }
//            if (pelaajaRajat.Intersects(pystySeina2Rajat))
//            {
//                Console.WriteLine("Osui 2.");
//                CloseGame(false);
//            }
//            if (pelaajaRajat.Intersects(pystySeina3Rajat))
//            {
//                Console.WriteLine("Osui 3.");
//                CloseGame(false);
//            }
//            if (pelaajaRajat.Intersects(pystySeina4Rajat))
//            {
//                Console.WriteLine("Osui 4.");
//                CloseGame(false);
//            }
//            if (pelaajaRajat.Intersects(pystySeina5Rajat))
//            {
//                Console.WriteLine("Osui 5.");
//                CloseGame(false);
//            }
//            if (pelaajaRajat.Intersects(pystySeina6Rajat))
//            {
//                Console.WriteLine("Osui 6.");
//                CloseGame(false);
//            }
//            if (pelaajaRajat.Intersects(pystySeina7Rajat))
//            {
//                Console.WriteLine("Osui 7.");
//                CloseGame(false);
//            }
//            if (pelaajaRajat.Intersects(pystySeina8Rajat))
//            {
//                Console.WriteLine("Osui 8.");
//                CloseGame(false);
//            }
//            if (pelaajaRajat.Intersects(pystySeina9Rajat))
//            {
//                Console.WriteLine("Osui 9.");
//                CloseGame(false);
//            }
//            if (pelaajaRajat.Intersects(pystySeina10Rajat))
//            {
//                Console.WriteLine("Osui 10.");
//                CloseGame(false);
//            }
//            if (pelaajaRajat.Intersects(pystySeina11Rajat))
//            {
//                Console.WriteLine("Osui 11.");
//                CloseGame(false);
//            }
//            if (pelaajaRajat.Intersects(pystySeina12Rajat))
//            {
//                Console.WriteLine("Osui 12.");
//                CloseGame(false);
//            }
//            if (pelaajaRajat.Intersects(pystySeina13Rajat))
//            {
//                Console.WriteLine("Osui 13.");
//                CloseGame(false);
//            }
//            if (pelaajaRajat.Intersects(pystySeina14Rajat))
//            {
//                Console.WriteLine("Osui 14.");
//                CloseGame(false);
//            }




//            pelaajaRajat.X = (int)pelaajaLiike.X;
//            pelaajaRajat.Y = (int)pelaajaLiike.Y;

//            // powerUpRajat.X = (int)powerUpPaikka.X;
//            // powerUpRajat.Y = (int)powerUpPaikka.Y;

//        }

//        /**
//         * Pelkkä piirtäminen
//         * 
//         * ELÄ sijoita pelilogiikkaa tänne.
//         *
//         * gameTime avulla voidaan synkata nopeus tasaikseksi vaikka framerate ei olisi tasainen.
//         */
//        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
//        {

//            spriteBatch.Draw(backgroundTexture, backgroundRect, Color.White);
//            spriteBatch.Draw(pelaaja, pelaajaLiike, Color.White);

//            spriteBatch.Draw(pystySeina1, pystySeina1Paikka, Color.White);
//            spriteBatch.Draw(pystySeina2, pystySeina2Paikka, Color.White);
//            spriteBatch.Draw(pystySeina3, pystySeina3Paikka, Color.White);
//            spriteBatch.Draw(pystySeina4, pystySeina4Paikka, Color.White);
//            spriteBatch.Draw(pystySeina5, pystySeina5Paikka, Color.White);
//            spriteBatch.Draw(pystySeina6, pystySeina6Paikka, Color.White);
//            spriteBatch.Draw(pystySeina7, pystySeina7Paikka, Color.White);
//            spriteBatch.Draw(pystySeina8, pystySeina8Paikka, Color.White);
//            spriteBatch.Draw(pystySeina9, pystySeina9Paikka, Color.White);
//            spriteBatch.Draw(pystySeina10, pystySeina10Paikka, Color.White);
//            spriteBatch.Draw(pystySeina11, pystySeina11Paikka, Color.White);
//            spriteBatch.Draw(pystySeina12, pystySeina12Paikka, Color.White);
//            spriteBatch.Draw(pystySeina13, pystySeina13Paikka, Color.White);
//            spriteBatch.Draw(pystySeina14, pystySeina14Paikka, Color.White);

//            spriteBatch.Draw(vaakaSeina1, vaakaSeina1Paikka, Color.White);
//            spriteBatch.Draw(vaakaSeina2, vaakaSeina2Paikka, Color.White);
//            spriteBatch.Draw(vaakaSeina3, vaakaSeina3Paikka, Color.White);
//            spriteBatch.Draw(vaakaSeina4, vaakaSeina4Paikka, Color.White);

//            spriteBatch.Draw(maali, maaliPaikka, Color.White);



//            if (powerUpNosto == false)
//            {
//                spriteBatch.Draw(powerUp, powerUpPaikka, Color.White);
//            }

//            if (powerDown1Nosto == false)
//            {
//                spriteBatch.Draw(powerDown1, powerDown1Paikka, Color.White);
//            }

//            if (powerDown2Nosto == false)
//            {
//                spriteBatch.Draw(powerDown2, powerDown2Paikka, Color.White);
//            }

//            if (powerMixNosto == false)
//            {
//                spriteBatch.Draw(powerMix, powerMixPaikka, Color.White);
//            }





//            //spriteBatch.Draw funktiolla voit piirtää ruudulle.
//            //Palikka piirretään y akselilla, valuen kohtaan
//            //spriteBatch.Draw(spriteBox, new Vector2(50, value), Color.White);
//        }

//    }
//}

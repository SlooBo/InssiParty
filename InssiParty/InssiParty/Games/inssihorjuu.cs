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
     * Classin nimi pitää vaihtaa, mieluiten sama kuin tiedoston nimi.
     * IGameBase pitää jättää semmoiseksi
     */

    /**
     * PelinNimi
     * 
     * Selitys pelistä
     * 
     * By: Tekijän Nimi
     */
    class inssihorjuu : GameBase
    {
        //Mahdolliset variablet mitä tarvitset pelin aikana on hyvä listata tässä kohdassa.
        private int value;
        private int forward = 0;
        private int inssi_movement;
        private Rectangle collisionRect;
        private Rectangle collisionRect2;
        Random random;
        //Tekstuurit pitää myös listata tässä kohdassa.
        private Texture2D inssi;
        private Texture2D este;


        /**
         * Lataa tekstuureihin itse data.
         * 
         * Ajetaan kun koko ohjelma käynnistyy.
         */
        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //Tiedoston pitäisi olla InssiPartyContent projektin alla solution explorerissa.
            inssi = Content.Load<Texture2D>("obj");
            random = new Random();
            este = Content.Load<Texture2D>("palikka");
            collisionRect2 = new Rectangle(250, 400, 8, 8);
        }

        /**
         * Kaikki mitä pitää tehdä kun peli käynnistyy.
         * 
         * Esimerkiksi aseta muuttujat tarvittaviin arvoihin, tai käynnistä musiikki.
         */
        public override void Start()
        {
            forward = 0;
            inssi_movement = 250;
            Console.WriteLine("Starting hello world");
            value = 500;

        }

        /**
         * Ajetaan kun peli sulkeutuu. Piilota äänet ja puhdista roskasi seuraavaa peliä varten.
         */
        public override void Stop()
        {
            Console.WriteLine("Closing hello world");
        }

        /**
         * Ajetaan kun peliä pitää päivittää. Tänne menee itse pelin logiikka koodi,
         * törmäys chekkaukset, pisteen laskut, yms.
         * 
         * gameTime avulla voidaan synkata nopeus tasaikseksi vaikka framerate ei olisi tasainen.
         */
        public override void Update(GameTime gameTime)
        {
            collisionRect = new Rectangle(forward, inssi_movement, 64, 64);
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.W))
            {
                inssi_movement = inssi_movement - 2;
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                inssi_movement = inssi_movement + 2;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                forward += 2;
            }
            if (keyboard.IsKeyDown(Keys.A))
            {
                forward -= random.Next(3, 5);
            }
            if (random.Next(1, 10) == 3)
            {
                forward -= 5;
            }

            for (int i = 0; i < random.Next(10, 15); i++)
            {
                i++;
                inssi_movement += random.Next(2,3);
            }
            for (int b = 0; b < random.Next(10, 15); b++)
            {
                b++;
                inssi_movement -= random.Next(1, 4);
            }

            if (collisionRect.Intersects(collisionRect2))
            {
                CloseGame(true);
            }
            if (collisionRect.Intersects(new Rectangle(300,300,0,0)));
            {
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
            spriteBatch.Draw(inssi, new Vector2(forward, inssi_movement), Color.White);
            spriteBatch.Draw(este, new Vector2(250, 400), Color.White);
            Console.WriteLine(inssi_movement);
            Console.WriteLine(collisionRect);
        }
    }

}

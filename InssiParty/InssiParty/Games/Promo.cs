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
     * Väistä ATJ-Promoja
     * 
     * Väistä ATJ promotointeja tarpeeksi kauan!
     * 
     * By: Toni Sarvela
     */
    class Promo : GameBase
    {
        GraphicsDevice graphics;
        SpriteBatch spritebatch;

        //variablesit
        private int value;
        private bool osuma = false;

        private Vector2 inssi_kohta, inssi_nopeus, inssi_vauhti;
        private List<ATJ> ATJs;
        private Random random = new Random();
        private float spawn = 0;

        //Tekstuurit

        private Texture2D inssi;
        private Texture2D atj_tex;
        private Texture2D background;

        //rectanglet
        private Rectangle inssi_alue;
        private Rectangle atj_alue;

        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //Tiedoston pitäisi olla InssiPartyContent projektin alla solution explorerissa.
            spritebatch = new SpriteBatch(GraphicsDevice);
            inssi = Content.Load<Texture2D>("Nyancat");
            atj_tex = Content.Load<Texture2D>("propelli");
        }

        /**
         * Kaikki mitä pitää tehdä kun peli käynnistyy.
         * 
         * Esimerkiksi aseta muuttujat tarvittaviin arvoihin, tai käynnistä musiikki.
         */
        public override void Start()
        {
            Console.WriteLine("Starting Väistä ATJ-Promoja");

            value = 1000;

            //inssin liikkeiden vektoreita
            inssi_kohta = new Vector2(400, 300);
            inssi_vauhti = Vector2.Zero;
            inssi_nopeus = new Vector2(5, 5);
            inssi_alue = new Rectangle((int)(inssi_kohta.X - inssi.Width / 2),
                (int)(inssi_kohta.Y - inssi.Height / 2), inssi.Width, inssi.Height);

            ATJs = new List<ATJ>();
        }

        /**
         * Ajetaan kun peli sulkeutuu. Piilota äänet ja puhdista roskasi seuraavaa peliä varten.
         */
        public override void Stop()
        {
            Console.WriteLine("Closing Väistä ATJ-Promoja");

            ATJs = null;
        }

        /**
         * Ajetaan kun peliä pitää päivittää. Tänne menee itse pelin logiikka koodi,
         * törmäys chekkaukset, pisteen laskut, yms.
         * 
         * gameTime avulla voidaan synkata nopeus tasaikseksi vaikka framerate ei olisi tasainen.
         */

        public override void Update(GameTime gameTime)
        {

            value--;


            //    sammuta peli, true jos voitto tapahtui, false jos pelaaaja hävisi.

            //liikkumistoiminnot
            inssi_vauhti = Vector2.Zero;

            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.D))
            {
                inssi_vauhti.X = inssi_nopeus.X;
            }

            if (keyboard.IsKeyDown(Keys.A))
            {
                inssi_vauhti.X = -inssi_nopeus.X;
            }

            if (keyboard.IsKeyDown(Keys.S))
            {
                inssi_vauhti.Y = inssi_nopeus.Y;
            }

            if (keyboard.IsKeyDown(Keys.W))
            {
                inssi_vauhti.Y = -inssi_nopeus.Y;
            }
            int randY = random.Next(100, 400);
            if (keyboard.IsKeyDown(Keys.Space))
            {

                ATJs.Add(new ATJ(atj_tex, new Vector2(500, randY)));
            }
            //rajaa pelaajan liikkumisen ruudun sisään
            inssi_kohta += inssi_vauhti;

            if (inssi_kohta.X < 0)
            {
                inssi_kohta.X = 0;
            }
            if (inssi_kohta.Y < 0)
            {
                inssi_kohta.Y = 0;
            }
            if (inssi_kohta.X > 800 - inssi.Width / 2)
            {
                inssi_kohta.X = 800 - inssi.Width / 2;
            }
            if (inssi_kohta.Y > 600 - inssi.Height / 2)
            {
                inssi_kohta.Y = 600 - inssi.Height / 2;
            }



            if (inssi_alue.Intersects(atj_alue))
            {
                Console.WriteLine("Osuu!");
                inssi_kohta.X = 400;
                inssi_kohta.Y = 300;
            }

            inssi_alue.X = (int)inssi_kohta.X;
            inssi_alue.Y = (int)inssi_kohta.Y;

            spawn += (float)gameTime.ElapsedGameTime.Seconds;

            foreach (ATJ ATJ in ATJs)
            {
                ATJ.Update(graphics);
            }

            if (value < 0)
            {
                CloseGame(true);
            }


        }

        //vihollisten spawnaaminen
        public void LoadATJ()
        {

            int randY = random.Next(100, 400);

            if (spawn >= 1)
            {
                if (ATJs.Count() > 4)
                {
                    spawn = 0;
                    ATJs.Add(new ATJ(atj_tex, new Vector2(500, randY)));
                    Console.WriteLine("ATJt:" + ATJs.Count);
                }

            }

            for (int i = 0; i < ATJs.Count; i++)
            {
                if (!ATJs[i].Visible)
                {
                    ATJs.RemoveAt(i);
                    i--;
                }
            }
        }

        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (ATJ ATJ in ATJs)
            {
                ATJ.Draw(spriteBatch);               
            }
            
            Console.WriteLine("Draw " + value);
            spriteBatch.Draw(inssi, inssi_kohta, Color.White);
                   
        }
    }
}

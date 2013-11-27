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
using Microsoft.Xna.Framework.Audio;

namespace InssiParty.Games
{

    /*
     * Ruokkimi-speli

     * Löydä syötävää tai kuolet
     * By: Hannu
     */
    class FeedGame : GameBase
    {
        //Mahdolliset variablet mitä tarvitset pelin aikana on hyvä listata tässä kohdassa.
        private List<Kaappi> kaapit;
        private Vector2 HandPos;

        //Tekstuurit pitää myös listata tässä kohdassa.
        private Texture2D backround_texture;
        private Texture2D box;
        private Texture2D handu;
        private Texture2D win;
        private Texture2D lose;
        private Texture2D poison;
        private Texture2D ruoka;
        private Texture2D testi;
        private Texture2D Ajastin;

        SoundEffect open, eat, drink, die;

        private bool elossa,hungry;
        private int timer,timer2;

        private Rectangle timer_bar;
        Rectangle HandRect = new Rectangle(0, 0, 4, 4);
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
            Ajastin = new Texture2D(GraphicsDevice, 1, 1);
            Ajastin.SetData(new Color[] {Color.Wheat});

            timer_bar = new Rectangle(0,580,800,20);

            try
            {
                open = Content.Load<SoundEffect>("kaappi_auki");
                eat = Content.Load<SoundEffect>("eat_food");

                drink = Content.Load<SoundEffect>("drink_and_die");
            }
            catch(Exception eek)
            {
                Console.WriteLine(eek.Message);
            }

        }

        /**
         * Kaikki mitä pitää tehdä kun peli käynnistyy.
         * 
         * Esimerkiksi aseta muuttujat tarvittaviin arvoihin, tai käynnistä musiikki.
         */
        public override void Start()
        {
            timer = 0;
            timer2 = 0;
            elossa = true;
            hungry = true;
            Random rand = new Random();
            kaapit = new List<Kaappi>();

            Kaappi temp = new Kaappi();
            temp.auki = false;
            temp.sijainti = new Vector2(0, 0);
            temp.koko = new Vector2(128, 128);
            temp.tavara_id = RandomTavara(rand);
            kaapit.Add(temp);

            temp = new Kaappi();
            temp.auki = false;
            temp.sijainti = new Vector2(200, 300);
            temp.koko = new Vector2(128, 128);
            temp.tavara_id = RandomTavara(rand);
            kaapit.Add(temp);

            temp = new Kaappi();
            temp.auki = false;
            temp.sijainti = new Vector2(500, 0);
            temp.koko = new Vector2(128, 128);
            temp.tavara_id = RandomTavara(rand);
            kaapit.Add(temp);


             
        }

        private int RandomTavara(Random rand)
        {
            int luku = rand.Next(0, 4);
            bool lukuOk = false;
            while (lukuOk == false)
            {
                lukuOk = true; //Oletuksen kaikki o ok, katotaan jos alta löytyy ongelmia
                for (int i = 0; i < kaapit.Count; ++i)
                {
                    if (luku == kaapit[i].tavara_id)
                    {
                        //Tavara löyty kaapista, uusi random
                        luku = rand.Next(0, 2);
                        lukuOk = false; //Uusi luku asetettu, pitää kaikki chekata läpi
                    }
                }
            }

            return luku;
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
                if (HandRect.Intersects(new Rectangle((int)kaapit[i].sijainti.X, (int)kaapit[i].sijainti.Y, (int)kaapit[i].koko.X, (int)kaapit[i].koko.Y)) && mouseState.LeftButton == ButtonState.Pressed && kaapit[i].auki == false)
                {
                    Console.WriteLine("Hand hits the cupboard");
                    kaapit[i].auki = true;
                    open.Play();

                }

                if (HandRect.Intersects(new Rectangle((int)kaapit[i].sijainti.X + 64, (int)kaapit[i].sijainti.Y + 64, (int)kaapit[i].koko.X - 64, (int)kaapit[i].koko.Y - 64)) && kaapit[i].auki == true && mouseState.LeftButton == ButtonState.Pressed)
                {
                    Console.WriteLine("Hand hits item");
                    if (kaapit[i].tavara_id==0)
                    {
                        Console.WriteLine("You Die!!!");
                        if (elossa == true)
                        { 
                            drink.Play(); 
                        }
                        elossa = false;
                    }

                    else if (kaapit[i].tavara_id == 1)
                    {
                        Console.WriteLine("Wololooo");
                    }

                    else if (kaapit[i].tavara_id == 2)
                    {
                        Console.WriteLine("Selvisit hengissä!");
                        if (hungry == true)
                        {
                            eat.Play();
                        }
                        hungry = false;
                    }
                    
                }
            }

            ++timer;

            timer_bar.Width = 800 - timer;

            if (timer == 0 || elossa ==false)
            {
                CloseGame(false);
            }

            if (timer == 0 && hungry == false)
            {
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
                    if (kaapit[i].tavara_id == 0)
                    {
                        spriteBatch.Draw(poison, kaapit[i].sijainti + new Vector2(64, 64), Color.White);
                    }
                    else if (kaapit[i].tavara_id == 1)
                    {
                        spriteBatch.Draw(testi, kaapit[i].sijainti + new Vector2(64, 64), Color.White);
                    }

                    else if (kaapit[i].tavara_id == 2)
                    {
                        spriteBatch.Draw(ruoka, kaapit[i].sijainti + new Vector2(64, 64), Color.White);
                    }
                }
            
            }
            spriteBatch.Draw(Ajastin,timer_bar,Color.White);

            spriteBatch.Draw(handu, HandPos, Color.White);
            //    spriteBatch.Draw(win, new Vector2(0, 0), Color.White);
            //spriteBatch.Draw(lose, new Vector2(0, 0), Color.White);
        }

    }
}
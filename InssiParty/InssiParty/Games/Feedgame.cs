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
        private Texture2D backround_texture, box, handu, win, lose;
        private Texture2D poison,ruoka,testi;
        private Texture2D Ajastin;
        private Texture2D dildo, kopteri, oil, grenade;

        SoundEffect open, eat, drink, die;

        private bool elossa,hungry;
        private int timer,timer2;

        private Rectangle timer_bar;
        Rectangle HandRect = new Rectangle(0, 0, 4, 4);
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
            dildo = Content.Load<Texture2D>("Dildo");
            oil = Content.Load<Texture2D>("Oil");
            kopteri = Content.Load<Texture2D>("Kopteri");
            grenade = Content.Load<Texture2D>("Kranu");
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
            timer2 = 100;
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
            temp.sijainti = new Vector2(0, 472);
            temp.koko = new Vector2(128, 128);
            temp.tavara_id = RandomTavara(rand);
            kaapit.Add(temp);

            temp = new Kaappi();
            temp.auki = false;
            temp.sijainti = new Vector2(128, 472);
            temp.koko = new Vector2(128, 128);
            temp.tavara_id = RandomTavara(rand);
            kaapit.Add(temp);

            temp = new Kaappi();
            temp.auki = false;
            temp.sijainti = new Vector2(256, 472);
            temp.koko = new Vector2(128, 128);
            temp.tavara_id = RandomTavara(rand);
            kaapit.Add(temp);

            temp = new Kaappi();
            temp.auki = false;
            temp.sijainti = new Vector2(384, 0);
            temp.koko = new Vector2(128, 128);
            temp.tavara_id = RandomTavara(rand);
            kaapit.Add(temp);

            temp = new Kaappi();
            temp.auki = false;
            temp.sijainti = new Vector2(512, 0);
            temp.koko = new Vector2(128, 128);
            temp.tavara_id = RandomTavara(rand);
            kaapit.Add(temp);

            temp = new Kaappi();
            temp.auki = false;
            temp.sijainti = new Vector2(672, 472);
            temp.koko = new Vector2(128, 128);
            temp.tavara_id = RandomTavara(rand);
            kaapit.Add(temp);


             
        }

        private int RandomTavara(Random rand)
        {
            int luku = rand.Next(0, 8);
            bool lukuOk = false;
            while (lukuOk == false)
            {
                lukuOk = true; //Oletuksen kaikki o ok, katotaan jos alta löytyy ongelmia
                for (int i = 0; i < kaapit.Count; ++i)
                {
                    if (luku == kaapit[i].tavara_id)
                    {
                        //Tavara löyty kaapista, uusi random
                        luku = rand.Next(0, 8);
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
                    if (HandRect.Intersects(new Rectangle((int)kaapit[i].sijainti.X + 64, (int)kaapit[i].sijainti.Y + 64, (int)kaapit[i].koko.X - 64, (int)kaapit[i].koko.Y - 64))
                        && InputManager.IsMouseButton1Pressed() == true 
                        && kaapit[i].auki == true)
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

                    if (HandRect.Intersects(new Rectangle((int)kaapit[i].sijainti.X, (int)kaapit[i].sijainti.Y, (int)kaapit[i].koko.X, (int)kaapit[i].koko.Y))
        && InputManager.IsMouseButton1Pressed() == true
        && kaapit[i].auki == false)
                    {
                        Console.WriteLine("Hand hits the cupboard");
                        kaapit[i].auki = true;
                        open.Play();

                    }

            }

            ++timer;

            timer_bar.Width = 800 - timer;
            if (elossa == false)
            {
                --timer2;
            }

            if (timer == 800 || elossa ==false && timer2==0)
            {
                CloseGame(false);
            }

            if (timer == 800 && hungry == false)
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
                        spriteBatch.Draw(kopteri, kaapit[i].sijainti + new Vector2(64, 64), Color.White);
                    }
                    else if (kaapit[i].tavara_id == 3)
                    {
                        spriteBatch.Draw(oil, kaapit[i].sijainti + new Vector2(64, 64), Color.White);
                    }
                    else if (kaapit[i].tavara_id == 4)
                    {
                        spriteBatch.Draw(dildo, kaapit[i].sijainti + new Vector2(64, 64), Color.White);
                    }
                    else if (kaapit[i].tavara_id == 5)
                    {
                        spriteBatch.Draw(grenade, kaapit[i].sijainti + new Vector2(64, 64), Color.White);
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
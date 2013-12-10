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
     * Ruokkimis-peli

     * Löydä syötävää tai kuolet
     * By: Hannu
     */
    class FeedGame : GameBase
    {
        private List<Kaappi> kaapit;
        private Vector2 HandPos;

        private Texture2D backround_texture, box,box_open;
        private Texture2D handu, handu_aina, handu_avaus;
        private Texture2D poison,ruoka,testi;
        private Texture2D Ajastin;
        private Texture2D dildo, kopteri, oil, grenade;

        SoundEffect open, eat, drink, heijari, escape,explosion;

        private bool elossa,hungry;
        private int timer,timer2,timer3;

        private Rectangle timer_bar;
        Rectangle HandRect = new Rectangle(0, 0, 32, 32);

        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            backround_texture = Content.Load<Texture2D>("FeedGame_background");
            box = Content.Load<Texture2D>("kaappi");
            box_open = Content.Load<Texture2D>("Auki");
            handu_aina = Content.Load<Texture2D>("hand");
            handu_avaus = Content.Load<Texture2D>("hand_open");
            poison = Content.Load<Texture2D>("Pullo");
            ruoka = Content.Load<Texture2D>("ruoka");
            testi = Content.Load<Texture2D>("testi_item");
            dildo = Content.Load<Texture2D>("Dildo");
            oil = Content.Load<Texture2D>("Oil");
            kopteri = Content.Load<Texture2D>("Kopteri");
            grenade = Content.Load<Texture2D>("Kranu");
            Ajastin = new Texture2D(GraphicsDevice, 1, 1);
            Ajastin.SetData(new Color[] {Color.Indigo});
            timer_bar = new Rectangle(0,580,800,20);

            try
            {
                open = Content.Load<SoundEffect>("kaappi_auki");
                eat = Content.Load<SoundEffect>("eat_food");
                escape = Content.Load<SoundEffect>("Choppaa");
                drink = Content.Load<SoundEffect>("drink_and_die");
                explosion = Content.Load<SoundEffect>("Xplosion");
                heijari = Content.Load<SoundEffect>("Pingas");
            }
            catch(Exception eek)
            {
                Console.WriteLine(eek.Message);
            }

        }

        public override void Start()
        {
            handu = handu_aina;
            timer = 0;
            timer2 = 400;
            timer3 = 250;
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
            temp.sijainti = new Vector2(0, 452);
            temp.koko = new Vector2(128, 128);
            temp.tavara_id = RandomTavara(rand);
            kaapit.Add(temp);

            temp = new Kaappi();
            temp.auki = false;
            temp.sijainti = new Vector2(128, 452);
            temp.koko = new Vector2(128, 128);
            temp.tavara_id = RandomTavara(rand);
            kaapit.Add(temp);

            temp = new Kaappi();
            temp.auki = false;
            temp.sijainti = new Vector2(256, 452);
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
            temp.sijainti = new Vector2(672, 452);
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

        public override void Stop()
        {
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            HandRect.X = mouseState.X;
            HandRect.Y = mouseState.Y;
            HandPos = new Vector2(mouseState.X-32, mouseState.Y-32);

            if(InputManager.IsMouseButton1Pressed() ==true)
            {
                handu = handu_avaus;
            }
            else
            {
                handu=handu_aina;
            }

            for (int i = 0; i < kaapit.Count; i++)
            {
                    if (HandRect.Intersects(new Rectangle((int)kaapit[i].sijainti.X + 64, (int)kaapit[i].sijainti.Y + 64, (int)kaapit[i].koko.X - 64, (int)kaapit[i].koko.Y - 64))
                        && InputManager.IsMouseButton1Pressed() == true 
                        && kaapit[i].auki == true)
                {
                    if (kaapit[i].tavara_id == 0 || kaapit[i].tavara_id == 3)
                    {
                        if (elossa == true)
                        { 
                            drink.Play(); 
                        }
                        elossa = false;
                    }

                    if (kaapit[i].tavara_id == 2)
                    {
                        if (elossa == true)
                        {
                            escape.Play();
                        }
                        hungry = false;
                    }


                    else if (kaapit[i].tavara_id == 1)
                    {
                       heijari.Play();
                    }

                    else if (kaapit[i].tavara_id == 4)
                    {
                        if (hungry == true)
                        {
                            eat.Play();
                        }
                        hungry = false;
                    }
                    else if (kaapit[i].tavara_id == 5)
                    {
                        if (elossa == true)
                        {
                            explosion.Play();
                        }
                        elossa = false;
                    }

                    
                }

                    if (HandRect.Intersects(new Rectangle((int)kaapit[i].sijainti.X, (int)kaapit[i].sijainti.Y, (int)kaapit[i].koko.X, (int)kaapit[i].koko.Y))
        && InputManager.IsMouseButton1Pressed() == true
        && kaapit[i].auki == false)
                    {
                        kaapit[i].auki = true;
                        open.Play();
                    }
            }

            timer+=4;


            timer_bar.Width = 800 - timer;
            if (elossa == false)
            {
                --timer2;
            }

            if (hungry == false)
            {
                --timer3;
            }

            if (timer == 800 || elossa ==false && timer2==0)
            {
                CloseGame(false);
            }

            if (timer == 800 && hungry == false||timer3 == 0 && hungry == false)
            {
                CloseGame(true);
            }

        }

        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Console.WriteLine("Perkele");

            spriteBatch.Draw(backround_texture, new Vector2(0, 0), Color.White);

            for(int i=0; i< kaapit.Count;i++)
            {
                spriteBatch.Draw(box, kaapit[i].sijainti, Color.White);
                if (kaapit[i].auki == true)
                {
                    spriteBatch.Draw(box_open, kaapit[i].sijainti, Color.White);
                    if (kaapit[i].tavara_id == 0)
                    {
                        spriteBatch.Draw(poison, kaapit[i].sijainti + new Vector2(60, 60), Color.White);
                    }
                    else if (kaapit[i].tavara_id == 4)
                    {
                        spriteBatch.Draw(ruoka, kaapit[i].sijainti + new Vector2(60, 60), Color.White);
                    }

                    else if (kaapit[i].tavara_id == 2)
                    {
                        spriteBatch.Draw(kopteri, kaapit[i].sijainti + new Vector2(60, 60), Color.White);
                    }
                    else if (kaapit[i].tavara_id == 3)
                    {
                        spriteBatch.Draw(oil, kaapit[i].sijainti + new Vector2(60, 60), Color.White);
                    }
                    else if (kaapit[i].tavara_id == 1)
                    {
                        spriteBatch.Draw(dildo, kaapit[i].sijainti + new Vector2(60, 60), Color.White);
                    }
                    else if (kaapit[i].tavara_id == 5)
                    {
                        spriteBatch.Draw(grenade, kaapit[i].sijainti + new Vector2(60, 60), Color.White);
                    }
                }
            
            }
            spriteBatch.Draw(Ajastin,timer_bar,Color.White);

            spriteBatch.Draw(handu, HandPos, Color.White);

        }
    }
}
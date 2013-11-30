using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InssiParty.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace InssiParty.Games
{
    /**
     * Silitä Kissaa
     * 
     * Silitä kissaa hiiren vasemmalla, töki oikealla. Tavoitteena silittää niin kauan että kisu nukahtaa
     * 
     * By: Annika Veteli
     */
    class SilitaKissaa : GameBase
    {
        //Mahdolliset variablet mitä tarvitset pelin aikana on hyvä listata tässä kohdassa.
        private int value;
        private int silityskerrat;
        private int silitysvirhe;

        private bool soundLoaded;

        //Kissakuvat
        Texture2D kissatextuuri; //aloituskuva
        Texture2D kissatextuuriPaa;
        Texture2D kissantextuuriMasis;
        Texture2D kissatextuuriSeriously;
        Texture2D kissatextuuriIloinen;
        Texture2D kissavoittokuva;
        Texture2D kissaGameOver;

        //käsikuvat
        Texture2D kasialku; //aloituskäsikuva
        Texture2D silityskasi; //silityskädelle oma kuva
        Texture2D tokkaasormi; //tökkimissormelle myös oma kuva
        Texture2D kasialkutoinen; //toinen käsikuva

        //hiirelle nykyinen ja edellinen määritys
        MouseState currentMouseState;
        MouseState lastMouseState;

        //alueet kissan päälle ja vartalolle erikseen, + ruudunkoko
        Rectangle vartalonelio = new Rectangle(150, 200, 200, 200);
        Rectangle paanelio = new Rectangle(500, 100, 100, 100);
        Rectangle taustakissa = new Rectangle(0, 0, 800, 600);

        // fontti
        SpriteFont fontti;

        //kissaääniä
        SoundEffect muruvihainen;
        SoundEffect murukehrays;
        SoundEffect murupaaosuma;

        private bool voitto = false;
        private bool gameOver = false;

        //insinöörin muuttujat
        Texture2D insinööri;
        Vector2 inssinopeus = new Vector2(110.0f, 0f);
        Vector2 inssiposition = new Vector2(0, 100);

        public override void Load(ContentManager Content, GraphicsDevice device)
        {
            //ladataan kissakuvia :3
            kissatextuuri = Content.Load<Texture2D>("aloitusmurucopy");
            kissatextuuriPaa = Content.Load<Texture2D>("murupaacopy");
            kissantextuuriMasis = Content.Load<Texture2D>("masismuru copy");
            kissatextuuriSeriously = Content.Load<Texture2D>("seriouslymurucopy");
            kissatextuuriIloinen = Content.Load<Texture2D>("muruiloinenpää");
            kissavoittokuva = Content.Load<Texture2D>("nukkuvamurucopy");
            kissaGameOver = Content.Load<Texture2D>("gameovermurucopy");

            //ladataan kasikuvia
            kasialku = Content.Load<Texture2D>("aloituskasi");
            silityskasi = Content.Load<Texture2D>("silityskäsi");
            tokkaasormi = Content.Load<Texture2D>("tokkimissormi");
            kasialkutoinen = Content.Load<Texture2D>("aloituskasi1");

            //ladataan fontti
            fontti = Content.Load<SpriteFont>("DefaultFont");

            //ladataan insinöörinkuva
            insinööri = Content.Load<Texture2D>("aawinsinööri");

            try
            {
                //ladataan kissaääniä
                murukehrays = Content.Load<SoundEffect>("KehräysCutVersion");
                muruvihainen = Content.Load<SoundEffect>("vihaisempi");
                murupaaosuma = Content.Load<SoundEffect>("silmäänosu");

                soundLoaded = true;
            }
            catch (Exception eek)
            {
                Console.WriteLine(eek.Message);

                soundLoaded = false;
            }
        }

        public override void Start()
        {
            //Console.WriteLine("Starting hello world");
            value = 500;    
        }

        public override void Stop()
        {
            //Console.WriteLine("Closing hello world");
        }

        public override void Update(GameTime gameTime)
        {
            var MouseState = Mouse.GetState(); //Määritetään hiiri
            var MousePosition = new Point(MouseState.X, MouseState.Y); // hiiren sijainti ruudulla koordinaatteina

            //lisätty insinöörikuvan liikkuminen pois ruudulta
            int MaxX = 0;
            inssiposition += inssinopeus * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (inssiposition.X >= MaxX)
            {
                inssinopeus *= -1;
                inssiposition.X = MaxX;
                inssiposition.Y = 100;
            }

            //hiiren määrityksiä yhdelle klikkaukselle
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            if (vartalonelio.Contains(MousePosition)) //tarkistetaan onko hiiri vartaloaluella
            {
                if (lastMouseState.LeftButton == ButtonState.Released &&
                        currentMouseState.LeftButton == ButtonState.Pressed)
                //Tarkistetaan onko hiiren vasenpainike painettuna, jos on silitetään.
                //Tarkistetaan yksi hiiren klikkaus
                {
                    kissatextuuri = kissatextuuriPaa; //vaihdetaan kissan kuvaa
                    kasialku = silityskasi; // vaihdetaan käsi silityskädeksi

                    if (soundLoaded)
                    {
                        murukehrays.Play();
                    }
                    silityskerrat++; // lisätään yksi piste silityskertoihin
                }
                if (lastMouseState.RightButton == ButtonState.Released &&
                       currentMouseState.RightButton == ButtonState.Pressed)
                //Tarkistetaan onko hiiren oikeapainike painettuna, jos on tökitään
                //Tarkistetaan yksi klikkaus
                {
                    kissatextuuri = kissantextuuriMasis; // vaihdetaan kissatextuuria
                    kasialku = tokkaasormi; //vaihdetaan kuva tökkäämiskädeksi
                    if (soundLoaded)
                    {
                        muruvihainen.Play();
                    }
                    silitysvirhe++; //lisätään silitysvirhe   
                }
            }
            else if (paanelio.Contains(MousePosition))
            {
                if (lastMouseState.LeftButton == ButtonState.Released &&
                        currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    kissatextuuri = kissatextuuriSeriously;
                    if (soundLoaded)
                    {
                        murupaaosuma.Play();
                    }
                    silitysvirhe++;
                }
                if (lastMouseState.RightButton == ButtonState.Released &&
                        currentMouseState.RightButton == ButtonState.Pressed)
                {
                    kasialku = tokkaasormi;
                    if (soundLoaded)
                    {
                        murupaaosuma.Play();
                    }
                    silitysvirhe++;
                }
            }
            else
            {
                kasialku = kasialkutoinen;
            }

            value--;

            if (silityskerrat == 5)
            {
                voitto = true;
            }

            if (silitysvirhe == 5 || value == 50 && voitto == false)
            {
                gameOver = true;
            }

            if (silityskerrat == 5 && value == 0 && voitto == true)
            {
                CloseGame(true);
            }

            if ( value == 0 && voitto == false)
            {
                CloseGame(false);
            }

            //if (value < 0 )
            //{
            //    //sammuta peli, true jos voitto tapahtui, false jos pelaaaja hävisi.
            //    CloseGame(true);
            //}
        }

        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Console.WriteLine("Draw " + value);

            if (voitto == false && gameOver == false)
            {

                spriteBatch.Draw(kissatextuuri, taustakissa, Color.White);
                spriteBatch.Draw(kasialku, new Vector2(Mouse.GetState().X - 100,
                        Mouse.GetState().Y - 100), Color.White);
            }

            if (voitto == false && gameOver == false && value > 395)
            {
                spriteBatch.Draw(insinööri, inssiposition, Color.White);
            }

            if (voitto == true)
            {
                spriteBatch.Draw(kissavoittokuva, taustakissa, Color.White);
                spriteBatch.DrawString(fontti, "Aaw, kissa nukahti. Voitit pelin!", new Vector2(80, 200), Color.Aquamarine);
            }
            if (gameOver == true)
            {
                spriteBatch.Draw(kissaGameOver, taustakissa, Color.White);
                spriteBatch.DrawString(fontti, "Suututit kissan >:(. Hävisit pelin!", new Vector2(400, 200), Color.Aquamarine);
            }
            spriteBatch.DrawString(fontti, "Silityksia: " + silityskerrat.ToString() + "Virheelliset: " + silitysvirhe.ToString(),
                new Vector2(10, 10), Color.Turquoise);
        }

    }
}

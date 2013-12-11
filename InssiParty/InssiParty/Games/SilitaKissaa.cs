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
        Texture2D kissatextuuri1;

        //käsikuvat
        Texture2D kasialku; //aloituskäsikuva
        Texture2D silityskasi; //silityskädelle oma kuva
        Texture2D tokkaasormi; //tökkimissormelle myös oma kuva
        Texture2D kasialkutoinen; //toinen käsikuva

        //hiirelle nykyinen ja edellinen määritys
        MouseState currentMouseState;
        MouseState lastMouseState;

        //alueet kissan päälle ja vartalolle erikseen, + ruudunkoko
        Rectangle vartalonelio = new Rectangle(150, 200, 250, 200);
        Rectangle paanelio = new Rectangle(450, 100, 150, 200);
        Rectangle taustakissa = new Rectangle(0, 0, 800, 600);

        // fontti
        SpriteFont fontti;

        //kissaääniä
        SoundEffect muruvihainen;
        SoundEffect murukehrays;
        SoundEffect murupaaosuma;
        SoundEffect murutaustalaulu;
        SoundEffectInstance muru;

        private bool voitto;
        private bool gameOver;

        //insinöörin muuttujat
        Texture2D insinööri;
        Vector2 inssinopeus;
        Vector2 inssiposition;
        int MaxX;

        //z-muuttujat
        Texture2D Zkirjain;
        Vector2 Znopeus;
        Vector2 Zposition;
        int ZmaxY;
        float animation_timer = 0.0f; //animaation ajastus
        int animation_frame_count = 2; // animaation kehysten määrä

        //vihainen naama
        Texture2D murrNaama;
        Vector2 murrNopeus;
        Vector2 murrPosition;
        int murrMaxY;
        float animaatioTimer = 0.0f;

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
            kissatextuuri1 = Content.Load<Texture2D>("aloitusmurucopy");

            //ladataan kasikuvia
            kasialku = Content.Load<Texture2D>("aloituskasi");
            silityskasi = Content.Load<Texture2D>("silityskäsi");
            tokkaasormi = Content.Load<Texture2D>("tokkimissormi");
            kasialkutoinen = Content.Load<Texture2D>("aloituskasi1");

            //ladataan fontti
            fontti = Content.Load<SpriteFont>("DefaultFont");

            //ladataan insinöörinkuva
            insinööri = Content.Load<Texture2D>("insinöörianimaatio");

            //z
            Zkirjain = Content.Load<Texture2D>("kirjaimet");

            //vihainen naama
            murrNaama = Content.Load<Texture2D>("murr");

            try
            {
                //ladataan kissaääniä
                murukehrays = Content.Load<SoundEffect>("KehräysCutVersion");
                muruvihainen = Content.Load<SoundEffect>("vihaisempi");
                murupaaosuma = Content.Load<SoundEffect>("silmäänosu");
                murutaustalaulu = Content.Load<SoundEffect>("meowsong");
                muru = murutaustalaulu.CreateInstance();

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
            voitto = false;
            gameOver = false;
            silityskerrat = 0;
            silitysvirhe = 0;
            MaxX = -5;

            kissatextuuri = kissatextuuri1;

            if (soundLoaded)
            {
                muru.Play();
            }

            inssinopeus = new Vector2(110.0f, 0f);
            inssiposition = new Vector2(-5, 100);

            //z
            Znopeus = new Vector2(-20f, 50f);
            Zposition = new Vector2(600, 300);
            ZmaxY = 300;

            //vihainen naama
            murrNopeus = new Vector2(20f, 50f);
            murrPosition = new Vector2(200, 500);
            murrMaxY = 500;
        }

        public override void Stop()
        {
            //Console.WriteLine("Closing hello world");
            muru.Stop();
        }

        public override void Update(GameTime gameTime)
        {
            var MouseState = Mouse.GetState(); //Määritetään hiiri
            var MousePosition = new Point(MouseState.X, MouseState.Y); // hiiren sijainti ruudulla koordinaatteina

            //lisätty insinöörikuvan liikkuminen pois ruudulta
            inssiposition += inssinopeus * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (inssiposition.X >= MaxX)
            {
                inssinopeus *= -1;
                inssiposition.X = MaxX;
                inssiposition.Y = 100;
            }

            //z:tan liikkuminen
            Zposition += Znopeus * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Zposition.Y >= ZmaxY)
            {
                Znopeus *= -1;
                Zposition.X = 600;
                Zposition.Y = ZmaxY;

            }

            murrPosition += murrNopeus * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (murrPosition.Y >= murrMaxY)
            {
                murrNopeus *= -1;
                murrPosition.X = 200;
                murrPosition.Y = murrMaxY;
            }

            //hiiren määrityksiä yhdelle klikkaukselle
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();



            if (vartalonelio.Contains(MousePosition) && gameOver == false && voitto == false) //tarkistetaan onko hiiri vartaloaluella
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
                        muru.Pause();
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
                        muru.Pause();
                        muruvihainen.Play();
                    }
                    silitysvirhe++; //lisätään silitysvirhe   
                }
            }
            else if (paanelio.Contains(MousePosition) && gameOver == false && voitto == false)
            {
                if (lastMouseState.LeftButton == ButtonState.Released &&
                        currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    kissatextuuri = kissatextuuriSeriously;
                    if (soundLoaded)
                    {
                        muru.Pause();
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
                        muru.Pause();
                        murupaaosuma.Play();
                    }
                    silitysvirhe++;
                }
            }
            else
            {
                muru.Resume();
                kasialku = kasialkutoinen;
            }

            value--;

            if (silityskerrat == 10)
            {
                voitto = true;
            }

            if (silitysvirhe == 5 || value == 50 && voitto == false)
            {
                gameOver = true;
            }

            if (silityskerrat == 10 && value == 0 && voitto == true)
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
            //Console.WriteLine("Draw " + value);

            animation_timer += 0.08f;
            int currentFrame = (int)(animation_timer % animation_frame_count);


            if (voitto == false && gameOver == false)
            {
                spriteBatch.Draw(kissatextuuri, taustakissa, Color.White);
                spriteBatch.Draw(kasialku, new Vector2(Mouse.GetState().X - 100,
                        Mouse.GetState().Y - 100), Color.White);

            }

            if (voitto == false && gameOver == false && value > 395)
            {
                //spriteBatch.Draw(insinööri, inssiposition, Color.White);
                spriteBatch.Draw(insinööri, inssiposition, new Rectangle(insinööri.Width  / animation_frame_count * currentFrame, 0, insinööri.Width / animation_frame_count, insinööri.Height),
              Color.White, 0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.0F);
            }

            if (voitto == true && gameOver == false)
            {
                spriteBatch.Draw(kissavoittokuva, taustakissa, Color.White);
                spriteBatch.DrawString(fontti, "Aaw, kissa nukahti. Voitit pelin!", new Vector2(80, 200), Color.Aquamarine);

                spriteBatch.Draw(Zkirjain, Zposition, new Rectangle(Zkirjain.Width / animation_frame_count * currentFrame, 0, Zkirjain.Width / animation_frame_count, Zkirjain.Height),
              Color.White, 0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.0F);
            }
            if (gameOver == true && voitto == false)
            {
                animaatioTimer += 0.05f;
                int currentFrame1 = (int)(animaatioTimer % animation_frame_count);

                spriteBatch.Draw(kissaGameOver, taustakissa, Color.White);
                spriteBatch.DrawString(fontti, "Suututit kissan >:(. Hävisit pelin!", new Vector2(400, 200), Color.Aquamarine);
                spriteBatch.Draw(murrNaama, murrPosition, new Rectangle(murrNaama.Width / animation_frame_count * currentFrame1, 0, murrNaama.Width / animation_frame_count, murrNaama.Height),
                    Color.White, 0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0.0f);
            }
            spriteBatch.DrawString(fontti, "Silityksia: " + silityskerrat.ToString() + "Virheelliset: " + silitysvirhe.ToString(),
                new Vector2(10, 10), Color.Turquoise);
        }

    }
}

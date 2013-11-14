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
     * By: Annika Veteli
     */
    class SilitaKissaa : GameBase
    {
        //Mahdolliset variablet mitä tarvitset pelin aikana on hyvä listata tässä kohdassa.
        private int value;
        private int silityskerrat;
        private int silitysvirhe;

        //Kissakuvat
        Texture2D kissatextuuri; //aloituskuva
        Texture2D kissatextuuriPaa;
        Texture2D kissantextuuriMasis;
        Texture2D kissatextuuriSeriously;
        Texture2D kissatextuuriIloinen;

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


        public override void Load(ContentManager Content, GraphicsDevice device)
        {
            //ladataan kissakuvia :3
            kissatextuuri = Content.Load<Texture2D>("aloitusmuru");
            kissatextuuriPaa = Content.Load<Texture2D>("murupaa");
            kissantextuuriMasis = Content.Load<Texture2D>("masismuru");
            kissatextuuriSeriously = Content.Load<Texture2D>("seriouslymuru");
            kissatextuuriIloinen = Content.Load<Texture2D>("muruiloinenpää");

            //ladataan kasikuvia
            kasialku = Content.Load<Texture2D>("aloituskasi");
            silityskasi = Content.Load<Texture2D>("silityskäsi");
            tokkaasormi = Content.Load<Texture2D>("tokkimissormi");
            kasialkutoinen = Content.Load<Texture2D>("aloituskasi1");

            //ladataan fontti
            fontti = Content.Load<SpriteFont>("fontfont");

            //ladataan kissaääniä
            murukehrays = Content.Load<SoundEffect>("kehräys");
            muruvihainen = Content.Load<SoundEffect>("vihaisempi");
            murupaaosuma = Content.Load<SoundEffect>("silmäänosu");
        }

        /**
         * Kaikki mitä pitää tehdä kun peli käynnistyy.
         * 
         * Esimerkiksi aseta muuttujat tarvittaviin arvoihin, tai käynnistä musiikki.
         */
        public override void Start()
        {
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
            var MouseState = Mouse.GetState(); //Määritetään hiiri
            var MousePosition = new Point(MouseState.X, MouseState.Y); // hiiren sijainti ruudulla koordinaatteina

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
                    murukehrays.Play();
                    silityskerrat++; // lisätään yksi piste silityskertoihin
                }
                if (lastMouseState.RightButton == ButtonState.Released &&
                       currentMouseState.RightButton == ButtonState.Pressed)
                //Tarkistetaan onko hiiren oikeapainike painettuna, jos on tökitään
                //Tarkistetaan yksi klikkaus
                {
                    kissatextuuri = kissantextuuriMasis; // vaihdetaan kissatextuuria
                    kasialku = tokkaasormi; //vaihdetaan kuva tökkäämiskädeksi
                    muruvihainen.Play();
                    silitysvirhe++; //lisätään silitysvirhe   
                }
            }
            else if (paanelio.Contains(MousePosition))
            {
                if (lastMouseState.LeftButton == ButtonState.Released &&
                        currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    kissatextuuri = kissatextuuriSeriously;
                    murupaaosuma.Play();
                    silitysvirhe++;
                }
                if (lastMouseState.RightButton == ButtonState.Released &&
                        currentMouseState.RightButton == ButtonState.Pressed)
                {
                    kasialku = tokkaasormi;
                    murupaaosuma.Play();
                    silitysvirhe++;
                }
            }
            else
            {
                kasialku = kasialkutoinen;
            }

            value--;

            if (value < 0)
            {
                //sammuta peli, true jos voitto tapahtui, false jos pelaaaja hävisi.
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
            Console.WriteLine("Draw " + value);

            //spriteBatch.Draw funktiolla voit piirtää ruudulle

            spriteBatch.Draw(kissatextuuri, taustakissa, Color.White);
            spriteBatch.Draw(kasialku, new Vector2(Mouse.GetState().X - 100,
                    Mouse.GetState().Y - 100), Color.White);
            spriteBatch.DrawString(fontti, "Silityksia: " + silityskerrat.ToString() + "Virheelliset: " + silitysvirhe.ToString(),
                new Vector2(10, 10), Color.Turquoise);
        }

    }
}

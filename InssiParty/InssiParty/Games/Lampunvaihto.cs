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
     * Lampun vaihto
     * 
     * Auta insinööriä vaihtamaan kattolamppu.
     * Muista kytkeä virta pois ennen uuden lampun asettamista.
     * By: Jari Tuomainen
     */
    class Lampunvaihto : GameBase
    {
        //Variablet
        private int uusi = 0;
        private int add = 0;
        private int kiinni = 0;
        private int power = 0;
        int puoli = 1;
        int value = 0;
        int value2 = 0;
        private Vector2 cursorPos;

        //Rectumit
        Rectangle cursorRect = new Rectangle(0, 0, 1, 1);
        Rectangle background = new Rectangle(0, 0, 800, 600);
        Rectangle Lamppu = new Rectangle(292, 180, 184, 180);
        Rectangle Lamppu1 = new Rectangle(292, 180, 92, 180);
        Rectangle Lamppu2 = new Rectangle(384, 180, 92, 180);
        Rectangle Katkaisin = new Rectangle(530, 285, 30, 30);
        Rectangle Uusilamppu = new Rectangle(710, 500, 60, 50);
        Rectangle Hollilla = new Rectangle(292, 240, 184, 180);
        Rectangle Hollilla1 = new Rectangle(292, 240, 92, 180);
        Rectangle Hollilla2 = new Rectangle(384, 240, 92, 180);

        //Tekstuurit
        private Texture2D Background;
        private Texture2D Lamppupaalla;
        private Texture2D Lamppupois;
        private Texture2D Lamppuhollilla;
        private Texture2D Lamppuvoitto;
        private Texture2D Lamppuboom;
        private Texture2D Lamppuaika;
        private Texture2D Kursori;

        //Load content
        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            Background = Content.Load<Texture2D>("lamppupaalla");
            Lamppupaalla = Content.Load<Texture2D>("lamppupaalla");
            Lamppupois = Content.Load<Texture2D>("lamppupois");
            Lamppuhollilla = Content.Load<Texture2D>("lamppuhollilla");
            Lamppuvoitto = Content.Load<Texture2D>("lamppuvoitto");
            Lamppuboom = Content.Load<Texture2D>("lamppuboom");
            Lamppuaika = Content.Load<Texture2D>("lamppuaika");
            Kursori = Content.Load<Texture2D>("Lamppukursori");
        }
        public override void Start()
        {
            Console.WriteLine("Starting Lampun vaihto");
            Background = Lamppupaalla;
            uusi = 0;
            add = 0;
            kiinni = 0;
            power = 0;
            puoli = 1;
            value = 0;
            value2 = 0;
        }
        public override void Stop()
        {
            Console.WriteLine("Closing Lampun vaihto");
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
            cursorRect.X = mouseState.X;
            cursorRect.Y = mouseState.Y;
            cursorPos = new Vector2(mouseState.X, mouseState.Y);
            value++;
            //Otetaan lamppu irti
            if (add < 20)
            {
                if (Lamppu.Intersects(cursorRect))
                {
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        if (Lamppu1.Intersects(cursorRect) && puoli == 2)
                        {
                            add++;
                            puoli = 1;
                        }
                        if (Lamppu2.Intersects(cursorRect) && puoli == 1)
                        {
                            add++;
                            puoli = 2;
                        }
                    }   
                }
                if (Katkaisin.Intersects(cursorRect))
                {
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        power = 1;
                    }
                }
            }
            //Laitetaan toinen lamppu hollille
            if (add > 19)
            {
                Background = Lamppupois;
                if (Katkaisin.Intersects(cursorRect))
                {
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        power = 1;
                    }
                }
                if (Uusilamppu.Intersects(cursorRect))
                {
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        uusi = 1;
                    }
                }
            }
            if (uusi == 1)
            {
                Background = Lamppuhollilla;
                //Ruuvataan uusi lamppu kiinni ja viimeistään tässä vaiheessa pitää muistaa katkaista virta
                if (kiinni < 20)
                {
                    if (Hollilla.Intersects(cursorRect))
                    {
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            if (Hollilla1.Intersects(cursorRect) && puoli == 2)
                            {
                                kiinni++;
                                puoli = 1;
                            }                            
                            if (Hollilla2.Intersects(cursorRect) && puoli == 1)
                            {
                                kiinni++;
                                puoli = 2;
                            }
                        }
                    }
                    if (Katkaisin.Intersects(cursorRect))
                    {
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            power = 1;
                        }
                    }
                }
                if (kiinni > 19)
                {
                    if (power == 1)
                    {
                        value = 0;
                        value2++;
                        if (value2 < 150)
                        {
                            Background = Lamppuvoitto;
                        }
                        if (value2 > 150)
                        {
                            CloseGame(true);
                        }
                    }

                    if (power == 0)
                    {
                        value = 0;
                        value2++;
                        if (value2 < 150)
                        {
                            Background = Lamppuboom;
                        }
                        if (value2 > 150)
                        {
                            CloseGame(false);
                        }
                    }
                }
            }
            if (value > 400)
            {
                Background = Lamppuaika;
            }

            if (value > 550)
            {
                CloseGame(false);
            }
        }
        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Background, background, Color.White);
            spriteBatch.Draw(Kursori, cursorPos, Color.White);
        }
    }
}

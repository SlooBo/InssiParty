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
    class lapsytys : GameBase
    {
        //Variaabelit
        private int value;
        private Vector2 cursorPos;
        private int i, k, a, alpha, fadeinc, win;

        //äänet
        //SoundEffect lapsy,murahdus, musa, raakasy;

        //Tekstuurit
        private Texture2D backgroundTexture, cursorTexture, Fade, teksti;
        private Texture2D jari1, jari2, jari3, jari4;

        //parit rectanglet
        Rectangle objectRect = new Rectangle(0, 0, 100, 800);   //törmättävä rectangle
        Rectangle cursorRect = new Rectangle(0, 0, 100, 100);   //hiiren rectangle
        Rectangle tekstiRect = new Rectangle(0,0,554,136);
        Rectangle background = new Rectangle(0, 0, 800, 600);

        //kontentin loadaus
        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //tekstuurit
            backgroundTexture = Content.Load<Texture2D>("jari1");
            jari1 = Content.Load<Texture2D>("jari1");
            jari2 = Content.Load<Texture2D>("jari2");
            jari3 = Content.Load<Texture2D>("jari3");
            jari4 = Content.Load<Texture2D>("jari4");
            cursorTexture = Content.Load<Texture2D>("aloituskasi1");
            Fade = Content.Load<Texture2D>("alphalayer");
            teksti = Content.Load<Texture2D>("lapsijari");

            //äänet
            /*
            try
            {
                lapsy = Content.Load<SoundEffect>("lapsy1");
                murahdus = Content.Load<SoundEffect>("murahdus");
                musa = Content.Load<SoundEffect>("musa1");
                raakasy = Content.Load<SoundEffect>("raakasy");
            }
            catch (Exception eek)
            {
                Console.WriteLine(eek.Message);
            }
             */
        }

        //peli alku
        public override void Start()
        {
            Console.WriteLine("start game");

            //alustus laskureille
            i = 0;
            value = 0;
            alpha = 1;
            a = 0;
            fadeinc = 10;
           
            //sijainteja
            objectRect.Y = 0;
            tekstiRect.Y = 200;
            tekstiRect.X = 1000;

            // musa.Play( 0.2f , 0 , 0 );
            backgroundTexture = jari1;
            
        }
        
        //pelin loppu
        public override void Stop()
        {
            Console.WriteLine("close game");
        }

        //Update
        public override void Update(GameTime gameTime)
        {
            //Hiiri
            var mouseState = Mouse.GetState();
            cursorRect.X = mouseState.X;
            cursorRect.Y = mouseState.Y;
            cursorPos = new Vector2(mouseState.X, mouseState.Y);
            a++;
            Console.WriteLine(a);

            tekstiRect.X -= 10;

            //Fade

            alpha += fadeinc;
            
            if (alpha == 201)
            {
                fadeinc = 0;
            }
            if (a == 200) 
            {
                fadeinc = -10;
            }
            if (alpha == 1) 
            {
                Mouse.SetPosition(700, 300);
                k = 1;
            }
            
            if (objectRect.Intersects(cursorRect) && k == 1)
            {
                objectRect.X = 0;
                value++;
                i++;
                Console.WriteLine("osuma: " + i);
                k = 2;
                //lapsy.Play();
                backgroundTexture = jari3;
            }
            //M1
            if (objectRect.Intersects(cursorRect) && k == 2)
            {
                objectRect.X = 400 - objectRect.Width / 2;
                value++;
                i++;
                Console.WriteLine("osuma: " + i);
                k = 3;
            }
            //RR
            if (objectRect.Intersects(cursorRect) && k == 3)
            {
                objectRect.X = 800 - objectRect.Width;
                value++;
                i++;
                Console.WriteLine("osuma: " + i);
                k = 4;
                //lapsy.Play();
                backgroundTexture = jari2;
            }
            //M2
            if (objectRect.Intersects(cursorRect) && k == 4)
            {
                objectRect.X = 400 - objectRect.Width / 2;
                value++;
                i++;
                Console.WriteLine("osuma: " + i);
                k = 1;
            }

            //murahtelu
            if (value == 30 || value == 60)
            {
                //murahdus.Play();
            }

            //Loppucheck
            if (value == 100)
            {
                win = 1;
                //raakasy.Play(1,0,0);
                value = 101;
                backgroundTexture = jari4;
                k = 0;
                a = 0;
            }
            else if (a == 100 && value == 101) 
                {
                    CloseGame(true);
                }

            if (a == 1100 && win == 0)
            {
                value = 102;
                k = 0;
                a = 0;
            }
            else if (a == 100 && value == 102)
                {
                    CloseGame(false);
                }

        }

        //Piirtäminen
        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(backgroundTexture, background, Color.White);
            spriteBatch.Draw(cursorTexture, cursorPos, Color.White);
            spriteBatch.Draw(Fade, background, new Color(255,255,255,(byte)MathHelper.Clamp(alpha,0,255)));
            spriteBatch.Draw(teksti,tekstiRect, Color.White);
        }

    }
}
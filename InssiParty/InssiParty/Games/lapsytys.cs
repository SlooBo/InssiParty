

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
        Rectangle background = new Rectangle(0, 0, 800, 600);
        private Vector2 cursorPos;
        private int i, k, a, alpha, fadeinc;

        //äänet
        SoundEffect lapsy;
        SoundEffect murahdus;
        SoundEffect musa;

        //Tekstuurit
        private Texture2D backgroundTexture;
        private Texture2D jari2;
        private Texture2D jari3;
        private Texture2D cursorTexture;
        private Texture2D Fade;
        private Texture2D teksti;

        //parit rectanglet
        Rectangle objectRect = new Rectangle(0, 0, 100, 800);   //törmättävä rectangle
        Rectangle cursorRect = new Rectangle(0, 0, 100, 100);   //hiiren rectangle
        Rectangle tekstiRect = new Rectangle(0,0,554,136);

        //kontentin loadaus
        public override void Load(ContentManager Content)
        {
            //tekstuurit
            backgroundTexture = Content.Load<Texture2D>("jari1");
            jari2 = Content.Load<Texture2D>("jari2");
            jari3 = Content.Load<Texture2D>("jari3");
            cursorTexture = Content.Load<Texture2D>("cursor");
            Fade = Content.Load<Texture2D>("alphalayer");
            teksti = Content.Load<Texture2D>("lapsijari");

            //äänet
            lapsy = Content.Load<SoundEffect>("lapsy1");
            murahdus = Content.Load<SoundEffect>("murahdus");
            musa = Content.Load<SoundEffect>("musa1");
        }

        //peli alku
        public override void Start()
        {
            Console.WriteLine("start game");

            //alustus laskureille
            i = 0;
            value = 0;
            alpha = 1;
            fadeinc = 10;
           
            //sijainteja
            objectRect.Y = 0;
            tekstiRect.Y = 200;
            tekstiRect.X = 1000;

            musa.Play( 0.2f , 0 , 0 );
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
                lapsy.Play();
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
                lapsy.Play();
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
            if (value == 10)
            {
                murahdus.Play();
            }

            //Loppucheck
            if (value == 100 || a == 900)
            {
                musa.Dispose();
                IsRunning = false;
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
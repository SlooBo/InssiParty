

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
    class lapsytys : GameBase
    {
        //Variaabelit
        private int value;
        Rectangle background = new Rectangle(0, 0, 800, 600);
        private Vector2 cursorPos;
        private int i, k;

        //Tekstuurit
        private Texture2D backgroundTexture;
        private Texture2D cursorTexture;
        private Texture2D objectTexture;

        //parit rectanglet
        Rectangle objectRect = new Rectangle(0, 0, 100, 800);   //törmättävä rectangle
        Rectangle cursorRect = new Rectangle(0, 0, 100, 100);   //hiiren rectangle

        //tekstuurien loadaus
        public override void Load(ContentManager Content)
        {
            backgroundTexture = Content.Load<Texture2D>("tausta_temp");
            cursorTexture = Content.Load<Texture2D>("cursor");
        }

        //peli alku
        public override void Start()
        {
            Console.WriteLine("start game");

            //alustus laskureille
            i = 0;
            k = 1;
            value = 0;

            //sijainteja
            Mouse.SetPosition(700, 300);
            objectRect.Y = 0;
        }

        //pelin loppu
        public override void Stop()
        {
            Console.WriteLine("close game");
        }

        //Update
        public override void Update(GameTime gameTime)
        {
            //hiiri
            var mouseState = Mouse.GetState();
            cursorPos = new Vector2(mouseState.X, mouseState.Y);
            cursorRect.X = mouseState.X;
            cursorRect.Y = mouseState.Y;

            //LL
            if (objectRect.Intersects(cursorRect) && k == 1)
            {
                objectRect.X = 0;
                value++;
                i++;
                Console.WriteLine("osuma: " + i);
                k = 2;
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

            //Loppucheck
            if (value == 200)
            {
                IsRunning = false;
            }

        }

        //Piirtäminen
        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(backgroundTexture, background, Color.White);
            spriteBatch.Draw(cursorTexture, cursorPos, Color.White);
        }

    }
}


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
    class valikko : GameBase
    {
        //variaabelit
        private int MENU_value, MENU_value2, MENU_scaleX, MENU_scaleY, MENU_moveX, MENU_moveY;

        //Tekstuurit
        Texture2D MENU_backgroundTexture, MENU_logoTexture, MENU_koodiTexture, MENU_arcadeTexture, MENU_exitTexture, MENU_storyTexture;

        //rektanglet
        Rectangle MENU_background = new Rectangle(0, 0, 800, 600);
        Rectangle MENU_logoRect = new Rectangle(130, 50, 561, 299);
        Rectangle MENU_arcadeRect = new Rectangle(353, 400, 85, 21);
        Rectangle MENU_exitRect = new Rectangle(368, 450, 50, 21);
        Rectangle MENU_storyRect = new Rectangle(325, 350, 147, 21);
        Rectangle MENU_cursorRect = new Rectangle(0, 0, 100, 100);   //hiiren rectangle
        Vector2 cursorPos;
        //vektorit
        Vector2 MENU_koodiVector = new Vector2();
        Vector2 MENU_logoVector = new Vector2();

        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //tekstuurit
            MENU_backgroundTexture = Content.Load<Texture2D>("tausta");
            MENU_logoTexture = Content.Load<Texture2D>("logo_rotate");
            MENU_koodiTexture = Content.Load<Texture2D>("koodit_rotate");
            MENU_arcadeTexture = Content.Load<Texture2D>("arcade");
            MENU_exitTexture = Content.Load<Texture2D>("exit");
            MENU_storyTexture = Content.Load<Texture2D>("storymode");
        }

        public override void Start()
        {
            //alustukset
            MENU_koodiVector.Y = 0;
            MENU_koodiVector.X = -1502 + 800;
            MENU_logoVector.X = 0;
            MENU_logoVector.Y = 0;
            MENU_scaleX = 8;
            MENU_scaleY = 8;
            MENU_moveX = -4;
            MENU_moveY = -4;
            MENU_value = 0;
            MENU_value2 = 0;
        }

        public override void Stop()
        {
            Console.WriteLine("Closing hello world");
        }

        public override void Update(GameTime gameTime)
        {
            //value/gametime
            MENU_value++;

            //Hiiri
            var mouseState = Mouse.GetState();
            MENU_cursorRect.X = mouseState.X;
            MENU_cursorRect.Y = mouseState.Y;
            cursorPos = new Vector2(mouseState.X, mouseState.Y);

            //koodivektori hallinta
            MENU_koodiVector.Y -= 5;
            MENU_koodiVector.X += 1.81f;

            if (MENU_koodiVector.X > 107)
            {
                MENU_koodiVector.X = -1502 + 800;
                MENU_koodiVector.Y = 0;
            }

            //logo hallinta
            if (MENU_value > 100)
            {
                MENU_logoRect.Width += MENU_scaleX;
                MENU_logoRect.Height += MENU_scaleY;
                MENU_logoRect.X += MENU_moveX;
                MENU_logoRect.Y += MENU_moveY;

                if (MENU_logoRect.Height > 400)
                {
                    MENU_scaleX = -8;
                    MENU_scaleY = -8;
                    MENU_moveX = 4;
                    MENU_moveY = 4;
                }
                if (MENU_logoRect.Height < 300)
                {
                    MENU_scaleX = 8;
                    MENU_scaleY = 8;
                    MENU_moveX = -4;
                    MENU_moveY = -4;
                    MENU_value2++;
                }

            }
            if (MENU_value2 > 2 || MENU_value2 == 2)
            {
                MENU_value = 0;
                MENU_value2 = 0;
            }
            
            //valikkokohteet

            // Work in progress
/*
            valikkoState = false;

            if (exitRect.Intersects(cursorRect))
            {
                exitRect.Width = 0;
                exitRect.Height = 0;
                valikkoState = true;
            }

            else
            {
                if (valikkoState == false)
                {
                    exitRect.Height = 21;
                    exitRect.Width = 50;
                }
            }
*/
            if (MENU_value < 0)
            {
                CloseGame(true);
            }

        }

        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(MENU_backgroundTexture, MENU_background, Color.White);
            spriteBatch.Draw(MENU_koodiTexture, MENU_koodiVector, Color.White);
            spriteBatch.Draw(MENU_logoTexture, MENU_logoRect, Color.White);
            spriteBatch.Draw(MENU_arcadeTexture, MENU_arcadeRect, Color.White);
            spriteBatch.Draw(MENU_exitTexture, MENU_exitRect, Color.White);
            spriteBatch.Draw(MENU_storyTexture, MENU_storyRect, Color.White);
        }

    }
}

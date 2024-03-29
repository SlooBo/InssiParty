﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InssiParty.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace InssiParty.Games
{
    //TODO:
    //
    // * Add sound
    // * Add timer to loss / win
    // * create new crosshair

    /**
     * Don't shoot jorma, do nothing to win!
     * 
     * By: Lauri Mäkinen
     */
    class DontShootJorma : GameBase
    {
        /* Resources */
        private Texture2D jormaTexture;
        private Texture2D shotgun1Texture;
        private Texture2D shotgun2Texture;
        private Texture2D cursorTexture;
        private Texture2D barTexture;

        private Rectangle timerBar;

        private SpriteFont font;

        /* Variables */
        private Vector2 cursorPosition;

        private int timer;
        private bool shotDone;

        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            jormaTexture = Content.Load<Texture2D>("dontShoot/jorma");
            shotgun1Texture = Content.Load<Texture2D>("dontShoot/shotgun1");
            shotgun2Texture = Content.Load<Texture2D>("dontShoot/shotgun2");
            cursorTexture = Content.Load<Texture2D>("k00panhiiri");

            font = Content.Load<SpriteFont>("DefaultFont");

            barTexture = new Texture2D(GraphicsDevice, 1, 1);
            barTexture.SetData(new Color[] { Color.White });

            cursorPosition = new Vector2(0, 0);

            timerBar = new Rectangle(0, 580, 800, 20);
        }

        public override void Start()
        {
            timer = 0;
            shotDone = false;
        }

        public override void Stop() { }

        public override void Update(GameTime gameTime)
        {
            cursorPosition.X = Mouse.GetState().X;
            cursorPosition.Y = Mouse.GetState().Y;

            //TODO: Do a check for the game start so the player wont shoot by starting the game.
            if (Mouse.GetState().RightButton == ButtonState.Pressed || Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                shotDone = true;
            }

            ++timer;

            timerBar.Width = 800 - timer;
        }

        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (shotDone == false)
            {
                spriteBatch.Draw(shotgun1Texture, new Vector2(320, 463), Color.WhiteSmoke);
            }
            else
            {
                spriteBatch.Draw(shotgun2Texture, new Vector2(320, 463), Color.WhiteSmoke);
            }

            spriteBatch.Draw(barTexture, timerBar, Color.White);

            spriteBatch.Draw(cursorTexture, new Vector2(Mouse.GetState().X, Mouse.GetState().Y) , Color.White);
        }

    }
}

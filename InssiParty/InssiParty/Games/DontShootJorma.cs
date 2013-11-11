using System;
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
    // * add sound
    // * Add timer to loss / win
    // * create new crosshair

    /**
     * Don't shoot jorma, do nothing to win!
     * 
     * By: Lauri Mäkinen
     */
    class DontShootJorma : GameBase
    {
        private Texture2D jormaTexture;
        private Texture2D shotgun1Texture;
        private Texture2D shotgun2Texture;
        private Texture2D cursorTexture;

        private Vector2 cursorPosition;

        public override void Load(ContentManager Content)
        {
            jormaTexture = Content.Load<Texture2D>("dontShoot/jorma");
            shotgun1Texture = Content.Load<Texture2D>("dontShoot/shotgun1");
            shotgun2Texture = Content.Load<Texture2D>("dontShoot/shotgun2");
            cursorTexture = Content.Load<Texture2D>("Target");

            cursorPosition = new Vector2(0, 0);
        }

        public override void Start()
        {
            //TODO: reset timers
        }

        public override void Stop() { }

        public override void Update(GameTime gameTime)
        {
            cursorPosition.X = Mouse.GetState().X;
            cursorPosition.Y = Mouse.GetState().Y;

            //TODO: Do a check for the game start so the player wont shoot by starting the game.
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                //TODO: Add timer + particles
                CloseGame(false);
            }
        }

        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(shotgun1Texture, new Vector2(320, 463), Color.WhiteSmoke);
            spriteBatch.Draw(cursorTexture, cursorPosition, Color.White);
        }

    }
}

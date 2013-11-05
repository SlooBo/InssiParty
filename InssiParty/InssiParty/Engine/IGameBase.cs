using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace InssiParty.Engine
{
    /**
     * GameBase is a base class for all the games that are build.
     */
    interface IGameBase
    {
        //Is the game running or not? The game can close itself with this.
        bool IsRunning
        {
            get;
            set;
        }

        // Load all the content that the game needs before starting
        void Load(ContentManager Content);

        // Start/stop the game from running
        void Start();
        void Stop();

        //These functions handle the actual gameplay and rendering
        void Render(SpriteBatch spriteBatch, GameTime gameTime);
        void Update(GameTime gameTime);
    }
}

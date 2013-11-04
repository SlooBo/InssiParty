using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace InssiParty.Engine
{
    /**
     * GameBase is a base class for all the games that are build.
     */
    interface IGameBase
    {
        void Load(ContentManager Content);
        void Start();
        void Stop();
        void Render(SpriteBatch spriteBatch);
        void Update();
    }
}

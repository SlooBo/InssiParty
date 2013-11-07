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
    abstract class GameBase
    {
        public String Name { get; set; }
        public ParticleManager particleManager { get; set; }

        //Is the game running or not? The game can close itself with this.
        public bool IsRunning { get; set; }
        public bool IsVictory { get; set; }

        // Load all the content that the game needs before starting
        public abstract void Load(ContentManager Content);

        // Start/stop the game from running
        public abstract void Start();
        public abstract void Stop();

        //These functions handle the actual gameplay and rendering
        public abstract void Render(SpriteBatch spriteBatch, GameTime gameTime);
        public abstract void Update(GameTime gameTime);
        
        /* Following functions are for the game developers */

        /**
         * Close the game with victoryStatus, true for victory, false for failure
         */
        protected void CloseGame(Boolean victoryStatus)
        {
            //Closes the game and gives a victory status

            IsRunning = false;
            IsVictory = victoryStatus;
        }

        /* Functionality below is used by the engine itself */

        /**
         * Proxy for starting the game.
         * This is used to clear up ParticleManager, etc.
         */
        public void StartProxy()
        {
            IsVictory = false;
            particleManager.Reset();

            //GC between games to keep them running smoothly.
            GC.Collect();

            Start();
        }

        /**
         * Instead of directly calling the update, updateProxy is called!
         * This handles custom engine stuff like ParticleManager before the game loop
         */
        public void UpdateProxy(GameTime gameTime)
        {
            particleManager.UpdateParticles();

            Update(gameTime);
        }

        /**
         * Draw particles on top of the game stuff!
         */
        public void RenderProxy(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Render(spriteBatch, gameTime);

            particleManager.RenderParticles(spriteBatch);
        }
    }
}

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
    /**
     * Particle Example
     * 
     * Example usage of the ParticleManager helper.
     * 
     * By: Lauri Mäkinen
     */
    class ParticleExample : GameBase
    {
        private Texture2D particleTexture;

        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            particleTexture = Content.Load<Texture2D>("palikka");
        }

        public override void Start()
        {
            particleManager.setGravity(new Vector2(0,1));
        }

        public override void Stop()
        {
            Console.WriteLine("Closing ParticleExample!");
        }

        public override void Update(GameTime gameTime) {
            if(Mouse.GetState().LeftButton == ButtonState.Pressed){

                particleManager.AddParticle(
                    particleTexture,                                       // Texture
                    new Vector2(Mouse.GetState().X, Mouse.GetState().Y),   // Position
                    new Vector2(-20, -20),                                 // Min speed on x / y axis
                    new Vector2(20, 20),                                   // Max speed on x / y axis
                    10,                                                    // Min time to live
                    50,                                                    // Max time to live
                    10);                                                   // Count of the particles

            }
        }

        public override void Render(SpriteBatch spriteBatch, GameTime gameTime) { }

    }
}

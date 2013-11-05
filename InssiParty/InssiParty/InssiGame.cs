using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using InssiParty.Games;
using InssiParty.Engine;

namespace InssiParty
{

    //TODO:
    // -> Starting/closing games
    // -> forcing specific game start
    // -> transition screens

    public class InssiGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        IGameBase currentGame;

        List<IGameBase> games;

        public InssiGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth  = 800;
            graphics.PreferredBackBufferHeight = 600;
        }

       
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            games = new List<IGameBase>();

            //Lis‰‰ pelisi t‰h‰n listaan!
            /* ############ */
            addGame(new SampleGame());
            addGame(new HelloWorld());
            /* ############ */

            startGame(games[0]);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            // CHANGE GAME LOGIC GOES HERE

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            currentGame.Update(gameTime);

            if (currentGame.IsRunning == false)
            {
                startGame(games[1]);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            currentGame.Render(spriteBatch,gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /**
         * Add a new game and load it!
         */
        private void addGame(IGameBase game)
        {
            games.Add(game);
            game.Load(Content);
        }

        /**
         * Switch game that is running
         */
        private void startGame(IGameBase game)
        {
            game.IsRunning = false;
            currentGame = game;
            currentGame.IsRunning = true;
            currentGame.Start();
        }
    }
}

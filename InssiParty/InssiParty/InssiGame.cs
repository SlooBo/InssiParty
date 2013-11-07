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
    // -> transition screens

    public class InssiGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //EngineSystems
        ParticleManager particleManager;

        //Global resources
        SpriteFont font;

        bool gameActive;
        int menuSelection;
        GameBase currentGame;

        List<GameBase> games;

        public InssiGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            particleManager = new ParticleManager();

            gameActive = false;
            menuSelection = 0;

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

            // Global resources
            font = Content.Load<SpriteFont>("DefaultFont");

            games = new List<GameBase>();

            //Lis‰‰ pelisi t‰h‰n listaan!
            /* ############ */
            addGame(new SampleGame(), "Sample Game");
            addGame(new HelloWorld(), "Hello World");
            addGame(new lapsytys(), "Lapsytys");
            addGame(new ParticleExample(), "Particle Example");
<<<<<<< HEAD
            addGame(new SpotLanguage(), "Spot the real coding language");
=======
            addGame(new Koodirage(), "Koodi Rage");
>>>>>>> 412a568cd58ed2b09b3d5eb0069a298ada7486c2
            /* ############ */

            //startGame(games[2]);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }

            // Close the current game if esc is pressed.
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) == true)
            {
                stopGame();
            }

            if (gameActive == true)
            {
                //Game
                currentGame.UpdateProxy(gameTime);

                if (currentGame.IsRunning == false)
                {
                    startGame(games[1]);
                }
            }
            else
            {
            
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    var mouseState = Mouse.GetState();
                    for (int i = 0; i < games.Count(); ++i)
                    {
                        if (mouseState.Y > 20 + (i * 20) && mouseState.Y < 40 + (i * 20))
                        {
                            startGame(games[i]);
                        }
                    }

                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            if (gameActive == true)
            {
                currentGame.RenderProxy(spriteBatch, gameTime);
            }
            else
            {
                //Title
                spriteBatch.DrawString(font, "InssiParty 2000!", new Vector2(0, 0), Color.Red);

                //List games

                var mouseState = Mouse.GetState();

                for (int i = 0; i < games.Count; ++i)
                {
                    //Check if the mouse is on position:
                    if (mouseState.X < 400 && mouseState.X > 0)
                    {
                        if (mouseState.Y > 20 + (i * 20) && mouseState.Y < 40 + (i * 20))
                        {
                            spriteBatch.DrawString(font, games[i].Name, new Vector2(5, 20 + (i * 20)), Color.Red);
                            continue;
                        }
                    }

                    spriteBatch.DrawString(font, games[i].Name, new Vector2(5, 20 + (i * 20)), Color.Green);
                }

                //Draw the cursor
                spriteBatch.DrawString(font, "*", new Vector2(mouseState.X, mouseState.Y) , Color.Black);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /**
         * Add a new game and load it!
         */
        private void addGame(GameBase game, String name)
        {
            games.Add(game);
            game.Name = name;
            game.Load(Content);
        }

        /**
         * Switch game that is running
         */
        private void startGame(GameBase game)
        {
            gameActive            = true;
            game.IsRunning        = false;
            game.particleManager  = particleManager;
            currentGame           = game;
            currentGame.IsRunning = true;
            currentGame.StartProxy();
        }

        /**
         * Stop the current game from running, fallback to menu.
         */
        private void stopGame()
        {
            gameActive = false;
            currentGame.IsRunning = false;
            currentGame.Stop();
        }
    }
}

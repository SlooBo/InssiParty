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
    // -> Better menu screen
    // -> Private/public state on this class
    // -> Point / Life counters for the gameplay.

    public class InssiGame : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //EngineSystems
        private ParticleManager particleManager;
        private TipManager tipManager;

        //Global resources
        private SpriteFont font;

        //Menu stuff
        private Texture2D cursorTexture;
        private bool gameActive;
        private GameBase currentGame;
        private Vector2 cursorPosition;
        private String currentTip;

        //Transition stuff
        private int transitionTimer;

        //Game objects
        List<GameBase> games;

        public InssiGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            particleManager = new ParticleManager();
            tipManager = new TipManager();

            gameActive = false;
            cursorPosition = new Vector2(0, 0);

            graphics.PreferredBackBufferWidth  = 800;
            graphics.PreferredBackBufferHeight = 600;

            currentTip = "failed to load tips.";
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

            // Local resources
            cursorTexture = Content.Load<Texture2D>("palikka");

            games = new List<GameBase>();
            
            //Lis‰‰ pelisi t‰h‰n listaan!
            /* ############ */

            addGame(new SampleGame(), "Sample Game");
            addGame(new HelloWorld(), "Hello World");
            addGame(new lapsytys(), "L‰psytys");
            addGame(new DontShootJorma(), "Don't shoot Jorma!");
            addGame(new ParticleExample(), "Particle Example");
            addGame(new SpotLanguage(), "Spot the real coding language");
            addGame(new Koodirage(), "Koodi Rage");
            addGame(new FeedGame(),"Ruokkimis-peli");
            addGame(new Lampunvaihto(),"Lampun Vaihto");
            addGame(new tentti(), "Auta inssi‰ tentiss‰");
            addGame(new Shooty(), "Shoot the Nyan-cat!");
            addGame(new rollibyrintti(), "rollaile labyrintissa");
            addGame(new SilitaKissaa(), "Silit‰ kissaa");
            addGame(new Kysymys(), "Random kysymyksi‰");

            /* ############ */

            Console.WriteLine("# Loaded " + games.Count + " games.");
            //startGame(games[2]);

            TipList.InitTipList(tipManager);

            //Choose random tip for the main menu
            currentTip = "Protip: " + tipManager.getRandomTip();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            cursorPosition.X = mouseState.X;
            cursorPosition.Y = mouseState.Y;

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }

            // Close the current game if esc is pressed.
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) == true)
            {
                if (gameActive == true)
                {
                    stopGame();
                }
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
                    for (int i = 0; i < games.Count(); ++i)
                    {
                        if (cursorPosition.Y > 20 + (i * 20) && cursorPosition.Y < 40 + (i * 20))
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
            GraphicsDevice.Clear(Color.Black);
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

                //Draw the tip
                spriteBatch.DrawString(font, currentTip, new Vector2(5, 570), Color.White);

                //Draw the cursor
                spriteBatch.Draw(cursorTexture, cursorPosition, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /**
         * Add a new game and load it!
         */
        private void addGame(GameBase game, String name)
        {
            Console.Write("Loading game: " + name + "... ");

            games.Add(game);
            game.Name = name;
            game.Load(Content,GraphicsDevice);

            Console.WriteLine("Done!");
        }

        /**
         * Switch game that is running
         */
        private void startGame(GameBase game)
        {
            Console.WriteLine("Starting game: " + game.Name);

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
            Console.WriteLine("Stopping game: " + currentGame.Name);

            gameActive = false;
            currentGame.IsRunning = false;
            currentGame.Stop();
        }
    }
}

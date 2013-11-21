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
    // -> Schedule timing for this project
    // -> transition screens
    // -> Better menu screen
    // -> Private/public state on this class
    // -> Point / Life counters for the gameplay.
    // -> CENTER THE GUIDE TEXT AND APPLY THE ANIMATION AND STUFF
    // -> stats/achievement system

    public class InssiGame : Microsoft.Xna.Framework.Game
    {
        private const int TRANSITION_TIME = 150;

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
        private List<GameBase> games;

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
            
            //Lisää pelisi tähän listaan!
            /* ############ */

            addGame(new SampleGame(), "Sample Game", "Pohjapeli");
            addGame(new HelloWorld(), "Hello World", "HelloWorld esimerkki");
            addGame(new lapsytys(), "Läpsytys", "Päivitä ohje InssiGame.cs!");
            addGame(new DontShootJorma(), "Don't shoot Jorma!", "Elä ammu jormaa!");
            addGame(new ParticleExample(), "Particle Example", "Partikkeli esimerkki partikkelijärjestelmälle.");
            addGame(new SpotLanguage(), "Spot the real coding language", "Valitse oikea ohjelmointi kieli.");
            addGame(new Koodirage(), "Koodi Rage", "Päivitä ohje InssiGame.cs!");
            addGame(new FeedGame(), "Ruokkimis-peli", "Find something to eat or die!");
            addGame(new Lampunvaihto(), "Lampun Vaihto", "Auta insinööriä vaihtamaan vessan lamppu.");
            addGame(new tentti(), "Auta inssiä tentissä", "Päivitä ohje InssiGame.cs!");
            addGame(new Shooty(), "Shoot the Nyan-cat!", "Shoot the Nyancat!");
            addGame(new rollibyrintti(), "rollaile labyrintissa", "Päivitä ohje InssiGame.cs!");
            addGame(new SilitaKissaa(), "Silitä kissaa", "Silitä hiiren vasemmalla, töki oikealla");
            addGame(new Kysymys(), "Random kysymyksiä", "Päivitä ohje InssiGame.cs!");
            addGame(new Promo(), "Väistä ATJ-Promoja", "Päivitä ohje InssiGame.cs!");
            addGame(new inssihorjuu(), "Auta inssi kotiin", "Auta huojuva inssi kämpille");
            addGame(new vali(), "demodemodemodemo", "ASFJOPASFJOPASJOPF");

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

            ++transitionTimer;

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

                if (transitionTimer > TRANSITION_TIME)
                {
                    currentGame.UpdateProxy(gameTime);
                }

                if (currentGame.IsRunning == false)
                {
                    startGame(games[1]);
                }
            }
            else
            {
            
                if (Mouse.GetState().LeftButton == ButtonState.Pressed || Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    for (int i = 0; i < games.Count(); ++i)
                    {
                        if (cursorPosition.Y > 20 + (i * 20) && cursorPosition.Y < 40 + (i * 20))
                        {
                            startGame(games[i]);

                            if (Mouse.GetState().RightButton == ButtonState.Pressed)
                            {
                                transitionTimer = TRANSITION_TIME + 10;
                            }
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

                if (transitionTimer < TRANSITION_TIME)
                {
                    spriteBatch.DrawString(font, currentGame.GuideString, new Vector2(300, 280), Color.Red);
                }
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
                spriteBatch.DrawString(font, currentTip, new Vector2(5, 540), Color.White);

                //Draw the cursor
                spriteBatch.Draw(cursorTexture, cursorPosition, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /**
         * Add a new game and load it!
         */
        private void addGame(GameBase game, String name, String help)
        {
            Console.Write("Loading game: " + name + "... ");

            games.Add(game);
            game.Name = name;
            game.GuideString = help;
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

            transitionTimer = 0;
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

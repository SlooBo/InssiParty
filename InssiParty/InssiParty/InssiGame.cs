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
    // -> Point / Life counters for the gameplay.
    // -> CENTER THE GUIDE TEXT AND APPLY THE ANIMATION AND STUFF
    // -> stats/achievement system ?!?

    public class InssiGame : Microsoft.Xna.Framework.Game
    {
        private enum MenuState { IntroScreen, MainMenu, GameList }

        private const int TRANSITION_TIME = 150;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //EngineSystems
        private ParticleManager particleManager;
        private TipManager tipManager;

        //Global resources
        private SpriteFont font;

        //Menu stuff
        private MenuState menuState;
        private Texture2D cursorTexture;
        private bool gameActive;
        private GameBase currentGame;
        private Vector2 cursorPosition;
        private String currentTip;
        private int menuPosition;

        //Music
        private bool soundsLoaded;
        private SoundEffect introThemeMusic;
        private SoundEffectInstance introThemeMusicInstance;
        
        //Transition stuff
        private int transitionTimer;

        //Game objects
        private List<GameBase> games;

        public InssiGame()
        {
            soundsLoaded = false;
            menuState = MenuState.IntroScreen; // START MENU
            menuPosition = 0;

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
            //Menu music, etc globally played music.
            try
            {
                introThemeMusic = Content.Load<SoundEffect>("InssiPartyOpenTheme");
                soundsLoaded = true;

                //DEBUG: Music disabled for debugging 
                introThemeMusicInstance = introThemeMusic.CreateInstance();
               // introThemeMusicInstance.Play();
            }
            catch (Exception eek)
            {
                Console.WriteLine(eek.Message);
            }

            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Global resources
            font = Content.Load<SpriteFont>("DefaultFont");

            // Local resources
            cursorTexture = Content.Load<Texture2D>("palikka");

            games = new List<GameBase>();
            
            //Lisää pelisi tähän listaan!
            /* ############ */

            addGame(new SampleGame(), "Sample Game", "Pohjapeli",false);
            addGame(new HelloWorld(), "Hello World", "HelloWorld esimerkki", false);
            addGame(new lapsytys(), "Läpsytys", "Päivitä ohje InssiGame.cs!", false);
            addGame(new DontShootJorma(), "Don't shoot Jorma!", "Elä ammu jormaa!", false);
            addGame(new ParticleExample(), "Particle Example", "Partikkeli esimerkki partikkelijärjestelmälle.", true);   //DEBUG: Set as true for debugging purposes! Should be false on the final release!
            addGame(new SpotLanguage(), "Spot the real coding language", "Valitse oikea ohjelmointi kieli.", false);
            addGame(new Koodirage(), "Koodi Rage", "Päivitä ohje InssiGame.cs!", false);
            addGame(new FeedGame(), "Ruokkimis-peli", "Find something to eat or die!", false);
            addGame(new Lampunvaihto(), "Lampun Vaihto", "Auta insinööriä vaihtamaan vessan lamppu.", false);
            addGame(new tentti(), "Auta inssiä tentissä", "Päivitä ohje InssiGame.cs!", false);
            addGame(new Shooty(), "Shoot the Nyan-cat!", "Shoot the Nyancat!", false);
            addGame(new rollibyrintti(), "rollaile labyrintissa", "Päivitä ohje InssiGame.cs!", false);
            addGame(new SilitaKissaa(), "Silitä kissaa", "Silitä hiiren vasemmalla, töki oikealla", false);
            addGame(new Kysymys(), "Random kysymyksiä", "Päivitä ohje InssiGame.cs!", false);
            addGame(new Promo(), "Väistä ATJ-Promoja", "Väistä ATJ promotointia tarpeeksi kauan!", false);
            addGame(new inssihorjuu(), "Auta inssi kotiin", "Auta huojuva inssi kämpille", false);
            addGame(new vali(), "demodemodemodemo", "ASFJOPASFJOPASJOPF", false);
            addGame(new speedtester(), "Speedtester", "Näppäimet A, S, K JA L", false);
            addGame(new Olut(), "Avaa Oluttölkki", "Näkeehän sen nimestä", false);

            /* ############ */

            Console.WriteLine("# Loaded " + games.Count + " games.");
            Console.WriteLine("# " + finalGameCount() + " finished games, " + (games.Count - finalGameCount()) + " games under development");
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
            InputManager.UpdateState();
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
                //We are in some menu! Lets switch and run the correct one
                switch (menuState)
                {
                    case MenuState.IntroScreen:
                        IntroUpdate();
                        break;
                    case MenuState.MainMenu:
                        MenuUpdate();
                        break;
                    case MenuState.GameList:
                        GameListUpdate();
                        break;
                }

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend);

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
                //The game is in a menu! Draw em.
                switch (menuState)
                {
                    case MenuState.IntroScreen:
                        IntroDraw();
                        break;
                    case MenuState.MainMenu:
                        MenuDraw();
                        break;
                    case MenuState.GameList:
                        GameListDraw();
                        break;
                }

                //Cursor gets draw on the menu always:
                spriteBatch.Draw(cursorTexture, cursorPosition, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /**
         * Add a new game and load it!
         */
        private void addGame(GameBase game, String name, String help, bool finalVersion)
        {
            Console.Write("Loading game: " + name + "... ");

            games.Add(game);
            game.Name = name;
            game.GuideString = help;
            game.FinalVersion = finalVersion;
            game.Load(Content,GraphicsDevice);

            Console.WriteLine("Done!");
        }

        /**
         * Switch game that is running
         */
        private void startGame(GameBase game)
        {
            introThemeMusicInstance.Stop();
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

        /**
         * Menu systems
         */

        private void IntroUpdate()
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed || Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                menuState = MenuState.MainMenu;
            }

            if (Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                menuState = MenuState.MainMenu;
            }
        }

        private void IntroDraw()
        {
            //TODO: Draw the epic intro sreen here!
            spriteBatch.DrawString(font, "EEPPINEN INTRO RUUTU!\n\nPress any key", new Vector2(0, 0), Color.Pink);
        }

        private void MenuUpdate()
        {
            /*
                * 
                * reference for the menu positions
            spriteBatch.DrawString(font, "Story mode", new Vector2(20, 100), Color.Green);
            spriteBatch.DrawString(font, "Arcade mode", new Vector2(20, 140), Color.Green);
            spriteBatch.DrawString(font, "Exit", new Vector2(20, 180), Color.Green);
            */

            if (Mouse.GetState().LeftButton == ButtonState.Pressed || Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                //The menu is only checked on the Y axis, because logic

                if (cursorPosition.Y > 100 && cursorPosition.Y < 120)
                {
                    Console.WriteLine("Start the damn story mode!");
                }

                if (cursorPosition.Y > 140 && cursorPosition.Y < 160)
                {
                    Console.WriteLine("Arcade Model selected!");
                    menuState = MenuState.GameList;
                }

                if (cursorPosition.Y > 180 && cursorPosition.Y < 200)
                {
                    //Exit the application
                    Exit();
                }
            }
        }

        private void GameListDraw()
        {
            for (int i = 0; i < games.Count; ++i)
            {
                //Check if the mouse is on position:
                if (cursorPosition.X < 400 && cursorPosition.X > 0)
                {
                    if (cursorPosition.Y > 20 + (i * 20) && cursorPosition.Y < 40 + (i * 20))
                    {
                        spriteBatch.DrawString(font, games[i].Name, new Vector2(5, 20 + (i * 20)), Color.Red);
                        continue;
                    }
                }

                spriteBatch.DrawString(font, games[i].Name, new Vector2(5, 20 + (i * 20)), Color.Green);
            }
        }

        private void GameListUpdate()
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

        private void MenuDraw()
        {
            //Title
            spriteBatch.DrawString(font, "InssiParty 2000!", new Vector2(0, 0), Color.Red);

            spriteBatch.DrawString(font, "Päävalikko", new Vector2(20, 20), Color.Red);

            spriteBatch.DrawString(font, "Story mode", new Vector2(20, 100), Color.Green);
            spriteBatch.DrawString(font, "Arcade mode", new Vector2(20, 140), Color.Green);
            spriteBatch.DrawString(font, "Exit", new Vector2(20, 180), Color.Green);

            //Draw the tip
            spriteBatch.DrawString(font, currentTip, new Vector2(5, 540), Color.White);
        }

        /**
         * Get the count of games that are final.
         */
        public int finalGameCount()
        {
            int count = 0;

            for (int i = 0; i < games.Count; ++i)
            {
                if (games[i].FinalVersion == true)
                    ++count;
            }

            return count;
        }

    }
}

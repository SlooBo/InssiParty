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
    // -> Point / Life counters for the gameplay.
    // -> CENTER THE GUIDE TEXT AND APPLY THE ANIMATION AND STUFF
    // -> HelloWorld starts for no good reason when closing a game!

    public class InssiGame : Microsoft.Xna.Framework.Game
    {
        /**
         * Intro screen provides "press any key" screen.
         * Main menu has selection if story,arcade and exit
         * Gamelist is before the arcade mode.
         */
        private enum MenuState { IntroScreen, MainMenu, GameList }

        /**
         * In storymode next game and transitions are played automatically.
         * In arcade mode the game reverts back to the menu after the game.
         */
        private enum GameMode { StoryMode, ArcadeMode }

        /* INTRO SCREEN VARIABLES */
        private int value;

        Texture2D backgroundTexture, logoTexture, koodiTexture;
        AnimatedSprite sprite;

        Rectangle background = new Rectangle(0, 0, 800, 600);
        Rectangle logoRect = new Rectangle(130, 50, 561, 299);
        Rectangle koodiRect = new Rectangle(-1502 + 800, 0, 1502, 2985);
        /*  </intro screen variables> */


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
        private Vector2 cursorPosition;
        private String currentTip;

        //Game state management
        private bool gameActive;
        private GameBase currentGame;
        private GameMode currentGameMode;

        //Music
        private bool soundsLoaded;
        private SoundEffect introThemeMusic;
        private SoundEffectInstance introThemeMusicInstance;
        
        //Transition stuff
        private int transitionTimer;

        //Game objects
        private List<GameBase> games;

        //Story mode stuff
        private int HP;
        private int points;

        private List<GameBase> playableGames;
        private List<GameBase> gamesPlayed;

        //Random stuff
        Random random;

        public InssiGame()
        {
            soundsLoaded = false;
            menuState = MenuState.IntroScreen; // START MENU

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            games = new List<GameBase>();
            gamesPlayed = new List<GameBase>();
            playableGames = new List<GameBase>();

            particleManager = new ParticleManager();
            tipManager = new TipManager();

            gameActive = false;
            cursorPosition = new Vector2(0, 0);

            graphics.PreferredBackBufferWidth  = 800;
            graphics.PreferredBackBufferHeight = 600;

            currentTip = "failed to load tips.";

            random = new Random();
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

            //Intro screen resources
            backgroundTexture = Content.Load<Texture2D>("tausta");
            logoTexture = Content.Load<Texture2D>("logo_rotate");
            koodiTexture = Content.Load<Texture2D>("koodit_rotate");

            sprite = new AnimatedSprite(Content.Load<Texture2D>("anykey60f"), 0, 296, 42);

            sprite.Position = new Vector2(400, 450);

            
            //Lisää pelisi tähän listaan!
            /* ############ */

            //Valmiit pelit listataan ylös niin helpompi kattoa missä mennään.
            addGame(new SpotLanguage(), "Spot the real coding language", "Tunnista ohjelmointi-kielie, skripti-kielistä.", true, "Lauri Mäkinen");
            addGame(new tentti(), "Auta inssiä tentissä", "Päivitä ohje InssiGame.cs!", true, "Miika 'Toopala' Saastamoinen");
            addGame(new Shooty(), "Shoot the Nyan-cat!", "Shoot the Nyancat!", true, "Creator missing!");

            addGame(new SampleGame(), "Sample Game", "Pohjapeli",false,"Lauri Mäkinen");
            addGame(new HelloWorld(), "Hello World", "HelloWorld esimerkki", false,"Lauri Mäkinen");
            addGame(new lapsytys(), "Läpsytys", "Päivitä ohje InssiGame.cs!", false,"Creator missing!");
            addGame(new DontShootJorma(), "Don't shoot Jorma!", "Elä ammu jormaa!", false, "Lauri Mäkinen");
            addGame(new ParticleExample(), "Particle Example", "Partikkeli esimerkki partikkelijärjestelmälle.", false, "Lauri Mäkinen");   //DEBUG: Set as true for debugging purposes! Should be false on the final release!
            addGame(new Koodirage(), "Koodi Rage", "Päivitä ohje InssiGame.cs!", false, "Creator missing!");
            addGame(new FeedGame(), "Ruokkimis-peli", "Find something to eat or die!", false, "Creator missing!");
            addGame(new Lampunvaihto(), "Lampun Vaihto", "Auta insinööriä vaihtamaan vessan lamppu.", false, "Creator missing!");
            addGame(new rollibyrintti(), "rollaile labyrintissa", "Päivitä ohje InssiGame.cs!", false, "Creator missing!");
            addGame(new SilitaKissaa(), "Silitä kissaa", "Silitä hiiren vasemmalla, töki oikealla", false, "Annika Veteli");
            addGame(new Kysymys(), "Random kysymyksiä", "Vastaa Kysymyksiin (K/E)", false, "Creator missing!");
            addGame(new Promo(), "Väistä ATJ-Promoja", "Väistä ATJ promotointia tarpeeksi kauan!", false, "Creator missing!");
            addGame(new inssihorjuu(), "Auta inssi kotiin", "Auta huojuva inssi kämpille", false, "Miika 'Toopala' Saastamoinen");
            addGame(new vali(), "demodemodemodemo", "ASFJOPASFJOPASJOPF", false, "Creator missing!");
            addGame(new speedtester(), "Speedtester", "Näppäimet A, S, K JA L", false, "Annika Veteli");
            addGame(new Olut(), "Avaa Oluttölkki", "Näkeehän sen nimestä", false, "Creator missing!");
            addGame(new valikko_demo(), "demodmeo2", "demodmeo2", false, "Creator missing!");
            addGame(new valikko(), "valikko:demo", "valikko:demo", false, "Creator missing!");
            addGame(new Siivoa(), "Siivoa insinöörin kämppä","klikkaile hiirellä tavarat pois", false, "Annika Veteli");
            addGame(new Pallo(), "Pallo peli", "Käytä A:ta ja D:tä", false,"Marko Sydänmaa");

            /* ############ */

            Console.WriteLine("# Loaded " + games.Count + " games.");
            Console.WriteLine("# " + GameHelpers.finalGameCount(games) + " finished games, " + (games.Count - GameHelpers.finalGameCount(games)) + " games under development");
            //startGame(games[2]);

            TipList.InitTipList(tipManager);

            //Choose random tip for the main menu
            currentTip = "Protip: " + tipManager.getRandomTip();

            //Fill up the "playeable games" list
            for (int i = 0; i < games.Count; ++i)
            {
                if (games[i].FinalVersion)
                    playableGames.Add(games[i]);
            }
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
                    menuState = MenuState.MainMenu;
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
                        IntroUpdate(gameTime);
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
        private void addGame(GameBase game, String name, String help, bool finalVersion, String creator)
        {
            Console.Write("Loading game: " + name + " by " + creator + " ");

            games.Add(game);
            game.Name = name;
            game.GuideString = help;
            game.FinalVersion = finalVersion;
            game.Creator = creator;
            game.Load(Content,GraphicsDevice);

            Console.WriteLine("Done!");
        }

        /**
         * Switch game that is running
         */
        private void startGame(GameBase game)
        {
            if(soundsLoaded)
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
         * Stop the current game from running, if running in arcade mode, fall back to menu.
         */
        private void stopGame()
        {
            Console.WriteLine("Stopping game: " + currentGame.Name);

            gameActive = false;
            currentGame.IsRunning = false;
            currentGame.Stop();

            if (currentGameMode == GameMode.ArcadeMode)
            {
                menuState = MenuState.GameList;
                return;
            }

            if (currentGameMode == GameMode.StoryMode)
            {
                //Start next game, if game is not lost.
                StoryNextGame(currentGame.IsVictory);
                return;
            }
        }

        /**
         * Menu systems
         */

        private void IntroUpdate(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();

            sprite.anykeyMovement(gameTime);
            sprite.Animate(gameTime);

            if (value < 0)
            {
                menuState = MenuState.MainMenu;
            }
            koodiRect.X += 1;
            koodiRect.Y -= 5;

            if (InputManager.IsMouseButton1Pressed())
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
            spriteBatch.Draw(backgroundTexture, background, Color.White);
            spriteBatch.Draw(koodiTexture, koodiRect, Color.White);
            spriteBatch.Draw(logoTexture, logoRect, Color.White);
            spriteBatch.Draw(sprite.Texture, sprite.Position, sprite.SourceRect, Color.White, 0f, sprite.Origin, 1.0f, SpriteEffects.None, 0);
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

            if (InputManager.IsMouseButton1Pressed())
            {
                //The menu is only checked on the Y axis, because logic

                if (cursorPosition.Y > 100 && cursorPosition.Y < 120)
                {
                    Console.WriteLine("Start the story mode!");
                    currentGameMode = GameMode.StoryMode;
                    StartStory();
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
            if (InputManager.IsMouseButton1Pressed() || InputManager.IsMouseButton2Pressed())
            {
                for (int i = 0; i < games.Count(); ++i)
                {
                    if (cursorPosition.Y > 20 + (i * 20) && cursorPosition.Y < 40 + (i * 20))
                    {
                        currentGameMode = GameMode.ArcadeMode;
                        startGame(games[i]);

                        if (Mouse.GetState().RightButton == ButtonState.Pressed)
                        {
                            transitionTimer = TRANSITION_TIME + 10; //skip the transition
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


        /**#########################
         * STORYMODE RELATED STUFF
         *#########################*/

        public void StopStory()
        {
            Console.WriteLine("[StoryManager] Story stop forced.");
        }

        public void StartStory()
        {
            Console.WriteLine("[StoryManager] Starting story...");
            points = 0;
            HP = 0;

            StartNextGame();
        }

        private void StartNextGame()
        {
            bool gameSelected = false;
            int id = 0;
            int missCount = -1;

            if (gamesPlayed.Count == playableGames.Count)
            {
                //All games have been played! Give extra +5 points and clear the list.
                points += 5;

                gamesPlayed.Clear();
                Console.WriteLine("[StoryManager] All games played! +5 points and clearing list");
            }

            while (gameSelected == false)
            {
                id = random.Next(0, playableGames.Count);
                gameSelected = true;

                //check if it has been already played
                for (int i = 0; i < gamesPlayed.Count; ++i)
                {
                    if (playableGames[id] == gamesPlayed[i])
                        gameSelected = false; //The game was already played.
                }

                ++missCount;
            }

            Console.WriteLine("[StoryManager] " + playableGames[id].Name + " chosen. (" + missCount + ") miss count on randoming.");

            gamesPlayed.Add(playableGames[id]);
            startGame(playableGames[id]);
        }


        public void StoryNextGame(bool victoryState)
        {
            if (victoryState == true)
            {
                ++points;
                Console.WriteLine("[StoryManager] +1 point for victory");
            }
            else
            {
                --HP;
                Console.WriteLine("[StoryManager] -1 hp for failure");
            }

            //TODO: Randomize next game.
            StartNextGame();
        }
    }
}

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
        private enum MenuState { IntroScreen, MainMenu, GameList, TransitionMode }

        //Yes, this is getting really ugly, but the project just doesn't have enough time to do it on a proper scale.

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

        //Transition stuff
        //variaabelit
        private int TRANSITION_value, TRANSITION_moveX, TRANSITION_moveY, TRANSITION_scaleY, TRANSITION_scaleX;

        //Tekstuurit
        Texture2D TRANSITION_backgroundTexture, TRANSITION_valiTexture, TRANSITION_sydanTexture, TRANSITION_tekstiTexture;

        //rektanglet
        Rectangle TRANSITION_background = new Rectangle(0, 0, 800, 600);
        Rectangle TRANSITION_keskipalkki = new Rectangle(0, 180, 800, 220);
        Rectangle TRANSITION_sydanRect = new Rectangle(136, 236, 128, 128);
        Rectangle TRANSITION_sydanRect2 = new Rectangle(336, 236, 128, 128);
        Rectangle TRANSITION_sydanRect3 = new Rectangle(536, 236, 128, 128);
        Rectangle TRANSITION_tekstiRect = new Rectangle(-500, 290, 135, 21);

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

            //Transition stuff
            TRANSITION_backgroundTexture = Content.Load<Texture2D>("tausta");
            TRANSITION_valiTexture = Content.Load<Texture2D>("trans_palkit");
            TRANSITION_sydanTexture = Content.Load<Texture2D>("sydan");
            TRANSITION_tekstiTexture = Content.Load<Texture2D>("storymode");
            
            //Lis�� pelisi t�h�n listaan!
            /* ############ */

            //Valmiit pelit listataan yl�s niin helpompi kattoa miss� menn��n.
            addGame(new SpotLanguage(), "Spot the real coding language", "Tunnista ohjelmointi-kielie, skripti-kielist�.", true, "Lauri M�kinen");
            addGame(new tentti(), "Auta inssi� tentiss�", "P�ivit� ohje InssiGame.cs!", true, "Miika 'Toopala' Saastamoinen");
            addGame(new Shooty(), "Shoot the Nyan-cat!", "Ammu Nyan Cat hiiren vasemmalla!", true, "Taneli Vallo");
            addGame(new Siivoa(), "Siivoa insin��rin k�mpp�", "klikkaile hiirell� tavarat pois", true, "Annika Veteli");
            addGame(new speedtester(), "Speedtester", "N�pp�imet A, S, K JA L", true, "Annika Veteli");
            addGame(new SilitaKissaa(), "Silit� kissaa", "Silit� hiiren vasemmalla, t�ki oikealla", true, "Annika Veteli");

            addGame(new lapsytys(), "L�psytys", "P�ivit� ohje InssiGame.cs!", false,"Creator missing!");
            addGame(new DontShootJorma(), "Don't shoot Jorma!", "El� ammu jormaa!", false, "Lauri M�kinen");
            addGame(new ParticleExample(), "Particle Example", "Partikkeli esimerkki partikkelij�rjestelm�lle.", false, "Lauri M�kinen");   //DEBUG: Set as true for debugging purposes! Should be false on the final release!
            addGame(new Koodirage(), "Koodi Rage", "P�ivit� ohje InssiGame.cs!", false, "Creator missing!");
            addGame(new FeedGame(), "Ruokkimis-peli", "Find something to eat or die!", false, "Creator missing!");
            addGame(new Lampunvaihto(), "Lampun Vaihto", "Auta insin��ri� vaihtamaan vessan lamppu.", false, "Creator missing!");
            //addGame(new rollibyrintti(), "Rallittele R�llibyrintiss�", "Hurvaa k�nniss�, mutta varo t�rm�ilem�st� seiniin!", false, "Jouni Friman");
            addGame(new Kysymys(), "Random kysymyksi�", "Vastaa Kysymyksiin (K/E)", false, "Creator missing!");
            addGame(new Promo(), "V�ist� ATJ-Promoja", "V�ist� ATJ promotointia tarpeeksi kauan!", false, "Toni Sarvela!");
            addGame(new inssihorjuu(), "Auta inssi kotiin", "Auta huojuva inssi k�mpille", false, "Miika 'Toopala' Saastamoinen");
            addGame(new Olut(), "Avaa Olutt�lkki", "N�keeh�n sen nimest�", false, "Markus Tolvanen!");   //This game is buggy
            addGame(new Pallo(), "Pallo peli", "K�yt� A:ta ja D:t�", false,"Marko Syd�nmaa");

            //### Games to be implemented outside the game system:
            addGame(new vali(), "Transition demo", "ASFJOPASFJOPASJOPF", false, "Creator missing!");
            addGame(new valikko(), "valikko:demo", "valikko:demo", false, "Creator missing!");

            //### Games to be dropped:
            //    addGame(new valikko_demo(), "demodmeo2", "demodmeo2", false, "Creator missing!");    // Intro menu has been implemented.

            // Example games for development:

            //addGame(new SampleGame(), "Sample Game", "Pohjapeli", false, "Lauri M�kinen");
            //addGame(new HelloWorld(), "Hello World", "HelloWorld esimerkki", false, "Lauri M�kinen");

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
                    //Running of game has been stopped. Lets stop it from the engine side also.
                    stopGame();
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
                    case MenuState.TransitionMode:
                        TransitionUpdate();
                        break;
                }

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

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
                    case MenuState.TransitionMode:
                        TransitionModeDraw();
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

        private void MenuDraw()
        {
            //Title
            spriteBatch.DrawString(font, "InssiParty 2000!", new Vector2(0, 0), Color.Red);

            spriteBatch.DrawString(font, "P��valikko", new Vector2(20, 20), Color.Red);

            spriteBatch.DrawString(font, "Story mode", new Vector2(20, 100), Color.Green);
            spriteBatch.DrawString(font, "Arcade mode", new Vector2(20, 140), Color.Green);
            spriteBatch.DrawString(font, "Exit", new Vector2(20, 180), Color.Green);

            //Draw the tip
            spriteBatch.DrawString(font, currentTip, new Vector2(5, 540), Color.White);
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


        /**#########################
         * STORYMODE RELATED STUFF
         *#########################*/

        public void StopStory()
        {
            menuState = MenuState.IntroScreen;
            gamesPlayed.Clear();
            gameActive = false;
            Console.WriteLine("[StoryManager] Story stop forced.");
        }

        public void StartStory()
        {
            Console.WriteLine("[StoryManager] Starting story...");
            points = 0;
            HP = 3;
            gamesPlayed.Clear();

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

            Console.WriteLine("[StoryManager] Points: " + points);
            Console.WriteLine("[StoryManager] HP    : " + HP);

            //Stop games while the transition goes

            if (HP > 0)
            {
                //start the transition, it will start the next game when needed
                ResetTransition();
                menuState = MenuState.TransitionMode;
            }
            else
            {
                StopStory();
            }
        }

        //############################
        //  TRANSITION RELATED STUFF
        //############################

        private void TransitionUpdate()
        {
            TRANSITION_value++;

            TRANSITION_sydanRect.X += TRANSITION_moveX; TRANSITION_sydanRect2.X += TRANSITION_moveX; TRANSITION_sydanRect3.X += TRANSITION_moveX; TRANSITION_tekstiRect.X += TRANSITION_moveX;
            TRANSITION_sydanRect.Y += TRANSITION_moveY; TRANSITION_sydanRect2.Y += TRANSITION_moveY; TRANSITION_sydanRect3.Y += TRANSITION_moveY;
            TRANSITION_sydanRect.Height += TRANSITION_scaleY; TRANSITION_sydanRect.Width += TRANSITION_scaleX;
            TRANSITION_sydanRect2.Height += TRANSITION_scaleY; TRANSITION_sydanRect2.Width += TRANSITION_scaleX;
            TRANSITION_sydanRect3.Height += TRANSITION_scaleY; TRANSITION_sydanRect3.Width += TRANSITION_scaleX;

            if (TRANSITION_value > 0 && TRANSITION_value < 100)
            {
                if (TRANSITION_value == 30)
                {
                    TRANSITION_moveX = -3;
                    TRANSITION_moveY = -6;
                    TRANSITION_scaleX = 6;
                    TRANSITION_scaleY = 6;
                }

                if (TRANSITION_value == 40)
                {
                    TRANSITION_moveX = 3;
                    TRANSITION_moveY = 6;
                    TRANSITION_scaleX = -6;
                    TRANSITION_scaleY = -6;
                }

                if (TRANSITION_value == 50)
                {
                    TRANSITION_moveX = -3;
                    TRANSITION_moveY = -6;
                    TRANSITION_scaleX = 6;
                    TRANSITION_scaleY = 6;
                }

                if (TRANSITION_value == 60)
                {
                    TRANSITION_moveX = 3;
                    TRANSITION_moveY = 6;
                    TRANSITION_scaleX = -6;
                    TRANSITION_scaleY = -6;
                }
                if (TRANSITION_value == 70)
                {
                    TRANSITION_moveX = 0;
                    TRANSITION_moveY = 0;
                    TRANSITION_scaleY = 0;
                    TRANSITION_scaleX = 0;
                }

            }

            if (TRANSITION_value > 150)
            {
                TRANSITION_moveX = 20;

                if (TRANSITION_tekstiRect.X > 200 && TRANSITION_value < 400)
                {
                    TRANSITION_moveX = 4;
                }

                if (TRANSITION_tekstiRect.X > 400 && TRANSITION_value < 400)
                {
                    TRANSITION_moveX = 20;
                }
            }
            if (TRANSITION_value > 300)
            {
                StartNextGame();
            }
        }


        private void TransitionModeDraw()
        {
            spriteBatch.Draw(TRANSITION_backgroundTexture, TRANSITION_background, Color.White);
            spriteBatch.Draw(TRANSITION_valiTexture, TRANSITION_keskipalkki, Color.White);
            //TODO: only render the correct count of hearts
            if(HP > 0)
                spriteBatch.Draw(TRANSITION_sydanTexture, TRANSITION_sydanRect, Color.White);

            if(HP > 1)
                spriteBatch.Draw(TRANSITION_sydanTexture, TRANSITION_sydanRect2, Color.White);

            if(HP > 2)
                spriteBatch.Draw(TRANSITION_sydanTexture, TRANSITION_sydanRect3, Color.White);

            spriteBatch.Draw(TRANSITION_tekstiTexture, TRANSITION_tekstiRect, Color.White);
        }

        private void ResetTransition()
        {
            //rektanglet
            TRANSITION_background = new Rectangle(0, 0, 800, 600);
            TRANSITION_keskipalkki = new Rectangle(0, 180, 800, 220);
            TRANSITION_sydanRect = new Rectangle(136, 236, 128, 128);
            TRANSITION_sydanRect2 = new Rectangle(336, 236, 128, 128);
            TRANSITION_sydanRect3 = new Rectangle(536, 236, 128, 128);
            TRANSITION_tekstiRect = new Rectangle(-500, 290, 147, 21);

            TRANSITION_value = 0;
            TRANSITION_moveX = 0;
            TRANSITION_moveY = 0;
            TRANSITION_scaleY = 0;
            TRANSITION_scaleX = 0;
            TRANSITION_sydanRect.X = 136; TRANSITION_sydanRect.Y = 236;
            TRANSITION_sydanRect2.X = 336; TRANSITION_sydanRect2.Y = 236;
            TRANSITION_sydanRect3.X = 536; TRANSITION_sydanRect3.Y = 236;
            TRANSITION_tekstiRect.X = -500; TRANSITION_tekstiRect.Y = 290;
        }

    }
}

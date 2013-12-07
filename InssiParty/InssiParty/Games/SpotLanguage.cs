using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InssiParty.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace InssiParty.Games
{

    //TODO:
    // * Timers
    // * Epic sound effects
    // * Ending triggers
    // * +1 and -1 floating texts
    // * Actual images


    /**
     * Spot the real coding language
     * 
     * Valitse oikea ohjelmointikieli! Jos valitset pythonin, häviät pelin.
     * 
     * By: Lauri Mäkinen
     */
    class SpotLanguage : GameBase
    {
        Random random;

        private const int MAX_ERRORS = 2;
        private const int POINTS_TO_WIN = 7;

        private const int LANG_CPP_COUNT = 3;
        private const int LANG_PYTHON_COUNT = 3;

        private Texture2D[] cppImages;
        private Texture2D[] pythonImages;

        private Texture2D cursorTexture;
        private Texture2D barTexture;

        private SpriteFont font;

        private bool soundOk;
        private SoundEffect soundWrong;
        private SoundEffect soundRight;

        //Player needs 5 points to win
        private int points;
        private int errors;
        private int timer;

        /* True for cpp, false for python! */
        private bool leftOption;
        private bool rightOption;

        /* What image from the array ? */
        private int leftID;
        private int rightID;

        private ButtonState lastMouseState;
        private Vector2 cursorPos;

        Rectangle timerBar;

        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            random = new Random();

            //Create the arrays for the images
            pythonImages = new Texture2D[LANG_PYTHON_COUNT];
            cppImages = new Texture2D[LANG_CPP_COUNT];

            cursorTexture = Content.Load<Texture2D>("palikka");

            font = Content.Load<SpriteFont>("DefaultFont");

            timerBar = new Rectangle(0, 580, 800, 20);

            //Load the images
            pythonImages[0] = Content.Load<Texture2D>("SpotTheLanguage/spot_wrong1");
            pythonImages[1] = Content.Load<Texture2D>("SpotTheLanguage/spot_wrong2");
            pythonImages[2] = Content.Load<Texture2D>("SpotTheLanguage/spot_wrong3");

            cppImages[0] = Content.Load<Texture2D>("SpotTheLanguage/spot_right1");
            cppImages[1] = Content.Load<Texture2D>("SpotTheLanguage/spot_right2");
            cppImages[2] = Content.Load<Texture2D>("SpotTheLanguage/spot_right3");

            barTexture = new Texture2D(GraphicsDevice, 1, 1);
            barTexture.SetData(new Color[] { Color.White });

            try
            {
                soundWrong = Content.Load<SoundEffect>("SpotTheLanguage/wrongFinal");
                soundRight = Content.Load<SoundEffect>("SpotTheLanguage/rightFinal");
                soundOk = true;
            }
            catch (Exception eeek)
            {
                Console.WriteLine(eeek.Message);
                soundOk = false;
            }

            //Create objects

            cursorPos = new Vector2(0, 0);
        }

        public override void Start()
        {
            points = 0;
            errors = 0;
            lastMouseState = ButtonState.Released;
            leftOption = false;
            rightOption = false;
            leftID = 0;
            rightID = 0;

            resetLanguages();
            timer = 800;
        }

        public override void Stop() { }

        public override void Update(GameTime gameTime)
        {
            timer = timer - 1;
            timerBar.Width = timer;

            //get a button click "event"
            if (Mouse.GetState().LeftButton != lastMouseState && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                //which side the click was?

                if (Mouse.GetState().X < 400)
                {
                    //Click on left side
                    if (leftOption == true)
                    {
                        correctChoice();
                    }
                    else
                    {
                        invalidChoice();
                    }
                }
                else
                {
                    //Click on right side
                    if (rightOption == true)
                    {
                        correctChoice();
                    }
                    else
                    {
                        invalidChoice();
                    }
                }
            }

            lastMouseState = Mouse.GetState().LeftButton;

            if (errors > MAX_ERRORS)
                CloseGame(false);

            if (points > POINTS_TO_WIN)
                CloseGame(true);

            if (timer < 0)
                CloseGame(false);
        }

        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            cursorPos.X = Mouse.GetState().X;
            cursorPos.Y = Mouse.GetState().Y;

            if (leftOption == true)
            {
                spriteBatch.Draw(cppImages[leftID], new Vector2(0, 0), Color.White);
            }
            else
            {
                spriteBatch.Draw(pythonImages[leftID], new Vector2(0, 0), Color.White);
            }

            if (rightOption == true)
            {
                spriteBatch.Draw(cppImages[leftID], new Vector2(400, 0), Color.White);
            }
            else
            {
                spriteBatch.Draw(pythonImages[leftID], new Vector2(400, 0), Color.White);
            }

            spriteBatch.Draw(cursorTexture, cursorPos, Color.White);

            //Draw current status

            //Timer
            spriteBatch.Draw(barTexture, timerBar, Color.White);

            //Scores and errors
            spriteBatch.DrawString(font, "Points: " + points + " " + " Errors: " + errors, new Vector2(0, 580), Color.Green);
        }

        /**
         * Reset the current choice of languages and randomize the new ones.
         */
        private void resetLanguages()
        {
            if (random.Next(0, 2) == 1)
            {
                leftOption = true;
                rightOption = false;
            }
            else
            {
                leftOption = false;
                rightOption = true;
            }

            leftID = random.Next(0, 3);
            rightID = random.Next(0, 3);

        }

        private void correctChoice()
        {
            points++;
            resetLanguages();
            Console.WriteLine("[SpotTheLanguage] Correct choice!");

            particleManager.AddParticle(
                cursorTexture, //TODO: Change this to green something or "+1" image.
                new Vector2(215, 590),                                // Position
                new Vector2(-50, -50),                                // Min speed on x / y axis
                new Vector2(50, 50),                                  // Max speed on x / y axis
                2,                                                    // Min time to live
                5,                                                    // Max time to live
                10);                                                  // Count of the particles

            if (soundOk)
                soundRight.Play();
        }

        private void invalidChoice()
        {
            errors++;
            resetLanguages();
            Console.WriteLine("[SpotTheLanguage] Invalid choice!");


            particleManager.AddParticle(
                cursorTexture,                                        // Texture
                new Vector2(215, 590),                                // Position
                new Vector2(-50, -50),                                // Min speed on x / y axis
                new Vector2(50, 50),                                  // Max speed on x / y axis
                2,                                                    // Min time to live
                5,                                                    // Max time to live
                10);                                                  // Count of the particles

            if(soundOk)
                soundWrong.Play();
        }
    }
}

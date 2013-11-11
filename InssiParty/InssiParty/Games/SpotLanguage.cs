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

    //TODO:
    // * Timers
    // * Epic sound effects
    // * Ending triggers


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

        private const int LANG_CPP_COUNT = 3;
        private const int LANG_PYTHON_COUNT = 3;

        private Texture2D[] cppImages;
        private Texture2D[] pythonImages;

        private Texture2D cursorTexture;

        //Player needs 5 points to win
        private int points;
        private int errors;

        /* True for cpp, false for python! */
        private bool leftOption;
        private bool rightOption;

        /* What image from the array ? */
        private int leftID;
        private int rightID;

        private ButtonState lastMouseState;
        private Vector2 cursorPos;

        public override void Load(ContentManager Content)
        {
            random = new Random();

            //Create the arrays for the images
            pythonImages = new Texture2D[LANG_PYTHON_COUNT];
            cppImages = new Texture2D[LANG_CPP_COUNT];

            cursorTexture = Content.Load<Texture2D>("palikka");

            //Load the images
            pythonImages[0] = Content.Load<Texture2D>("SpotTheLanguage/spot_python1");
            pythonImages[1] = Content.Load<Texture2D>("SpotTheLanguage/spot_python1");
            pythonImages[2] = Content.Load<Texture2D>("SpotTheLanguage/spot_python1");

            cppImages[0] = Content.Load<Texture2D>("SpotTheLanguage/spot_cpp1");
            cppImages[1] = Content.Load<Texture2D>("SpotTheLanguage/spot_cpp1");
            cppImages[2] = Content.Load<Texture2D>("SpotTheLanguage/spot_cpp1");

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
        }

        public override void Stop() { }

        public override void Update(GameTime gameTime)
        {
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

            cursorPos.X = Mouse.GetState().X;
            cursorPos.Y = Mouse.GetState().Y;
        }

        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
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
            Console.WriteLine("Correct choice!");
        }

        private void invalidChoice()
        {
            errors++;
            resetLanguages();
            Console.WriteLine("Invalid!");
            particleManager.AddParticle(
                cursorTexture,                                         // Texture
                new Vector2(Mouse.GetState().X, Mouse.GetState().Y),   // Position
                new Vector2(-200, -200),                                 // Min speed on x / y axis
                new Vector2(200, 200),                                   // Max speed on x / y axis
                10,                                                    // Min time to live
                70,                                                    // Max time to live
                500);                                                   // Count of the particles
        }
    }
}

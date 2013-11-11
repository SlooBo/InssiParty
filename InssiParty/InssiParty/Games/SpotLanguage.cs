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
     * Spot the real coding language
     * 
     * Valitse oikea ohjelmointikieli! Jos valitset pythonin, häviät pelin.
     * 
     * By: Lauri Mäkinen
     */
    class SpotLanguage : GameBase
    {
        private const int LANG_CPP_COUNT = 3;
        private const int LANG_PYTHON_COUNT = 3;

        private Texture2D[] cppImages;
        private Texture2D[] pythonImages;

        //Player needs 5 points to win
        private int points;

        /* True for cpp, false for python! */
        private bool leftOption;
        private bool rightOption;

        /* What image from the array ? */
        private int leftID;
        private int rightID;

        public override void Load(ContentManager Content)
        {
            //Create the arrays for the images
            pythonImages = new Texture2D[LANG_PYTHON_COUNT];
            cppImages = new Texture2D[LANG_CPP_COUNT];

            //Load the images
            pythonImages[0] = Content.Load<Texture2D>("SpotTheLanguage/spot_python1");
            pythonImages[1] = Content.Load<Texture2D>("SpotTheLanguage/spot_python1");
            pythonImages[2] = Content.Load<Texture2D>("SpotTheLanguage/spot_python1");

            cppImages[0] = Content.Load<Texture2D>("SpotTheLanguage/spot_cpp1");
            cppImages[1] = Content.Load<Texture2D>("SpotTheLanguage/spot_cpp1");
            cppImages[2] = Content.Load<Texture2D>("SpotTheLanguage/spot_cpp1");

        }

        public override void Start()
        {
            points = 0;

            leftOption = false;
            rightOption = true;

            leftID = 0;
            rightID = 0;
        }

        public override void Stop() { }

        public override void Update(GameTime gameTime)
        {
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
        }

        /**
         * Reset the current choice of languages and randomize the new ones.
         */
        private void resetLanguages()
        {
        }
    }
}

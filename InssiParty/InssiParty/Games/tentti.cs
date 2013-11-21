using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InssiParty.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace InssiParty.Games
{
    /**
     * Nimi: Auta Inssiä tentissä
     * 
     * Selitys: Inssi kohtaa suuria vaikeuksia tentissä ja pyrkii selviämään siitä hakkaamalla päätään tenttipaperiin.
     * 
     * By: Miika Saastamoinen
     */
    class tentti : GameBase
    {
        //Muuttujat
        private double timer = 0;
        private int value = 0;
        private int picture = 0;
        private float blood_gravity = 0;
        private KeyboardState k_state_old;
        private Rectangle background = new Rectangle(0, 0, 800, 600);
        private Rectangle render = new Rectangle(0, 0, 800, 600);
        private Rectangle timerBar = new Rectangle(0, 580, 800, 200);
        private SpriteFont font;
        //Tekstuurit
        private Texture2D inssi_start;
        private Texture2D inssi_mid;
        private Texture2D inssi_end;
        private Texture2D blood;
        private Texture2D barTexture;


        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            inssi_start = Content.Load<Texture2D>("inssi_start position");
            inssi_mid = Content.Load<Texture2D>("inssi_mid position");
            inssi_end = Content.Load<Texture2D>("inssi_end position");
            k_state_old = Keyboard.GetState();
            blood = Content.Load<Texture2D>("veritippa");
            barTexture = new Texture2D(GraphicsDevice, 1, 1);
            barTexture.SetData(new Color[] { Color.Purple });
            font = Content.Load<SpriteFont>("DefaultFont");
        }

        public override void Start()
        {
            Console.WriteLine("Start game");
            //voitto laskuri
            value = 0;
            timerBar.Width = 800;
            blood_gravity = 0;
            timer = 0;
        }

        public override void Stop()
        {
            Console.WriteLine("End of game");
        }

        //update looppi
        public override void Update(GameTime gameTime)
        {
            timer++;

            do
            {
                timerBar.Width = timerBar.Width - 3;
            }
            while (timerBar.Width == 0);
                if (value == 50)
                {
                    //sammuta peli, true jos voitto tapahtui, false jos pelaaaja hävisi.
                    CloseGame(true);
                }
                if (timer == 250)
                {
                    CloseGame(false);
                }
        }
        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(inssi_start, background, new Color(255, 255, 255));

            KeyboardState k_state = Keyboard.GetState();
            for (int i = 0; i < 10; i++)
            {
                if (k_state.IsKeyDown(Keys.Space))
                {
                    spriteBatch.Draw(inssi_mid, render, Color.White);
                    if (!k_state_old.IsKeyDown(Keys.Space))
                    {
                       value++;
                       blood_gravity -= 0.3f;
                    }
                 }          
     
                else if (k_state_old.IsKeyDown(Keys.Space))
                {
                    spriteBatch.Draw(inssi_end, render, new Color (255,255,255));
                    particleManager.setGravity(new Vector2(0, blood_gravity));
                    particleManager.AddParticle(
                    blood,                                       // Texture
                    new Vector2(390,275),                        // Position
                    new Vector2(-5, -5),                         // Min speed on x / y axis
                    new Vector2(5, 5),                           // Max speed on x / y axis
                    50,                                          // Min time to live
                    100,                                         // Max time to live
                    50);                                         // Particle amount
                }
                k_state_old = k_state;
                spriteBatch.Draw(barTexture, timerBar, Color.Purple);
            }

            Console.WriteLine("Valiluonnin iskut" + value);
            Console.WriteLine("kuvat" + picture);
            Console.WriteLine(timer);
        }

    }
}

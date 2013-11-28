using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InssiParty.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

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
        private int timer, value, cycle, soundTrigger;
        private float timerBarCount;
        private KeyboardState k_state_old;
        private Rectangle background = new Rectangle(0, 0, 800, 600);
        private Rectangle render = new Rectangle(0, 0, 800, 600);
        //Tekstuurit
        private Texture2D inssi_start;
        private Texture2D inssi_mid;
        private Texture2D inssi_end;
        private Texture2D blood;
        private Texture2D barTexture;
        //Äänet
        private SoundEffectInstance depressionInstance;
        private SoundEffect hitting;
        private SoundEffect depression;





        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            inssi_start = Content.Load<Texture2D>("inssi_start position");
            inssi_mid = Content.Load<Texture2D>("inssi_mid position");
            inssi_end = Content.Load<Texture2D>("inssi_end position");
            blood = Content.Load<Texture2D>("veritippa");
            barTexture = Content.Load<Texture2D>("timer_bar");
            k_state_old = Keyboard.GetState();

            hitting = Content.Load<SoundEffect>("päänhakkaus_edit");
            depression = Content.Load<SoundEffect>("tenttiähinää2_edit");
            depressionInstance = depression.CreateInstance();
        }

        public override void Start()
        {
            Console.WriteLine("Start game");
            value = 0;
            timer = 0;
            cycle = 0;
            soundTrigger = 0;
            timerBarCount = 0;
        }

        public override void Stop()
        {
            Console.WriteLine("End of game");
        }

        //update looppi
        public override void Update(GameTime gameTime)
        {
            timer++;

            KeyboardState k_state = Keyboard.GetState();
            for (int i = 0; i < 10; i++)
            {
                if (k_state.IsKeyDown(Keys.Space))
                {
                    if (!k_state_old.IsKeyDown(Keys.Space))
                    {
                        value++;
                        //partikkelit
                        particleManager.setGravity(new Vector2(0, -0.2f));
                        particleManager.AddParticle(
                        blood,                                       // Texture
                        new Vector2(390, 275),                        // Position
                        new Vector2(20, -5),                         // Min speed on x / y axis
                        new Vector2(10, 5),                           // Max speed on x / y axis
                        50,                                          // Min time to live
                        100,                                         // Max time to live
                        100);
                        cycle++;
                        soundTrigger++;
                        hitting.Play(1, 0, 0);
                        if (soundTrigger == 1)
                        {
                            depression.Play(1, 0, 0);
                        }
                    }
                }

                else if (k_state_old.IsKeyDown(Keys.Space))
                {
                    cycle++;
                }
                k_state_old = k_state;
                do
                {
                    timerBarCount -= 0.27f;
                }
                while (timerBarCount == 0);
                {
                    if (value == 45)
                    {
                        depressionInstance.Stop();
                        //sammuta peli, true jos voitto tapahtui, false jos pelaaaja hävisi.
                        CloseGame(true);
                    }
                    if (timer == 300)
                    {
                        depressionInstance.Stop();
                        CloseGame(false);
                    }
                }
            }
        }
        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(inssi_start, background, new Color(255, 255, 255));
            if(cycle==1)
            {
             spriteBatch.Draw(inssi_end, render, Color.White);
            }
            if(cycle==2)
            {
             spriteBatch.Draw(inssi_mid, render, new Color(255, 255, 255));
             cycle = 0;
            }
            spriteBatch.Draw(barTexture, new Vector2(timerBarCount, 575), Color.Purple);

            //Console.WriteLine("Valiluonnin iskut" + value);
            //Console.WriteLine("kuvat" + picture);
            //Console.WriteLine(timer);
            //Console.WriteLine(cycle);
        }

    }
}

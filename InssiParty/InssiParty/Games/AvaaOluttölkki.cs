using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InssiParty.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using InssiParty.Games.FeedGameSrc;


namespace InssiParty.Games
{

    /**
     * Avaa oluttölkki
     * 
     * Auta inssiä avaamaan oluttölkki ja välttämään janoon kuoleminen.
     * 
     * By: Markus Tolvanen
     */
    class Olut : GameBase
    {
        //Esitellään muuttujat
        int nappi;
        private int value;
        private int fail_counter;
        private int success_counter;
        Random random;


        KeyboardState oldState;

        //Esitellään tekstuurit
        private Texture2D Can;
        private Texture2D OpeningCan;
        private Texture2D EmptyCan;
        private Texture2D backround_texture;
        SpriteFont font;

        bool gameover = false;
        bool win = false;
        bool gameRunning = true;

        
        /**
         * 
         * 
         * Ladataan tekstuurit
         */
        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            //Tiedoston pitäisi olla InssiPartyContent projektin alla solution explorerissa.
            Can = Content.Load<Texture2D>("Can");
            OpeningCan = Content.Load<Texture2D>("Can-Hissing");
            EmptyCan = Content.Load<Texture2D>("Can-Empty");
            font = Content.Load<SpriteFont>("DefaultFont");
        }

        /**
         *
         * 
         * Annetaan kirjaimille numeroarvot ja alustetaan laskimet nollille
         */
        public override void Start()
        {
            Console.WriteLine("Starting Avaa oluttölkki");

            value = 500;

            oldState = Keyboard.GetState();

            Console.WriteLine("Starting hello world");

            random = new Random();
            
            fail_counter=0;

            success_counter=0; 

            backround_texture = Can;
        }

        /**
         * Ajetaan kun peli sulkeutuu. Piilota äänet ja puhdista roskasi seuraavaa peliä varten.
         */
        public override void Stop()
        {
            Console.WriteLine("Closing Avaa oluttölkki");
        }

     
        public override void Update(GameTime gameTime)
        {
            
            value--;

            if (gameRunning == true)
            {
                
                if (success_counter <6)
                {
                    success_counter = 0;
                    gameRunning = true;
                    gameover = false;

                   
                }
                else if (fail_counter == 3)
                {

                    gameRunning = false;
                    gameover = true;
                
                }
            }

            KeyboardState newState = Keyboard.GetState();
            if (gameRunning == true)
            {
                backround_texture = Can;

                if (newState.IsKeyDown(Keys.A)) 
                {
                    if (!oldState.IsKeyDown(Keys.A))
                    {
                        success_counter = success_counter + 1; 
                        
                    }
                    
                }
                else if (oldState.IsKeyDown(Keys.A))
                { 
                    
                }
                oldState = newState;

                if (newState.IsKeyDown(Keys.S)) 
                {
                    if (!oldState.IsKeyDown(Keys.S))
                    {
                        success_counter = success_counter + 1; 
                        
                    }
                    
                }
                else if (oldState.IsKeyDown(Keys.S))
                { 
                    
                }
                oldState = newState;
                

                if (newState.IsKeyDown(Keys.D)) 
                {
                    if (!oldState.IsKeyDown(Keys.D))
                    {
                        success_counter = success_counter + 1; 
                        
                    }
                    
                }
                else if (oldState.IsKeyDown(Keys.D))
                { 
                    
                }
                oldState = newState;

                if (newState.IsKeyDown(Keys.F)) 
                {
                    if (!oldState.IsKeyDown(Keys.F))
                    {
                        success_counter = success_counter + 1; 
                        
                    }
                    
                }
                else if (oldState.IsKeyDown(Keys.F))
                { 
                    
                }
                oldState = newState;

            }
            if (success_counter == 3 && success_counter < 5)
            {
                Can = OpeningCan;
            
            }

            else if (success_counter == 5)
            {
                OpeningCan = EmptyCan;

                //sammuta peli, true jos voitto tapahtui, false jos pelaaaja hävisi.
                win = true;
                gameover = false;
                CloseGame(true);
            }

            else if (fail_counter == 3 || value == 0)
            {
                win = false;
                gameover = true;

                CloseGame(false);
            }
            if (value == 0 && win == true && gameover == false)
            {
                CloseGame(true);
            }

            if (gameover == true && win == false && value == 0)
            {
                CloseGame(false);
            }
            

            
              
        }

        /**
         * Pelkkä piirtäminen
         * 
         * ELÄ sijoita pelilogiikkaa tänne.
         *
         * gameTime avulla voidaan synkata nopeus tasaikseksi vaikka framerate ei olisi tasainen.
         */
        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {  
            //spriteBatch.Draw funktiolla voit piirtää ruudulle.
            //Palikka piirretään y akselilla, valuen kohtaan
            
            Console.WriteLine("Draw " + value);

            spriteBatch.Draw(backround_texture, new Vector2(100, 100), Color.White);

        }

    }
}
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
     * Auta Insinööri baari-illan jälkeen insinööritähden luokse turvaan
     * 
     * Insinööri on nauttinut hieman liikaa alkoholia ja nyt hän pyrkii pääsemään turvaan
     * myytinomaisen insinööritähden luokse. On kerrottu että kun insinööri saavuttaa tietyn
     * promillemäärän kykenee hän näkemään tämän valoilmiön ja päästessään sen luokse insinööri
     * saavuttaa ylemmän insinööriyden tason.
     * 
     * By: Miika Saastamoinen
     */
    class inssihorjuu : GameBase
    {
        //Muuttujat
        private int health=0;
        private int forward = 0;
        private int inssi_movement;
        private Rectangle collisionRect;
        private Rectangle windowBounds;
        private Rectangle background = new Rectangle(0,0,800,600);
        Random random;
        //Tekstuurit
        private Texture2D inssi;
        private Texture2D map;
        //Äänet
        private SoundEffectInstance drunkenBabbleInstance;
        private SoundEffect drunkenBabble;


        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            inssi = Content.Load<Texture2D>("inssi");
            map = Content.Load<Texture2D>("horjuu_map");
            windowBounds = new Rectangle(0,0,800,600);

            random = new Random();

            drunkenBabble = Content.Load<SoundEffect>("Känniölinää");
            drunkenBabbleInstance = drunkenBabble.CreateInstance();
        }
        public override void Start()
        {
            forward = 0;
            inssi_movement = 250;
            health = 500;

        }
        public override void Stop()
        {
            Console.WriteLine("Closing hello world");
        }
        public override void Update(GameTime gameTime)
        {
            //liikkuminen ja randomhuojunta
            collisionRect = new Rectangle(forward, inssi_movement, 32, 64);
            KeyboardState keyboard = Keyboard.GetState();
            if (forward == 10)
            {
                drunkenBabbleInstance.Play();
            }

            if (keyboard.IsKeyDown(Keys.W))
            {
                inssi_movement = inssi_movement - 2;
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                inssi_movement = inssi_movement + 2;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                forward += 2;
            }
            if (keyboard.IsKeyDown(Keys.A))
            {
                forward -= random.Next(3, 5);
            }
            if (random.Next(1, 10) == 3)
            {
                forward -= 5;
            }

            for (int i = 0; i < random.Next(10, 15); i++)
            {
                i++;
                inssi_movement += random.Next(1,2);
            }
            for (int b = 0; b < random.Next(10, 15); b++)
            {
                b++;
                inssi_movement -= random.Next(1, 2);
            }

            if (forward < 0)
            {
                forward = 0;
            }
            if (inssi_movement < 0)
            {
                inssi_movement = 0;
            }
            if (forward > windowBounds.Width)
            {
                forward = windowBounds.Width;
            }
            if (inssi_movement > windowBounds.Height - inssi.Bounds.Height)
            {
                inssi_movement = windowBounds.Height - inssi.Bounds.Height;
            }

            //Esteet
                                                        //X,Y,WIDHT,HEIGTH
            //puuaita horisontaali
            if (collisionRect.Intersects(new Rectangle(0,180,233,58)))
            {
                CloseGame(false);
                drunkenBabbleInstance.Stop();
            }
            //puuaita vertikaali
            if (collisionRect.Intersects(new Rectangle(233, 180, 62, 197)))
            {
                CloseGame(false);
                drunkenBabbleInstance.Stop();
            }
            //sillankaide
            if (collisionRect.Intersects(new Rectangle(386, 84, 220, 21)))
            {
                CloseGame(false);
                drunkenBabbleInstance.Stop();
            }
            //joki
            if (collisionRect.Intersects(new Rectangle(401, 110, 116, 490)))
            {
                CloseGame(false);
                drunkenBabbleInstance.Stop();
            }
            //kiviaita horisontaali
            if (collisionRect.Intersects(new Rectangle(614, 256, 186, 50)))
            {
                CloseGame(false);
                drunkenBabbleInstance.Stop();
            }
            //kiviaita vertikaali
            if (collisionRect.Intersects(new Rectangle(614, 256, 50, 245)))
            {
                CloseGame(false);
                drunkenBabbleInstance.Stop();
            }

            //insinööritähti
            if (collisionRect.Intersects(new Rectangle(754, 310, 46, 90)))
            {
                CloseGame(true);
                drunkenBabbleInstance.Stop();
            }
        }
        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {

            spriteBatch.Draw(map, background, new Color(255, 255, 255));
            spriteBatch.Draw(inssi, new Vector2(forward, inssi_movement), Color.White);
        }
    }

}

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
        private int health = 0;
        private int death = 0;
        private int forward = 0;
        private int inssi_movement;
        private Rectangle collisionRect;
        private Rectangle windowBounds;
        private Rectangle background = new Rectangle(0, 0, 800, 600);
        Random random;
        //Tekstuurit
        private Texture2D inssi;
        private Texture2D map;
        private Texture2D inssiDeath;
        //Äänet
        private SoundEffectInstance drunkenBabbleInstance;
        private SoundEffect drunkenBabble;
        private SoundEffect scream;
        //Törmäysruudut
        private Rectangle woodHorisontal = new Rectangle(0, 180, 233, 58);
        private Rectangle woodVertical = new Rectangle(233, 180, 62, 197);
        private Rectangle bridgeFence = new Rectangle(386, 84, 220, 21);
        private Rectangle river = new Rectangle(401, 110, 116, 490);
        private Rectangle brickHorisontal = new Rectangle(614, 256, 186, 50);
        private Rectangle brickVertical = new Rectangle(614, 256, 50, 245);


        public override void Load(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            inssi = Content.Load<Texture2D>("inssi");
            map = Content.Load<Texture2D>("horjuu_map");
            inssiDeath = Content.Load<Texture2D>("inssi_water");
            windowBounds = new Rectangle(0, 0, 800, 600);

            random = new Random();

            drunkenBabble = Content.Load<SoundEffect>("Känniölinää");
            scream = Content.Load<SoundEffect>("WilhelmScream");
            drunkenBabbleInstance = drunkenBabble.CreateInstance();
        }
        public override void Start()
        {
            forward = 0;
            inssi_movement = 250;
            death = 0;
            health = 100;

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
                inssi_movement = inssi_movement - 4;
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                inssi_movement = inssi_movement + 4;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                forward += 4;
            }
            if (keyboard.IsKeyDown(Keys.A))
            {
                forward -= 4;
            }
            //if (random.Next(1, 10) == 3)
            //{
            //    forward -= 5;
            //}

            //for (int i = 0; i < random.Next(10, 15); i++)
            //{
            //    i++;
            //    inssi_movement += random.Next(1, 2);
            //}
            //for (int b = 0; b < random.Next(10, 15); b++)
            //{
            //    b++;
            //    inssi_movement -= random.Next(1, 2);
            //}

            if (forward < 0)
            {
                forward = 0;
            }
            if (inssi_movement < 0)
            {
                inssi_movement = 0;
            }
            if (forward > windowBounds.Width - inssi.Bounds.Width)
            {
                forward = windowBounds.Width - inssi.Bounds.Width;
            }
            if (inssi_movement > windowBounds.Height - inssi.Bounds.Height)
            {
                inssi_movement = windowBounds.Height - inssi.Bounds.Height;
            }

            //Esteet
            //X,Y,WIDHT,HEIGTH
            //puuaita horisontaali
            if (collisionRect.Intersects(woodHorisontal))
            {
                if (inssi_movement > woodHorisontal.Height)
                {
                    inssi_movement = woodHorisontal.Bottom - inssi.Bounds.Top;
                }

                health--;
                if (health == 0)
                {
                    CloseGame(false);
                    drunkenBabbleInstance.Stop();
                }
            }
            //puuaita vertikaali
            if (collisionRect.Intersects(woodVertical))
            {
                if (forward > woodVertical.Y)
                {
                    forward = woodVertical.Left - inssi.Bounds.Right;
                }
                if (forward > woodVertical.Right)
                {
                    forward = woodVertical.Right + inssi.Bounds.Left;
                }
                health--;
                if (health == 0)
                {
                    CloseGame(false);
                    drunkenBabbleInstance.Stop();
                }
            }
            //sillankaide
            if (collisionRect.Intersects(bridgeFence))
            {
                health--;
                if (inssi_movement > bridgeFence.Height)
                {
                    inssi_movement = bridgeFence.Top - inssi.Bounds.Bottom;
                }
                if (health == 0)
                {
                    CloseGame(false);
                    drunkenBabbleInstance.Stop();
                }
            }
            //joki
            if (collisionRect.Intersects(river))
            {
                death++;
                if (death == 1)
                {
                    scream.Play(1, 0, 0);
                }
                if (death == 30)
                {
                    CloseGame(false);
                }
                drunkenBabbleInstance.Stop();
            }
            //kiviaita horisontaali
            if (collisionRect.Intersects(brickHorisontal))
            {
                health--;
                if (health == 0)
                {
                    CloseGame(false);
                    drunkenBabbleInstance.Stop();
                }
            }
            //kiviaita vertikaali
            if (collisionRect.Intersects(brickVertical))
            {
                health--;
                if (health == 0)
                {
                    CloseGame(false);
                    drunkenBabbleInstance.Stop();
                }
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
            if (death < 1)
            {
                spriteBatch.Draw(inssi, new Vector2(forward, inssi_movement), Color.White);
            }
            if (death >= 1)
            {
                spriteBatch.Draw(inssiDeath, new Vector2(400, 300), Color.White);
            }
            Console.WriteLine(inssi_movement);
            Console.WriteLine(forward);
        }
    }

}

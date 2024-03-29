﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InssiParty.Games
{
    class AnimatedSprite
    {
        Texture2D spriteTexture;
        float timer = 0f;
        float interval = 100f;
        int spriteSpeed = 2;
        int currentFrame = 0;
        int spriteWidht = 296;
        int spriteHeight = 42;
        Rectangle sourceRect;
        Vector2 position;
        Vector2 origin;

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public Vector2 Origin
        {
            get
            {
                return origin;
            }
            set
            {
                origin = value;
            }
        }
        
        public Texture2D Texture
        {
            get
            {
                return spriteTexture;
            }
            set
            {
                spriteTexture = value;
            }
        }
        public Rectangle SourceRect
        {
            get            
            {
                return sourceRect;
            }
            set
            {
                sourceRect = value;
            }
        }
        
        public AnimatedSprite(Texture2D texture, int currentFrame, int spriteWidht, int spriteHeight)
        {
            this.spriteTexture = texture;
            this.currentFrame = currentFrame;
            this.spriteWidht = spriteWidht;
            this.spriteHeight = spriteHeight;
        }

        public void anykeyMovement(GameTime gameTime)
        {
            sourceRect = new Rectangle(0, currentFrame * spriteHeight, spriteWidht, spriteHeight); 

            if (currentFrame > 55)
            {
                currentFrame = 0;
            }
                        
            Animate(gameTime);
            if (position.Y < 0) 
            {
                position.Y += spriteSpeed;
            }
            
            origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
        }

        public void Animate(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if(timer > interval)
            {
                currentFrame++;
                timer = 0f;
            }
        }
    }
}

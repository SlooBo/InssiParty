using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace InssiParty.Games
{
    public class Cannonball
    {
        protected Texture2D _texture;
        protected Vector2 _position;
        protected Vector2 _speed;

        public Vector2 Position { get { return _position; } }
        public Rectangle CollisionRect { get; private set; }
        public bool IsDead;

        public Cannonball(Texture2D texture, Vector2 position, Vector2 speed)
        {
            _texture = texture;
            _position = position;
            _speed = speed;
        }

        public void Update()
        {
            CollisionRect = new Rectangle((int)_position.X,(int)_position.Y,
                _texture.Bounds.Width, _texture.Bounds.Height);
            _position += new Vector2(1, -1) *_speed;
            _speed-= new Vector2(0, 0.02f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace SleepyCat
{
    public class Sprite : Component
    {
        protected readonly AnimationManager _animationManager;

        protected readonly Dictionary<string, Animation> _animations;

        protected readonly Texture2D _texture;

        protected Vector2 _position;

        public Vector2 Velocity;

        public Input Input { get; set; }

        public float Speed { get; set; }

        public bool IsRemoved { get; set; }

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }

        public float X
        {
            get { return Position.X; }
            set
            {
                Position = new Vector2(value, Position.Y);
            }
        }

        public float Y
        {
            get { return Position.Y; }
            set
            {
                Position = new Vector2(Position.X, value);
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                int width = 0;
                int height = 0;

                if (_texture != null)
                {
                    width = _texture.Width;
                    height = _texture.Height;
                }
                else if (_animationManager != null)
                {
                    width = _animationManager.FrameWidth;
                    height = _animationManager.FrameHeight;
                }

                return new Rectangle((int)Position.X, (int)Position.Y, width, height);
            }
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public Sprite(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }

        public override void Update(GameTime gameTime) { }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);

            if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
        }

        protected virtual bool OnCollide(Sprite sprite) => Rectangle.Intersects(sprite.Rectangle);

        public virtual void CollideWithPlayer(Sprite player, GameState game) { }

        public bool IsTouchingLeft(Sprite sprite)
        {
            return this.Rectangle.Right + this.Velocity.X > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Left &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        public bool IsTouchingRight(Sprite sprite)
        {
            return this.Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right &&
              this.Rectangle.Right > sprite.Rectangle.Right &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        public bool IsTouchingTop(Sprite sprite)
        {
            return this.Rectangle.Bottom + this.Velocity.Y >= sprite.Rectangle.Top &&
              this.Rectangle.Top <= sprite.Rectangle.Top &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

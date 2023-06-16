using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SleepyCat
{
    public class Dog : Sprite
    {
        private readonly Vector2 _startPosition;

        public Vector2 EndPosition { get; set; }

        public Dog(Dictionary<string, Animation> animations, Vector2 startPosition) : base(animations)
        {
            _startPosition = startPosition;
            Position = startPosition;
        }

        public override void Update(GameTime gameTime)
        {
            if (Rectangle.Right >= EndPosition.X || Position.X < _startPosition.X)
                Speed = -Speed;

            Velocity.X = Speed;
            Position += Velocity;

            UpdateAnimations(gameTime);

            Velocity = Vector2.Zero;
        }

        private void UpdateAnimations(GameTime gameTime)
        {
            if (Velocity.X > 0)
                _animationManager.Play(_animations["MoveRight"]);
            else if (Velocity.X < 0)
                _animationManager.Play(_animations["MoveLeft"]);
            else
                _animationManager.Stop();

            _animationManager.Update(gameTime);
        }

        public override void CollideWithPlayer(Sprite player, GameState game)
        {
            game.ChangeLoseState();
        }
    }
}

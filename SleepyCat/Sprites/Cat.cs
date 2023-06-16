using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SleepyCat
{
    public class Cat : Sprite
    {
        private bool isJumping;

        private bool wasJumping;

        private float jumpTime;

        public bool IsOnGround { get; set; }

        public Cat(Dictionary<string, Animation> animations) : base(animations) { }

        private void GetInput()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Left))
                Velocity.X = -Speed;

            else if (Keyboard.GetState().IsKeyDown(Input.Right))
                Velocity.X = Speed;

            isJumping = Keyboard.GetState().IsKeyDown(Input.Up);
        }

        private float DoJump(float velocityY, GameTime gameTime)
        {
            if (isJumping)
            {
                if ((!wasJumping && IsOnGround) || jumpTime > 0.0f)
                {
                    _animationManager.Stop();
                    jumpTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (0.0f < jumpTime && jumpTime <= 0.35f)
                    velocityY = -80 * (1.0f - (float)Math.Pow(jumpTime / 0.5f, 0.15f));
                else
                    jumpTime = 0.0f;
            }
            else
                jumpTime = 0.0f;

            wasJumping = isJumping;
            return velocityY;
        }

        public void Update(GameTime gameTime, List<Sprite> sprites, GameState game)
        {
            GetInput();

            Velocity.Y = MathHelper.Clamp(Velocity.Y + 20, -100f, 100f);

            Collides(sprites, game);

            Velocity.Y = DoJump(Velocity.Y, gameTime);

            UpdateAnimations(gameTime);

            Position = new Vector2(MathHelper.Clamp(Position.X + Velocity.X, 0, game.Game.GameWidth - Rectangle.Width), MathHelper.Clamp(Position.Y + Velocity.Y, 0, game.Game.GameHeight - Rectangle.Height));

            IsOnGround = false;

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

        private void Collides(List<Sprite> sprites, GameState game)
        {
            foreach (var sprite in sprites)
            {
                if (OnCollide(sprite))
                    sprite.CollideWithPlayer(this, game);

                if (sprite is Platform)
                    sprite.CollideWithPlayer(this, game);
            }
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SleepyCat
{
    public class Platform : Sprite
    {
        public Platform(Texture2D texture) : base(texture) { }

        public override void CollideWithPlayer(Sprite player, GameState game)
        {
            var p = player as Cat;
            if (p.Velocity.X > 0 && p.IsTouchingLeft(this) ||
                p.Velocity.X < 0 && p.IsTouchingRight(this))
                p.Velocity.X = 0;
            if (p.Velocity.Y > 0 && p.IsTouchingTop(this))
            {
                p.IsOnGround = true;
                p.Velocity.Y = 0;
                p.Position = new Vector2(p.X, Y - p.Rectangle.Height);
            }
        }
    }
}

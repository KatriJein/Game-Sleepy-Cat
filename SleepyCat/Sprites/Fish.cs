using Microsoft.Xna.Framework.Graphics;

namespace SleepyCat
{
    public class Fish : Sprite
    {
        public Fish(Texture2D texture) : base(texture) { }

        public override void CollideWithPlayer(Sprite player, GameState game)
        {
            game.Level.Score++;
            IsRemoved = true;
        }
    }
}

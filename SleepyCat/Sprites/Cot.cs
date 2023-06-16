using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SleepyCat
{
    public class Cot : Sprite
    {
        public Cot(Texture2D texture) : base(texture) { }

        public Rectangle RectangleOfExit =>
            new Rectangle((int)Position.X + _texture.Width / 4, (int)Position.Y, _texture.Width / 2, _texture.Height);

        public override void CollideWithPlayer(Sprite player, GameState game)
        {
            if (RectangleOfExit.Intersects(player.Rectangle) && game.Level.CountOfFish == game.Level.Score)
                game.ChangeSuccessState();
            else if (RectangleOfExit.Intersects(player.Rectangle) && game.Level.CountOfFish != game.Level.Score)
                game.ChangeLoseState();
        }
    }
}

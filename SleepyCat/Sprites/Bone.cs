using Microsoft.Xna.Framework.Graphics;

namespace SleepyCat
{
    public class Bone : Sprite
    {
        public Bone(Texture2D texture) : base(texture) { }

        public override void CollideWithPlayer(Sprite player, GameState game)
        {
            game.ChangeLoseState();
        }
    }
}

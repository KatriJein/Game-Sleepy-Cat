using Microsoft.Xna.Framework.Graphics;

namespace SleepyCat
{
    public class Animation
    {
        public int FrameHeight => Texture.Height;

        public int FrameWidth => Texture.Width / FrameCount;

        public int CurrentFrame { get; set; }

        public float FrameSpeed { get; private set; }

        public int FrameCount { get; private set; }

        public bool IsLooping { get; private set; }

        public Texture2D Texture { get; private set; }

        public Animation(Texture2D texture, int frameCount)
        {
            Texture = texture;

            FrameCount = frameCount;

            IsLooping = true;

            FrameSpeed = 0.1f;
        }
    }
}

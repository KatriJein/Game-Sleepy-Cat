using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SleepyCat
{
    public class Score : Component
    {
        private readonly Color _penColour;

        private readonly SpriteFont _font;

        private readonly Vector2 _positionOfText;

        private readonly Texture2D _texture;

        private readonly Vector2 _position;

        private string _text;

        public Rectangle Rectangle =>
            new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);

        public Score(Texture2D texture, SpriteFont font, string text)
        {
            _position = new Vector2(150, 10);
            _texture = texture;
            _penColour = new Color(140, 79, 58);
            _font = font;
            _text = text;
            _positionOfText = new Vector2(_position.X + 132, _position.Y + 16);
        }

        public void Update(string text) => _text = text;

        public override void Update(GameTime gameTime) => throw new System.NotImplementedException();

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rectangle, Color.White);

            if (!string.IsNullOrEmpty(_text))
                spriteBatch.DrawString(_font, _text, _positionOfText, _penColour);
        }
    }
}

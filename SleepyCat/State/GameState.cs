using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace SleepyCat
{
    public class GameState : State
    {
        private Cat player;

        private readonly Score score;

        private string textForScore;

        public Level Level { get; private set; }

        public GameState(IGame game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            backgroundTexture = textures.GameStateBack;
            score = new Score(textures.TextureForScore, textures.FontForScore, textForScore);
        }

        public override void Initialize()
        {
            Level = Game.Level;
            player = Level.Cat;
            textForScore = string.Format("{0}/{1}", Level.Score, Level.CountOfFish);

            MediaPlayer.Play(textures.GameMusic);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.2f;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, Game.GameWidth, Game.GameHeight), Color.White);

            foreach (var sprite in Level.Sprites)
                sprite.Draw(gameTime, spriteBatch);

            score.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(_content.Load<SpriteFont>("Level"), $"Уровень: {Game.LevelIndex}/{Game.NumberOfLevels}", new Vector2(850, 20), new Color(236, 214, 197));

            player.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var sprite in Level.Sprites)
                sprite.Update(gameTime);

            player.Update(gameTime, Level.Sprites, this);

            textForScore = string.Format("{0}/{1}", Level.Score, Level.CountOfFish);
            score.Update(textForScore);

            for (var i = 0; i < Level.Sprites.Count; i++)
            {
                if (Level.Sprites[i].IsRemoved)
                {
                    Level.Sprites.RemoveAt(i);
                    i--;
                }
            }
        }

        public void ChangeSuccessState()
        {
            if (Game.LevelIndex == Game.NumberOfLevels)
                Game.ChangeState(Game.dictOfStates["FinalState"]);

            else
                Game.ChangeState(Game.dictOfStates["SuccessState"]);
        }

        public void ChangeLoseState() => Game.ChangeState(Game.dictOfStates["LoseState"]);
    }
}

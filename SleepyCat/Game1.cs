using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace SleepyCat
{
    public class Game1 : Game, IGame
    {
        public GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;

        private State _currentState;

        private State _nextState;

        public int GameWidth { get; set; }

        public int GameHeight { get; set; }

        public int NumberOfLevels { get; set; }

        public Level Level { get; set; }

        public Dictionary<string, State> dictOfStates { get; set; }

        public bool IsGameAgain { get; set; }

        public int LevelIndex { get; set; }

        public void StartLevelAgain()
        {
            if (Level != null)
                Level.Dispose();

            Level = new Level(Services, GetFileStream());
        }

        private Stream GetFileStream()
        {
            var levelPath = string.Format("Content/Levels/{0}.txt", LevelIndex);
            return TitleContainer.OpenStream(levelPath);
        }

        public void LoadNextLevel()
        {
            LevelIndex++;

            if (Level != null)
                Level.Dispose();

            Level = new Level(Services, GetFileStream());
        }

        public void ChangeState(State state) => _nextState = state;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            GameWidth = 1366;
            GameHeight = 768;

            _graphics.PreferredBackBufferWidth = GameWidth;
            _graphics.PreferredBackBufferHeight = GameHeight;
            _graphics.ApplyChanges();

            LevelIndex = 1;
            NumberOfLevels = 5;

            Level = new Level(Services, GetFileStream());

            dictOfStates = new Dictionary<string, State>()
            {
                { "MenuState", new MenuState(this, GraphicsDevice, Content) },
                { "GameState", new GameState(this, GraphicsDevice, Content) },
                { "FinalState", new FinalState(this, GraphicsDevice, Content) },
                { "LoseState", new LoseState(this, GraphicsDevice, Content) },
                { "SuccessState", new SuccessState(this, GraphicsDevice, Content) },
                { "Rules", new Rules(this, GraphicsDevice, Content) },
            };

            _currentState = dictOfStates["MenuState"];
            _currentState.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (_nextState != null)
            {
                _currentState = _nextState;
                _currentState.Initialize();
                _nextState = null;
            }

            _currentState.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                ChangeState(dictOfStates["MenuState"]);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _currentState.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();
        }
    }
}
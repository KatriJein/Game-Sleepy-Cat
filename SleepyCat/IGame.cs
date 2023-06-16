using System.Collections.Generic;

namespace SleepyCat
{
    public interface IGame
    {
        public int GameWidth { get; set; }

        public int GameHeight { get; set; }

        public Dictionary<string, State> dictOfStates { get; set; }

        public bool IsGameAgain { get; set; }

        public Level Level { get; set; }

        public int LevelIndex { get; set; }

        public int NumberOfLevels { get; set; }

        public void ChangeState(State state);

        public void StartLevelAgain();

        public void LoadNextLevel();

        public void Exit();
    }
}

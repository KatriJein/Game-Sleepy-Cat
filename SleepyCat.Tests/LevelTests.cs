using Microsoft.Xna.Framework;
using System;
using System.IO;
using Xunit;

namespace SleepyCat.Tests
{
    public class LevelTests
    {
        private Game1 game;

        public LevelTests()
        {
            game = new Game1();
            game._graphics.ApplyChanges();
        }

        [Theory]
        [InlineData("MultipleStarts")] //Проверка на невозможность нескольких стартов
        [InlineData("DifferenStringLength")] //Проверка на невозможность строки другой длины
        [InlineData("NoStart")] //Проверка на невозможность отсутствия старта
        [InlineData("MultipleExits")] //Проверка на невозможность нескольких выходов
        [InlineData("NoExit")] //Проверка на невозможность отсутствия выхода
        [InlineData("DifferentTile")] //Проверка на невозможность введения неизвестной клетки 
        public void Incorrect_input_map(string input)
        {
            Assert.Throws<NotSupportedException>(() => new Level(game.Services, GetFileStream(input)));
            game.Dispose();
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("3")]
        [InlineData("4")]
        public void Correct_input_map(string input)
        {
            var level = new Level(game.Services, GetFileStream(input));
            Assert.NotEmpty(level.Sprites);
            Assert.NotNull(level.Cat);
            Assert.NotEqual(0, level.CountOfFish);
            game.Dispose();
        }

        private Stream GetFileStream(string level)
        {
            var levelPath = string.Format("Content/{0}.txt", level);
            return TitleContainer.OpenStream(levelPath);
        }
    }
}
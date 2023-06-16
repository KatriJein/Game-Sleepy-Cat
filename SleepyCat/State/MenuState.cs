using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace SleepyCat
{
    public class MenuState : State
    {
        public MenuState(IGame game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            backgroundTexture = textures.MenuStateBack;

            var newGameButton = new Button(textures.ButtonTexture, textures.ButtonFont)
            {
                Position = positionFirstButton,
                Text = "New Game",
            };

            newGameButton.Click += NewGameButton_Click;

            var quitGameButton = new Button(textures.ButtonTexture, textures.ButtonFont)
            {
                Position = positionSecondButton,
                Text = "Exit",
            };

            quitGameButton.Click += QuitGameButton_Click;

            components = new List<Component>()
            {
                newGameButton,
                quitGameButton,
            };
        }

        public override void Initialize()
        {
            MediaPlayer.Play(textures.MenuMusic);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.3f;
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            Game.LevelIndex = 0;
            Game.LoadNextLevel();

            if (Game.IsGameAgain)
                Game.ChangeState(Game.dictOfStates["GameState"]);
            else
                Game.ChangeState(Game.dictOfStates["Rules"]);
        }
    }
}

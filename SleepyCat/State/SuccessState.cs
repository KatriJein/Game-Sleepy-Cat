using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace SleepyCat
{
    public class SuccessState : State
    {
        public SuccessState(IGame game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            backgroundTexture = textures.SuccessStateBack;

            var nextLevelButton = new Button(textures.ButtonTexture, textures.ButtonFont)
            {
                Position = positionFirstButton,
                Text = "Next Level",
            };

            nextLevelButton.Click += NextLevelButton_Click;

            var mainMenuButton = new Button(textures.ButtonTexture, textures.ButtonFont)
            {
                Position = positionSecondButton,
                Text = "Main Menu",
            };

            mainMenuButton.Click += MainMenuButton_Click;

            components = new List<Component>()
            {
                nextLevelButton,
                mainMenuButton,
            };
        }

        public override void Initialize()
        {
            MediaPlayer.Play(textures.SuccessMusic);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.3f;
        }

        private void NextLevelButton_Click(object sender, EventArgs e)
        {
            Game.LoadNextLevel();
            Game.ChangeState(Game.dictOfStates["GameState"]);
        }
    }
}

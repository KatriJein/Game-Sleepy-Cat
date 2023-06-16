using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace SleepyCat
{
    public class LoseState : State
    {
        public LoseState(IGame game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            backgroundTexture = textures.LoseStateBack;

            var repeatLevelButton = new Button(textures.ButtonTexture, textures.ButtonFont)
            {
                Position = positionFirstButton,
                Text = "Start Again",
            };

            repeatLevelButton.Click += RepeatLevelButton_Click;

            var mainMenuButton = new Button(textures.ButtonTexture, textures.ButtonFont)
            {
                Position = positionSecondButton,
                Text = "Main Menu",
            };

            mainMenuButton.Click += MainMenuButton_Click;

            components = new List<Component>()
            {
                repeatLevelButton,
                mainMenuButton,
            };
        }

        public override void Initialize()
        {
            MediaPlayer.Play(textures.LoseMusic);
            MediaPlayer.IsRepeating = false;
            MediaPlayer.Volume = 0.3f;
        }

        private void RepeatLevelButton_Click(object sender, EventArgs e)
        {
            Game.StartLevelAgain();
            Game.ChangeState(Game.dictOfStates["GameState"]);
        }
    }
}

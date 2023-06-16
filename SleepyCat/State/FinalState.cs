using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace SleepyCat
{
    public class FinalState : State
    {
        public FinalState(IGame game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            backgroundTexture = textures.FinalStateBack;

            var mainMenuButton = new Button(textures.ButtonTexture, textures.ButtonFont)
            {
                Position = positionFirstButton,
                Text = "Main Menu",
            };

            mainMenuButton.Click += MainMenuButton_Click;

            var quitGameButton = new Button(textures.ButtonTexture, textures.ButtonFont)
            {
                Position = positionSecondButton,
                Text = "Exit",
            };

            quitGameButton.Click += QuitGameButton_Click;

            components = new List<Component>()
            {
                mainMenuButton,
                quitGameButton,
            };
        }

        public override void Initialize()
        {
            MediaPlayer.Play(textures.SuccessMusic);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.3f;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SleepyCat
{
    public class Rules : State
    {
        public Rules(IGame game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            backgroundTexture = textures.RulesBack;
        }

        public override void Initialize()
        {
            Game.IsGameAgain = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                Game.ChangeState(Game.dictOfStates["GameState"]);
        }
    }
}

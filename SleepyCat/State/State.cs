using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace SleepyCat
{
    public abstract class State
    {
        protected readonly ContentManager _content;

        protected readonly GraphicsDevice _graphicsDevice;

        protected readonly Vector2 positionFirstButton;

        protected readonly Vector2 positionSecondButton;

        protected Texture2D backgroundTexture;

        protected List<Component> components;

        protected readonly Textures textures;

        public IGame Game { get; private set; }

        public State(IGame game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            Game = game;
            _graphicsDevice = graphicsDevice;
            _content = content;

            positionFirstButton = new Vector2(441, 405);
            positionSecondButton = new Vector2(441, 572);
            textures = new Textures(_content);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, Game.GameWidth, Game.GameHeight), Color.White);

            if (components != null)
                foreach (var component in components)
                    component.Draw(gameTime, spriteBatch);
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var component in components)
                component.Update(gameTime);
        }

        public abstract void Initialize();

        protected void MainMenuButton_Click(object sender, EventArgs e) => Game.ChangeState(Game.dictOfStates["MenuState"]);

        protected void QuitGameButton_Click(object sender, EventArgs e) => Game.Exit();
    }
}

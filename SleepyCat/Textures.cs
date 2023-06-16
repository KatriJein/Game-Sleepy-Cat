using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace SleepyCat
{
    public class Textures
    {
        protected readonly ContentManager content;

        public Texture2D ButtonTexture { get; private set; }

        public SpriteFont ButtonFont { get; private set; }

        public Texture2D GameStateBack { get; private set; }

        public Texture2D MenuStateBack { get; private set; }

        public Texture2D SuccessStateBack { get; private set; }

        public Texture2D FinalStateBack { get; private set; }

        public Texture2D LoseStateBack { get; private set; }

        public Texture2D RulesBack { get; private set; }

        public Texture2D TextureForScore { get; private set; }

        public SpriteFont FontForScore { get; private set; }

        public Song MenuMusic { get; private set; }

        public Song GameMusic { get; private set; }

        public Song LoseMusic { get; private set; }

        public Song SuccessMusic { get; private set; }

        public Textures(ContentManager _content)
        {
            content = _content;
            ButtonTexture = content.Load<Texture2D>("Button");
            ButtonFont = content.Load<SpriteFont>("State");
            GameStateBack = content.Load<Texture2D>("background/background");
            MenuStateBack = content.Load<Texture2D>("background/main_menu");
            SuccessStateBack = content.Load<Texture2D>("background/success");
            FinalStateBack = content.Load<Texture2D>("background/game_over");
            LoseStateBack = content.Load<Texture2D>("background/fail");
            RulesBack = content.Load<Texture2D>("background/rules");
            TextureForScore = content.Load<Texture2D>("ScoreTexture");
            FontForScore = content.Load<SpriteFont>("score");
            MenuMusic = content.Load<Song>("Music/MenuMusic");
            GameMusic = content.Load<Song>("Music/GameMusic");
            LoseMusic = content.Load<Song>("Music/FailMusic");
            SuccessMusic = content.Load<Song>("Music/SuccessMusic");
        }
    }
}

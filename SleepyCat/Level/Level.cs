using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;


namespace SleepyCat
{
    public class Level
    {
        private readonly ContentManager content;

        private static readonly Vector2 invalidPosition = new(-1, -1);

        private readonly List<Vector2> ends = new();

        private Tile[,] tiles;

        private Vector2 exit = invalidPosition;

        public List<Sprite> Sprites = new();

        public Cat Cat { get; private set; }

        public int CountOfFish { get; private set; }

        public int Score { get; set; }

        public Level(IServiceProvider serviceProvider, Stream fileStream)
        {
            content = new ContentManager(serviceProvider, "Content");

            LoadSprites(fileStream);
        }

        private void LoadSprites(Stream fileStream)
        {
            int width;
            var lines = new List<string>();
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line = reader.ReadLine();
                width = line.Length;
                while (line != null)
                {
                    lines.Add(line);
                    if (line.Length != width)
                        throw new NotSupportedException(String.Format("Длина строки {0} отличается от других строк.", lines.Count));
                    line = reader.ReadLine();
                }
            }

            tiles = new Tile[width, lines.Count];

            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    char tileType = lines[y][x];
                    LoadTile(tileType, GetPosition(x, y));
                }
            }

            if (Cat == null)
                throw new NotSupportedException("Уровень должен иметь старт.");

            if (exit == invalidPosition)
                throw new NotSupportedException("Уровень должен иметь выход.");
        }

        private void LoadTile(char tileType, Point position)
        {
            switch (tileType)
            {
                case '.':
                    break;

                case 'X':
                    LoadExit(position);
                    break;

                case 'F':
                    LoadFishSprite(position);
                    break;

                case 'B':
                    LoadBoneSprite(position);
                    break;

                case '-':
                    LoadPlatform(position, content.Load<Texture2D>("floor"));
                    break;

                case 'K':
                    LoadPlatform(position, content.Load<Texture2D>("Furniture/furn1"));
                    break;

                case 'P':
                    LoadPlatform(position, content.Load<Texture2D>("Furniture/furn3"));
                    break;

                case 'O':
                    LoadPlatform(position, content.Load<Texture2D>("Furniture/furn2"));
                    break;

                case 'L':
                    LoadPlatform(position, content.Load<Texture2D>("Furniture/furn4"));
                    break;

                case 'E':
                    LoadEndDog(position);
                    break;

                case 'S':
                    LoadStart(position);
                    break;

                case 'D':
                    LoadDogSprite(position);
                    break;

                default:
                    throw new NotSupportedException("Неизвестный тип клетки");
            }
        }

        private void LoadStart(Point position)
        {
            if (Cat != null)
                throw new NotSupportedException("Уровень может иметь только 1 старт.");

            Cat = new Cat(new Dictionary<string, Animation>()
            {
                { "MoveRight", new Animation(content.Load<Texture2D>("CatMoveRight"), 3) },
                { "MoveLeft", new Animation(content.Load<Texture2D>("CatMoveLeft"), 3) }
            })
            {
                Position = new Vector2(position.X, position.Y - 24),
                Speed = 7f,
                Input = new Input()
                {
                    Left = Keys.Left,
                    Right = Keys.Right,
                    Up = Keys.Space
                }
            };
        }

        private void LoadEndDog(Point position)
        {
            ends.Add(new Vector2(position.X, position.Y - 30));
            for (var i = 0; i < ends.Count; i++)
                foreach (var sprite in Sprites)
                    if (sprite is Dog)
                    {
                        var dog = sprite as Dog;
                        if (dog.EndPosition == invalidPosition && dog.Position.Y == ends[i].Y)
                        {
                            dog.EndPosition = ends[i];
                            ends.RemoveAt(i);
                            i--;
                        }
                    }
        }

        private void LoadExit(Point position)
        {
            if (exit != invalidPosition)
                throw new NotSupportedException("Уровень может иметь только 1 выход.");

            exit = new Vector2(position.X, position.Y);
            Sprites.Add(new Cot(content.Load<Texture2D>("Cot"))
            {
                Position = new Vector2(position.X, position.Y + 10)
            });
        }

        private void LoadDogSprite(Point position)
        {
            Sprites.Add(new Dog(new Dictionary<string, Animation>()
            {
                { "MoveRight", new Animation(content.Load<Texture2D>("DogMoveRight"), 3) },
                { "MoveLeft", new Animation(content.Load<Texture2D>("DogMoveLeft"), 3) }
            },
            new Vector2(position.X, position.Y - 30))
            {
                Speed = 2f,
                EndPosition = invalidPosition
            });
        }

        private void LoadPlatform(Point position, Texture2D texture)
        {
            Sprites.Add(new Platform(texture)
            {
                Position = new Vector2(position.X, position.Y)
            });
        }

        private void LoadFishSprite(Point position)
        {
            Sprites.Add(new Fish(content.Load<Texture2D>("Fish"))
            {
                Position = new Vector2(position.X + 27, position.Y + 13)
            });
            CountOfFish++;
        }

        private void LoadBoneSprite(Point position)
        {
            Sprites.Add(new Bone(content.Load<Texture2D>("bone"))
            {
                Position = new Vector2(position.X + 30, position.Y + 15)
            });
        }

        public void Dispose()
        {
            content.Unload();
        }

        public static Point GetPosition(int x, int y) =>
            new Rectangle(x * Tile.Width, y * Tile.Height, Tile.Width, Tile.Height).Location;

        public int Width => tiles.GetLength(0);

        public int Height => tiles.GetLength(1);
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Platformer
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        List<Platform> platforms = new List<Platform>();
        Vector2 oldPlayerPos = Vector2.Zero;
        Random random = new Random();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            base.Initialize();
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D t = new Texture2D(GraphicsDevice, 1, 1);
            t.SetData(new Color[1] { Color.White });
            player = new Player(t, new Vector2(500, 100), new Point(20, 20));
            platforms.Add(new Platform(t, new Vector2(random.Next(50, 200), random.Next(100, 200)), new Point(random.Next(150, 350), 30)));
            platforms.Add(new Platform(t, new Vector2(random.Next(650, 850), random.Next(100, 200)), new Point(random.Next(150, 350), 30)));
            platforms.Add(new Platform(t, new Vector2(random.Next(1300, 1500), random.Next(100, 200)), new Point(random.Next(150, 350), 30)));

            platforms.Add(new Platform(t, new Vector2(random.Next(325, 525), random.Next(250, 350)), new Point(random.Next(150, 350), 30)));
            platforms.Add(new Platform(t, new Vector2(random.Next(975, 1175), random.Next(250, 350)), new Point(random.Next(150, 350), 30)));
            platforms.Add(new Platform(t, new Vector2(random.Next(1625, 1825), random.Next(250, 350)), new Point(random.Next(150, 350), 30)));

            platforms.Add(new Platform(t, new Vector2(random.Next(50, 200),  random.Next(400, 500)), new Point(random.Next(150, 350), 30)));
            platforms.Add(new Platform(t, new Vector2(random.Next(650, 850), random.Next(400, 500)), new Point(random.Next(150, 350), 30)));
            platforms.Add(new Platform(t, new Vector2(random.Next(1300, 1500), random.Next(400, 500)), new Point(random.Next(150, 350), 30)));

            platforms.Add(new Platform(t, new Vector2(random.Next(325, 525), random.Next(550, 650)), new Point(random.Next(150, 350), 30)));
            platforms.Add(new Platform(t, new Vector2(random.Next(975, 1175), random.Next(550, 650)), new Point(random.Next(150, 350), 30)));
            platforms.Add(new Platform(t, new Vector2(random.Next(1625, 1825), random.Next(550, 650)), new Point(random.Next(150, 350), 30)));

            platforms.Add(new Platform(t, new Vector2(random.Next(50, 200),  random.Next(700, 800)), new Point(random.Next(150, 350), 30)));
            platforms.Add(new Platform(t, new Vector2(random.Next(650, 850), random.Next(700, 800)), new Point(random.Next(150, 350), 30)));
            platforms.Add(new Platform(t, new Vector2(random.Next(1300, 1500), random.Next(700, 800)), new Point(random.Next(150, 350), 30)));

            platforms.Add(new Platform(t, new Vector2(random.Next(325, 525), random.Next(850, 950)), new Point(random.Next(150, 350), 30)));
            platforms.Add(new Platform(t, new Vector2(random.Next(975, 1175), random.Next(850, 950)), new Point(random.Next(150, 350), 30)));
            platforms.Add(new Platform(t, new Vector2(random.Next(1625, 1825), random.Next(850, 950)), new Point(random.Next(150, 350), 30)));
        }
        
        protected override void UnloadContent() { }

        public void PlatformCollision()
        {
            for (int i = 0; i < platforms.Count; i++)
            {
                if (player.Rectangle.Intersects(platforms[i].Rectangle) && oldPlayerPos.X + player.Rectangle.Width < platforms[i].Position.X) // Platform 1 Left Wall
                {
                    player.WallCollision();
                    player.Position = new Vector2(platforms[i].Position.X - player.Rectangle.Width - 1, player.Position.Y);
                }

                else if (player.Rectangle.Intersects(platforms[i].Rectangle) && oldPlayerPos.X > platforms[i].Position.X + platforms[i].Rectangle.Width) // Platform 1 Rigth Wall
                {
                    player.WallCollision();
                    player.Position = new Vector2(platforms[i].Position.X + platforms[i].Rectangle.Width + 1, player.Position.Y);
                }

                else if (player.Rectangle.Intersects(platforms[i].Rectangle) && oldPlayerPos.Y < platforms[i].Position.Y) // Platform 1 Floor
                {
                    player.FloorCollision();
                    player.Position = new Vector2(player.Position.X, platforms[i].Position.Y - player.Rectangle.Height + 1);
                }

                else if (player.Rectangle.Intersects(platforms[i].Rectangle) && oldPlayerPos.Y + player.Rectangle.Height > platforms[i].Position.Y + platforms[i].Rectangle.Height) // Platform 1 Ceiling
                {
                    player.RoofCollision();
                    player.Position = new Vector2(player.Position.X, platforms[i].Position.Y + platforms[i].Rectangle.Height);
                }
            }
        }
        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            oldPlayerPos = player.Position;

            player.Update();

            PlatformCollision();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (Platform platforms in platforms)
            {
                platforms.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

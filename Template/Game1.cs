using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Template
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Platform platform;
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
            player = new Player(t, new Vector2(100, 100), new Point(20, 20));
            platform = new Platform(t, new Vector2(0, 400), new Point(300, 50));
        }

        protected override void UnloadContent() { }

        public void Collision()
        {
            if (player.Rectangle.Intersects(platform.Rectangle) && player.Position.Y < platform.Position.Y) // Platform 1 Floor
            {
                player.FloorCollision();
                player.Position = new Vector2(player.Position.X, platform.Position.Y - player.Rectangle.Height);
            }

            if (player.Rectangle.Intersects(platform.Rectangle) && player.Position.Y > platform.Position.Y) // Platform 1 Roof
            {
                player.RoofCollision();
                player.Position = new Vector2(player.Position.X, platform.Position.Y + platform.Rectangle.Height);
            }
        }
        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            Collision();

            player.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            platform.Draw(spriteBatch);
            player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Platformer
{
    public class Game1 : Game
    {
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;
        Gun rightGun, leftGun;
        readonly List<Platform> platforms = new List<Platform>();
        readonly List<Bullet> bullets = new List<Bullet>();
        int bulletCooldown = 0;

        float bulletRot;
        Vector2 bulletDir;

        Vector2 oldPlayerPos = Vector2.Zero;
        readonly Random random = new Random();
        Texture2D crosshairTex, gunRightTex, gunLeftTex, bulletTex;
        MouseState mouseState;
        KeyboardState keyboardState;
        bool isGunLeft;
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

            crosshairTex = Content.Load<Texture2D>("crosshair");
            gunRightTex = Content.Load<Texture2D>("gunRight");
            gunLeftTex = Content.Load<Texture2D>("gunLeft");
            bulletTex = Content.Load<Texture2D>("bullet");

            Texture2D t = new Texture2D(GraphicsDevice, 1, 1);
            t.SetData(new Color[1] { Color.White });

            player = new Player(t, new Vector2(500, 100), new Point(30, 30));
            rightGun = new Gun(gunRightTex, new Vector2(500, 100));
            leftGun = new Gun(gunLeftTex, new Vector2(500, 100));

            platforms.Add(new Platform(t, new Vector2(random.Next(50, 200), random.Next(100, 200)), new Point(random.Next(150, 350), 30)));           // Platforms
            platforms.Add(new Platform(t, new Vector2(random.Next(650, 850), random.Next(100, 200)), new Point(random.Next(150, 350), 30)));          // Platforms
            platforms.Add(new Platform(t, new Vector2(random.Next(1300, 1500), random.Next(100, 200)), new Point(random.Next(150, 350), 30)));        // Platforms
                                                                                                                                                      // Platforms
            platforms.Add(new Platform(t, new Vector2(random.Next(325, 525), random.Next(250, 350)), new Point(random.Next(150, 350), 30)));          // Platforms
            platforms.Add(new Platform(t, new Vector2(random.Next(975, 1175), random.Next(250, 350)), new Point(random.Next(150, 350), 30)));         // Platforms
            platforms.Add(new Platform(t, new Vector2(random.Next(1625, 1825), random.Next(250, 350)), new Point(random.Next(150, 350), 30)));        // Platforms
                                                                                                                                                      // Platforms
            platforms.Add(new Platform(t, new Vector2(random.Next(50, 200),  random.Next(400, 500)), new Point(random.Next(150, 350), 30)));          // Platforms
            platforms.Add(new Platform(t, new Vector2(random.Next(650, 850), random.Next(400, 500)), new Point(random.Next(150, 350), 30)));          // Platforms
            platforms.Add(new Platform(t, new Vector2(random.Next(1300, 1500), random.Next(400, 500)), new Point(random.Next(150, 350), 30)));        // Platforms
                                                                                                                                                      // Platforms
            platforms.Add(new Platform(t, new Vector2(random.Next(325, 525), random.Next(550, 650)), new Point(random.Next(150, 350), 30)));          // Platforms
            platforms.Add(new Platform(t, new Vector2(random.Next(975, 1175), random.Next(550, 650)), new Point(random.Next(150, 350), 30)));         // Platforms
            platforms.Add(new Platform(t, new Vector2(random.Next(1625, 1825), random.Next(550, 650)), new Point(random.Next(150, 350), 30)));        // Platforms
                                                                                                                                                      // Platforms
            platforms.Add(new Platform(t, new Vector2(random.Next(50, 200),  random.Next(700, 800)), new Point(random.Next(150, 350), 30)));          // Platforms
            platforms.Add(new Platform(t, new Vector2(random.Next(650, 850), random.Next(700, 800)), new Point(random.Next(150, 350), 30)));          // Platforms
            platforms.Add(new Platform(t, new Vector2(random.Next(1300, 1500), random.Next(700, 800)), new Point(random.Next(150, 350), 30)));        // Platforms
                                                                                                                                                      // Platforms
            platforms.Add(new Platform(t, new Vector2(random.Next(325, 525), random.Next(850, 950)), new Point(random.Next(150, 350), 30)));          // Platforms
            platforms.Add(new Platform(t, new Vector2(random.Next(975, 1175), random.Next(850, 950)), new Point(random.Next(150, 350), 30)));         // Platforms
            platforms.Add(new Platform(t, new Vector2(random.Next(1625, 1825), random.Next(850, 950)), new Point(random.Next(150, 350), 30)));        // Platforms
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
             keyboardState = Keyboard.GetState();
             mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            if (mouseState.X < player.Position.X + 15)
                isGunLeft = true;
            else
                isGunLeft = false;

                oldPlayerPos = player.Position;

            player.Update();

            rightGun.ChangePosition(player.Position);
            rightGun.Update();

            leftGun.ChangePosition(player.Position);
            leftGun.Update();

            PlatformCollision();

            if (mouseState.LeftButton == ButtonState.Pressed && isGunLeft == true && bulletCooldown == 0) // Shoot Left
            {
                bulletCooldown = 30;

                bulletDir.X = mouseState.X - leftGun.Position.X;
                bulletDir.Y = mouseState.Y - leftGun.Position.Y;
                bulletDir.Normalize();

                bulletRot = (float)Math.Atan2(bulletDir.Y, bulletDir.X);

                bullets.Add(new Bullet(
                    bulletTex, // Texture
                    leftGun.Position, // Position
                    new Point(18, 8), // Size
                    (float)Math.Atan2(bulletDir.Y, bulletDir.X), // Rotation
                    new Vector2((float)Math.Cos(bulletRot),
                                (float)Math.Sin(bulletRot)))); // Direction
            }

            else if (mouseState.LeftButton == ButtonState.Pressed && isGunLeft == false && bulletCooldown == 0) // Shoot Right
            {
                bulletCooldown = 30;

                bulletDir.X = mouseState.X - rightGun.Position.X - 26;
                bulletDir.Y = mouseState.Y - rightGun.Position.Y;
                bulletDir.Normalize();

                bulletRot = (float)Math.Atan2(bulletDir.Y, bulletDir.X);

                bullets.Add(new Bullet(
                    bulletTex, // Texture
                    new Vector2(rightGun.Position.X + 26, rightGun.Position.Y), // Position
                    new Point(18, 8), // Size
                    (float)Math.Atan2(bulletDir.Y, bulletDir.X), // Rotation
                    new Vector2((float)Math.Cos(bulletRot),
                                (float)Math.Sin(bulletRot)))); // Direction
            }

            if (bulletCooldown > 0)
                bulletCooldown--;

            foreach (Bullet bullets in bullets)
            {
                bullets.Move();
            }

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

            foreach (Bullet bullets in bullets)
            {
                bullets.Draw(spriteBatch);
            }

            player.Draw(spriteBatch);

            if (isGunLeft == true)
                leftGun.DrawLeft(spriteBatch);
            else
                rightGun.DrawRight(spriteBatch);

            spriteBatch.Draw(crosshairTex, new Vector2(mouseState.X - 10, mouseState.Y - 10), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

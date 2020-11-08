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
        Glasses leftGlasses, rightGlasses;
        Gun leftGun, rightGun;
        readonly List<Platform> platforms = new List<Platform>();
        readonly List<Bullet> bullets = new List<Bullet>();
        readonly List<Enemy> enemies = new List<Enemy>();
        int bulletCooldown = 0;

        float bulletRot;
        Vector2 bulletDir;

        Vector2 oldPlayerPos = Vector2.Zero;
        readonly Random random = new Random();
        Texture2D defaultTex, crosshairTex, gunRightTex, gunLeftTex, bulletTex, glassesRightTex, glassesLeftTex;
        MouseState mouseState;
        KeyboardState keyboardState;
        bool isTurnedLeft;
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
            glassesRightTex = Content.Load<Texture2D>("glassesRight");
            glassesLeftTex = Content.Load<Texture2D>("glassesLeft");
            bulletTex = Content.Load<Texture2D>("bullet");

            defaultTex = new Texture2D(GraphicsDevice, 1, 1);
            defaultTex.SetData(new Color[1] { Color.White });

            player = new Player(defaultTex, new Vector2(500, 100), new Point(30, 30));
            leftGlasses = new Glasses(glassesLeftTex, new Vector2(500, 100));
            rightGlasses = new Glasses(glassesRightTex, new Vector2(500, 100));
            rightGun = new Gun(gunRightTex, new Vector2(500, 100));
            leftGun = new Gun(gunLeftTex, new Vector2(500, 100));

            platforms.Add(new Platform(defaultTex, new Vector2(random.Next(50, 200), random.Next(100, 200)), new Point(random.Next(150, 350), 30)));           // Platforms
            platforms.Add(new Platform(defaultTex, new Vector2(random.Next(650, 850), random.Next(100, 200)), new Point(random.Next(150, 350), 30)));          // Platforms
            platforms.Add(new Platform(defaultTex, new Vector2(random.Next(1300, 1500), random.Next(100, 200)), new Point(random.Next(150, 350), 30)));        // Platforms
                                                                                                                                                      // Platforms
            platforms.Add(new Platform(defaultTex, new Vector2(random.Next(325, 525), random.Next(250, 350)), new Point(random.Next(150, 350), 30)));          // Platforms
            platforms.Add(new Platform(defaultTex, new Vector2(random.Next(975, 1175), random.Next(250, 350)), new Point(random.Next(150, 350), 30)));         // Platforms
            platforms.Add(new Platform(defaultTex, new Vector2(random.Next(1625, 1825), random.Next(250, 350)), new Point(random.Next(150, 350), 30)));        // Platforms
                                                                                                                                                      // Platforms
            platforms.Add(new Platform(defaultTex, new Vector2(random.Next(50, 200),  random.Next(400, 500)), new Point(random.Next(150, 350), 30)));          // Platforms
            platforms.Add(new Platform(defaultTex, new Vector2(random.Next(650, 850), random.Next(400, 500)), new Point(random.Next(150, 350), 30)));          // Platforms
            platforms.Add(new Platform(defaultTex, new Vector2(random.Next(1300, 1500), random.Next(400, 500)), new Point(random.Next(150, 350), 30)));        // Platforms
                                                                                                                                                      // Platforms
            platforms.Add(new Platform(defaultTex, new Vector2(random.Next(325, 525), random.Next(550, 650)), new Point(random.Next(150, 350), 30)));          // Platforms
            platforms.Add(new Platform(defaultTex, new Vector2(random.Next(975, 1175), random.Next(550, 650)), new Point(random.Next(150, 350), 30)));         // Platforms
            platforms.Add(new Platform(defaultTex, new Vector2(random.Next(1625, 1825), random.Next(550, 650)), new Point(random.Next(150, 350), 30)));        // Platforms
                                                                                                                                                      // Platforms
            platforms.Add(new Platform(defaultTex, new Vector2(random.Next(50, 200),  random.Next(700, 800)), new Point(random.Next(150, 350), 30)));          // Platforms
            platforms.Add(new Platform(defaultTex, new Vector2(random.Next(650, 850), random.Next(700, 800)), new Point(random.Next(150, 350), 30)));          // Platforms
            platforms.Add(new Platform(defaultTex, new Vector2(random.Next(1300, 1500), random.Next(700, 800)), new Point(random.Next(150, 350), 30)));        // Platforms
                                                                                                                                                      // Platforms
            platforms.Add(new Platform(defaultTex, new Vector2(random.Next(325, 525), random.Next(850, 950)), new Point(random.Next(150, 350), 30)));          // Platforms
            platforms.Add(new Platform(defaultTex, new Vector2(random.Next(975, 1175), random.Next(850, 950)), new Point(random.Next(150, 350), 30)));         // Platforms
            platforms.Add(new Platform(defaultTex, new Vector2(random.Next(1625, 1825), random.Next(850, 950)), new Point(random.Next(150, 350), 30)));        // Platforms
        }
        
        protected override void UnloadContent() { }

        public void Collision()
        {
            foreach (Platform platforms in platforms)
            {
                if (player.Rectangle.Intersects(platforms.Rectangle) && oldPlayerPos.X + player.Rectangle.Width < platforms.Position.X) // Player collision with left wall
                {
                    player.WallCollision();
                    player.Position = new Vector2(platforms.Position.X - player.Rectangle.Width - 1, player.Position.Y);
                }

                else if (player.Rectangle.Intersects(platforms.Rectangle) && oldPlayerPos.X > platforms.Position.X + platforms.Rectangle.Width) // Player collision with right wall
                {
                    player.WallCollision();
                    player.Position = new Vector2(platforms.Position.X + platforms.Rectangle.Width + 1, player.Position.Y);
                }

                else if (player.Rectangle.Intersects(platforms.Rectangle) && oldPlayerPos.Y < platforms.Position.Y) // Player collision with floor
                {
                    player.FloorCollision();
                    player.Position = new Vector2(player.Position.X, platforms.Position.Y - player.Rectangle.Height + 1);
                }

                else if (player.Rectangle.Intersects(platforms.Rectangle) && oldPlayerPos.Y + player.Rectangle.Height > platforms.Position.Y + platforms.Rectangle.Height) // Player collision with ceiling
                {
                    player.RoofCollision();
                    player.Position = new Vector2(player.Position.X, platforms.Position.Y + platforms.Rectangle.Height);
                }

                for (int i = 0; i < bullets.Count; i++) // Bullet collision
                {
                    if (bullets[i].Rectangle.Intersects(platforms.Rectangle)) // Bullet & Platform collision
                    {
                        bullets.RemoveAt(i);
                        i--;
                    }

                    else
                        for (int j = 0; j < enemies.Count; j++)
                        {
                            if (bullets[i].Rectangle.Intersects(enemies[j].Rectangle)) // Enemy & Bullet collision
                            {
                                bullets.RemoveAt(i);
                                i--;

                                enemies.RemoveAt(j);
                            }
                        }
                }

                for (int i = 0; i < enemies.Count; i++) // Enemy collision
                {
                    if (enemies[i].Rectangle.Intersects(player.Rectangle))
                        Exit();
                }
            }
        }
        protected override void Update(GameTime gameTime)
        {
             keyboardState = Keyboard.GetState();
             mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape)) // Exit
                Exit();

            if (mouseState.X < player.Position.X + 15) // Is the player turned left?
                isTurnedLeft = true; // Yes
            else
                isTurnedLeft = false; // No

                oldPlayerPos = player.Position; // Record the previous player position

            player.Update();

            if (isTurnedLeft == true) // Update glasses position
                leftGlasses.UpdatePos(new Vector2(player.Position.X, player.Position.Y + 4));
            else
                rightGlasses.UpdatePos(new Vector2(player.Position.X - 4, player.Position.Y + 4));

            rightGun.UpdatePos(player.Position);
            rightGun.Update();

            leftGun.UpdatePos(player.Position);
            leftGun.Update();

            Collision();

            if (mouseState.LeftButton == ButtonState.Pressed && isTurnedLeft == true && bulletCooldown == 0) // Shoot Left
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
                    new Vector2((float)Math.Cos(bulletRot), (float)Math.Sin(bulletRot)), // Direction
                    15)); // Speed
                    
            }

            else if (mouseState.LeftButton == ButtonState.Pressed && isTurnedLeft == false && bulletCooldown == 0) // Shoot Right
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
                    new Vector2((float)Math.Cos(bulletRot), (float)Math.Sin(bulletRot)), // Direction
                    15)); // Speed
            }

            if (bulletCooldown > 0) // Bullet cooldown
                bulletCooldown--;
                                                                                                                                             // Add enemies
            if (random.Next(300) == 1) // Spawn Chance                                                                                       // Up
                enemies.Add(new Enemy(
                    defaultTex, // Texture
                    new Vector2(random.Next(1920), -100), // Position
                    new Point(30, 30), // Size
                    random.Next(2, 6) * 1f / 60f)); // Speed

            if (random.Next(300) == 1) // Spawn Chance                                                                                       // Left
                enemies.Add(new Enemy(
                    defaultTex, // Texture
                    new Vector2(-100, random.Next(1080)), // Position
                    new Point(30, 30), // Size
                    random.Next(2, 6) * 1f / 60f)); // Speed

            if (random.Next(300) == 1) // Spawn Chance                                                                                       // Right
                enemies.Add(new Enemy(
                    defaultTex, // Texture
                    new Vector2(2020, random.Next(1080)), // Position
                    new Point(30, 30), // Size
                    random.Next(2, 6) * 1f / 60f)); // Speed

            if (random.Next(300) == 1) // Spawn Chance                                                                                       // Down
                enemies.Add(new Enemy(
                    defaultTex, // Texture
                    new Vector2(random.Next(1920), 1180), // Position
                    new Point(30, 30), // Size
                    random.Next(2, 6) * 1f / 60f)); // Speed

            foreach (Bullet bullets in bullets) // Move bullets
            {
                bullets.Move();
            }

            foreach (Enemy enemies in enemies)
            {
                enemies.Move(player.Position);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue); // Draw background

            spriteBatch.Begin();

            foreach (Bullet bullets in bullets) // Draw bullets
            {
                bullets.Draw(spriteBatch);
            }

            foreach (Platform platforms in platforms) // Draw platforms
            {
                platforms.Draw(spriteBatch);
            }

            foreach (Enemy enemies in enemies)
            {
                enemies.Draw(spriteBatch);
            }

            player.Draw(spriteBatch); // Draw player


            if (isTurnedLeft == true)
            {
                leftGlasses.Draw(spriteBatch);
                leftGun.DrawLeft(spriteBatch);
            }
            else
            {
                rightGlasses.Draw(spriteBatch);
                rightGun.DrawRight(spriteBatch);
            }

            spriteBatch.Draw(crosshairTex, new Vector2(mouseState.X - 10, mouseState.Y - 10), Color.White); // Draw crosshair

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

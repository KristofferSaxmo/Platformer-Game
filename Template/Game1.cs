using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Platformer
{
    public class Game1 : Game
    {
        readonly int NORMAL_ENEMY_HEALTH = 1;
        readonly int BOSS_ENEMY_HEALTH = 10;

        readonly Point NORMAL_ENEMY_SIZE = new Point(30, 30);
        readonly Point BOSS_ENEMY_SIZE = new Point(100, 100);

        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;
        Glasses glasses;
        Gun gun;
        readonly List<Platform> platforms = new List<Platform>();
        readonly List<Bullet> bullets = new List<Bullet>();
        readonly List<Enemy> enemies = new List<Enemy>();
        readonly List<HealthBar> healthBars = new List<HealthBar>();

        Vector2 oldPlayerPos = Vector2.Zero;
        readonly Random random = new Random();
        Texture2D defaultTex, crosshairTex, gunTex, bulletTex, glassesTex;
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
            glassesTex = Content.Load<Texture2D>("glassesRight");
            glassesTex = Content.Load<Texture2D>("glassesLeft");
            bulletTex = Content.Load<Texture2D>("bullet");

            defaultTex = new Texture2D(GraphicsDevice, 1, 1);
            defaultTex.SetData(new Color[1] { Color.White });

            player = new Player(defaultTex, new Vector2(500, 100), new Point(30, 30));
            gun = new Gun(new Vector2(500, 100), 1, 30);
            glasses = new Glasses();

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
                            if (i < 0) break; // if bullets < 0, Break
                            if (bullets[i].Rectangle.Intersects(enemies[j].Rectangle)) // Enemy & Bullet collision
                            {
                                bullets.RemoveAt(i);
                                i--;

                                enemies[j].Health -= gun.Damage;
                                if (enemies[j].Health <= 0)
                                {
                                    enemies.RemoveAt(j);
                                    healthBars.RemoveAt(j);
                                    j--;
                                }
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
        public void SpawnEnemies()
        {
                                                                                                                                             // Add enemies //
            if (random.Next(300) == 1) // Spawn Chance                                                                                       // Up // NORMAL //
            {
                enemies.Add(new Enemy(
                    defaultTex, // Texture
                    new Vector2(random.Next(1920), -100), // Position
                    NORMAL_ENEMY_SIZE, // Size
                    random.Next(2, 6) * 1f / 60f, // Speed
                    NORMAL_ENEMY_HEALTH)); // Health

                AddHealthBar();
            }

            if (random.Next(300) == 1) // Spawn Chance                                                                                       // Left // NORMAL //
            {
                enemies.Add(new Enemy(
                    defaultTex, // Texture
                    new Vector2(-100, random.Next(1080)), // Position
                    NORMAL_ENEMY_SIZE, // Size
                    random.Next(2, 6) * 1f / 60f, // Speed
                    NORMAL_ENEMY_HEALTH)); // Health

                AddHealthBar();
            }
                
            if (random.Next(300) == 1) // Spawn Chance                                                                                       // Right // NORMAL //
            {
                enemies.Add(new Enemy(
                    defaultTex, // Texture
                    new Vector2(2020, random.Next(1080)), // Position
                    NORMAL_ENEMY_SIZE, // Size
                    random.Next(2, 6) * 1f / 60f, // Speed
                    NORMAL_ENEMY_HEALTH)); // Health

                AddHealthBar();
            }
                
            if (random.Next(300) == 1) // Spawn Chance                                                                                       // Down // NORMAL //
            {
                enemies.Add(new Enemy(
                    defaultTex, // Texture
                    new Vector2(random.Next(1920), 1180), // Position
                    NORMAL_ENEMY_SIZE, // Size
                    random.Next(2, 6) * 1f / 60f, // Speed
                    NORMAL_ENEMY_HEALTH)); // Health

                AddHealthBar();
            }
                
            if (random.Next(3000) == 1) // Spawn Chance                                                                                       // Up // BOSS //
            {
                enemies.Add(new Enemy(
                    defaultTex, // Texture
                    new Vector2(random.Next(1920), -100), // Position
                    BOSS_ENEMY_SIZE, // Size
                    random.Next(2, 4) * 1f / 60f, // Speed
                    BOSS_ENEMY_HEALTH)); // Health

                AddHealthBar();
            }
                
            if (random.Next(3000) == 1) // Spawn Chance                                                                                       // Left // BOSS //
            {
                enemies.Add(new Enemy(
                    defaultTex, // Texture
                    new Vector2(-100, random.Next(1080)), // Position
                    BOSS_ENEMY_SIZE, // Size
                    random.Next(2, 4) * 1f / 60f, // Speed
                    BOSS_ENEMY_HEALTH)); // Health

                AddHealthBar();
            }
                
            if (random.Next(3000) == 1) // Spawn Chance                                                                                       // Right // BOSS //
            {
                enemies.Add(new Enemy(
                    defaultTex, // Texture
                    new Vector2(2020, random.Next(1080)), // Position
                    BOSS_ENEMY_SIZE, // Size
                    random.Next(2, 4) * 1f / 60f, // Speed
                    BOSS_ENEMY_HEALTH)); // Health

                AddHealthBar();
            }

            if (random.Next(3000) == 1) // Spawn Chance                                                                                       // Down // BOSS //
            {
                enemies.Add(new Enemy(
                    defaultTex, // Texture
                    new Vector2(random.Next(1920), 1180), // Position
                    BOSS_ENEMY_SIZE, // Size
                    random.Next(2, 4) * 1f / 60f, // Speed
                    BOSS_ENEMY_HEALTH)); // Health

                AddHealthBar();
            }   
        }
        public void AddHealthBar()
        {
            healthBars.Add(new HealthBar(
                    defaultTex,
                    enemies[enemies.Count - 1].Position,
                    new Point((int)(enemies[enemies.Count - 1].Rectangle.Width * 0.8), (int)(enemies[enemies.Count - 1].Rectangle.Width * 0.2)),
                    enemies[enemies.Count - 1].Health));
        }
        protected override void Update(GameTime gameTime)
        {
             keyboardState = Keyboard.GetState();
             mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape)) // Exit
                Exit();
            oldPlayerPos = player.Position; // Record the previous player position
            player.Update();

            if (mouseState.X < player.Position.X + (player.Rectangle.Width) / 2) // Is the player turned left?
            {
                isTurnedLeft = true; // Yes
                gunTex = Content.Load<Texture2D>("gunLeft");
                glassesTex = Content.Load<Texture2D>("glassesLeft");
                glasses.Update(new Vector2(player.Position.X, player.Position.Y + 4), glassesTex);
                gun.Shoot(bullets, bulletTex, 0);
            }
            else
            {
                isTurnedLeft = false; // No
                gunTex = Content.Load<Texture2D>("gunRight");
                glassesTex = Content.Load<Texture2D>("glassesRight");
                glasses.Update(new Vector2(player.Position.X - 4, player.Position.Y + 4), glassesTex);
                gun.Shoot(bullets, bulletTex, gunTex.Width);
            }

            gun.Update(player.Position, gunTex);

            Collision();

            SpawnEnemies();

            foreach (Bullet bullets in bullets) // Move bullets
            {
                bullets.Move();
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].MoveToPlayer(player.Position);
                healthBars[i].Update(enemies[i].Position, enemies[i].Rectangle, enemies[i].Health);
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

            for (int i = 0; i < enemies.Count; i++) // Draw enemies and their health bars
            {
                enemies[i].Draw(spriteBatch);
                healthBars[i].Draw(spriteBatch, gun.Damage);
            }

            player.Draw(spriteBatch); // Draw player

            glasses.Draw(spriteBatch); // Draw glasses

            if (isTurnedLeft == true)
                gun.DrawLeft(spriteBatch); // Draw left gun

            else
                gun.DrawRight(spriteBatch); // Draw right gun

            spriteBatch.Draw(crosshairTex, new Vector2(mouseState.X - 10, mouseState.Y - 10), Color.White); // Draw crosshair

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

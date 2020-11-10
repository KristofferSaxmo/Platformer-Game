using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    class Gun : BaseGun
    {
        Vector2 rightOrigin = new Vector2(0, 8);
        Vector2 leftOrigin = new Vector2(26, 8);

        float leftRotation;
        float rightRotation;

        float bulletRot;
        Vector2 bulletDir;

        int shootCooldown;

        Rectangle sourceRectangle = new Rectangle(0, 0, 26, 16);

        public Gun(Vector2 gunPos, int gunDamage, int gunRateOfFire)
        {
            position = gunPos;
            damage = gunDamage;
            rateOfFire = gunRateOfFire;
        }
        public override void Shoot(List<Bullet> bullets, Texture2D bulletTex, int gunOffset)
        {
            var mouseState = Mouse.GetState();
            if (shootCooldown == 0 && mouseState.LeftButton == ButtonState.Pressed)
            {
                bulletDir.X = mouseState.X - position.X - gunOffset;
                bulletDir.Y = mouseState.Y - position.Y;
                bulletDir.Normalize();

                bulletRot = (float)Math.Atan2(bulletDir.Y, bulletDir.X);

                bullets.Add(new Bullet(
                    bulletTex, // Texture
                    new Vector2(position.X + gunOffset, position.Y), // Position
                    new Point(18, 8), // Size
                    (float)Math.Atan2(bulletDir.Y, bulletDir.X), // Rotation
                    new Vector2((float)Math.Cos(bulletRot), (float)Math.Sin(bulletRot)), // Direction
                    15)); // Speed

                shootCooldown = rateOfFire;
            }
            else if (shootCooldown > 0)
                shootCooldown--;
        }
        public void Update(Vector2 playerPos, Texture2D gunTex)
        {
            position = playerPos;
            texture = gunTex;
            var mouseState = Mouse.GetState();

            direction.X = mouseState.X - position.X;
            direction.Y = mouseState.Y - position.Y;
            direction.Normalize();

            leftRotation = (float)Math.Atan2(0 - direction.Y, 0 - direction.X);   // Left angle
            rightRotation = (float)Math.Atan2(direction.Y, direction.X);          // Right angle

        }
        public override void DrawRight(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(position.X + 20, position.Y + 12), sourceRectangle, Color.White, rightRotation, rightOrigin, 1.0f, SpriteEffects.None, 1);
        }
        public override void DrawLeft(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(position.X + 10, position.Y + 12), sourceRectangle, Color.White, leftRotation, leftOrigin, 1.0f, SpriteEffects.None, 1);
        }
    }
}
